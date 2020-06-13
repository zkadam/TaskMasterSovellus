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




        //Projekt connection lisk ottaa projektin idn ja näyttää kaikki siihen kuuluvia sprinttejä
        public ActionResult SprintsOfPrjekt(int? id)
        {
            if (Session["UserName"] == null)
            {
               return RedirectToAction("login", "home");
            }
            else
            {

                if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var projectConnection = db.ProjectConnection.Include(p => p.Projects).Include(p => p.Sprints).Where(p=>p.ProjectId==id);

            return View(projectConnection.ToList());
        }
            }

        // action result palauttaa kaikki sprintiin kuuluvia taskeja
        public ActionResult SprintTasksList(int id)
            {
            //var sprints = db.Sprints.Include(s => s.Colors).Include(s => s.Colors1).Include(s => s.Users).Include(s => s.SprintTemplate.TemplateTaskConnection.Select(t => t.TaskState.Tasks));
            //    return View(sprints.ToList());
           
                if (Session["UserName"] == null)
                {
                   
                    string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
                    return RedirectToAction("login", "home");
                }
                else
                {

                var tasks = from tas in db.Tasks
                        join tst in db.TaskState on tas.StateId equals tst.StateId /*into tas_tst*/
                        join ttc in db.TemplateTaskConnection on tst.StateId equals ttc.StateId /*into tas_ttc*/
                        join stl in db.SprintTemplate on ttc.SprintTemplateId equals stl.SprintTemplateId
                        join stc in db.SprintTemplateConnection on stl.SprintTemplateId equals stc.SprintTemplateId
                        join sp in db.Sprints on stc.SprintId equals sp.SprintId

                        join cl in db.Colors on sp.BackgColor equals cl.ColorId
                        join cl2 in db.Colors on sp.ProcessColor equals cl2.ColorId
                        
                        select new SprintClassList
                        {
                            TaskId=tas.TaskId,
                            StateId=tas.StateId,
                            TaskName= (string)tas.TaskName,
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
