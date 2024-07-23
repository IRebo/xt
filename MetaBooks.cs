using System.Collections.Generic;

namespace xtrance
{
    public class MetaBooks : Dictionary<string, MetaBook>
    {
        public void FillBook(string masterId, Book book, string author, string bookName)
        {
            MetaBook metaBook;
            if (!TryGetValue(masterId, out metaBook))
            {
                metaBook = new MetaBook();
                metaBook.Id = masterId;
                metaBook.Author = author;
                metaBook.Name = bookName;
                Add(masterId, metaBook);
            }
            book.Parent = metaBook;
        }
    }
}