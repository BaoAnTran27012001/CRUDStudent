using CRUDStudent.Data;
using CRUDStudent.Models;
using CRUDStudent.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CRUDStudent.Controllers
{
    public class StudentsController : Controller
    {
        private readonly ApplicationDBContext dBContext;

        public StudentsController(ApplicationDBContext dBContext) {
            this.dBContext = dBContext;
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddStudentViewModel viewModel)
        {
            var student = new Student { Name = viewModel.Name,Email= viewModel.Email,Phone=viewModel.Phone,Subscribed=viewModel.Subscribed};
            await dBContext.Students.AddAsync(student);
            await dBContext.SaveChangesAsync();
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> List()
        {
            var students = await dBContext.Students.ToListAsync();
            return View(students);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var foundStudent = await dBContext.Students.FindAsync(id);
            return View(foundStudent);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Student student)
        {
            var foundStudent = await dBContext.Students.FindAsync(student.Id);
            if(foundStudent != null)
            {
                foundStudent.Name = student.Name;
                foundStudent.Email = student.Email;
                foundStudent.Phone = student.Phone;
                foundStudent.Subscribed = student.Subscribed;
                await dBContext.SaveChangesAsync();
            }
            return RedirectToAction("List","Students");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(Student student)
        {
            var foundStudent = await dBContext.Students.FindAsync(student.Id);
            if (foundStudent != null)
            {
               dBContext.Students.Remove(foundStudent);
               await dBContext.SaveChangesAsync();
            }
            return RedirectToAction("List", "Students");
        }
    }
}
