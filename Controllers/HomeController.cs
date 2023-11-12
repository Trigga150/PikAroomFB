using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Owin.Logging;
using PikAroomFB.Models;
using PikAroomFB.Repository;
using PikAroomFB.Repository.Property;

namespace PikAroomFB.Controllers
{
    public class HomeController : Controller
    {
        private PropertyRepository _propertyRepository;

        public HomeController()
        {
            _propertyRepository = new PropertyRepository();

        }
        public ActionResult Index()
        {

            List<Property> propertyList = _propertyRepository.PropertyList();
            if (propertyList == null)
            {
                ModelState.AddModelError(string.Empty, "No properties to display");
            }
            return View(_propertyRepository.PropertyList());


            //return View();

        }

        public ActionResult UserLayout()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}