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
   private IResult GetStaticFiles(string page) {
      string filePath = Path.Combine(environment.WebRootPath, page);
      if (!System.IO.File.Exists(filePath)) return Results.NotFound($"{page} not found");
      // files under wwwroot should be read-only
      // FileInfo fileInfo = new(filePath);
      // if (!fileInfo.IsReadOnly) return Results.BadRequest($"{page} is not read-only");
      var html = System.IO.File.ReadAllText(filePath);
      return Results.Content(html, "text/html");
   }

   
   // GET   /   or   /index
   [HttpGet("")]
   [HttpGet("index")]
   public IResult GetIndex() => GetStaticFiles("index.html");

   // GET   /page1
   [HttpGet("page1")]
   public IResult GetPage1() => GetStaticFiles("page1.html");
  
   // GET   /page2
   [HttpGet("page2")]
   public IResult GetPage2() =>  GetStaticFiles("page2.html");

}