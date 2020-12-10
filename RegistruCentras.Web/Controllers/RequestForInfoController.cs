using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RegistruCentras.Data;
using RegistruCentras.Domains;

namespace RegistruCentras.Web.Controllers
{
    [Authorize]
    public class RequestForInfoController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly Context _context;

        public RequestForInfoController(Context context, UserManager<AppUser> userManager)
        {
            _userManager = userManager;
            _context = context;
        }

        // GET: RequestForInfo
        public async Task<IActionResult> Index()
        {
            AppUser user = await _userManager.GetUserAsync(User);
            List<RequestForInfo> requests = _context.Requests.Where(r=> r.Requestor == user)
                                                             .Include(r=>r.ResponseInfo).ToList();
            return View(requests);
        }

        // GET: RequestForInfo/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var requestForInfo = await _context.Requests
                .FirstOrDefaultAsync(m => m.ID == id);
            if (requestForInfo == null)
            {
                return NotFound();
            }

            return View(requestForInfo);
        }

        // GET: RequestForInfo/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: RequestForInfo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,RequestorId,Question")] RequestForInfo request)
        {
            if (ModelState.IsValid)
            {
                AppUser user = await _userManager.GetUserAsync(User);
                request.ID = Guid.NewGuid();
                request.Requestor = user;
                _context.Add(request);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(request);
        }

        // GET: RequestForInfo/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var requestForInfo = await _context.Requests.FindAsync(id);
            if (requestForInfo == null)
            {
                return NotFound();
            }
            return View(requestForInfo);
        }

        // POST: RequestForInfo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, RequestForInfo requestForInfo)
        {
            if (id != requestForInfo.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(requestForInfo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RequestForInfoExists(requestForInfo.ID))
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
            return View(requestForInfo);
        }

        // GET: RequestForInfo/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var requestForInfo = await _context.Requests
                .FirstOrDefaultAsync(m => m.ID == id);
            if (requestForInfo == null)
            {
                return NotFound();
            }

            return View(requestForInfo);
        }

        // POST: RequestForInfo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var requestForInfo = await _context.Requests.FindAsync(id);
            _context.Requests.Remove(requestForInfo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RequestForInfoExists(Guid id)
        {
            return _context.Requests.Any(e => e.ID == id);
        }
    }
}
