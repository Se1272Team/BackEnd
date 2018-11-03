using BookShopWithAuthen.Data.Infrastructure;
using BookShopWithAuthen.Model.Models;
using System.Collections.Generic;
using System.Linq;
namespace BookShopWithAuthen.Service.Services
{
    public interface IBookService
    {
        IEnumerable<Book> GetAll();
        IEnumerable<Book> GetBooksBySomeCondition(string searchValue, int priceFrom, int priceTo,
            int authorID, int categoryID);
        IEnumerable<Book> GetBooksSameCategory(int limit, int categoryID);
        Book GetByID(int ID);
        void UpdateQuantityBook(int bookID, int newQuantity);
    }


    public class BookService : IBookService
    {
        private readonly IBaseRepository<Book> _bookRepository;
        private readonly IUnitOfWork _unitOfWork;

        public BookService(IBaseRepository<Book> bookRepository, IUnitOfWork unitOfWork)
        {
            _bookRepository = bookRepository;
            _unitOfWork = unitOfWork;
        }
        public void Commit()
        {
            _unitOfWork.Commit();
        }
        public void Dispose()
        {
            _unitOfWork.Dispose();
        }

        public IEnumerable<Book> GetAll()
        {
            return _bookRepository.GetAll();
        }

        public IEnumerable<Book> GetBooksBySomeCondition(string searchValue, int priceFrom, int priceTo, int authorID, int categoryID)
        {
            var allBooks = from b in GetAll()
                           where b.Price >= priceFrom && b.Price <= priceTo
                           select b;
            if (!string.IsNullOrEmpty(searchValue))
            {
                allBooks = allBooks.Where(b => b.Name.Contains(searchValue));
            }
            if (categoryID != -1)
            {
                allBooks = allBooks.Where(b => b.CategoryID == categoryID);
            }
            if (authorID != -1)
            {
                allBooks = allBooks.Where(b => b.Authors.FirstOrDefault(a => a.ID == authorID) != null);
            };
            return allBooks.ToList();
        }

        public IEnumerable<Book> GetBooksSameCategory(int limit, int categoryID)
        {
            return _bookRepository.GetMany(b => b.CategoryID == categoryID).Take(limit);
        }

        public Book GetByID(int ID)
        {
            return _bookRepository.GetById(ID);
        }

        public void UpdateQuantityBook(int bookID, int newQuantity)
        {
            var tmpBook = GetByID(bookID);
            tmpBook.Quantity = newQuantity;
            Commit();
        }

        #region BookService member



        #endregion
    }
}
