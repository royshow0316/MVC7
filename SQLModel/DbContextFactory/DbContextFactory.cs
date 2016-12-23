using System;
using MVC.Models;

namespace SQLModel.DbContextFactory
{
    public class DbContextFactory : IDbContextFactory
    {
        private ApplicationDbContext dbContext;

        public DbContextFactory()
        {
        }

        private ApplicationDbContext DBContext
        {
            get
            {
                if (this.dbContext == null)
                {
                    Type type = typeof(ApplicationDbContext);
                    this.dbContext = (ApplicationDbContext)Activator.CreateInstance(type);
                }
                return dbContext;
            }
        }


        public ApplicationDbContext GetDbContext()
        {
            return DBContext;
        }
    }
}
