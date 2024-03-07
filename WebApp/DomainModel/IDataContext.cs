using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Routing.Matching;
using WebApp.DomainModel.Model;
namespace WebApp;

public interface IDataContext {
   Dictionary<Guid, Book> Books { get; }
   bool SaveAllChanges();
}