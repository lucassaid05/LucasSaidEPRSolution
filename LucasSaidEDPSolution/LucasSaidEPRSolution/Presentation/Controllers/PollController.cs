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

        public async Task<IActionResult> Index()
        {
            var polls = await _pollRepository.GetPolls();
            return View(polls);
        }

        public async Task<IActionResult> Details(int id)
        {
            var polls = await _pollRepository.GetPolls();
            var poll = polls.FirstOrDefault(p => p.Id == id);

            if (poll == null)
            {
                return NotFound();
            }

            return View(poll);
        }
    }
}