using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CWAC19AcluMo.Data;
using CWAC19AcluMo.Models;

namespace CWAC19Aclu_Mo.Controllers
{
    public class ComplaintStatusController : Controller
    {
        private readonly CWAC19AcluMoContext _context;

        public ComplaintStatusController(CWAC19AcluMoContext context)
        {
            _context = context;
        }

        // GET: ComplaintStatus
        public async Task<IActionResult> Index()
        {
            return View(await _context.ComplaintStatuses.ToListAsync());
        }

        // GET: ComplaintStatus/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var complaintStatus = await _context.ComplaintStatuses
                .FirstOrDefaultAsync(m => m.ID == id);
            if (complaintStatus == null)
            {
                return NotFound();
            }

            return View(complaintStatus);
        }

        // GET: ComplaintStatus/Create
        public IActionResult New()
        {
            return View();
        }

        // POST: ComplaintStatus/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> New([Bind("StatusCode,Description,ID")] ComplaintStatus complaintStatus)
        {
            if (ModelState.IsValid)
            {
                _context.Add(complaintStatus);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(complaintStatus);
        }

        // GET: ComplaintStatus/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var complaintStatus = await _context.ComplaintStatuses.FindAsync(id);
            if (complaintStatus == null)
            {
                return NotFound();
            }
            return View(complaintStatus);
        }

        // POST: ComplaintStatus/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StatusCode,Description,ID")] ComplaintStatus complaintStatus)
        {
            if (id != complaintStatus.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(complaintStatus);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ComplaintStatusExists(complaintStatus.ID))
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
            return View(complaintStatus);
        }

        // GET: ComplaintStatus/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var complaintStatus = await _context.ComplaintStatuses
                .FirstOrDefaultAsync(m => m.ID == id);
            if (complaintStatus == null)
            {
                return NotFound();
            }

            return View(complaintStatus);
        }

        // POST: ComplaintStatus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var complaintStatus = await _context.ComplaintStatuses.FindAsync(id);
            _context.ComplaintStatuses.Remove(complaintStatus);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ComplaintStatusExists(int id)
        {
            return _context.ComplaintStatuses.Any(e => e.ID == id);
        }
    }
}
