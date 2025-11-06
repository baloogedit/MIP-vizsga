using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using UMFST.MIP.Variant2.Models;
using static UMFST.MIP.Variant2.Database.CarServiceContext;

namespace UMFST.MIP.Variant2.Database
{
    public class DatabaseManager
    {
        private string connectionString = "Data Source=service.db";

        public DatabaseManager()
        {
            if (!File.Exists("service.db"))
            {
                SQLiteConnection.CreateFile("service.db");
            }
            InitializeDatabase();
        }

        private SQLiteConnection GetConnection()
        {
            var conn = new SQLiteConnection(connectionString);
            conn.Open();
            return conn;
        }

        public void InitializeDatabase()
        {
            using (var conn = GetConnection())
            {
                string[] createTableQueries = new string[]
                {
                    "CREATE TABLE IF NOT EXISTS Mechanics (MechanicId INTEGER PRIMARY KEY, Name TEXT, Specialization TEXT)",
                    "CREATE TABLE IF NOT EXISTS Clients (ClientId INTEGER PRIMARY KEY, Name TEXT, Email TEXT, Phone TEXT)",
                    "CREATE TABLE IF NOT EXISTS Cars (CarId INTEGER PRIMARY KEY, ClientId INTEGER, Make TEXT, Model TEXT, Year INTEGER, VIN TEXT, Odometer INTEGER, FOREIGN KEY(ClientId) REFERENCES Clients(ClientId))",
                    "CREATE TABLE IF NOT EXISTS WorkOrders (WorkOrderId INTEGER PRIMARY KEY, CarId INTEGER, MechanicId INTEGER, Date TEXT, Description TEXT, Status TEXT, FOREIGN KEY(CarId) REFERENCES Cars(CarId), FOREIGN KEY(MechanicId) REFERENCES Mechanics(MechanicId))",
                    "CREATE TABLE IF NOT EXISTS Tasks (TaskId INTEGER PRIMARY KEY, WorkOrderId INTEGER, Description TEXT, LaborHours REAL, Rate REAL, FOREIGN KEY(WorkOrderId) REFERENCES WorkOrders(WorkOrderId))",
                    "CREATE TABLE IF NOT EXISTS Parts (PartId INTEGER PRIMARY KEY, WorkOrderId INTEGER, Name TEXT, Quantity INTEGER, UnitPrice REAL, FOREIGN KEY(WorkOrderId) REFERENCES WorkOrders(WorkOrderId))",
                    "CREATE TABLE IF NOT EXISTS Invoices (InvoiceId INTEGER PRIMARY KEY, WorkOrderId INTEGER, Amount REAL, Date TEXT, IsPaid INTEGER, Currency TEXT, FOREIGN KEY(WorkOrderId) REFERENCES WorkOrders(WorkOrderId))",
                    "CREATE TABLE IF NOT EXISTS Diagnostics (DiagnosticId INTEGER PRIMARY KEY, CarId INTEGER, Date TEXT, OBDCodes TEXT, FOREIGN KEY(CarId) REFERENCES Cars(CarId))",
                    "CREATE TABLE IF NOT EXISTS Tests (TestId INTEGER PRIMARY KEY, DiagnosticId INTEGER, Name TEXT, Result TEXT, IsOk INTEGER, FOREIGN KEY(DiagnosticId) REFERENCES Diagnostics(DiagnosticId))"
                };

                foreach (var query in createTableQueries)
                {
                    using (var cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        public void ResetDatabase()
        {
            using (var conn = GetConnection())
            {
                string[] dropTableQueries = new string[]
                {
                    "DROP TABLE IF EXISTS Tests",
                    "DROP TABLE IF EXISTS Diagnostics",
                    "DROP TABLE IF EXISTS Invoices",
                    "DROP TABLE IF EXISTS Parts",
                    "DROP TABLE IF EXISTS Tasks",
                    "DROP TABLE IF EXISTS WorkOrders",
                    "DROP TABLE IF EXISTS Cars",
                    "DROP TABLE IF EXISTS Clients",
                    "DROP TABLE IF EXISTS Mechanics"
                };

                foreach (var query in dropTableQueries)
                {
                    using (var cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            InitializeDatabase(); // Re-create the tables
        }

        // --- Insert Methods ---

        public void InsertMechanic(Mechanic m)
        {
            using (var conn = GetConnection())
            {
                var cmd = new SQLiteCommand("INSERT INTO Mechanics (MechanicId, Name, Specialization) VALUES (@Id, @Name, @Spec)", conn);
                cmd.Parameters.AddWithValue("@Id", m.MechanicId);
                cmd.Parameters.AddWithValue("@Name", m.Name);
                cmd.Parameters.AddWithValue("@Spec", m.Specialization);
                cmd.ExecuteNonQuery();
            }
        }

        public void InsertClient(Client c)
        {
            using (var conn = GetConnection())
            {
                var cmd = new SQLiteCommand("INSERT INTO Clients (ClientId, Name, Email, Phone) VALUES (@Id, @Name, @Email, @Phone)", conn);
                cmd.Parameters.AddWithValue("@Id", c.ClientId);
                cmd.Parameters.AddWithValue("@Name", c.Name);
                cmd.Parameters.AddWithValue("@Email", c.Email);
                cmd.Parameters.AddWithValue("@Phone", c.Phone);
                cmd.ExecuteNonQuery();
            }
        }

        public void InsertCar(Car car)
        {
            using (var conn = GetConnection())
            {
                var cmd = new SQLiteCommand("INSERT INTO Cars (CarId, ClientId, Make, Model, Year, VIN, Odometer) VALUES (@CarId, @ClientId, @Make, @Model, @Year, @VIN, @Odometer)", conn);
                cmd.Parameters.AddWithValue("@CarId", car.CarId);
                cmd.Parameters.AddWithValue("@ClientId", car.ClientId);
                cmd.Parameters.AddWithValue("@Make", car.Make);
                cmd.Parameters.AddWithValue("@Model", car.Model);
                cmd.Parameters.AddWithValue("@Year", car.Year);
                cmd.Parameters.AddWithValue("@VIN", car.VIN);
                cmd.Parameters.AddWithValue("@Odometer", car.Odometer);
                cmd.ExecuteNonQuery();
            }
        }

        public void InsertWorkOrder(WorkOrder wo)
        {
            using (var conn = GetConnection())
            {
                var cmd = new SQLiteCommand("INSERT INTO WorkOrders (WorkOrderId, CarId, MechanicId, Date, Description, Status) VALUES (@WoId, @CarId, @MechId, @Date, @Desc, @Status)", conn);
                cmd.Parameters.AddWithValue("@WoId", wo.WorkOrderId);
                cmd.Parameters.AddWithValue("@CarId", wo.CarId);
                cmd.Parameters.AddWithValue("@MechId", wo.MechanicId);
                cmd.Parameters.AddWithValue("@Date", wo.Date);
                cmd.Parameters.AddWithValue("@Desc", wo.Description);
                cmd.Parameters.AddWithValue("@Status", wo.Status);
                cmd.ExecuteNonQuery();
            }
        }

        public void InsertTask(Task task)
        {
            using (var conn = GetConnection())
            {
                var cmd = new SQLiteCommand("INSERT INTO Tasks (TaskId, WorkOrderId, Description, LaborHours, Rate) VALUES (@TaskId, @WoId, @Desc, @Hours, @Rate)", conn);
                cmd.Parameters.AddWithValue("@TaskId", task.TaskId);
                cmd.Parameters.AddWithValue("@WoId", task.WorkOrderId);
                cmd.Parameters.AddWithValue("@Desc", task.Description);
                cmd.Parameters.AddWithValue("@Hours", task.LaborHours);
                cmd.Parameters.AddWithValue("@Rate", task.Rate);
                cmd.ExecuteNonQuery();
            }
        }

        public void InsertPart(Part part)
        {
            using (var conn = GetConnection())
            {
                var cmd = new SQLiteCommand("INSERT INTO Parts (PartId, WorkOrderId, Name, Quantity, UnitPrice) VALUES (@PartId, @WoId, @Name, @Qty, @Price)", conn);
                cmd.Parameters.AddWithValue("@PartId", part.PartId);
                cmd.Parameters.AddWithValue("@WoId", part.WorkOrderId);
                cmd.Parameters.AddWithValue("@Name", part.Name);
                cmd.Parameters.AddWithValue("@Qty", part.Quantity);
                cmd.Parameters.AddWithValue("@Price", part.UnitPrice);
                cmd.ExecuteNonQuery();
            }
        }

        public void InsertInvoice(Invoice inv)
        {
            using (var conn = GetConnection())
            {
                var cmd = new SQLiteCommand("INSERT INTO Invoices (InvoiceId, WorkOrderId, Amount, Date, IsPaid, Currency) VALUES (@InvId, @WoId, @Amount, @Date, @IsPaid, @Currency)", conn);
                cmd.Parameters.AddWithValue("@InvId", inv.InvoiceId);
                cmd.Parameters.AddWithValue("@WoId", inv.WorkOrderId);
                cmd.Parameters.AddWithValue("@Amount", inv.Amount);
                cmd.Parameters.AddWithValue("@Date", inv.Date);
                cmd.Parameters.AddWithValue("@IsPaid", inv.IsPaid ? 1 : 0);
                cmd.Parameters.AddWithValue("@Currency", inv.Currency);
                cmd.ExecuteNonQuery();
            }
        }
        public void InsertDiagnostic(Diagnostic diag)
        {
            using (var conn = GetConnection())
            {
                var cmd = new SQLiteCommand("INSERT INTO Diagnostics (DiagnosticId, CarId, Date, OBDCodes) VALUES (@DiagId, @CarId, @Date, @OBD)", conn);
                cmd.Parameters.AddWithValue("@DiagId", diag.DiagnosticId);
                cmd.Parameters.AddWithValue("@CarId", diag.CarId);
                cmd.Parameters.AddWithValue("@Date", diag.Date);
                cmd.Parameters.AddWithValue("@OBD", diag.OBDCodes);
                cmd.ExecuteNonQuery();
            }
        }

        public void InsertTest(Test test)
        {
            using (var conn = GetConnection())
            {
                var cmd = new SQLiteCommand("INSERT INTO Tests (TestId, DiagnosticId, Name, Result, IsOk) VALUES (@TestId, @DiagId, @Name, @Result, @IsOk)", conn);
                cmd.Parameters.AddWithValue("@TestId", test.TestId);
                cmd.Parameters.AddWithValue("@DiagId", test.DiagnosticId);
                cmd.Parameters.AddWithValue("@Name", test.Name);
                cmd.Parameters.AddWithValue("@Result", test.Result);
                cmd.Parameters.AddWithValue("@IsOk", test.IsOk ? 1 : 0);
                cmd.ExecuteNonQuery();
            }
        }

        // --- Existence Checks ---

        public bool DoesCarExist(int carId)
        {
            using (var conn = GetConnection())
            {
                var cmd = new SQLiteCommand("SELECT COUNT(1) FROM Cars WHERE CarId = @CarId", conn);
                cmd.Parameters.AddWithValue("@CarId", carId);
                return (long)cmd.ExecuteScalar() > 0;
            }
        }

        public bool DoesMechanicExist(int mechId)
        {
            using (var conn = GetConnection())
            {
                var cmd = new SQLiteCommand("SELECT COUNT(1) FROM Mechanics WHERE MechanicId = @MechId", conn);
                cmd.Parameters.AddWithValue("@MechId", mechId);
                return (long)cmd.ExecuteScalar() > 0;
            }
        }

        // --- Get Methods for UI ---

        public List<WorkOrderViewModel> GetWorkOrders()
        {
            var list = new List<WorkOrderViewModel>();
            string query = @"
                SELECT
                    wo.WorkOrderId,
                    c.Make || ' ' || c.Model AS CarName,
                    wo.Status,
                    CASE WHEN i.IsPaid = 1 THEN 'Paid' ELSE 'Unpaid' END AS PaidStatus,
                    (SELECT SUM(t.LaborHours * t.Rate) FROM Tasks t WHERE t.WorkOrderId = wo.WorkOrderId) +
                    (SELECT SUM(p.Quantity * p.UnitPrice) FROM Parts p WHERE p.WorkOrderId = wo.WorkOrderId) AS TotalCost
                FROM WorkOrders wo
                JOIN Cars c ON wo.CarId = c.CarId
                LEFT JOIN Invoices i ON wo.WorkOrderId = i.WorkOrderId
            ";

            using (var conn = GetConnection())
            using (var cmd = new SQLiteCommand(query, conn))
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    list.Add(new WorkOrderViewModel
                    {
                        WorkOrderId = reader.GetInt32(0),
                        Car = reader.GetString(1),
                        Status = reader.GetString(2),
                        PaidStatus = reader.GetString(3),
                        TotalCost = reader.IsDBNull(4) ? 0 : reader.GetDecimal(4)
                    });
                }
            }
            return list;
        }

        public List<Client> GetClientsAndCars(string makeFilter, string yearFilter)
        {
            var clients = new Dictionary<int, Client>();
            var cars = new List<Car>();

            // Base queries
            string clientQuery = "SELECT ClientId, Name, Email, Phone FROM Clients";
            string carQuery = "SELECT CarId, ClientId, Make, Model, Year, VIN, Odometer FROM Cars";

            // Apply filters
            var conditions = new List<string>();
            if (!string.IsNullOrWhiteSpace(makeFilter))
                conditions.Add("Make LIKE @Make");
            if (int.TryParse(yearFilter, out _))
                conditions.Add("Year = @Year");

            if (conditions.Count > 0)
                carQuery += " WHERE " + string.Join(" AND ", conditions);

            using (var conn = GetConnection())
            {
                // Get all cars that match filter
                using (var cmd = new SQLiteCommand(carQuery, conn))
                {
                    if (!string.IsNullOrWhiteSpace(makeFilter))
                        cmd.Parameters.AddWithValue("@Make", $"%{makeFilter}%");
                    if (int.TryParse(yearFilter, out int year))
                        cmd.Parameters.AddWithValue("@Year", year);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            cars.Add(new Car
                            {
                                CarId = reader.GetInt32(0),
                                ClientId = reader.GetInt32(1),
                                Make = reader.GetString(2),
                                Model = reader.GetString(3),
                                Year = reader.GetInt32(4),
                                VIN = reader.GetString(5),
                                Odometer = reader.GetInt32(6)
                            });
                        }
                    }
                }

                // If filtering, we only want clients who own the filtered cars
                if (cars.Count > 0 && conditions.Count > 0)
                {
                    var clientIds = new HashSet<int>(cars.Select(c => c.ClientId));
                    var sb = new StringBuilder(" WHERE ClientId IN (");
                    int i = 0;
                    foreach (int id in clientIds)
                    {
                        sb.Append(id);
                        if (++i < clientIds.Count) sb.Append(",");
                    }
                    sb.Append(")");
                    clientQuery += sb.ToString();
                }
                else if (conditions.Count > 0 && cars.Count == 0)
                {
                    return new List<Client>(); // No cars matched, so no clients
                }


                // Get all matching clients
                using (var cmd = new SQLiteCommand(clientQuery, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var client = new Client
                        {
                            ClientId = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Email = reader.GetString(2),
                            Phone = reader.GetString(3)
                        };
                        clients.Add(client.ClientId, client);
                    }
                }

                // Attach cars to their clients
                foreach (var car in cars)
                {
                    if (clients.ContainsKey(car.ClientId))
                    {
                        clients[car.ClientId].Cars.Add(car);
                    }
                }
            }
            return clients.Values.ToList();
        }

        public List<Car> GetCars()
        {
            var list = new List<Car>();
            using (var conn = GetConnection())
            using (var cmd = new SQLiteCommand("SELECT CarId, Make, Model, VIN FROM Cars", conn))
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    list.Add(new Car
                    {
                        CarId = reader.GetInt32(0),
                        Make = reader.GetString(1),
                        Model = reader.GetString(2),
                        VIN = reader.GetString(3)
                    });
                }
            }
            return list;
        }

        public List<Diagnostic> GetDiagnosticsForCar(int carId)
        {
            var diags = new Dictionary<int, Diagnostic>();
            using (var conn = GetConnection())
            {
                // Get diagnostics
                var diagCmd = new SQLiteCommand("SELECT DiagnosticId, Date, OBDCodes FROM Diagnostics WHERE CarId = @CarId", conn);
                diagCmd.Parameters.AddWithValue("@CarId", carId);
                using (var reader = diagCmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var d = new Diagnostic
                        {
                            DiagnosticId = reader.GetInt32(0),
                            Date = reader.GetString(1),
                            OBDCodes = reader.GetString(2),
                            CarId = carId
                        };
                        diags.Add(d.DiagnosticId, d);
                    }
                }

                // Get tests for all found diagnostics
                var testCmd = new SQLiteCommand("SELECT TestId, DiagnosticId, Name, Result, IsOk FROM Tests WHERE DiagnosticId IN (SELECT DiagnosticId FROM Diagnostics WHERE CarId = @CarId)", conn);
                testCmd.Parameters.AddWithValue("@CarId", carId);
                using (var reader = testCmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var t = new Test
                        {
                            TestId = reader.GetInt32(0),
                            DiagnosticId = reader.GetInt32(1),
                            Name = reader.GetString(2),
                            Result = reader.GetString(3),
                            IsOk = reader.GetInt32(4) == 1
                        };
                        if (diags.ContainsKey(t.DiagnosticId))
                        {
                            diags[t.DiagnosticId].Tests.Add(t);
                        }
                    }
                }
            }
            return diags.Values.ToList();
        }

