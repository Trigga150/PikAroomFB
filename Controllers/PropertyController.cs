using Firebase.Auth;
using Firebase.Storage;
using PikAroomFB.Models;
using PikAroomFB.Repository.Apply;
using PikAroomFB.Repository.DataConnection;
using PikAroomFB.Repository.Property;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PikAroomFB.Controllers
{
    public class PropertyController : Controller
    {

        public static string AuthorizationSecret = "TGVBnt5ZwVZp341isZUk7TJmDpkP5ozPRwoqFKRr";
        public static string FirebaseDatabaseAdress = "https://pikaroom-bef5e-default-rtdb.firebaseio.com/";
        public static string Web_ApiKey = "AIzaSyAMCPbpzgXauvU9DBD3sJuXCN8RPv1AbIM";
        public static string Auth_Domain = "pikaroom-bef5e.firebaseapp.com";
        public static string FromMail = "thandoalex206@gmail.com";
        public static string FromPsw = "Jack45321#";
        public static string ResponseMessageEmailTemplate = @"/EmailTemplate/ResponseMessageTemplate.txt";
        public static string Bucket = "pikaroom-bef5e.appspot.com";


        private PropertyRepository _propertyRepository;
        private ManageFiles _fileUtility;

        public PropertyController()
        {
            _propertyRepository = new PropertyRepository();
            _fileUtility = new ManageFiles();
        }
        // GET: Property
        [HttpGet]
        public ActionResult Index()
        {
            List<Property> propertyList = _propertyRepository.PropertyList();
            if (propertyList == null)
            {
                ModelState.AddModelError(string.Empty, "No properties to display");
            }
            return View(_propertyRepository.PropertyList());
        }

        // GET: Property/Details/5
        [HttpGet]
        public ActionResult Details(string id)
        {
            return View(_propertyRepository.ShowProperty(id));
        }

        // GET: Property/Create
        [HttpGet]
        public ActionResult Create()
        {            
            return View();
        }

        // POST: Property/Create
        [HttpPost]
        public async Task<ActionResult> Create(Models.Property property, HttpPostedFileBase file)
        {
            FileStream stream;
            Models.Property propertyNotExist = _propertyRepository.ShowProperty(property.PropertyName);

            if (propertyNotExist != null)
            {
                ModelState.AddModelError(string.Empty, propertyNotExist.PropertyName + " already exists!!!");
                return RedirectToAction("Index", "Property");
            }
            if (file.ContentLength > 0)
            {
                string path = Path.Combine(Server.MapPath("~/Content/images"), file.FileName);
                file.SaveAs(path);
                stream = new FileStream(Path.Combine(path), FileMode.Open);
                var fileStoredPath = await Task.Run(() => _fileUtility.Upload(stream, file.FileName));

                if (fileStoredPath != null)
                {
                    if (fileStoredPath.Contains("Error"))
                    {
                        ModelState.AddModelError(string.Empty, fileStoredPath.ToString());
                    }
                    else
                    {
                        property.ProfileImagePath = fileStoredPath.ToString();
                        
                        property.ImageName = file.FileName;
                        _propertyRepository.AddProperty(property);
                        return RedirectToAction("Index", "Property");
                    }
                }
            }
            return View();
        }

        // GET: Property/Edit/5
        [HttpGet]
        public ActionResult Edit(string id)
        {
            Property selectedProperty = _propertyRepository.ShowProperty(id);

            return View(selectedProperty);
        }

        // POST: Property/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(Models.Property property, HttpPostedFileBase file)
        {
            FileStream stream;
            string removeProfileImage = string.Empty;
            
            string removeImageName = string.Empty;
            Property oldProperty = _propertyRepository.ShowProperty(property.PropertyName);
            removeProfileImage = oldProperty.ProfileImagePath;
            
            removeImageName = oldProperty.ImageName;

            if (file != null && file.ContentLength > 0)
            {
                if (string.IsNullOrEmpty(property.PropertyName))
                {
                    if (file.FileName != removeImageName)
                    {
                        string removePath = Path.Combine(Server.MapPath("~/Content/images/"), removeImageName);
                        //Removing the old image from the contemnt directory
                        {
                            if (System.IO.File.Exists(removePath))
                            {
                                System.IO.File.Delete(removePath);
                                await _fileUtility.Delete(removeImageName);
                            }
                        }
                        string path = Path.Combine(Server.MapPath("~/Content/images/"), file.FileName);
                        file.SaveAs(path);
                        stream = new FileStream(Path.Combine(path), FileMode.Open);
                        var fileStoredPath = await Task.Run(() => _fileUtility.Upload(stream, file.FileName));

                        if (fileStoredPath.Contains("Error"))
                        {
                            ModelState.AddModelError(string.Empty, fileStoredPath.ToString());
                        }
                        else
                        {
                            property.ProfileImagePath = fileStoredPath.ToString();
                            property.ImageName = file.FileName;

                        }
                    }
                }

            }
            else
            {
                property.ProfileImagePath = oldProperty.ProfileImagePath;              
            }
            _propertyRepository.EditProperty(property);
            return RedirectToAction("Index", "Property");


        }

        // GET: Property/Delete/5
        //[HttpGet]
        //public ActionResult Delete(int id)
        //{
        //    return View(_propertyRepository.ShowProperty(id));
        //}

        // POST: Property/Delete/5
        [HttpGet]
        public ActionResult Delete(string id)
        {
            if (string.IsNullOrEmpty(id) == false)
            {
                _propertyRepository.RemoveProperty(id);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Property cannot be found!!!");
                return View();
                
            }
            return RedirectToAction("Index", "Property");
        }


        public async void Upload(FileStream stream, string fileName)
        {
            string returnLink_error = string.Empty;
            var auth = new FirebaseAuthProvider(new FirebaseConfig(Web_ApiKey));
            var a = await auth.SignInWithEmailAndPasswordAsync(FirebaseConstants.FromMail, FirebaseConstants.FromPsw);
            var cancellation = new CancellationTokenSource();
            var task = new FirebaseStorage(
                FirebaseConstants.Bucket,
                new FirebaseStorageOptions
                {
                    AuthTokenAsyncFactory = () => Task.FromResult(a.FirebaseToken),
                    ThrowOnCancel = true
                }
                )
                .Child("images")
                .Child(fileName)
                .PutAsync(stream, cancellation.Token);
            try
            {
                string link = await task;
                returnLink_error = link;
            }
            catch (Exception ex)
            {
                returnLink_error = "Error occured during upload" + ex.Message;
            }
        }
    }
}