using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TaskMasterSovellus.Models;

namespace TaskMasterSovellus.Controllers
{
    public class ProjectsController : Controller
    {
        private TaskMasterTietokantaEntities db = new TaskMasterTietokantaEntities();

        // GET: Projects
        public ActionResult Index()
        {
            if (Session["UserName"] == null)
            {
               
                
                return RedirectToAction("index", "home");
            }
            else
            {

            var projects = db.Projects.Include(p => p.Users);
            return View(projects.ToList());

            }
        }

        

        // GET: Projects/Create
        public ActionResult Create()
        {
            if (Session["UserName"] == null)
            {

                return RedirectToAction("index", "home");
            }
            else
            {

            ViewBag.AdminId = new SelectList(db.Users, "UserId", "Surname");
            return View();

            }
        }

        // POST: Projects/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProjectId,ProjectName,AdminId,StartDate,EndDate,ProjectPicture")] Projects projects)
        {
            if (Session["UserName"] == null)
            {
               
                return RedirectToAction("index", "home");
            }
            else
            {


            if (ModelState.IsValid)
            {
                db.Projects.Add(projects);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AdminId = new SelectList(db.Users, "UserId", "Surname", projects.AdminId);
            return View(projects);
            }
        }

        // GET: Projects/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["UserName"] == null)
            {
               
               
                return RedirectToAction("index", "home");
            }
            else
            {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Projects projects = db.Projects.Find(id);
            if (projects == null)
            {
                return HttpNotFound();
            }
            ViewBag.AdminId = new SelectList(db.Users, "UserId", "Surname", projects.AdminId);
            return View(projects);
        }

            }

        // POST: Projects/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProjectId,ProjectName,AdminId,StartDate,EndDate,ProjectPicture")] Projects projects)
        {
            if (Session["UserName"] == null)
            {
               
                return RedirectToAction("index", "home");
            }
            else
            {

            if (ModelState.IsValid)
            {
                db.Entry(projects).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AdminId = new SelectList(db.Users, "UserId", "Surname", projects.AdminId);
            return View(projects);

            }
        }

        // GET: Projects/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["UserName"] == null)
            {
               
               
                return RedirectToAction("index", "home");
            }
            else
            {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Projects projects = db.Projects.Find(id);
            if (projects == null)
            {
                return HttpNotFound();
            }
            return View(projects);

            }
        }

        // POST: Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("index", "home");
            }
            else
            {

            Projects projects = db.Projects.Find(id);
            db.Projects.Remove(projects);
            db.SaveChanges();
            return RedirectToAction("Index");
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
