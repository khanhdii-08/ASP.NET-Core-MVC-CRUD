using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplicationCRUD.Models;

namespace WebApplicationCRUD.Controllers
{
    public class StudentController : Controller
    {
        private readonly studentContext _context;
       
        public StudentController(studentContext context)
        {
            _context= context;
        }
        public IActionResult Index(int pg=1)
        {
            const int pageSize = 10;
            if (pg < 1)
                pg = 1;
            
            int count = _context.Students.Count();
            var pager = new Pager(count,pg, pageSize);

            int skip = (pg - 1) * pageSize;

            List<Student> students= _context.Students.Skip(skip).Take(pager.PageSize).ToList();
            this.ViewBag.Pager = pager;
            return View(students);
        }

        public IActionResult Details(int id)
        {
            Student student = _context.Students.Where(p=> p.Id == id).FirstOrDefault();
            return View(student);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Student student = _context.Students.Where(p => p.Id == id).FirstOrDefault();
            return View(student);
        }

        [HttpPost]
        public IActionResult Edit(Student student)
        {
            _context.Attach(student);
            _context.Entry(student).State = EntityState.Modified;
            _context.SaveChanges();
            return RedirectToAction("Index");   
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            Student student = _context.Students.Where(p => p.Id == id).FirstOrDefault();
            return View(student);
        }

        [HttpPost]
        public IActionResult Delete(Student student)
        {
            _context.Attach(student);
            _context.Entry(student).State = EntityState.Deleted;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Create()
        {
            Student student = new Student();
            return View(student);
        }

        [HttpPost]
        public IActionResult Create(Student student)
        {
            _context.Attach(student);
            _context.Entry(student).State = EntityState.Added;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
