using Dotnet5WebAPI.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dotnet5WebAPI.Services
{
    public class BookService
    {
        private readonly IMongoCollection<Book> _book;
        public BookService(IBookstoreDatabaseSettings bookstoreDatabaseSettings)
        {
            var client = new MongoClient(bookstoreDatabaseSettings.ConnectionString);
            var database = client.GetDatabase(bookstoreDatabaseSettings.DatabaseName);
            _book = database.GetCollection<Book>(bookstoreDatabaseSettings.BooksCollectionName);
        }

        public List<Book> Get()
        {
            return _book.Find(book => true).ToList();
        }

        public Book Get(string id)
        {
            return _book.Find(book => book.Id == id).FirstOrDefault();
        }

        public Book Create(Book book)
        {
            _book.InsertOne(book);
            return book;
        }

        public void Remove(string id)
        {
            _book.DeleteOne(book => book.Id == id);
        }
        public void Remove(Book bookIn)
        {
            _book.DeleteOne(book => book.Id == bookIn.Id);
        }

        public void Update(Book bookIn)
        {
            _book.ReplaceOne(book => book.Id == bookIn.Id, bookIn);
        }
    }
}
