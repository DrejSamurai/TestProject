using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using TestProject.Data;
using TestProject.Models;

namespace TestProject.Controllers
{
    public class StudentsController : Controller
    {
        public StudentsController(ApplicationDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public ApplicationDbContext DbContext { get; }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(Student student)
        {
            var NewStudent = new Student
            {
                Name = student.Name,
                Email = student.Email,
                Phone = student.Phone,
                Subscribed = student.Subscribed
            };

            await DbContext.Student.AddAsync(NewStudent);
            await DbContext.SaveChangesAsync();

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var students = await DbContext.Student.ToListAsync();

            return View(students);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
          var student =  await DbContext.Student.FindAsync(id);

            return View(student);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Student viewModel)
        {
            var student = await DbContext.Student.FindAsync(viewModel.Id);

            if(student is not null)
            {
                student.Name = viewModel.Name;
                student.Email = viewModel.Email;
                student.Phone = viewModel.Phone;
                student.Subscribed = viewModel.Subscribed;

                await DbContext.SaveChangesAsync();
            }

            return RedirectToAction("List", "Students");
        }

    }
}
