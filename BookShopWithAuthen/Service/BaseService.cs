using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebWithAuthentication.Repository;

namespace WebWithAuthentication.Service
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