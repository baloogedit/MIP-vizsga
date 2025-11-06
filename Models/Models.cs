using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMFST.MIP.Variant2.Models
{
    public class RootObject
    {
        public Garage garage { get; set; }
        public List<Client> clients { get; set; }
        public List<WorkOrder> work_orders { get; set; }
    }

    public class Garage
    {
        public string name { get; set; }
        public List<Mechanic> mechanics { get; set; }
    }

    public class Mechanic
    {
        public string name { get; set; }
        public string specialty { get; set; }
    }

    public class Client
    {
        public int client_id { get; set; }
        public string name { get; set; }
        public string email { get; set; } // Needs validation
        public string phone { get; set; }
        public List<Car> cars { get; set; }
    }

    public class Car
    {
        public string vin { get; set; } // Needs validation
        public string make { get; set; }
        public string model { get; set; }
        public int year { get; set; }
        public int odometer { get; set; } // Needs validation (>0)
    }

    public class WorkOrder
    {
        public string order_id { get; set; }
        public string car_vin { get; set; }
        public string status { get; set; }
        public double labor_hours { get; set; } // Needs validation (>0)
        public double hourly_rate { get; set; } // Needs validation (>0)
        public List<TaskItem> tasks { get; set; }
        public List<Part> parts { get; set; }
        public Invoice invoice { get; set; }
        public Diagnostic diagnostic { get; set; }

        // Helper property for Total Cost computation required in Tab 1
        // Formula: (labor * rate) + sum(parts * qty)
        public double TotalCost
        {
            get
            {
                double laborCost = labor_hours * hourly_rate;
                double partsCost = 0;
                if (parts != null)
                {
                    foreach (var p in parts)
                    {
                        partsCost += p.unit_price * p.quantity;
                    }
                }
                return laborCost + partsCost;
            }
        }
    }

    public class TaskItem
    {
        public string description { get; set; }
    }

    public class Part
    {
        public string name { get; set; }
        public int quantity { get; set; }
        public double unit_price { get; set; }
    }

    public class Invoice
    {
        public bool paid { get; set; }
        public string currency { get; set; } // Validate this is a string, not number
    }

    public class Diagnostic
    {
        public string obd_code { get; set; }
        public List<TestResult> tests { get; set; }
    }

    public class TestResult
    {
        public string test_name { get; set; }
        public bool ok { get; set; }
    }
}
