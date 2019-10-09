using AutoImports.Inventory.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AutoImports.Inventory.BL
{
    public class UsersBL : BaseBL
    {
        public List<UsersModel> GetUsers()
        {
            var ds = DB.ExecuteDataSet(SP.GetUsers);
            return ds.Tables[0].AsEnumerable().Select(r => new UsersModel()
            {
                Id = Convert.ToInt32(r["User_Id"]),
                UserName = r["User_Name"].ToString(),
                Password = DecryptPassword(r["UPassword"].ToString()),
                Role = r["URole"].ToString()
            }).ToList();
        }

        public List<UsersModel> GetAppUsers()
        {
            var ds = DB.ExecuteDataSet(SP.GetAppUsers);
            return ds.Tables[0].AsEnumerable().Select(r => new UsersModel()
            {
                Id = Convert.ToInt32(r["User_Id"]),
                FirstName = r["FirstName"].ToString(),
                LastName =  (r["LastName"] != null ? r["LastName"].ToString() : ""),
                Email = (r["Email"] != null ? r["Email"].ToString() : ""),
                Password = DecryptPassword(r["Password"].ToString())
            }).ToList();
        }

        public List<UsersModel> GetAppAppointments()
        {
            var ds = DB.ExecuteDataSet(SP.GetAppAppointments);
            return ds.Tables[0].AsEnumerable().Select(r => new UsersModel()
            {
                Id = Convert.ToInt32(r["Appointment_Id"]),
                CreatedDate = DateTime.Parse(r["ADateTime"].ToString()),
                FirstName = r["FirstName"].ToString(),
                LastName = (r["LastName"] != null ? r["LastName"].ToString() : ""),
                Email = (r["Email"] != null ? r["Email"].ToString() : ""),
                Phone = (r["Phone"] != null ? r["Phone"].ToString() : ""),
                Comments = (r["Comments"] != null ? r["Comments"].ToString() : "")
            }).ToList();
        }

        public UsersModel ValidateUser(string u, string p)
        {
            string[] paramNames = { "@User_Name", "@Password"};
            object[] paramValues = { u, EncryptPassword(p) };
            var ds = DB.ExecuteDataSet(SP.ValidateUser, paramNames, paramValues);

            if(ds.Tables[0].Rows.Count > 0)
            {
                UsersModel obj = new UsersModel();
                DataRow r = ds.Tables[0].Rows[0];
                obj.Id = Convert.ToInt32(r["User_Id"]);
                obj.UserName = r["User_Name"].ToString();
                //Password = DecryptPassword(r["UPassword"].ToString()),
                obj.Role = r["URole"].ToString();
                return obj;
            }
            return null;
        }
        public UsersModel ValidateAppUser(string u, string p)
        {
            string[] paramNames = { "@Email", "@Password" };
            object[] paramValues = { u, EncryptPassword(p) };
            var ds = DB.ExecuteDataSet(SP.ValidateAppUser, paramNames, paramValues);

            if (ds.Tables[0].Rows.Count > 0)
            {
                UsersModel obj = new UsersModel();
                DataRow r = ds.Tables[0].Rows[0];
                obj.Id = Convert.ToInt32(r["User_Id"]);
                obj.UserName = r["Email"].ToString();
                obj.FirstName = r["First_Name"].ToString();
                obj.LastName = r["Last_Name"].ToString();
                return obj;
            }
            return null;
        }
        public int CUD(UsersModel obj)
        {
            try
            {
                string[] paramNames = { "@User_Id", "@User_Name", "@Password", "@Role", "@Permissions", "@Operation" };
                object[] paramValues = { obj.Id, obj.UserName, EncryptPassword(obj.Password), obj.Role,obj.Permissions, obj.Operation };
                int res = DB.InsertData(SP.CUDUsers, paramNames, paramValues);
                return res;
            }
            catch
            {
                return 0;
            }
        }
        public int CUDAppUser(UsersModel obj)
        {
            try
            {
                string[] paramNames = { "@User_Id", "@FirstName", "@LastName","@Email", "@Password", "@Operation" };
                object[] paramValues = { obj.Id, obj.FirstName, obj.LastName, obj.Email, EncryptPassword(obj.Password), obj.Role, obj.Operation };
                int res = DB.InsertData(SP.CUDAppUsers, paramNames, paramValues);
                return res;
            }
            catch
            {
                return 0;
            }
        }
        public int CUDAppAppointment(UsersModel obj)
        {
            try
            {
                string[] paramNames = { "@Appointment_Id","@ADateTime", "@FirstName", "@LastName", "@Email", "@Phone", "@Comments", "@Operation" };
                object[] paramValues = { obj.Id, obj.CreatedDate,obj.FirstName,  obj.LastName, obj.Email, obj.Phone, obj.Comments , obj.Operation };
                int res = DB.InsertData(SP.CUDAppAppointments, paramNames, paramValues);
                return res;
            }
            catch
            {
                return 0;
            }
        }
        private string EncryptPassword(string clearText)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }
        private string DecryptPassword(string cipherText)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }
    }
}
