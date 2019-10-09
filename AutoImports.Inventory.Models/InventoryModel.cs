using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoImports.Inventory.Models
{
    public class InventoryModel : BaseModel
    {
        public DateTime PurchaseDate { get; set; }
        public string VIN { get; set; }
        public string Color { get; set; }
        public string PurchaseFrom { get; set; }
        public string LocationFrom { get; set; }
        public int iYear { get; set; }
        public int VehicleModelId { get; set; }
        public int VehicleMakeId { get; set; }
        public string VehicleModel { get; set; }
        public string VehicleMake { get; set; }
        public DateTime SaleDate { get; set; }
        public double SalePrice { get; set; }
        public string SoldTo { get; set; }
        public string SoldBy { get; set; }
        public int IsRegistration { get; set; }
        public int StockInDays { get; set; }
        public string RegistrationDone { get; set; }
        public string TypeOfSale { get; set; }
        public string Notes { get; set; }
        public double OriginalCost { get; set; }
        public double TotalCost { get; set; }
        public double AddedCost { get; set; }
        public double Profit { get; set; }
        public string Status { get; set; }
        public List<AddedCostModel> AddedCosts { get; set; }
        public string IsPaid { get; set; }
        public int BankId { get; set; }
        public string Buyer { get; set; }
        public string VTitle { get; set; }
        public int SoldById { get; set; }
        public int PurchaseFromId { get; set; }
        public int BuyerId { get; set; }
        public double FloorCost { get; set; }
        public double DealerFee { get; set; }
    }
}
