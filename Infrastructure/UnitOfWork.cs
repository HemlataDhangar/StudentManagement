using Domain;
using Infrastructure.Contract;
using System;
using System.Data.Entity;

namespace Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
       
        private readonly StudentEntity _dbContext;
       
        public UnitOfWork()
        {
            _dbContext = new StudentEntity();
    
        }

        public DbContext Db {
           
            get { return _dbContext; }
            

        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }


    }
}
