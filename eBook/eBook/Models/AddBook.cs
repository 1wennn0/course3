using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace eBook.Models
{
    public class AddBook
    {
        [DisplayName("書名")]
        /// <summary>
        /// 書籍名稱
        /// </summary>
        public string BookName { get; set; }
        [DisplayName("作者")]
        /// <summary>
        /// 作者
        /// </summary>
        public string BookAuthor { get; set; }
        [DisplayName(" 出版商")]
        /// <summary>
        /// 出版商
        /// </summary>
        public string BookPublisher { get; set; }
        [DisplayName("內容簡介")]
        /// <summary>
        /// 內容簡介
        /// </summary>
        public string BookNote { get; set; }
        [DisplayName("購書日期")]
        /// <summary>
        /// 購書日期
        /// </summary>
        public string BookBoughtDate { get; set; }
        [DisplayName("圖書類別")]
        /// <summary>
        /// 圖書類別
        /// </summary>
        public string BookClassName { get; set; }
    }
}