        // Get all work orders for export
        public List<WorkOrder> GetWorkOrdersForExport()
        {
            var woDict = new Dictionary<int, WorkOrder>();
            var carDict = new Dictionary<int, Car>();
            var invDict = new Dictionary<int, Invoice>();
            var taskDict = new Dictionary<int, List<Task>>();
            var partDict = new Dictionary<int, List<Part>>();

            using (var conn = GetConnection())
            {
                // Get Cars
                using (var cmd = new SQLiteCommand("SELECT CarId, Make, Model FROM Cars", conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read()) carDict.Add(reader.GetInt32(0), new Car { CarId = reader.GetInt32(0), Make = reader.GetString(1), Model = reader.GetString(2) });
                }

                // Get Invoices
                using (var cmd = new SQLiteCommand("SELECT InvoiceId, WorkOrderId, IsPaid FROM Invoices", conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read()) invDict.Add(reader.GetInt32(1), new Invoice { InvoiceId = reader.GetInt32(0), WorkOrderId = reader.GetInt32(1), IsPaid = reader.GetInt32(2) == 1 });
                }

                // Get Tasks
                using (var cmd = new SQLiteCommand("SELECT TaskId, WorkOrderId, LaborHours, Rate FROM Tasks", conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int woId = reader.GetInt32(1);
                        if (!taskDict.ContainsKey(woId)) taskDict.Add(woId, new List<Task>());
                        taskDict[woId].Add(new Task { TaskId = reader.GetInt32(0), WorkOrderId = woId, LaborHours = reader.GetDecimal(2), Rate = reader.GetDecimal(3) });
                    }
                }

                // Get Parts
                using (var cmd = new SQLiteCommand("SELECT PartId, WorkOrderId, Quantity, UnitPrice FROM Parts", conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int woId = reader.GetInt32(1);
                        if (!partDict.ContainsKey(woId)) partDict.Add(woId, new List<Part>());
                        partDict[woId].Add(new Part { PartId = reader.GetInt32(0), WorkOrderId = woId, Quantity = reader.GetInt32(2), UnitPrice = reader.GetDecimal(3) });
                    }
                }

                // Get WorkOrders
                using (var cmd = new SQLiteCommand("SELECT WorkOrderId, CarId FROM WorkOrders", conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int woId = reader.GetInt32(0);
                        int carId = reader.GetInt32(1);
                        var wo = new WorkOrder { WorkOrderId = woId, CarId = carId };

                        if (carDict.ContainsKey(carId)) wo.Car = carDict[carId];
                        if (invDict.ContainsKey(woId)) wo.Invoice = invDict[woId];
                        if (taskDict.ContainsKey(woId)) wo.Tasks = taskDict[woId];
                        if (partDict.ContainsKey(woId)) wo.Parts = partDict[woId];

                        woDict.Add(woId, wo);
                    }
                }
            }
            return woDict.Values.ToList();
        }
    }
}
