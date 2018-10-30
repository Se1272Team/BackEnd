using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookShopWithAuthen.Repository;

namespace BookShopWithAuthen.Service
{
    public class BaseService<T> where T:class
    {
        protected BaseRepo<T> _repo;
        public BaseService()
        {
            _repo = new BaseRepo<T>();
        }
    }
}