using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DAL
{
    public class RequestDAL 
    {
        public List<Request> GetAllByExpression(Expression<Func<Request, bool>> _Expression)
        {
            using (var db = new TaskDBContext())
            {
                try
                {
                    return db.Request.Where(_Expression).ToList();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error: " + e);
                    return null;
                }
            }
        }

        public bool Add(Request request)
        {
            using (var db = new TaskDBContext())
            {
                try
                {
                    db.Request.Add(request);
                    db.SaveChanges();
                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error: " + e);
                    return false;
                }
            }
        }
    }
}

