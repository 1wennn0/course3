using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eBook.Controllers
{
    public class IndexController : Controller
    {
        Models.CodeService codeService = new Models.CodeService();
        // GET: Index
        public ActionResult Index()
        {
            ViewBag.BookClassNameItem = codeService.GetBookClassName();
            ViewBag.BookBoughterItem = codeService.GetBookBoughter();
            ViewBag.BookStatusItem = codeService.GetBookStatus();
            return InsertBook();
        }
        [HttpPost()]
        public ActionResult Index(Models.BookSearchArg arg)
        {
            Models.CodeService codeService = new Models.CodeService();
            ViewBag.BookClassNameItem = codeService.GetBookClassName();
            ViewBag.BookBoughterItem = codeService.GetBookBoughter();
            ViewBag.BookStatusItem = codeService.GetBookStatus();

            Models.BookService bookService = new Models.BookService();
            ViewBag.BookSearchResult = bookService.GetBookByCondition(arg);

            return InsertBook();
        }
      
        [HttpGet()]
        public ActionResult InsertBook()
        {
            Models.BookService bookService = new Models.BookService();
            
            return View();
        }
       /// <summary>
       /// 新增書籍
       /// </summary>
       /// <param name="book"></param>
       /// <returns></returns>
        [HttpPost()]
        public ActionResult InsertBook(Models.AddBook book)
        {
            Models.BookService bookService = new Models.BookService();
            ViewBag.AddBookItem = bookService.InsertBook(book);
            return View();
        }
    }
}