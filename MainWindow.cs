using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UMFST.MIP.Variant2.Models;

namespace UMFST.MIP.Variant2
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnImportData_Click(object sender, EventArgs e)
        {
            string url = "https://cdn.shopify.com/s/files/1/0883/3282/8936/files/data_car_service.json?v=1762418871";
            string jsonContent = string.Empty;

            // 1. Download JSON
            try
            {
                using (WebClient wc = new WebClient())
                {
                    jsonContent = wc.DownloadString(url);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Download failed: " + ex.Message);
                return;
            }

            // 2. Deserialize
            var root = JsonConvert.DeserializeObject<RootObject>(jsonContent);
            List<string> invalidRecords = new List<string>();
            List<Client> validClients = new List<Client>();
            List<WorkOrder> validOrders = new List<WorkOrder>();

            // 3. Validation Loop (Clients & Cars)
            foreach (var client in root.clients)
            {
                if (!client.email.Contains("@") || client.email.Length < 5)
                {
                    invalidRecords.Add($"Client {client.client_id}: Malformed email {client.email}");
                    continue; // Skip this client
                }

                List<Car> validCars = new List<Car>();
                foreach (var car in client.cars)
                {
                    // Validate VIN length (usually 17 chars, adjust based on actual data if needed)
                    // and non-negative odometer
                    if (car.vin.Length < 10 || car.odometer < 0)
                    {
                        invalidRecords.Add($"Car {car.vin}: Invalid VIN or negative odometer ({car.odometer})");
                        continue; // Skip car
                    }
                    validCars.Add(car);
                }
                client.cars = validCars;
                validClients.Add(client);
            }

            // 4. Validation Loop (Work Orders)
            foreach (var wo in root.work_orders)
            {
                if (wo.labor_hours < 0 || wo.hourly_rate < 0)
                {
                    invalidRecords.Add($"Order {wo.order_id}: Negative labor values");
                    continue;
                }
                validOrders.Add(wo);
            }

            // 5. Log Invalid Data
            File.WriteAllLines("invalid_car_service.txt", invalidRecords);

            // 6. Save to DB (Implementation depends on your choice of EF or raw SQL)
            // SaveToDatabase(validClients, validOrders);

            MessageBox.Show($"Imported {validClients.Count} clients and {validOrders.Count} orders. Errors logged.");

            // 7. Refresh Grids
            // RefreshUI();

        }
    }
}
