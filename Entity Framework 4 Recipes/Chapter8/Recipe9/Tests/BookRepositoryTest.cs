using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BookRepository;

namespace Tests
{
    [TestClass]
    public class BookRepositoryTest
    {
        private TestEntities _context;

        [TestInitialize]
        public void TestSetup()
        {
            _context = new TestEntities();
            if (_context.DatabaseExists())
            {
                _context.DeleteDatabase();
            }
            _context.CreateDatabase();
        }

        [TestMethod]
        public void TestsBooksInCategory()
        {
            var repository = new BookRepository.BookRepository(_context);
            var construction = new Category { Name = "Construction" };
            var book = new Book { Title = "Building with Masonary", Author = "Dick Kreh", PublishDate = new DateTime(1998, 1, 1) };
            book.Category = construction;
            repository.InsertCategory(construction);
            repository.InsertBook(book);
            repository.SaveChanges();

            // test
            var books = repository.BooksByCategory("Construction");
            Assert.AreEqual(books.Count(), 1);
        }

        [TestMethod]
        public void TestBooksPublishedInTheYear()
        {
            var repository = new BookRepository.BookRepository(_context);
            var construction = new Category { Name = "Construction" };
            var book = new Book { Title = "Building with Masonary", Author = "Dick Kreh", PublishDate = new DateTime(1998, 1, 1) };
            book.Category = construction;
            repository.InsertCategory(construction);
            repository.InsertBook(book);
            repository.SaveChanges();

            // test
            var books = repository.BooksByYear(1998);
            Assert.AreEqual(books.Count(), 1);
        }
    }
}
