using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace eBook.Models
{
    public class BookService
    {
        private string GetDBConnectionString()
        {
            return
                System.Configuration.ConfigurationManager.ConnectionStrings["DBConn"].ConnectionString.ToString();
        }
        /// <summary>
        /// 新增書籍
        /// </summary>
        /// <param name="book"></param>
        /// <returns>員工編號</returns>
        public int InsertBook(Models.AddBook book)
        {
            string sql = @" INSERT INTO BOOK_DATA
						 (
                             BookName,BookAuthor,BookPublisher,BookNote,BookBoughtDate,BookClassName
						 )
						VALUES
						(
							 @BookName,@BookAuthor,@BookPublisher,@BookNote,@BookBoughtDate,@BookClassName
						)
						Select SCOPE_IDENTITY()";
            int EmployeeId;
            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@BookName", book.BookName == null ? (Object)DBNull.Value : book.BookName));
                cmd.Parameters.Add(new SqlParameter("@BookAuthor",book.BookAuthor == null ? (Object)DBNull.Value : book.BookAuthor));
                cmd.Parameters.Add(new SqlParameter("@BookPublisher", book.BookPublisher == null ? (Object)DBNull.Value : book.BookPublisher));
                cmd.Parameters.Add(new SqlParameter("@BookNote", book.BookNote == null ? (Object)DBNull.Value : book.BookNote));
                cmd.Parameters.Add(new SqlParameter("@BookBoughtDate", book.BookBoughtDate == null ? (Object)DBNull.Value : book.BookBoughtDate));
                cmd.Parameters.Add(new SqlParameter("@BookClassName", book.BookClassName == null ? (Object)DBNull.Value : book.BookClassName));
                EmployeeId = Convert.ToInt32(cmd.ExecuteScalar());
                conn.Close();
            }
            return EmployeeId;
        }
        /// <summary>
        /// 依查詢條件取得圖書
        /// </summary>
        /// <param name="arg">查詢條件</param>
        /// <returns></returns>
        public List<BookSearchResult> GetBookByCondition(BookSearchArg arg)
        {
            DataTable dt = new DataTable();
            string sql = @"SELECT DISTINCT D.BOOK_ID AS BookId,CL.BOOK_CLASS_NAME AS 圖書類別,D.BOOK_NAME AS 書名,CONVERT(VARCHAR,D.BOOK_BOUGHT_DATE,111) AS 購書日期,
                                  CO.CODE_NAME AS 借閱狀態,ISNULL(M.USER_ENAME,'') AS 借閱人
                           FROM BOOK_CLASS CL 
                                INNER JOIN BOOK_DATA D ON(CL.BOOK_CLASS_ID=D.BOOK_CLASS_ID)
                                LEFT JOIN MEMBER_M M ON(M.USER_ID=D.BOOK_KEEPER)
                                INNER JOIN BOOK_CODE CO ON(CO.CODE_ID=D.BOOK_STATUS)
                                AND CO.CODE_TYPE='BOOK_STATUS'
                           WHERE (D.BOOK_NAME LIKE '%'+@BOOK_NAME+'%' OR @BOOK_NAME='')AND
                                 (CL.BOOK_CLASS_NAME=@BOOK_CLASS_NAME OR @BOOK_CLASS_NAME='')AND
	                             (M.USER_ENAME=@USER_ENAME OR @USER_ENAME='')AND
	                             (CO.CODE_NAME=@CODE_NAME OR @CODE_NAME='')
                           ORDER BY 購書日期 DESC;";

            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@BOOK_NAME", arg.BookName== null ? string.Empty : arg.BookName));
                cmd.Parameters.Add(new SqlParameter("@BOOK_CLASS_NAME", arg.BookClassName == null ? string.Empty : arg.BookClassName));
                cmd.Parameters.Add(new SqlParameter("@USER_ENAME", arg.BookBoughter == null ? string.Empty : arg.BookBoughter));
                cmd.Parameters.Add(new SqlParameter("@CODE_NAME", arg.BookStatus == null ? string.Empty : arg.BookStatus));
                
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                sqlAdapter.Fill(dt);
                conn.Close();
            }
            return this.MapEmployeeDataToList(dt);
        }
        private List<Models.BookSearchResult> MapEmployeeDataToList(DataTable employeeData)
        {
            List<Models.BookSearchResult> result = new List<BookSearchResult>();
            foreach (DataRow row in employeeData.Rows)
            {
                result.Add(new BookSearchResult()
                {
                    BookName = row["圖書類別"].ToString(),
                    BookClassName =row["書名"].ToString(),
                    BookStatus = row["借閱狀態"].ToString(),
                    BookBoughtDate = row["購書日期"].ToString(),
                    BookBoughter = row["借閱人"].ToString(),
                });
            }
            return result;
        }
    }
}