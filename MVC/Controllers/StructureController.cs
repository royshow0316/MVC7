using System;
using System.Collections.Generic;
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
    public class StructureController : Controller
    {
        private readonly IStructureService _structureService;

        public StructureController(IStructureService structureService)
        {
            this._structureService = structureService;
        }

        // GET: Structure
        public ActionResult Index()
        {
            var structures = _structureService.GetAll().ToList();
            var viewModel = new StructureListViewModel();
            viewModel.StructureList = structures;

            return View(viewModel);
        }

        public ActionResult Create()
        {
            var viewModel = new StructureViewModel();

            return View(viewModel);
        }

        public ActionResult Edit(Guid id)
        {
            var viewModel = new StructureViewModel();
            var model = _structureService.GetById(id);
            viewModel.Id = id;
            viewModel.Name = model.Name;

            return View("Create", viewModel);
        }

        [HttpPost]
        public ActionResult Save(StructureViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                if (viewModel.Id == Guid.Empty)
                {
                    var model = new Structure();
                    model.Id = Guid.NewGuid();
                    model.Name = viewModel.Name;
                    _structureService.Create(model);
                }
                else
                {
                    var model = _structureService.GetById(viewModel.Id);
                    model.Name = viewModel.Name;
                    _structureService.Update(model);
                }
                return RedirectToAction("Index");
            }
            return View("Create", viewModel);
        }

        public ActionResult Delete(Guid id)
        {
            _structureService.Delete(id);

            return RedirectToAction("Index");
        }
    }
}