using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace eBook.Models
{
    public class CodeService
    {
        private string GetDBConnectionString()
        {
            return
                System.Configuration.ConfigurationManager.ConnectionStrings["DBConn"].ConnectionString.ToString();
        }
        /// <summary>
        /// 取得圖書類別
        /// </summary>
        /// <returns></returns>
        public List<SelectListItem> GetBookClassName()
        {
            DataTable dt = new DataTable(); //宣告datatable物件
            string sql = @"Select BOOK_CLASS_NAME As ClassName 
                           FROM BOOK_CLASS";
            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn); //宣告一個SqlCommand,並將connection和sql語法傳入
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd); 
                sqlAdapter.Fill(dt); //橋接器將資料拿出放入資料表裡
                conn.Close();
            }
            return this.MapCodeData(dt);
        }
        /// <summary>
        /// 取得借閱人
        /// </summary>
        /// <returns></returns>
        public List<SelectListItem> GetBookBoughter()
        {
            DataTable dt = new DataTable();
            string sql = @"Select USER_ENAME As BoughterName 
                           FROM MEMBER_M";
            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                sqlAdapter.Fill(dt);
                conn.Close();
            }
            return this.MapCodeDataBoughter(dt);
        }
        /// <summary>
        /// 取得書籍狀態
        /// </summary>
        /// <returns></returns>
        public List<SelectListItem> GetBookStatus()
        {
            DataTable dt = new DataTable();
            string sql = @"Select DISTINCT CODE_NAME As BookType 
                           FROM BOOK_CODE
                           WHERE CODE_TYPE='BOOK_STATUS'";
            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                sqlAdapter.Fill(dt);
                conn.Close();
            }
            return this.MapCodeDataStatus(dt);
        }
        /// <summary>
        /// Maping 代碼資料
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private List<SelectListItem> MapCodeData(DataTable dt)
        {
            List<SelectListItem> result = new List<SelectListItem>();
            foreach (DataRow row in dt.Rows)
            {
                result.Add(new SelectListItem()
                {
                    Text = row["ClassName"].ToString() ,
                    Value = row["ClassName"].ToString()
                });
            }
            return result;
        }
        private List<SelectListItem> MapCodeDataBoughter(DataTable dt)
        {
            List<SelectListItem> result = new List<SelectListItem>();
            foreach (DataRow row in dt.Rows)
            {
                result.Add(new SelectListItem()
                {
                    Text = row["BoughterName"].ToString(),
                    Value = row["BoughterName"].ToString()
                });
            }
            return result;
        }
        private List<SelectListItem> MapCodeDataStatus(DataTable dt)
        {
            List<SelectListItem> result = new List<SelectListItem>();
            foreach (DataRow row in dt.Rows)
            {
                result.Add(new SelectListItem()
                {
                    Text = row["BookType"].ToString(),
                    Value = row["BookType"].ToString()
                });
            }
            return result;
        }
    }
}