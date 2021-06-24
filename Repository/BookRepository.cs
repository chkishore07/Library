using Library.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace Library.Repository
{
    public class BookRepository
    {
        static string dirPath = ConfigurationManager.AppSettings["documentReposirty"].ToString();

        /// <summary>
        /// Method to get the list of books in the location
        /// </summary>
        /// <returns>List of books with Title and Id</returns>
        public List<Book> GetBooks()
        {
            List<Book> lstBooks = new List<Book>();
            DirectoryInfo dir = new DirectoryInfo(dirPath);

            FileInfo[] fileInfos = dir.GetFiles();

            for (int i = 0; i < fileInfos.Length; i++)
            {
                lstBooks.Add(new Book { Id = i + 1, Title = Path.GetFileNameWithoutExtension(fileInfos[i].FullName) });
            }
            return lstBooks;
        }
        /// <summary>
        /// Returns list of words with count
        /// </summary>
        /// <param name="id"></param>
        /// <returns>List of words with count</returns>
        public List<WordItem> GetWords(int id)
        {
            Book book = GetBooks().Where(i => i.Id == id).FirstOrDefault();

            DirectoryInfo hdDirectoryInWhichToSearch = new DirectoryInfo(dirPath);
            FileInfo filesInDir = hdDirectoryInWhichToSearch.GetFiles(book.Title + "*").FirstOrDefault();

            char[] delims = { '.', '!', '?', ',', '(', ')', '\t', '\n', '\r', ' ', '*', '"', '”', ':', ';', '-', '\'', '_', '’', '—', '“', '‘', '’', ' ' };

            var words = File.ReadAllText(filesInDir.FullName, Encoding.ASCII).Split(delims, StringSplitOptions.RemoveEmptyEntries);

            var wordItems = words.GroupBy(x => x.ToLower())
                .Select(x => new WordItem { Word = x.Key[0].ToString().ToUpper() + x.Key.Substring(1), Count = x.Count() })
                .OrderByDescending(x => x.Count);

            return wordItems.ToList();
        }
    }
}