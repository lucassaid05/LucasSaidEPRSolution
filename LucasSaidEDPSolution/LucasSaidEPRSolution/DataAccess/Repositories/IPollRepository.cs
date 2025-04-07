using Domain.Model;

namespace DataAccess.Repositories
{
    public interface IPollRepository
    {
        Task CreatePoll(PollCreationModel pollModel);
        Task<List<Poll>> GetPolls();
        Task<Poll> GetPollByIdAsync(int id);
        Task Vote(int pollId, int selectedOption);
    }
}
