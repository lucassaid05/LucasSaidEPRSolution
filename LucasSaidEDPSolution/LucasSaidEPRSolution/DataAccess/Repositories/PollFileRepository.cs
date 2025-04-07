using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class PollFileRepository
    {
        private readonly string _filePath = "polls.json";

        public async Task CreatePoll(PollCreationModel pollModel)
        {
            var polls = await GetPolls();

            var newPoll = new Poll
            {
                Id = polls.Any() ? polls.Max(p => p.Id) + 1 : 1,
                Title = pollModel.Title,
                Option1Text = pollModel.Option1Text,
                Option2Text = pollModel.Option2Text,
                Option3Text = pollModel.Option3Text,
                Option1VotesCount = 0,
                Option2VotesCount = 0,
                Option3VotesCount = 0,
                DateCreated = DateTime.UtcNow
            };

            polls.Add(newPoll);

            var json = JsonSerializer.Serialize(polls, new JsonSerializerOptions { WriteIndented = true });
            await File.WriteAllTextAsync(_filePath, json);
        }

        public async Task<List<Poll>> GetPolls()
        {
            if (!File.Exists(_filePath))
            {
                return new List<Poll>();
            }

            var json = await File.ReadAllTextAsync(_filePath);
            var polls = JsonSerializer.Deserialize<List<Poll>>(json);

            return polls ?? new List<Poll>();
        }
    }
}