using DataAccess.DataContext;
using Domain.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DataAccess.Repositories
{
    internal class PollRepository
    {
        private readonly PollDbContext _context;

        public PollRepository(PollDbContext context)
        {
            _context = context;
        }

        public async Task CreatePoll(string title, string option1Text, string option2Text, string option3Text)
        {
            var poll = new Poll
            {
                Title = title,
                Option1Text = option1Text,
                Option2Text = option2Text,
                Option3Text = option3Text,
                Option1VotesCount = 0,
                Option2VotesCount = 0,
                Option3VotesCount = 0,
                DateCreated = DateTime.UtcNow
            };

            await _context.Polls.AddAsync(poll);
            await _context.SaveChangesAsync();
        }

        public async Task<Poll> GetPollByIdAsync(int id)
        {
            return await _context.Polls.FindAsync(id);
        }

        public async Task<List<Poll>> GetAllPollsAsync()
        {
            return await _context.Polls.ToListAsync();
        }
    }
}
