using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApp.DomainModel.Model;
using WebApp.Persistence;
using WebApp.Views;

namespace WebApp.Controllers;

[Route("")]
public class StaticFilesController(
   IWebHostEnvironment environment
) : Controller {
   
   // Static Files Endpoint Handler
   private string GetStaticFiles(string page) {
      string filePath = Path.Combine(environment.WebRootPath, page);
      if (System.IO.File.Exists(filePath)) {
         return System.IO.File.ReadAllText(filePath);
      }
      else {
         throw new Exception("File not found.");
      }
      
   }
   
   // GET   /   or   /index
   [HttpGet("")]
   [HttpGet("index")]
   public IResult GetIndex() {
      // find data
      string html = GetStaticFiles("index.html");
      // return HTML view as HTTP response
      return Results.Content(html, "text/html");
   }

   // GET   /page1
   [HttpGet("page1")]
   public IResult GetPage1() {
      // find data
      string html = GetStaticFiles("page1.html");
      // return HTML view as HTTP response
      return Results.Content(html, "text/html");
   }
  
   // GET   /page2
   [HttpGet("page2")]
   public IResult GetPage2() {
      // find data
      string html = GetStaticFiles("page2.html");
      // return HTML view as HTTP response
      return Results.Content(html, "text/html");
   }
}