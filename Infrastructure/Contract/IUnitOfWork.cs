using System;
using System.Data.Entity;

namespace Infrastructure.Contract
{
    public interface IUnitOfWork : IDisposable
    {
        DbContext Db { get; }    

    }
}
