using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MultipleRowDelete.Models;
using System.Collections;

namespace MultipleRowDelete.Controllers
{
    public class EmployeeController : Controller
    {
        private InterviewEntities db = new InterviewEntities();
        // GET: Employee
        public ActionResult Index()
        {
            return View(db.tblEmployees.ToList());
        }

        [HttpPost]
        public ActionResult Delete(string[] ids)
        {
            if (ids == null || ids.Length == 0)
            {
                ModelState.AddModelError("", "No item selected to delete");
                return View();
            }
            //bind the task collection into list

            List<int> TaskIds = ids.Select(x => Int32.Parse(x)).ToList();

            for (var i = 0; i < TaskIds.Count(); i++)
            {
                var todo = db.tblEmployees.Find(TaskIds[i]);

                //remove the record from the database

                db.tblEmployees.Remove(todo);

                //call save changes action otherwise the table will not be updated

                db.SaveChanges();
            }
            // redirect to index view once record is deleted            
            return RedirectToAction("Index");
        }
    }
}