using System;
using System.Collections.Generic;
using WebApp.DomainModel;
using WebApp.DomainModel.Model;
namespace WebApp.DomainModel;

public interface IBooksRepository {
   IEnumerable<Book> Select();
   Book? FindById(Guid id);
   void Add(Book book);
   void Delete(Book book);
}