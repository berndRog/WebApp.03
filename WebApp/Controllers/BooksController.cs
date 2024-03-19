using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApp.DomainModel;
using WebApp.DomainModel.Model;
using WebApp.Persistence;
using WebApp.Views;

namespace WebApp.Controllers;

// Controller für Razor Pages (HtmlViews)
// public class BooksController : Controller {
//    // GET
//    public IActionResult Index() {
//       return View();
//    }
// }
// [Route("api/[controller]")]

[Route("products")]
public class BooksController : Controller {

   private readonly IDataContext _dataContext;
   private readonly IBooksRepository _repository;

   public BooksController() {
      _dataContext  = new DataContextFake();
                                         // Dependency injetion
      _repository = new BooksRepositoryFake(_dataContext);
   }
   
   // GET   /products/books
   [HttpGet("books")]
   public IResult Get() {
      // find data
      IEnumerable<Book> books = _repository.Select().OrderBy(b => b.Id);
      // create HTML view
      var html = HtmlViews.BooksList(books);
      // return HTML view as HTTP response
      return Results.Content(html, "text/html");
   }

   // GET   /products/books/{id}
   [HttpGet("books/{id:Guid}")]
   public IResult GetById(
      [FromRoute] Guid id
   ) {
      // find data
      var book = _repository.FindById(id);
      if(book == null) return Results.NotFound("Book not found.");
      // create HTML view
      var html = HtmlViews.BookDetails(book);   
      // return HTML view as HTTP response 
      return Results.Content(html, "text/html");
   }
   
   // POST  /products/books
   [HttpPost("books")]
   public IResult Post(
      [FromBody] Book book
   ) {
      // save data
      if (_repository.FindById(book.Id) != null)
         return Results.BadRequest("Posting the book fails: book already exists.");
      _repository.Add(book);
      _dataContext.SaveAllChanges();
      // return HTTP response
      return Results.Created($"/books/{book.Id}", book);
   }
   
   // DELETE /products/books/{id}
   [HttpDelete("books/{id:Guid}")]
   public IResult Delete(
      [FromRoute] Guid id
   ) {
      // remove data
      if (_repository.FindById(id) == null)
         return Results.BadRequest("Deleting the book fails: book does not exist.");
      var book = _repository.FindById(id);
       _repository.Delete(book!);
       _dataContext.SaveAllChanges();
      // return HTTP response (=View)
      return Results.NoContent();
   }
}