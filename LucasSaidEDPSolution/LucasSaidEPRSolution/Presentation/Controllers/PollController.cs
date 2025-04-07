using Microsoft.AspNetCore.Mvc;
using DataAccess.Repositories;
using Domain.Model;

namespace Presentation.Controllers
{
    public class PollController : Controller
    {
        private readonly IPollRepository _pollRepository;

        public PollController(IPollRepository pollRepository)
        {
            _pollRepository = pollRepository;
        }

        public IActionResult Create() => View();

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
            var poll = await _pollRepository.GetPollByIdAsync(id);
            if (poll == null) return NotFound();
            return View(poll);
        }

        [HttpPost]
        public async Task<IActionResult> Vote(int id, int selectedOption)
        {
            await _pollRepository.Vote(id, selectedOption);
            return RedirectToAction("Index");
        }
    }
}
