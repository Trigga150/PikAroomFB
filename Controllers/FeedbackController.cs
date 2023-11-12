using PikAroomFB.Models;
using PikAroomFB.Repository.Feedback;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PikAroomFB.Controllers
{
    public class FeedbackController : Controller
    {
        private FeedbackRepository _feedbackRepository;

        public FeedbackController()
        {
            _feedbackRepository = new FeedbackRepository();
        }
        // GET: Feedback
        public ActionResult Index()
        {
            return View(_feedbackRepository.GetAllFeedbacks());
        }

        [HttpGet]
        public ActionResult Create() 
        { 
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Feedback feedback)
        {
            try
            {
                if(string.IsNullOrEmpty(feedback.Message) == false)
                {
                    _feedbackRepository.AddFeedback(feedback);
                    ModelState.AddModelError(string.Empty, "Your message has been sent for review, we will be in contact soon!!!");

                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Kindly enter a message, then click Send!!!");
                }
            }
            catch 
            {
                ModelState.AddModelError(string.Empty, "Your message was not sent, Kindly view contact information!!!");
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
           
            return View(_feedbackRepository.ShowFeedBack(id));
        }

        public ActionResult Delete(string id)
        {
            if(string.IsNullOrEmpty(id)== false)
            {
                _feedbackRepository.RemoveFeedback(id);
            }
            else
            {
                ModelState.AddModelError(string.Empty,"Feedback message cannot be found!!!");
                return View();
            }
            return RedirectToAction("Index","Feedback");
        }
    }
}