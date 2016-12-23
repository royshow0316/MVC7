using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC.Models;
using MVC.Models.Interface;
using MVC.Models.Repository;
using MVC.ViewModels;
using Service;
using Service.Interface;

namespace MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProjectService _projectService;

        public HomeController(IProjectService projectService)
        {
            this._projectService = projectService;
        }

        public ActionResult Index()
        {
            var projects = _projectService.GetAll();
            var viewModel = new ProjectListViewModel();
            viewModel.ProjectList = projects;

            return View(viewModel);
        }

        public ActionResult Create()
        {
            var viewModel = new ProjectViewModel();
            return View(viewModel);
        }

        public ActionResult Edit(Guid id)
        {
            var viewModel = new ProjectViewModel();
            var model = _projectService.GetById(id);
            viewModel.Id = id;
            viewModel.Name = model.Name;

            return View("Create", viewModel);
        }

        [HttpPost]
        public ActionResult Save(ProjectViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                if (viewModel.Id == Guid.Empty)
                {
                    var model = new Project();
                    model.Id = Guid.NewGuid();
                    model.Name = viewModel.Name;
                    _projectService.Create(model);
                }
                else
                {
                    var model = _projectService.GetById(viewModel.Id);
                    model.Name = viewModel.Name;
                    _projectService.Update(model);
                }
                return RedirectToAction("Index");
            }

            return View("Create", viewModel);
        }

        public ActionResult Delete(Guid id)
        {
            _projectService.Delete(id);

            return RedirectToAction("Index");
        }
    }
}