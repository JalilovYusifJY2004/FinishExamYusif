﻿using FinalExamYusif.Areas.Admin.ViewModels.Service;
using FinalExamYusif.DAL;
using FinalExamYusif.Models;
using FinalExamYusif.Utilities.Extension;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinalExamYusif.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ServiceController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public ServiceController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            List<Service> services = await _context.Services.ToListAsync();
            return View(services);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateServiceVM serviceVM)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            bool result = await _context.Services.AnyAsync(s => s.Name.Trim().ToLower() == serviceVM.Name.Trim().ToLower());
            if (result)
            {
                ModelState.AddModelError("Name", "This Name is exists");
                return View(serviceVM);
            }
            if (!serviceVM.Photo.ValidateType("image/"))
            {
                ModelState.AddModelError("Photo", "Photo Type is unavailable");
                return View(serviceVM);
            }
            if (!serviceVM.Photo.ValidateSize(2 * 1024))
            {
                ModelState.AddModelError("Photo", "Photo size is unavailable  max 2mb");
                return View(serviceVM);
            }
            string filename = await serviceVM.Photo.CreateFileAsync(_env.WebRootPath, "assets", "images");
            Service service = new Service
            {
                Name = serviceVM.Name,
                Image = filename,
                Description = serviceVM.Description,
            };
            await _context.Services.AddAsync(service);
            await _context.SaveChangesAsync();
         return RedirectToAction("Index");
        }
      public async Task<IActionResult> Update(int id)
        {
            if (id<=0)
            {
                return BadRequest();
            }
            Service service= await _context.Services.FirstOrDefaultAsync(s => s.Id == id);
            if (service==null)
            {
                return NotFound();
            }
            UpdateServiceVM updateServiceVM = new UpdateServiceVM
            {
                Name = service.Name,
                Description = service.Description,
                Image = service.Image,
            };
            return View(updateServiceVM);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id,UpdateServiceVM updateServiceVM)
        {
            if (!ModelState.IsValid)
            {
                return View(updateServiceVM);
            }
            Service service = await _context.Services.FirstOrDefaultAsync(s => s.Id == id);
            if (service == null)
            {
                return NotFound();
            }
            bool result = await _context.Services.AnyAsync(s => s.Name.Trim().ToLower() == updateServiceVM.Name.Trim().ToLower()&&s.Id!=id);
            if (result)
            {
                ModelState.AddModelError("Name", "This Name is exists");
                return View(updateServiceVM);
            }
            if (updateServiceVM.Photo is not null)
            {
                if (!updateServiceVM.Photo.ValidateType("image/"))
                {
                    ModelState.AddModelError("Photo", "Photo Type is unavailable");
                    return View(updateServiceVM);
                }
                if (!updateServiceVM.Photo.ValidateSize(2 * 1024))
                {
                    ModelState.AddModelError("Photo", "Photo size is unavailable  max 2mb");
                    return View(updateServiceVM);
                }
                string newimage = await updateServiceVM.Photo.CreateFileAsync(_env.WebRootPath, "assets", "images");
                updateServiceVM.Image.DeleteFile(_env.WebRootPath, "assets", "images");
                service.Image = newimage;
            }
            service.Name= updateServiceVM.Name;
            service.Description= updateServiceVM.Description;
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(int id)
        {
            if (id<=0)
            {
                return BadRequest();
            }
            Service service = await _context.Services.FirstOrDefaultAsync(s => s.Id == id);
            if (service == null)
            {
                return NotFound();
            }

            service.Image.DeleteFile(_env.WebRootPath, "assets", "images");
            _context.Services.Remove(service);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        
    }
}
