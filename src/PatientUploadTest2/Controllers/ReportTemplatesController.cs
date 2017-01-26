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
    public class ReportTemplatesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReportTemplatesController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: ReportTemplates
        public async Task<IActionResult> Index()
        {
            return View(await _context.ReportTemplate.ToListAsync());
        }

        // GET: ReportTemplates/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reportTemplate = await _context.ReportTemplate.SingleOrDefaultAsync(m => m.id == id);
            if (reportTemplate == null)
            {
                return NotFound();
            }

            return View(reportTemplate);
        }

        // GET: ReportTemplates/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ReportTemplates/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,Diagnosis,Observation,Study")] ReportTemplate reportTemplate)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reportTemplate);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(reportTemplate);
        }

        // GET: ReportTemplates/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reportTemplate = await _context.ReportTemplate.SingleOrDefaultAsync(m => m.id == id);
            if (reportTemplate == null)
            {
                return NotFound();
            }
            return View(reportTemplate);
        }

        // POST: ReportTemplates/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,Diagnosis,Observation,Study")] ReportTemplate reportTemplate)
        {
            if (id != reportTemplate.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reportTemplate);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReportTemplateExists(reportTemplate.id))
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
            return View(reportTemplate);
        }

        // GET: ReportTemplates/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reportTemplate = await _context.ReportTemplate.SingleOrDefaultAsync(m => m.id == id);
            if (reportTemplate == null)
            {
                return NotFound();
            }

            return View(reportTemplate);
        }

        // POST: ReportTemplates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reportTemplate = await _context.ReportTemplate.SingleOrDefaultAsync(m => m.id == id);
            _context.ReportTemplate.Remove(reportTemplate);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool ReportTemplateExists(int id)
        {
            return _context.ReportTemplate.Any(e => e.id == id);
        }
    }
}
