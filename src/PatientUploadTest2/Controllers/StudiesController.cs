using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PatientUploadTest2.Data;
using PatientUploadTest2.Models;
using Microsoft.AspNetCore.Authorization;

namespace PatientUploadTest2.Controllers
{
    [Authorize]
    public class StudiesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StudiesController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Studies
        public async Task<IActionResult> Index()
        {
            return View(await _context.Study.ToListAsync());
        }

        // GET: Studies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var study = await _context.Study.SingleOrDefaultAsync(m => m.id == id);
            if (study == null)
            {
                return NotFound();
            }

            return View(study);
        }

        // GET: Studies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Studies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,Name")] Study study)
        {
            if (ModelState.IsValid)
            {
                _context.Add(study);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(study);
        }

        // GET: Studies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var study = await _context.Study.SingleOrDefaultAsync(m => m.id == id);
            if (study == null)
            {
                return NotFound();
            }
            return View(study);
        }

        // POST: Studies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,Name")] Study study)
        {
            if (id != study.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(study);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudyExists(study.id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(study);
        }

        // GET: Studies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var study = await _context.Study.SingleOrDefaultAsync(m => m.id == id);
            if (study == null)
            {
                return NotFound();
            }

            return View(study);
        }

        // POST: Studies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var study = await _context.Study.SingleOrDefaultAsync(m => m.id == id);
            _context.Study.Remove(study);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool StudyExists(int id)
        {
            return _context.Study.Any(e => e.id == id);
        }
    }
}
