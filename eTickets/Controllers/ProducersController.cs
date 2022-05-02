using eTickets.Data;
using eTickets.Data.Services;
using eTickets.Models;
using eTickets.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Controllers
{
    public class ProducersController : Controller
    {
        private readonly IProducersService _service;
        public ProducersController(IProducersService service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index()
        {
            var _producers = await _service.GetAllAsync();

            var producers = new List<ProducerViewModel>();

            foreach (var _producer in _producers)
            {
                var producer = new ProducerViewModel()
                {
                    Id = _producer.Id,
                    FullName = _producer.FullName,
                    ProfilePictureURL = _producer.ProfilePictureUrl,
                    Bio = _producer.Bio
                };
                producers.Add(producer);
            }


            return View(producers);
        }

        //Get: producers/details/1
        public async Task<IActionResult> Details(int id)
        {
            var producerDetails = await _service.GetByIdAsync(id);
            if (producerDetails == null) return View("NotFound");

            var _producer = new ProducerViewModel()
            {
                Id = producerDetails.Id,
                FullName = producerDetails.FullName,
                ProfilePictureURL = producerDetails.ProfilePictureUrl,
                Bio = producerDetails.Bio
            };
            return View(_producer);
        }

        //Get : producers/edit/1
        public async Task<IActionResult> Edit(int id)
        {
            var producer = await _service.GetByIdAsync(id);
            if (producer == null) return View("NotFound");


            var _producer = new ProducerViewModel
            {
                Id = producer.Id,
                FullName = producer.FullName,
                ProfilePictureURL = producer.ProfilePictureUrl,
                Bio = producer.Bio
            };
            return View(_producer);
        }

        //Post : producers/edit/1
        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FullName,ProfilePictureURL,Bio")] ProducerViewModel producer)
        {
            if (!ModelState.IsValid)
            {
                return View(producer);
            }
            if (id == producer.Id)
            {
                var _producer = new Producer
                {
                    Id = producer.Id,
                    FullName = producer.FullName,
                    ProfilePictureUrl = producer.ProfilePictureURL,
                    Bio = producer.Bio
                };
                await _service.UpdateAsync(id, _producer);
                return RedirectToAction(nameof(Index));
            }
            return View(producer);
        }

        //GET: producers/create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("ProfilePictureURL,FullName,Bio")] ProducerViewModel producer)
        {
            if (!ModelState.IsValid)
                return View(producer);


            var _producer = new Producer()
            {
                FullName = producer.FullName,
                ProfilePictureUrl = producer.ProfilePictureURL,
                Bio = producer.Bio
            };

            await _service.AddAsync(_producer);

            return RedirectToAction(nameof(Index));
        }

        //Get : producers/delete/1
        public async Task<IActionResult> Delete(int id)
        {
            var producer = await _service.GetByIdAsync(id);
            if (producer == null) return View("NotFound");


            var _producer = new ProducerViewModel
            {
                Id = producer.Id,
                FullName = producer.FullName,
                ProfilePictureURL = producer.ProfilePictureUrl,
                Bio = producer.Bio
            };
            return View(_producer);
        }

        //Post : producers/delete/1
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var producer = await _service.GetByIdAsync(id);
            if (producer == null) return View("NotFound");

            await _service.DeleteAsync(id);

        
            return RedirectToAction(nameof(Index));
        }
    }
}
