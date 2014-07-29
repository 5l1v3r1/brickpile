﻿using System.Web.Mvc;
using BrickPile.Samples.Models;
using BrickPile.Samples.Models.ViewModels;
using Raven.Client;

namespace BrickPile.Samples.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly IDocumentStore _store;
        //
        // GET: /Home/

        public ActionResult Index(Home currentPage) {

            HomeViewModel viewModel;
            using (var session = _store.OpenSession())
            {
                //var children = session.Advanced.GetChildrenFor<Container>(currentPage);
                //var parent = session.Advanced.GetParentFor(currentPage);
                //var ancestors = session.Advanced.GetAncestorsFor<Container>(currentPage, true);

                //var page = new Container();
                //session.Store(page); // Default RavenDB
                //session.Store(page, StoreAction.None); // Default RavenDB
                //session.Store(page, StoreAction.Save); // Save as draft
                //session.Store(page, StoreAction.Publish); // Publish page/draft
                //session.Store(page, StoreAction.UnPublish); // Unpublish page

                //// Maybe this could be an option
                //session.Store(page, parent, StoreAction.Save); // Add page as child and save

                //var navigationContext = new NavigationContext(ControllerContext.RequestContext);
                viewModel = new HomeViewModel
                {
                    CurrentPage = currentPage,
                    //NavigationContext = navigationContext
                };

            }

            

            return View(viewModel);
        }
        public HomeController(IDocumentStore store) {
            _store = store;
        }
    }
}
