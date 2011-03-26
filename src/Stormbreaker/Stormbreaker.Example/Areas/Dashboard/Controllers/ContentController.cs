﻿using System;
using System.Web.Mvc;
using Dashboard.Models;
using Dashboard.Web.Mvc.ViewModels;
using Stormbreaker.Exceptions;
using Stormbreaker.Models;
using Stormbreaker.Repositories;
using Stormbreaker.Web.UI;

namespace Dashboard.Controllers {
    public class ContentController : Controller {
        private readonly IPageRepository _repository;
        private readonly IStructureInfo _structureInfo;
        /// <summary>
        /// Default action
        /// </summary>
        /// <returns>
        /// Redirects to the Edit action with the home page loaded
        /// </returns>
        public ActionResult Index(dynamic model) {
            if(model != null && model is IPageModel) {
                return RedirectToAction("edit", new { model });   
            }
            return View();
        }
        /// <summary>
        /// Responsible for providing the Edit view with data from the current page
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public ActionResult Edit(dynamic model) {
            var viewModel = new DashboardViewModel(model, _structureInfo);
            return View(viewModel);
        }
        /// <summary>
        /// Responsible for saving all changes made to the current page
        /// </summary>
        /// <param name="editorModel">The editor model.</param>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public virtual ActionResult Update(dynamic editorModel, dynamic model) {

            if (!TryUpdateModel(model, "CurrentModel")) {
                return View("edit", new DashboardViewModel(model,_structureInfo));
            }

            UpdateModel(model);
            _repository.SaveChanges();

            return RedirectToAction("edit", new { model });
        }
        /// <summary>
        /// Responsible for providing the add page view with data
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public ActionResult Add(dynamic model) {

            if(model is IPageModel) {
                return View("add", new DashboardViewModel(model, _structureInfo));    
            }
            return View("add", new DashboardViewModel(null, _structureInfo));
        }
        /// <summary>
        /// Responsible for creating a new page based on the selected page model
        /// </summary>
        /// <param name="newPageModel">The new page model.</param>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public ActionResult Create([Bind(Prefix = "NewPageModel")] NewPageModel newPageModel, dynamic model) {

            if (ModelState.IsValid) {
                // create a new page from the selected page model
                var page = Activator.CreateInstance(Type.GetType(newPageModel.SelectedPageModel)) as IPageModel;
                // handle this gracefully in the future :)
                if (page == null) {
                    throw new StormbreakerException("The selected page model is not valid!");
                }
                // add the current page as a parent, the children of the current page is updated in the trigger
                if(model is IPageModel) {
                    page.Parent = model;
                }
                UpdateModel(page, "NewPageModel");
                _repository.Store(page);
                _repository.SaveChanges();

                return RedirectToAction("edit", new { model = page });
            }

            return View("add", new DashboardViewModel(newPageModel, _structureInfo));
        }
        /// <summary>
        /// Deletes the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public ActionResult Delete(IPageModel model)
        {
            _repository.Delete(model);
            _repository.SaveChanges();
            return RedirectToAction("index");
        }
        /// <summary>
        /// Initializes a new instance of the <b>PagesController</b> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="structureInfo">The structure info.</param>
        public ContentController(IPageRepository repository, IStructureInfo structureInfo)
        {
            _repository = repository;
            _structureInfo = structureInfo;
        }
    }
}
