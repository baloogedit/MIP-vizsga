using UMFST.MIP.Variant2.Database; // Corrected namespace
using UMFST.MIP.Variant2.Models; // Corrected namespace
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks; // Keep for HttpClient
using System.Windows.Forms;

namespace UMFST.MIP.Variant2 // Corrected namespace
{
    public partial class MainWindow : Form // Corrected class name
    {
        private DatabaseManager dbManager;
        private HttpClient httpClient;
        private List<string> errorLog = new List<string>();
        private const string JsonDataUrl = "https://cdn.shopify.com/s/files/1/0883/3282/8936/files/data_car_service.json?v=1762418871";
        private const string ErrorLogFile = "invalid_car_service.txt";
        private const string ExportFile = "workorder_summary.txt";

        public MainWindow() // Corrected constructor
        {
            InitializeComponent();
            InitializeApp();
        }

        private void InitializeApp()
        {
            dbManager = new DatabaseManager();
            httpClient = new HttpClient();
            WireUpEventHandlers();
        }

        private void WireUpEventHandlers()
        {
            // General
            this.Load += Form1_Load;

            // These controls now exist in the new MainWindow.Designer.cs
            buttonResetImport.Click += ButtonResetImport_Click;
            buttonExportSummary.Click += ButtonExportSummary_Click;
            dataGridViewWorkOrders.CellFormatting += DataGridViewWorkOrders_CellFormatting;
            listBoxClients.SelectedIndexChanged += ListBoxClients_SelectedIndexChanged;
            buttonFilterCars.Click += (s, e) => LoadClientsAndCars();
            comboBoxDiagCar.SelectedIndexChanged += ComBoxDiagCar_SelectedIndexChanged;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ShowLoading("Loading application...");
            LoadAllTabs();
            HideLoading();
        }

        private void LoadAllTabs()
        {
            ShowLoading("Loading data from database...");
            LoadWorkOrders();
            LoadClientsAndCars();
            LoadDiagnosticsTab();
            HideLoading();
        }

        #region Reset and Import Logic

        private async void ButtonResetImport_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show("This will delete all data and re-import from the internet. Continue?",
                                     "Confirm Reset",
                                     MessageBoxButtons.YesNo,
                                     MessageBoxIcon.Warning);

            if (confirmResult == DialogResult.No) return;

            ShowLoading("Resetting database and importing data...");
            errorLog.Clear();

