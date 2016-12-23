using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVC.Models;
using MVC.Models.Interface;
using MVC.Models.Repository;
using Service.Interface;
using SQLModel.UnitOfWork;

namespace Service
{
    public class StructureService : IStructureService
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IRepository<Structure> _repository;

        public StructureService(IUnitOfWork unitOfWork, IRepository<Structure> repository)
        {
            this._unitOfWork = unitOfWork;
            this._repository = repository;
        }

        public void Create(Structure entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            else
            {
                _repository.Create(entity);
                _unitOfWork.SaveChange();
            }
        }

        public void Update(Structure entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            else
            {
                _repository.Update(entity);
                _unitOfWork.SaveChange();
            }
        }

        public void Delete(Guid id)
        {
            if (!IsExists(id))
            {

            }
            else
            {
                var entity = GetById(id);
                _repository.Delete(entity);
                _unitOfWork.SaveChange();
            }
        }

        public bool IsExists(Guid id)
        {
            return _repository.GetAll().Any(x => x.Id == id);
        }

        public Structure GetById(Guid id)
        {
            return _repository.Get(x => x.Id == id);
        }

        public IEnumerable<Structure> GetAll()
        {
            return _repository.GetAll();
        }
    }
}
