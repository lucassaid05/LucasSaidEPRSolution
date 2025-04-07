using Microsoft.AspNetCore.Mvc;
using DataAccess.Repositories;
using Domain.Model;


namespace Presentation.Controllers
{
    public class PollController : Controller
    {
        private readonly PollRepository _pollRepository;

        public PollController(PollRepository pollRepository)
        {
            _pollRepository = pollRepository;
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(PollCreationModel pollModel)
        {
            if (ModelState.IsValid)
            {
                await _pollRepository.CreatePoll(pollModel);
                return RedirectToAction("Index");
            }

            return View(pollModel);
        }
    }
}