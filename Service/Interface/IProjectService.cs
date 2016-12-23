using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVC.Models;

namespace Service.Interface
{
    public interface IProjectService
    {
        void Create(Project entity);

        void Update(Project entity);

        void Delete(Guid id);

        bool IsExists(Guid id);

        Project GetById(Guid id);

        IEnumerable<Project> GetAll();
    }
}
