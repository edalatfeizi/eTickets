using eTickets.Data;
using eTickets.Data.Services;
using eTickets.Models;
using eTickets.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Controllers
{
    public class ActorsController : Controller
    {
        private readonly IActorsService _service;
        public ActorsController(IActorsService service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index()
        {
            var actors = await _service.GetAllAsync();
            var _actors = new List<ActorViewModel>();
            foreach (var actor in actors)
            {
                var actorViewModel = new ActorViewModel()
                {
                    Id = actor.Id,
                    FullName = actor.FullName,
                    ProfilePictureURL = actor.ProfilePictureURL,
                    Bio = actor.Bio
                };
                _actors.Add(actorViewModel);
            }
            return View(_actors);
        }  
        //Get: Actors/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("FullName,ProfilePictureURL,Bio")][FromForm] ActorViewModel _actor)
        {
            if (!ModelState.IsValid)
            {
                return View(_actor);
            }
            var actor = new Actor
            {
                Id = _actor.Id,
                FullName = _actor.FullName,
                ProfilePictureURL = _actor.ProfilePictureURL,
                Bio = _actor.Bio
            };
            await _service.AddAsync(actor);
            return RedirectToAction(nameof(Index));
        }
        //Get: Actors/Details/1
        public async Task<IActionResult> Details(int id)
        {
            var actorDetails = await _service.GetByIdAsync(id);

            if (actorDetails == null) return View("NotFound");
            var _actor = new ActorViewModel()
            {
                Id = actorDetails.Id,
                FullName = actorDetails.FullName,
                ProfilePictureURL = actorDetails.ProfilePictureURL,
                Bio = actorDetails.Bio
            };

            return View(_actor);
        }

        //Get: Actors/Edit/1
        public async Task<IActionResult> Edit(int id)
        {
            var actorDetails = await _service.GetByIdAsync(id);

            if (actorDetails == null) return View("NotFound");
            var _actor = new ActorViewModel()
            {
                Id = actorDetails.Id,
                FullName = actorDetails.FullName,
                ProfilePictureURL = actorDetails.ProfilePictureURL,
                Bio = actorDetails.Bio
            };
            return View(_actor);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FullName,ProfilePictureURL,Bio")] ActorViewModel _actor)
        {
            if (!ModelState.IsValid)
            {
                return View(_actor);
            }
            var actor = new Actor
            {
                Id = _actor.Id,
                FullName = _actor.FullName,
                ProfilePictureURL = _actor.ProfilePictureURL,
                Bio = _actor.Bio
            };
            await _service.UpdateAsync(id,actor);
            return RedirectToAction(nameof(Index));
        }
        //Get: Actors/Delete/1
        public async Task<IActionResult> Delete(int id)
        {
            var actorDetails = await _service.GetByIdAsync(id);

            if (actorDetails == null) return View("NotFound");
            var _actor = new ActorViewModel()
            {
                Id = actorDetails.Id,
                FullName = actorDetails.FullName,
                ProfilePictureURL = actorDetails.ProfilePictureURL,
                Bio = actorDetails.Bio
            };
            return View(_actor);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            
            await _service.DeleteAsync(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
