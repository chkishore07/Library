using Library.Repository;
using NUnit.Framework;
using System.Linq;

namespace Library.Tests
{
    [TestFixture]
    public class LibraryTests
    {
        private BookRepository repository;

        //private const string SAMPLE_TEXT = @"Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. 
        //        Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate 
        //        velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.";

        public LibraryTests()
        {
            repository = new BookRepository();
        }

        [Test]
        public void GetBooksTest()
        {
            var books = repository.GetBooks();
            Assert.AreEqual(books.Count, 3);
        }

        [Test]
        public void GetWordsTest()
        {
            var common = repository.GetWords(1);
            var Item = common.Where(x => x.Word.ToLower() == "there").FirstOrDefault();
            Assert.AreEqual(Item.Word, "There");
            Assert.AreEqual(Item.Count, 587);

            Item = common.Where(x => x.Word.ToLower() == "defarge").FirstOrDefault();

            Assert.AreEqual(Item.Word, "Defarge");
            Assert.AreEqual(Item.Count, 302);
        }
    }
}