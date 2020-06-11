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
    public class ProjectConnectionsController : Controller
    {
        private TaskMasterTietokantaEntities db = new TaskMasterTietokantaEntities();

        // GET: ProjectConnections
        public ActionResult Index()
        {
            var projectConnection = db.ProjectConnection.Include(p => p.Projects).Include(p => p.Sprints);
            return View(projectConnection.ToList());
        }

        // GET: ProjectConnections/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProjectConnection projectConnection = db.ProjectConnection.Find(id);
            if (projectConnection == null)
            {
                return HttpNotFound();
            }
            return View(projectConnection);
        }

        // GET: ProjectConnections/Create
        public ActionResult Create()
        {
            ViewBag.ProjectId = new SelectList(db.Projects, "ProjectId", "ProjectName");
            ViewBag.SprintId = new SelectList(db.Sprints, "SprintId", "SprintName");
            return View();
        }

        // POST: ProjectConnections/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProjectConnectionId,SprintId,ProjectId")] ProjectConnection projectConnection)
        {
            if (ModelState.IsValid)
            {
                db.ProjectConnection.Add(projectConnection);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProjectId = new SelectList(db.Projects, "ProjectId", "ProjectName", projectConnection.ProjectId);
            ViewBag.SprintId = new SelectList(db.Sprints, "SprintId", "SprintName", projectConnection.SprintId);
            return View(projectConnection);
        }

        // GET: ProjectConnections/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProjectConnection projectConnection = db.ProjectConnection.Find(id);
            if (projectConnection == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProjectId = new SelectList(db.Projects, "ProjectId", "ProjectName", projectConnection.ProjectId);
            ViewBag.SprintId = new SelectList(db.Sprints, "SprintId", "SprintName", projectConnection.SprintId);
            return View(projectConnection);
        }

        // POST: ProjectConnections/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProjectConnectionId,SprintId,ProjectId")] ProjectConnection projectConnection)
        {
            if (ModelState.IsValid)
            {
                db.Entry(projectConnection).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProjectId = new SelectList(db.Projects, "ProjectId", "ProjectName", projectConnection.ProjectId);
            ViewBag.SprintId = new SelectList(db.Sprints, "SprintId", "SprintName", projectConnection.SprintId);
            return View(projectConnection);
        }

        // GET: ProjectConnections/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProjectConnection projectConnection = db.ProjectConnection.Find(id);
            if (projectConnection == null)
            {
                return HttpNotFound();
            }
            return View(projectConnection);
        }

        // POST: ProjectConnections/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProjectConnection projectConnection = db.ProjectConnection.Find(id);
            db.ProjectConnection.Remove(projectConnection);
            db.SaveChanges();
            return RedirectToAction("Index");
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
