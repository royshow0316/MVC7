using MVC.Models;

namespace SQLModel.DbContextFactory
{
    public interface IDbContextFactory
    {
        ApplicationDbContext GetDbContext();
    }
}
