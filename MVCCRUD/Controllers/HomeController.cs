using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCCRUD.Controllers
{
    public class HomeController : Controller
    {
        // _context is instance of MVCCRUDDBContext
        MVCCRUDDBContext _context = new MVCCRUDDBContext();
        
        public ActionResult Index()
        {
            // retrives a list of students from the database and stored in `listOfData`
            var listOfData = _context.Students.ToList();
            return View(listOfData);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // To Create a new student
        [HttpPost]
        public ActionResult Create(Student model)
        {
            _context.Students.Add(model);
            _context.SaveChanges();
            ViewBag.Messages = "Data Insert Successfully.";
            return View();

        }

        // to update the student data
        [HttpPost]
        public ActionResult Edit(Student model)
        {
            var data = _context.Students.Where(x => x.StudentID == model.StudentID).FirstOrDefault();
            if (data != null)
            {
                data.StudentCity = model.StudentCity;
                data.StudentName = model.StudentName;
                data.StudentFees = model.StudentFees;
                _context.SaveChanges();

            }

            return RedirectToAction("Index");

        }


        // identify the specific student
        [HttpPost]
        public ActionResult Details(int id)
        {
            var data = _context.Students.Where(x => x.StudentID == id);
                return View(data);
                
        }


        // to delete the student
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var data = _context.Students.Where(x => x.StudentID == id).FirstOrDefault();
            _context.Students.Remove(data);
            _context.SaveChanges();
            ViewBag.Message = "Record Delete Successfully";
            return RedirectToAction("Index");

        }
    }
}