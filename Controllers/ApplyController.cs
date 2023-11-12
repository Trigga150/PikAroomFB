using PikAroomFB.Models;
using PikAroomFB.Repository.Apply;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PikAroomFB.Controllers
{
    public class ApplyController : Controller
    {
        private ApplyRepository _applyRepository;

        public ApplyController()
        {
            _applyRepository = new ApplyRepository();
        }
        // GET: Applications
        public ActionResult Index()
        {
            return View(_applyRepository.GetAllApplications());
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Apply apply)
        {
            try
            {
                if (string.IsNullOrEmpty(apply.Email) == false)
                {
                    _applyRepository.AddApplication(apply);
                    ModelState.AddModelError(string.Empty, "Your application has been sent for review, your favoured accommodatio will be in contact soon!!!");

                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Kindly fill form, then click Send!!!");
                }
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "Your application was not sent, Kindly view contact information!!!");
            }
            return View();
        }

        [HttpGet]
        public ActionResult Details()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Details(string id)
        {

            return View(_applyRepository.ShowApplication(id));
        }

        [HttpPost]
        public ActionResult Delete(string id)
        {
            if (string.IsNullOrEmpty(id) == false)
            {
                _applyRepository.RemoveApplication(id);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Application cannot be found!!!");
                return View();
            }
            return RedirectToAction("Index", "Apply");
        }
    }

}