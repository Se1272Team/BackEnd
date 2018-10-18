using BookShopDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShopBLL
{
    public class BaseService<T> where T:class
    {
        protected IBaseDAO<T> _repo;
        public BaseService()
        {
            _repo = new BaseDAO<T>();
        }
    }
}
