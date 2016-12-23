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
    public class ProjectService : IProjectService
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IRepository<Project> _repository;

        public ProjectService(IUnitOfWork unitOfWork, IRepository<Project> repository)
        {
            this._unitOfWork = unitOfWork;
            this._repository = repository;
        }

        public void Create(Project entity)
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

        public void Update(Project entity)
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

        public Project GetById(Guid id)
        {
            return _repository.Get(x => x.Id == id);
        }

        public IEnumerable<Project> GetAll()
        {
            return _repository.GetAll();
        }
    }
}
