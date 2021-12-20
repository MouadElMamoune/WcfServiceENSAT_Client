using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ServiceReferenceENSAT;
using WcfServiceENSAT_Client.Data;

namespace WcfServiceENSAT_Client.Controllers
{
    public class StudentsController : Controller
    {        
        private ServiceENSATClient client = new ServiceENSATClient();

        // GET: Students
        public async Task<IActionResult> Index(string searchText)
        {
            if (searchText == null)
            {
                return View(client.getAllStudents());
            }
            List<Student> s = new List<Student>();
            s.Add(client.getStudent(Convert.ToInt32(searchText)));
   
            return View(s);
            
        }

        // GET: Students/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var student = client.getStudent(id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // GET: Students/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FNAME,ID,LNAME,MAIL,PHONE")] Student student)
        {
            if (ModelState.IsValid)
            {
                client.addStudent(student);
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var student = client.getStudent(id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FNAME,ID,LNAME,MAIL,PHONE")] Student student)
        {
            if (id != student.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    client.updateStudent(student);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var student = client.getStudent(id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            client.deleteStudent(id);
            return RedirectToAction(nameof(Index));
        }

        private bool StudentExists(int id)
        {
            Student s = client.getStudent(id);
            if (s != null)
            {
                return true;

            }
            return false;
        }
    }
}
