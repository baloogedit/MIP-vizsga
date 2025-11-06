using Newtonsoft.Json;
using System.Collections.Generic;

namespace UMFST.MIP.Variant2.Models
{
    // --- DATABASE ENTITY MODELS ---

    public class Client
    {
        public int ClientId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public ICollection<Car> Cars { get; set; } = new List<Car>();
    }

    public class Car
    {
        public int CarId { get; set; }
        public int ClientId { get; set; }
        public Client Client { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string VIN { get; set; }
        public int Odometer { get; set; }
        public ICollection<WorkOrder> WorkOrders { get; set; } = new List<WorkOrder>();
        public ICollection<Diagnostic> Diagnostics { get; set; } = new List<Diagnostic>();
    }

    public class WorkOrder
    {
        public int WorkOrderId { get; set; }
        public int CarId { get; set; }
        public Car Car { get; set; }
        public int MechanicId { get; set; }
        public Mechanic Mechanic { get; set; }
        public string Date { get; set; } // Using string for simplicity from JSON
        public string Description { get; set; }
        public string Status { get; set; }
        public ICollection<Task> Tasks { get; set; } = new List<Task>();
        public ICollection<Part> Parts { get; set; } = new List<Part>();
        public Invoice Invoice { get; set; }
    }

    public class Task
    {
        public int TaskId { get; set; }
        public int WorkOrderId { get; set; }
        public WorkOrder WorkOrder { get; set; }
        public string Description { get; set; }
        public decimal LaborHours { get; set; }
        public decimal Rate { get; set; }
    }

    public class Part
    {
        public int PartId { get; set; }
        public int WorkOrderId { get; set; }
        public WorkOrder WorkOrder { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }

    public class Mechanic
    {
        public int MechanicId { get; set; }
        public string Name { get; set; }
        public string Specialization { get; set; }
        public ICollection<WorkOrder> WorkOrders { get; set; } = new List<WorkOrder>();
    }

    public class Invoice
    {
        public int InvoiceId { get; set; }
        public int WorkOrderId { get; set; }
        public WorkOrder WorkOrder { get; set; }
        public decimal Amount { get; set; } // This will be the one from JSON, we compute the real total.
        public string Date { get; set; }
        public bool IsPaid { get; set; }
        public string Currency { get; set; }
    }

    public class Diagnostic
    {
        public int DiagnosticId { get; set; }
        public int CarId { get; set; }
        public Car Car { get; set; }
        public string Date { get; set; }
        public string OBDCodes { get; set; } // Comma-separated list
        public ICollection<Test> Tests { get; set; } = new List<Test>();
    }

    public class Test
    {
        public int TestId { get; set; }
        public int DiagnosticId { get; set; }
        public Diagnostic Diagnostic { get; set; }
        public string Name { get; set; }
        public string Result { get; set; }
        public bool IsOk { get; set; }
    }

    // --- View Model for Work Order Data Grid ---
    public class WorkOrderViewModel
    {
        public int WorkOrderId { get; set; }
        public string Car { get; set; }
        public string Status { get; set; }
        public string PaidStatus { get; set; }
        public decimal TotalCost { get; set; }
    }


    // --- CLASSES FOR JSON DESERIALIZATION ---

    public class JsonDataRoot
    {
        public JsonMeta Meta { get; set; }
        public JsonGarage Garage { get; set; }
        public List<JsonClient> Clients { get; set; }
        [JsonProperty("work_orders")]
        public List<JsonWorkOrder> WorkOrders { get; set; }
        public List<JsonDiagnostic> Diagnostics { get; set; }
    }

    public class JsonMeta
    {
        [JsonProperty("data_version")]
        public string DataVersion { get; set; }
        [JsonProperty("generated_at")]
        public string GeneratedAt { get; set; }
    }

    public class JsonGarage
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public List<JsonMechanic> Mechanics { get; set; }
    }

    public class JsonMechanic
    {
        [JsonProperty("mechanic_id")]
        public int MechanicId { get; set; }
        public string Name { get; set; }
        public string Specialization { get; set; }
    }

    public class JsonClient
    {
        [JsonProperty("client_id")]
        public int ClientId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public List<JsonCar> Cars { get; set; }
    }

    public class JsonCar
    {
        [JsonProperty("car_id")]
        public int CarId { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string VIN { get; set; }
        public int Odometer { get; set; }
    }

    public class JsonWorkOrder
    {
        [JsonProperty("work_order_id")]
        public int WorkOrderId { get; set; }
        [JsonProperty("car_id")]
        public int CarId { get; set; }
        [JsonProperty("mechanic_id")]
        public int MechanicId { get; set; }
        public string Date { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public List<JsonTask> Tasks { get; set; }
        public List<JsonPart> Parts { get; set; }
        public JsonInvoice Invoice { get; set; }
    }

    public class JsonTask
    {
        [JsonProperty("task_id")]
        public int TaskId { get; set; }
        public string Description { get; set; }
        [JsonProperty("labor_hours")]
        public decimal LaborHours { get; set; }
        public decimal Rate { get; set; }
    }

    public class JsonPart
    {
        [JsonProperty("part_id")]
        public int PartId { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        [JsonProperty("unit_price")]
        public decimal UnitPrice { get; set; }
    }

    public class JsonInvoice
    {
        [JsonProperty("invoice_id")]
        public int InvoiceId { get; set; }
        public decimal Amount { get; set; }
        public string Date { get; set; }
        [JsonProperty("is_paid")]
        public bool IsPaid { get; set; }
        public object Currency { get; set; } // Can be number 978 or string "EUR"
    }

    public class JsonDiagnostic
    {
        [JsonProperty("diagnostic_id")]
        public int DiagnosticId { get; set; }
        [JsonProperty("car_id")]
        public int CarId { get; set; }
        public string Date { get; set; }
        [JsonProperty("obd_codes")]
        public List<string> OBDCodes { get; set; }
        [JsonProperty("test_results")]
        public List<JsonTest> TestResults { get; set; }
    }

    public class JsonTest
    {
        [JsonProperty("test_id")]
        public int TestId { get; set; }
        [JsonProperty("test_name")]
        public string TestName { get; set; }
        public string Result { get; set; }
        [JsonProperty("ok")]
        public bool IsOk { get; set; }
    }
}