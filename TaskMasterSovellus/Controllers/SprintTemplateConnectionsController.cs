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
    public class SprintTemplateConnectionsController : Controller
    {
        private TaskMasterTietokantaEntities db = new TaskMasterTietokantaEntities();

        // GET: SprintTemplateConnections
        public ActionResult Index()
        {
            var sprintTemplateConnection = db.SprintTemplateConnection.Include(s => s.Sprints).Include(s => s.SprintTemplate);
            return View(sprintTemplateConnection.ToList());
        }

        // GET: SprintTemplateConnections/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SprintTemplateConnection sprintTemplateConnection = db.SprintTemplateConnection.Find(id);
            if (sprintTemplateConnection == null)
            {
                return HttpNotFound();
            }
            return View(sprintTemplateConnection);
        }

        // GET: SprintTemplateConnections/Create
        public ActionResult Create()
        {
            ViewBag.SprintId = new SelectList(db.Sprints, "SprintId", "SprintName");
            ViewBag.SprintTemplateId = new SelectList(db.SprintTemplate, "SprintTemplateId", "SprintTemplateName");
            return View();
        }

        // POST: SprintTemplateConnections/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TemplateConnectionId,SprintId,SprintTemplateId")] SprintTemplateConnection sprintTemplateConnection)
        {
            if (ModelState.IsValid)
            {
                db.SprintTemplateConnection.Add(sprintTemplateConnection);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SprintId = new SelectList(db.Sprints, "SprintId", "SprintName", sprintTemplateConnection.SprintId);
            ViewBag.SprintTemplateId = new SelectList(db.SprintTemplate, "SprintTemplateId", "SprintTemplateName", sprintTemplateConnection.SprintTemplateId);
            return View(sprintTemplateConnection);
        }

        // GET: SprintTemplateConnections/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SprintTemplateConnection sprintTemplateConnection = db.SprintTemplateConnection.Find(id);
            if (sprintTemplateConnection == null)
            {
                return HttpNotFound();
            }
            ViewBag.SprintId = new SelectList(db.Sprints, "SprintId", "SprintName", sprintTemplateConnection.SprintId);
            ViewBag.SprintTemplateId = new SelectList(db.SprintTemplate, "SprintTemplateId", "SprintTemplateName", sprintTemplateConnection.SprintTemplateId);
            return View(sprintTemplateConnection);
        }

        // POST: SprintTemplateConnections/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TemplateConnectionId,SprintId,SprintTemplateId")] SprintTemplateConnection sprintTemplateConnection)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sprintTemplateConnection).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SprintId = new SelectList(db.Sprints, "SprintId", "SprintName", sprintTemplateConnection.SprintId);
            ViewBag.SprintTemplateId = new SelectList(db.SprintTemplate, "SprintTemplateId", "SprintTemplateName", sprintTemplateConnection.SprintTemplateId);
            return View(sprintTemplateConnection);
        }

        // GET: SprintTemplateConnections/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SprintTemplateConnection sprintTemplateConnection = db.SprintTemplateConnection.Find(id);
            if (sprintTemplateConnection == null)
            {
                return HttpNotFound();
            }
            return View(sprintTemplateConnection);
        }

        // POST: SprintTemplateConnections/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SprintTemplateConnection sprintTemplateConnection = db.SprintTemplateConnection.Find(id);
            db.SprintTemplateConnection.Remove(sprintTemplateConnection);
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
