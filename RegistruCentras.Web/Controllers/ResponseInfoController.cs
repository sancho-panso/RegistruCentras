using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RegistruCentras.Data;
using RegistruCentras.Domains;

namespace RegistruCentras.Web.Controllers
{
    public class ResponseInfoController : Controller
    {
        private readonly Context _context;

        public ResponseInfoController(Context context)
        {
            _context = context;
        }

        // GET: ResponseInfo
        public async Task<IActionResult> Index()
        {
            var context = _context.Requests.Include(r => r.ResponseInfo);
            return View(await context.ToListAsync());
        }

        // GET: ResponseInfo/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var responseInfo = await _context.Responses
                .Include(r => r.Request)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (responseInfo == null)
            {
                return NotFound();
            }

            return View(responseInfo);
        }

        // GET: ResponseInfo/Create
        public IActionResult Create()
        {
            ViewData["RequestForInfoID"] = new SelectList(_context.Requests, "ID", "ID");
            return View();
        }

        // POST: ResponseInfo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,ResponserId,Answer,RequestForInfoID")] ResponseInfo responseInfo)
        {
            if (ModelState.IsValid)
            {
                responseInfo.ID = Guid.NewGuid();
                _context.Add(responseInfo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RequestForInfoID"] = new SelectList(_context.Requests, "ID", "ID", responseInfo.RequestForInfoID);
            return View(responseInfo);
        }

        // GET: ResponseInfo/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var request = await _context.Requests.FindAsync(id);
            if (request == null)
            {

                return NotFound();
            }

            return View(request);
        }

        // POST: ResponseInfo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, RequestForInfo requestInfo)
        {
            if (id != requestInfo.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var request = _context.Requests.Where(r => r.ID == requestInfo.ID).FirstOrDefault();
                    request.ResponseInfo = requestInfo.ResponseInfo;
                    _context.Update(request);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ResponseInfoExists(requestInfo.ID))
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
            return View(requestInfo);
        }

        // GET: ResponseInfo/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var responseInfo = await _context.Responses
                .Include(r => r.Request)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (responseInfo == null)
            {
                return NotFound();
            }

            return View(responseInfo);
        }

        // POST: ResponseInfo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var responseInfo = await _context.Responses.FindAsync(id);
            _context.Responses.Remove(responseInfo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ResponseInfoExists(Guid id)
        {
            return _context.Responses.Any(e => e.ID == id);
        }
    }
}
