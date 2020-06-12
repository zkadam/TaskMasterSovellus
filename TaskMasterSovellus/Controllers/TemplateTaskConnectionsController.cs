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
    public class TemplateTaskConnectionsController : Controller
    {
        private TaskMasterTietokantaEntities db = new TaskMasterTietokantaEntities();

        // GET: TemplateTaskConnections
        public ActionResult Index()
        {
            var templateTaskConnection = db.TemplateTaskConnection.Include(t => t.SprintTemplate).Include(t => t.TaskState);
            return View(templateTaskConnection.ToList());
        }

        // GET: TemplateTaskConnections/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TemplateTaskConnection templateTaskConnection = db.TemplateTaskConnection.Find(id);
            if (templateTaskConnection == null)
            {
                return HttpNotFound();
            }
            return View(templateTaskConnection);
        }

        // GET: TemplateTaskConnections/Create
        public ActionResult Create()
        {
            ViewBag.SprintTemplateId = new SelectList(db.SprintTemplate, "SprintTemplateId", "SprintTemplateName");
            ViewBag.StateId = new SelectList(db.TaskState, "StateId", "StateName");
            return View();
        }

        // POST: TemplateTaskConnections/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TemplateConnectionId,SprintTemplateId,StateId")] TemplateTaskConnection templateTaskConnection)
        {
            if (ModelState.IsValid)
            {
                db.TemplateTaskConnection.Add(templateTaskConnection);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SprintTemplateId = new SelectList(db.SprintTemplate, "SprintTemplateId", "SprintTemplateName", templateTaskConnection.SprintTemplateId);
            ViewBag.StateId = new SelectList(db.TaskState, "StateId", "StateName", templateTaskConnection.StateId);
            return View(templateTaskConnection);
        }

        // GET: TemplateTaskConnections/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TemplateTaskConnection templateTaskConnection = db.TemplateTaskConnection.Find(id);
            if (templateTaskConnection == null)
            {
                return HttpNotFound();
            }
            ViewBag.SprintTemplateId = new SelectList(db.SprintTemplate, "SprintTemplateId", "SprintTemplateName", templateTaskConnection.SprintTemplateId);
            ViewBag.StateId = new SelectList(db.TaskState, "StateId", "StateName", templateTaskConnection.StateId);
            return View(templateTaskConnection);
        }

        // POST: TemplateTaskConnections/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TemplateConnectionId,SprintTemplateId,StateId")] TemplateTaskConnection templateTaskConnection)
        {
            if (ModelState.IsValid)
            {
                db.Entry(templateTaskConnection).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SprintTemplateId = new SelectList(db.SprintTemplate, "SprintTemplateId", "SprintTemplateName", templateTaskConnection.SprintTemplateId);
            ViewBag.StateId = new SelectList(db.TaskState, "StateId", "StateName", templateTaskConnection.StateId);
            return View(templateTaskConnection);
        }

        // GET: TemplateTaskConnections/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TemplateTaskConnection templateTaskConnection = db.TemplateTaskConnection.Find(id);
            if (templateTaskConnection == null)
            {
                return HttpNotFound();
            }
            return View(templateTaskConnection);
        }

        // POST: TemplateTaskConnections/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TemplateTaskConnection templateTaskConnection = db.TemplateTaskConnection.Find(id);
            db.TemplateTaskConnection.Remove(templateTaskConnection);
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
