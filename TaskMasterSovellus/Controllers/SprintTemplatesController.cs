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
    public class SprintTemplatesController : Controller
    {
        private TaskMasterTietokantaEntities db = new TaskMasterTietokantaEntities();

        // GET: SprintTemplates
        public ActionResult Index()
        {
            return View(db.SprintTemplate.ToList());
        }

        // GET: SprintTemplates/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SprintTemplate sprintTemplate = db.SprintTemplate.Find(id);
            if (sprintTemplate == null)
            {
                return HttpNotFound();
            }
            return View(sprintTemplate);
        }

        // GET: SprintTemplates/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SprintTemplates/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SprintTemplateId,SprintTemplateName")] SprintTemplate sprintTemplate)
        {
            if (ModelState.IsValid)
            {
                db.SprintTemplate.Add(sprintTemplate);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sprintTemplate);
        }

        // GET: SprintTemplates/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SprintTemplate sprintTemplate = db.SprintTemplate.Find(id);
            if (sprintTemplate == null)
            {
                return HttpNotFound();
            }
            return View(sprintTemplate);
        }

        // POST: SprintTemplates/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SprintTemplateId,SprintTemplateName")] SprintTemplate sprintTemplate)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sprintTemplate).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sprintTemplate);
        }

        // GET: SprintTemplates/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SprintTemplate sprintTemplate = db.SprintTemplate.Find(id);
            if (sprintTemplate == null)
            {
                return HttpNotFound();
            }
            return View(sprintTemplate);
        }

        // POST: SprintTemplates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SprintTemplate sprintTemplate = db.SprintTemplate.Find(id);
            db.SprintTemplate.Remove(sprintTemplate);
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
