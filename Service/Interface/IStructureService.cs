using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVC.Models;

namespace Service.Interface
{
    public interface IStructureService
    {
        void Create(Structure entity);

        void Update(Structure entity);

        void Delete(Guid id);

        bool IsExists(Guid id);

        Structure GetById(Guid id);

        IEnumerable<Structure> GetAll();
    }
}
