using DAL;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace BLL
{
    public class RequestBLL
    {
        readonly RequestDAL requestDAL;
        public RequestBLL()
        {
            requestDAL = new RequestDAL();
        }

        public bool Add(Request request)
        {
            request.RequestDate = DateTime.Now;
            return requestDAL.Add(request);
        }

        public bool IsDuplicated(Expression<Func<Request, bool>> _Expression)
        {
            return requestDAL.GetAllByExpression(_Expression).Count > 0;
        }

        public List<Request> GetAllByExpression(Expression<Func<Request, bool>> _Expression)
        {
            return requestDAL.GetAllByExpression(_Expression);
        }
    }
}

