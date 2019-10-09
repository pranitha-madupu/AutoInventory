using AutoImports.Inventory.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoImports.Inventory.BL
{
    public class BankTransBL : BaseBL
    {
        public List<BankTransModel> GetBankTrans(int bankId)
        {
            List<BankTransModel> result = new List<BankTransModel>();
            var ds = DB.ExecuteDataSet(SP.GetBankTrans,"@Bank_Id",bankId.ToString());
            foreach(DataRow r in ds.Tables[0].Rows)
            {
                BankTransModel obj = new BankTransModel();
                obj.Id = Convert.ToInt32(r["BankTrans_Id"]);
                obj.BTDate = Convert.ToDateTime(r["BankTrans_Date"]);
                obj.InventoryId = (r["Inventory_Id"] == DBNull.Value ? 0 : Convert.ToInt32(r["Inventory_Id"]));
                obj.ChequeNo = (r["Cheque_No"] == DBNull.Value ? "" : r["Cheque_No"].ToString());
                obj.Memo = (r["Memo"] == DBNull.Value ? "" : r["Memo"].ToString());
                obj.BankId = Convert.ToInt32(r["Bank_Id"]);
                obj.PayeeName = (r["Payee_Name"] == DBNull.Value ? "" : r["Payee_Name"].ToString());
                obj.TransType = (r["Trans_Type"] == DBNull.Value ? "" : r["Trans_Type"].ToString());
                obj.Amount = (r["Amount"] == DBNull.Value ? 0 : Convert.ToDouble(r["Amount"]));
                obj.PaymentStatus = (r["Payment_Status"] == DBNull.Value ? "" : r["Payment_Status"].ToString());
                obj.Vin = (r["Vin"] == DBNull.Value ? "" : r["Vin"].ToString());
                obj.CurrentBalance = (r["Current_Balance"] == DBNull.Value ? 0 : Convert.ToDouble(r["Current_Balance"]));
                result.Add(obj);
            }
            return result;
        }

        public int CUD(BankTransModel obj)
        {
            try
            {
                string[] paramNames = { "@BankTrans_Id", "@BankTrans_Date", "@Inventory_Id", "@Cheque_No", "@Memo", "@Bank_Id","@Payee_Name","@Trans_Type","@Amount","@Payment_Status", "@Operation" };
                object[] paramValues = { obj.Id, obj.BTDate, obj.InventoryId, obj.ChequeNo, obj.Memo,obj.BankId, obj.PayeeName, obj.TransType, obj.Amount, obj.PaymentStatus, obj.Operation };
                int res = DB.InsertData(SP.CUDBankTrans, paramNames, paramValues);
                return res;
            }
            catch
            {
                return 0;
            }
        }
    }
}
