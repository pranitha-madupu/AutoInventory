using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoImports.Inventory.BL
{
    public class SP
    {
        public static readonly string GetInventory = "[Xeeded].[usp_getInventory]";
        public static readonly string GetInventoryById = "[Xeeded].[usp_getInventoryById]";
        public static readonly string CUDVehicles = "[Xeeded].[usp_CUDVehicles]";
        public static readonly string SetArchiveInventory = "[Xeeded].[usp_setArchiveInventory]";
        public static readonly string GetVehicleMake = "[dbo].[usp_getVehicleMake]";
        public static readonly string CUDVehicleMake = "[Xeeded].[usp_CUDVehicleMake]";
        public static readonly string GetVehicleModel = "[dbo].[usp_getVehicleModel]";
        public static readonly string CUDVehicleModel = "[Xeeded].[usp_CUDVehicleModel]";
        public static readonly string MarkAsSold = "[Xeeded].[usp_markAsSold]";
        public static readonly string GetAddedCost = "[dbo].[usp_getAddedCost]";
        public static readonly string CUDAddedCost = "[Xeeded].[usp_CUDAddedCost]";
        public static readonly string GetUsers = "[Xeeded].[usp_getUsers]";
        public static readonly string GetBanks = "[Xeeded].[usp_getBanks]";
        public static readonly string GetBankTrans = "[Xeeded].[usp_getBankTrans]";
        public static readonly string GetRepresentatives = "[Xeeded].[usp_getRepresentatives]";
        public static readonly string GetFields = "[Xeeded].[usp_getFields]";
        public static readonly string GetForms = "[Xeeded].[usp_getForms]";
        public static readonly string GetPermissions = "[Xeeded].[usp_getPermissions]";
        public static readonly string GetAppUsers = "[Xeeded].[usp_App_getUsers]";
        public static readonly string GetAppAppointments = "[Xeeded].[usp_App_getAppointments]";
        public static readonly string CUDUsers = "[Xeeded].[usp_CUDUsers]";
        public static readonly string CUDBanks = "[Xeeded].[usp_CUDBanks]";
        public static readonly string CUDRepresentatives = "[Xeeded].[usp_CUDRepresentatives]";
        public static readonly string CUDBankTrans = "[Xeeded].[usp_CUDBankTransactions]";
        public static readonly string CUDAppUsers = "[Xeeded].[usp_App_CUDUsers]";
        public static readonly string CUDAppAppointments = "[Xeeded].[usp_App_CUDAppointments]";
        public static readonly string ValidateUser = "[Xeeded].[usp_validateUser]";
        public static readonly string ValidateAppUser = "[Xeeded].[usp_validateAppUser]";
        public static readonly string GetBookmarks = "[Xeeded].[usp_getBookmarks]";
        public static readonly string CUDBookmarks = "[Xeeded].[usp_CUDBookmarks]";
        public static readonly string GetDashboard = "[Xeeded].[usp_getDashboard]";
        public static readonly string GetExpenses = "[Xeeded].[usp_getExpenses]";
        public static readonly string GetExpenseTypes = "[Xeeded].[usp_getExpenseTypes]";
        public static readonly string CUDExpenses = "[Xeeded].[usp_CUDExpenses]";
        public static readonly string CUDExpenseTypes = "[Xeeded].[usp_CUDExpenseTypes]";
        public static readonly string CUDFTPVehicles = "[dbo].[usp_FTPVehicles]";
    }
}