            try
            {
                dbManager.ResetDatabase();

                string jsonData = await httpClient.GetStringAsync(JsonDataUrl);
                JsonDataRoot dataRoot = JsonConvert.DeserializeObject<JsonDataRoot>(jsonData);

                ProcessImport(dataRoot);

                File.WriteAllLines(ErrorLogFile, errorLog);
                MessageBox.Show($"Import complete. {errorLog.Count} invalid entries were skipped and logged to {ErrorLogFile}.", "Import Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error during reset/import: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            LoadAllTabs();
            HideLoading();
        }

        private void ProcessImport(JsonDataRoot dataRoot)
        {
            // Import Mechanics
            foreach (var jsonMechanic in dataRoot.Garage.Mechanics)
            {
                dbManager.InsertMechanic(new Mechanic
                {
                    MechanicId = jsonMechanic.MechanicId,
                    Name = jsonMechanic.Name,
                    Specialization = jsonMechanic.Specialization
                });
            }

            // Import Clients and their Cars
            foreach (var jsonClient in dataRoot.Clients)
            {
                if (!IsValidEmail(jsonClient.Email) || !IsValidPhone(jsonClient.Phone))
                {
                    errorLog.Add($"Invalid client data: ID {jsonClient.ClientId}, Name {jsonClient.Name}. Email or Phone invalid. Skipping client and their cars.");
                    continue;
                }

                var newClient = new Client
                {
                    ClientId = jsonClient.ClientId,
                    Name = jsonClient.Name,
                    Email = jsonClient.Email,
                    Phone = jsonClient.Phone
                };
                dbManager.InsertClient(newClient);

                foreach (var jsonCar in jsonClient.Cars)
                {
                    if (!IsValidVIN(jsonCar.VIN) || jsonCar.Odometer < 0)
                    {
                        errorLog.Add($"Invalid car data: ID {jsonCar.CarId}, VIN {jsonCar.VIN}. Invalid VIN or negative odometer. Skipping car.");
                        continue;
                    }

                    var newCar = new Car
                    {
                        CarId = jsonCar.CarId,
                        ClientId = newClient.ClientId,
                        Make = jsonCar.Make,
                        Model = jsonCar.Model,
                        Year = jsonCar.Year,
                        VIN = jsonCar.VIN,
                        Odometer = jsonCar.Odometer
                    };
                    dbManager.InsertCar(newCar);
                }
            }

            // Import Work Orders (Tasks, Parts, Invoice)
            foreach (var jsonWo in dataRoot.WorkOrders)
            {
                // Check if related car and mechanic exist
                if (!dbManager.DoesCarExist(jsonWo.CarId) || !dbManager.DoesMechanicExist(jsonWo.MechanicId))
                {
                    errorLog.Add($"Invalid WorkOrder: ID {jsonWo.WorkOrderId}. Missing valid Car or Mechanic. Skipping.");
                    continue;
                }

                var newWorkOrder = new WorkOrder
                {
                    WorkOrderId = jsonWo.WorkOrderId,
                    CarId = jsonWo.CarId,
                    MechanicId = jsonWo.MechanicId,
                    Date = jsonWo.Date,
                    Description = jsonWo.Description,
                    Status = jsonWo.Status
                };
                dbManager.InsertWorkOrder(newWorkOrder);

                // Tasks
                foreach (var jsonTask in jsonWo.Tasks)
                {
                    if (jsonTask.LaborHours < 0 || jsonTask.Rate < 0)
                    {
                        errorLog.Add($"Invalid Task data: ID {jsonTask.TaskId} for WO {jsonWo.WorkOrderId}. Negative hours or rate. Skipping task.");
                        continue;
                    }
                    dbManager.InsertTask(new Task
                    {
                        TaskId = jsonTask.TaskId,
                        WorkOrderId = newWorkOrder.WorkOrderId, // Link to parent
                        Description = jsonTask.Description,
                        LaborHours = jsonTask.LaborHours,
                        Rate = jsonTask.Rate
                    });
                }

                // Parts
                foreach (var jsonPart in jsonWo.Parts)
                {
                    if (jsonPart.Quantity < 0 || jsonPart.UnitPrice < 0)
                    {
                        errorLog.Add($"Invalid Part data: ID {jsonPart.PartId} for WO {jsonWo.WorkOrderId}. Negative quantity or price. Skipping part.");
                        continue;
                    }
                    dbManager.InsertPart(new Part
                    {
                        PartId = jsonPart.PartId,
                        WorkOrderId = newWorkOrder.WorkOrderId, // Link to parent
                        Name = jsonPart.Name,
                        Quantity = jsonPart.Quantity,
                        UnitPrice = jsonPart.UnitPrice
                    });
                }

                // Invoice
                if (jsonWo.Invoice != null)
                {
                    string currency = jsonWo.Invoice.Currency?.ToString() ?? "N/A";
                    if (currency == "978") currency = "EUR"; // Handle numeric currency code
                    if (currency != "EUR")
                    {
                        errorLog.Add($"Invalid Invoice data: ID {jsonWo.Invoice.InvoiceId} for WO {jsonWo.WorkOrderId}. Invalid currency. Skipping invoice.");
                    }
                    else
                    {
                        dbManager.InsertInvoice(new Invoice
                        {
                            InvoiceId = jsonWo.Invoice.InvoiceId,
                            WorkOrderId = newWorkOrder.WorkOrderId, // Link to parent
                            Amount = jsonWo.Invoice.Amount, // Storing original amount, we compute total
                            Date = jsonWo.Invoice.Date,
                            IsPaid = jsonWo.Invoice.IsPaid,
                            Currency = currency
                        });
                    }
                }
            }

            // Import Diagnostics
            foreach (var jsonDiag in dataRoot.Diagnostics)
            {
                if (!dbManager.DoesCarExist(jsonDiag.CarId))
                {
                    errorLog.Add($"Invalid Diagnostic: ID {jsonDiag.DiagnosticId}. Missing valid Car. Skipping.");
                    continue;
                }

                var newDiag = new Diagnostic
                {
                    DiagnosticId = jsonDiag.DiagnosticId,
                    CarId = jsonDiag.CarId,
                    Date = jsonDiag.Date,
                    OBDCodes = string.Join(",", jsonDiag.OBDCodes)
                };
                dbManager.InsertDiagnostic(newDiag);

                foreach (var jsonTest in jsonDiag.TestResults)
                {
                    dbManager.InsertTest(new Test
                    {
                        TestId = jsonTest.TestId,
                        DiagnosticId = newDiag.DiagnosticId, // Link to parent
                        Name = jsonTest.TestName,
                        Result = jsonTest.Result,
                        IsOk = jsonTest.IsOk
                    });
                }
            }
        }

        #endregion

        #region Tab 1: Work Orders

        private void LoadWorkOrders()
        {
            var workOrdersData = dbManager.GetWorkOrders();

            // This control name now matches the designer
            dataGridViewWorkOrders.DataSource = workOrdersData;
            if (dataGridViewWorkOrders.Columns["TotalCost"] != null)
            {
                dataGridViewWorkOrders.Columns["TotalCost"].DefaultCellStyle.Format = "c"; // Currency format
                dataGridViewWorkOrders.Columns["TotalCost"].DefaultCellStyle.FormatProvider = CultureInfo.GetCultureInfo("de-DE"); // For EUR
            }
        }

        private void DataGridViewWorkOrders_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == dataGridViewWorkOrders.Columns["PaidStatus"]?.Index && e.Value != null)
            {
                if (e.Value.ToString() == "Unpaid")
                {
                    e.CellStyle.BackColor = Color.LightCoral;
                    e.CellStyle.ForeColor = Color.Black;
                }
                else if (e.Value.ToString() == "Paid")
                {
                    e.CellStyle.BackColor = Color.LightGreen;
                    e.CellStyle.ForeColor = Color.Black;
                }
            }
        }

