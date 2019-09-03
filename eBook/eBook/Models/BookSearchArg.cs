using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eBook.Models
{
    public class BookSearchArg
    {
        [DisplayName("書名")]
        /// <summary>
        /// 書籍名稱
        /// </summary>
        public string BookName { get; set; }
        [DisplayName("圖書類別")]
        /// <summary>
        /// 圖書類別
        /// </summary>
        public string BookClassName { get; set; }
        [DisplayName("借閱者")]
        /// <summary>
        /// 借閱者
        /// </summary>
        public string BookBoughter { get; set; }
        [DisplayName("借閱狀態")]
        /// <summary>
        /// 借閱狀態
        /// </summary>
        public string BookStatus { get; set; }

    }
}