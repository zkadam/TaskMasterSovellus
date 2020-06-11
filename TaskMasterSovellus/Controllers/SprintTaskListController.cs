using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TaskMasterSovellus.Models;
using TaskMasterSovellus.ViewModels;

namespace TaskMasterSovellus.Controllers
{
    public class SprintTaskListController : Controller
    {
        
            private TaskMasterTietokantaEntities db = new TaskMasterTietokantaEntities();

            // GET: Sprints
            public ActionResult SprintTasksList(int id)
            {
            //var sprints = db.Sprints.Include(s => s.Colors).Include(s => s.Colors1).Include(s => s.Users).Include(s => s.SprintTemplate.TemplateTaskConnection.Select(t => t.TaskState.Tasks));
            //    return View(sprints.ToList());

            var tasks = from tas in db.Tasks
                        join tst in db.TaskState on tas.StateId equals tst.StateId /*into tas_tst*/
                        join ttc in db.TemplateTaskConnection on tst.StateId equals ttc.StateId /*into tas_ttc*/
                        join stl in db.SprintTemplate on ttc.SprintTemplateId equals stl.SprintTemplateId
                        join sp in db.Sprints on stl.SprintTemplateId equals sp.TaskTemplateId
                        join cl in db.Colors on sp.BackgColor equals cl.ColorId
                        join cl2 in db.Colors on sp.ProcessColor equals cl2.ColorId

                        select new SprintClassList
                        {
                            TaskId=tas.StateId,
                            StateId=tas.StateId,
                            TaskName= (string)tst.StateName,
                          TaskDescription = (string)tas.TaskDescription,
                            TaskPoints=tas.TaskPoints,
                            TaskPriority=tas.TaskPriority,


                            //people connectiontaken out, later when brige to people is built could be included ZA
                            //public virtual ICollection<TaskPeople> TaskPeople { get; set; }
                            //public virtual TaskState TaskState { get; set; }

                            //info from task state table ZA

                            StateName=(string)tst.StateName,
                           TemplateConnectionId=ttc.TemplateConnectionId,
                           SprintTemplateId=stl.SprintTemplateId,
                           SprintId=sp.SprintId,
                           SprintName=(string)sp.SprintName,
       
                            //public Nullable<System.DateTime> StartDate { get; set; }
                            //public Nullable<System.DateTime> EndDate { get; set; }
                           BackgColor=(string)cl.ColorValue,
                            ProcessColor=(string)cl2.ColorValue,

    };

            tasks = tasks.Where(t => t.SprintId == id);




                        //where kat.Class == 1


                        //var ans = (from sar in context.StorageAreaRacks
                        //           join sa in context.StorageAreas on sar.StorageAreaId equals sa.Id
                        //           join sat in context.StorageAreaTypes on sa.StorageAreaTypeId equals sat.Id
                        //           join r in context.Racks on sar.RackId equals r.Id
                        //           where !sat.IsManual && r.IsEnabled && !r.IsVirtual
                        //           select new
                        //           {
                        //               sat.Name,
                        //               sat.Id
                        //           }).Distinct().ToList();






            return View(tasks.ToList());


        }

        //// GET: Sprints/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Sprints sprints = db.Sprints.Find(id);
        //    if (sprints == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(sprints);
        //}

        //// GET: Sprints/Create
        //public ActionResult Create()
        //{
        //    ViewBag.BackgColor = new SelectList(db.Colors, "ColorId", "ColorName");
        //    ViewBag.ProcessColor = new SelectList(db.Colors, "ColorId", "ColorName");
        //    ViewBag.AdminId = new SelectList(db.Users, "UserId", "Surname");
        //    ViewBag.TaskTemplateId = new SelectList(db.SprintTemplate, "SprintTemplateId", "SprintTemplateName");
        //    return View();
        //}

        //// POST: Sprints/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "SprintId,SprintName,TaskTemplateId,AdminId,StartDate,EndDate,BackgColor,ProcessColor")] Sprints sprints)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Sprints.Add(sprints);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.BackgColor = new SelectList(db.Colors, "ColorId", "ColorName", sprints.BackgColor);
        //    ViewBag.ProcessColor = new SelectList(db.Colors, "ColorId", "ColorName", sprints.ProcessColor);
        //    ViewBag.AdminId = new SelectList(db.Users, "UserId", "Surname", sprints.AdminId);
        //    ViewBag.TaskTemplateId = new SelectList(db.SprintTemplate, "SprintTemplateId", "SprintTemplateName", sprints.TaskTemplateId);
        //    return View(sprints);
        //}

        //// GET: Sprints/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Sprints sprints = db.Sprints.Find(id);
        //    if (sprints == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.BackgColor = new SelectList(db.Colors, "ColorId", "ColorName", sprints.BackgColor);
        //    ViewBag.ProcessColor = new SelectList(db.Colors, "ColorId", "ColorName", sprints.ProcessColor);
        //    ViewBag.AdminId = new SelectList(db.Users, "UserId", "Surname", sprints.AdminId);
        //    ViewBag.TaskTemplateId = new SelectList(db.SprintTemplate, "SprintTemplateId", "SprintTemplateName", sprints.TaskTemplateId);
        //    return View(sprints);
        //}

        //// POST: Sprints/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "SprintId,SprintName,TaskTemplateId,AdminId,StartDate,EndDate,BackgColor,ProcessColor")] Sprints sprints)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(sprints).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.BackgColor = new SelectList(db.Colors, "ColorId", "ColorName", sprints.BackgColor);
        //    ViewBag.ProcessColor = new SelectList(db.Colors, "ColorId", "ColorName", sprints.ProcessColor);
        //    ViewBag.AdminId = new SelectList(db.Users, "UserId", "Surname", sprints.AdminId);
        //    ViewBag.TaskTemplateId = new SelectList(db.SprintTemplate, "SprintTemplateId", "SprintTemplateName", sprints.TaskTemplateId);
        //    return View(sprints);
        //}

        //// GET: Sprints/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Sprints sprints = db.Sprints.Find(id);
        //    if (sprints == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(sprints);
        //}

        //// POST: Sprints/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Sprints sprints = db.Sprints.Find(id);
        //    db.Sprints.Remove(sprints);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

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
