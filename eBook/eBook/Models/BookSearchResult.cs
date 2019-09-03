using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eBook.Models
{
    public class BookSearchResult
    {
        /// <summary>
        /// 書籍編號
        /// </summary>
        public string BookId{ get; set; }
        /// <summary>
        /// 書籍名稱
        /// </summary>
        public string BookName { get; set; }
        /// <summary>
        /// 圖書類別
        /// </summary>
        public string BookClassName { get; set; }
        /// <summary>
        /// 借閱狀態
        /// </summary>
        public string BookStatus { get; set; }
        /// <summary>
        /// 借閱日期
        /// </summary>
        public string BookBoughtDate { get; set; }
        /// <summary>
        /// 借閱人
        /// </summary>
        public string BookBoughter { get; set; }

    }
}