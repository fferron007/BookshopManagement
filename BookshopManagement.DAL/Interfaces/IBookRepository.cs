﻿using BookshopManagement.DAL.Models;

namespace BookshopManagement.DAL.Interfaces
{
    public interface IBookRepository
    {
        IEnumerable<Book> GetAll();
        void Add(Book book);
        void Update(Book book);
        void Delete(Book book);
    }
}