        private void ButtonExportSummary_Click(object sender, EventArgs e)
        {
            ShowLoading("Exporting summary...");
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("WORK ORDER SUMMARY - AUTOFIX CENTRAL");
            sb.AppendLine("=====================================");

            var workOrders = dbManager.GetWorkOrdersForExport();

            int unpaidCount = 0;
            foreach (var wo in workOrders)
            {
                string paidStatus = "NO"; // Default
                if (wo.Invoice != null)
                {
                    paidStatus = wo.Invoice.IsPaid ? "YES" : "NO";
                    if (!wo.Invoice.IsPaid) unpaidCount++;
                }

                decimal total = 0;
                if (wo.Tasks != null)
                    total += wo.Tasks.Sum(t => t.LaborHours * t.Rate);
                if (wo.Parts != null)
                    total += wo.Parts.Sum(p => p.Quantity * p.UnitPrice);

                string carName = (wo.Car != null) ? $"{wo.Car.Make} {wo.Car.Model}" : "Unknown Car";
                sb.AppendLine($"W{wo.WorkOrderId} | {carName} | Total: {total:F2} EUR | Paid: {paidStatus}");
            }

            sb.AppendLine("-------------------------------------");
            sb.AppendLine($"Unpaid Orders: {unpaidCount}");
            sb.AppendLine($"Invalid Entries Skipped: {errorLog.Count}");

            try
            {
                File.WriteAllText(ExportFile, sb.ToString());
                MessageBox.Show($"Summary exported to {ExportFile}", "Export Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not write file: {ex.Message}", "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            HideLoading();
        }

        #endregion

        #region Tab 2: Clients & Cars

        private void LoadClientsAndCars()
        {
            string makeFilter = comboBoxCarFilter.Text;
            string yearFilter = textBoxYearFilter.Text;

            var clientList = dbManager.GetClientsAndCars(makeFilter, yearFilter);

            listBoxClients.DataSource = clientList;
            listBoxClients.DisplayMember = "Name";
            listBoxClients.ValueMember = "ClientId";
        }

        private void ListBoxClients_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxClients.SelectedItem is Client selectedClient)
            {
                dataGridViewCars.DataSource = selectedClient.Cars.ToList();
            }
        }

        #endregion

        #region Tab 3: Diagnostics

        private void LoadDiagnosticsTab()
        {
            var cars = dbManager.GetCars();
            comboBoxDiagCar.DataSource = cars;
            comboBoxDiagCar.ValueMember = "CarId";

            // Custom display formatting
            comboBoxDiagCar.Format -= ComboBoxDiagCar_Format; // Avoid duplicate handlers
            comboBoxDiagCar.Format += ComboBoxDiagCar_Format;
        }

        private void ComboBoxDiagCar_Format(object sender, ListControlConvertEventArgs e)
        {
            if (e.ListItem is Car car)
            {
                e.Value = $"{car.Make} {car.Model} ({car.VIN})";
            }
        }

        private void ComBoxDiagCar_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxDiagCar.SelectedItem is Car selectedCar)
            {
                treeViewDiagnostics.Nodes.Clear();
                var diagnostics = dbManager.GetDiagnosticsForCar(selectedCar.CarId);

                foreach (var diag in diagnostics)
                {
                    TreeNode diagNode = new TreeNode($"Diagnostic {diag.DiagnosticId} ({diag.Date}) - OBD: {diag.OBDCodes}");
                    foreach (var test in diag.Tests)
                    {
                        TreeNode testNode = new TreeNode($"{test.Name}: {test.Result} (OK: {test.IsOk})");
                        if (!test.IsOk)
                        {
                            testNode.ForeColor = Color.Red;
                            testNode.NodeFont = new Font(treeViewDiagnostics.Font, FontStyle.Bold);
                        }
                        diagNode.Nodes.Add(testNode);
                    }
                    treeViewDiagnostics.Nodes.Add(diagNode);
                }
                treeViewDiagnostics.ExpandAll();
            }
        }

        #endregion

        #region Validation Helpers

        private bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email)) return false;
            return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }

        private bool IsValidPhone(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone)) return false;
            // Simple validation: at least 7 digits, allows + ( ) - and spaces
            return Regex.IsMatch(phone, @"^[\+\s\(\)\-0-9]{7,}$");
        }

        private bool IsValidVIN(string vin)
        {
            if (string.IsNullOrWhiteSpace(vin) || vin.Length != 17) return false;
            // Basic VIN check (no 'I', 'O', 'Q')
            return Regex.IsMatch(vin, @"^[A-HJ-NPR-Z0-9]{17}$");
        }

        #endregion

        #region UI Helpers (Loading Panel)

        private void ShowLoading(string message)
        {
            labelLoadingStatus.Text = message;
            panelLoading.Visible = true;
            panelLoading.BringToFront();
            this.UseWaitCursor = true;
            Application.DoEvents(); // Force UI update
        }

        private void HideLoading()
        {
            panelLoading.Visible = false;
            this.UseWaitCursor = false;
        }
        #endregion
    }
}