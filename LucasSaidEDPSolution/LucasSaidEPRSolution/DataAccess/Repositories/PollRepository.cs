﻿using DataAccess.DataContext;
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
    public class PollRepository
    {
        private readonly PollDbContext _context;

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

        public async Task CreatePoll(PollCreationModel pollModel)
        {
            var poll = new Poll
            {
                Title = pollModel.Title,
                Option1Text = pollModel.Option1Text,
                Option2Text = pollModel.Option2Text,
                Option3Text = pollModel.Option3Text,
                Option1VotesCount = 0,
                Option2VotesCount = 0,
                Option3VotesCount = 0,
                DateCreated = DateTime.UtcNow
            };

            await _context.Polls.AddAsync(poll);
            await _context.SaveChangesAsync();
        }
        public async Task<List<Poll>> GetPolls()
        {
            return await _context.Polls
                .OrderByDescending(p => p.DateCreated)
                .ToListAsync();
        }

        public async Task Vote(int pollId, int selectedOption)
        {
            var poll = await _context.Polls.FindAsync(pollId);
            if (poll == null) return;

            if (selectedOption == 1)
            {
                poll.Option1VotesCount++;
            }
            else if (selectedOption == 2)
            {
                poll.Option2VotesCount++;
            }
            else if (selectedOption == 3)
            {
                poll.Option3VotesCount++;
            }
            else
            {
                return;
            }

            await _context.SaveChangesAsync();
        }
    }
}
