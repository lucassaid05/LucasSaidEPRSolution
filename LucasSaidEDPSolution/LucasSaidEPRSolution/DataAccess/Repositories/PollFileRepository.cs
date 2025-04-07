using Domain.Model;
using System.Text.Json;

namespace DataAccess.Repositories
{
    public class PollFileRepository : IPollRepository
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
                return new List<Poll>();

            var json = await File.ReadAllTextAsync(_filePath);
            return JsonSerializer.Deserialize<List<Poll>>(json) ?? new List<Poll>();
        }

        public async Task<Poll> GetPollByIdAsync(int id)
        {
            var polls = await GetPolls();
            return polls.FirstOrDefault(p => p.Id == id);
        }

        public async Task Vote(int pollId, int selectedOption)
        {
            var polls = await GetPolls();
            var poll = polls.FirstOrDefault(p => p.Id == pollId);
            if (poll == null) return;

            if (selectedOption == 1)
                poll.Option1VotesCount++;
            else if (selectedOption == 2)
                poll.Option2VotesCount++;
            else if (selectedOption == 3)
                poll.Option3VotesCount++;
            else
                return;

            var json = JsonSerializer.Serialize(polls, new JsonSerializerOptions { WriteIndented = true });
            await File.WriteAllTextAsync(_filePath, json);
        }
    }
}
