using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicEvents.Models;
using MusicEvents.Services;
using System.Threading.Tasks;

namespace MusicEvents.Controllers
{
    public class EventController : Controller
    {
        private readonly IEventService _eventService;

        public EventController(IEventService eventService)
        {
            _eventService = eventService;
        }

        public async Task<IActionResult> Index()
        {
            var events = await _eventService.GetAllEventsAsync();
            return View(events);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Event newEvent)
        {
            if (ModelState.IsValid)
            {
                await _eventService.AddEventAsync(newEvent);
                return RedirectToAction(nameof(Index));
            }
            return View(newEvent);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var eventToEdit = await _eventService.GetEventByIdAsync(id);
            if (eventToEdit == null)
            {
                return NotFound();
            }
            return View(eventToEdit);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Event updatedEvent)
        {
            if (ModelState.IsValid)
            {
                await _eventService.UpdateEventAsync(updatedEvent);
                return RedirectToAction(nameof(Index));
            }
            return View(updatedEvent);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var eventToDelete = await _eventService.GetEventByIdAsync(id);
            if (eventToDelete == null)
            {
                return NotFound();
            }
            return View(eventToDelete);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _eventService.DeleteEventAsync(id);
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }



        [HttpPost]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            await _eventService.DeleteEventAsync(id);
            return Ok(new { success = true });
        }
    }
}
