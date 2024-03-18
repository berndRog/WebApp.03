using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Routing.Matching;
using WebApp.DomainModel.Model;
namespace WebApp.DomainModel;

public interface IDataContext {
   Dictionary<Guid, Book> Books { get; }
   bool SaveAllChanges();
}