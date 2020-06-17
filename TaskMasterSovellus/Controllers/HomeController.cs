using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskMasterSovellus.Models;

namespace TaskMasterSovellus.Controllers
{
    public class HomeController : Controller
    {
        //----------------------------------actionName ja controllerNamein pitää tulla edellisessä controllerista
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]


        public ActionResult Authorize(Logins LoginModel, string actionName, string controllerName)
        {

            //setting action and controller names to index/Home if they arrive empty
            if (String.IsNullOrEmpty(actionName) && String.IsNullOrEmpty(controllerName))
            {
                actionName = "Index";
                controllerName = "Home";
            }
            TaskMasterTietokantaEntities db = new TaskMasterTietokantaEntities();
           //Haetaan käyttäjän/Loginin tiedot annetuilla tunnustiedoilla tietokannasta LINQ -kyselyllä
            var LoggedUser = db.Logins.SingleOrDefault(x => x.Email == LoginModel.Email && x.Password == LoginModel.Password);
            if (LoggedUser != null)
            {
                ViewBag.LoginMessage = "Successfull login";
                ViewBag.LoggedStatus = "In";
                ViewBag.LoginError = 0; //ei ole virhettä, tätä tarvitaan sen takia että viewissä jquery päättää kannattako login ikkunan avata uudestaan
                Session["UserName"] = LoggedUser.Email;
                return RedirectToAction(actionName, controllerName); //Tässä määritellään mihin onnistunut kirjautuminen johtaa --> Home/Index
            }
            else
            {
                ViewBag.LoginMessage = "Login unsuccessfull";
                ViewBag.LoggedStatus = "Out";
                ViewBag.LoginError = 1;//on jotain virhettä kirjautumisessa, tätä tarvitaan sen takia että viewissä jquery päättää kannattako login ikkunan avata uudestaan
                LoginModel.LoginErrorMessage = "The email or password is incorrect";
                return View(actionName, LoginModel);
            }
        }
        public ActionResult LogOut()
        {
            Session.Abandon();
            ViewBag.LoggedStatus = "Out";
            return RedirectToAction("Index", "Home"); //Uloskirjautumisen jälkeen pääsivulle
        }
        public ActionResult Index()
        {
            ViewBag.LoginError = 0; //ei ole virhettä, tätä tarvitaan sen takia että viewissä jquery päättää kannattako login ikkunan avata uudestaan

            return View();
        }


    }
}