using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

public class VotingService
{
    private readonly IHubContext<PollHub> _hubContext;

    public VotingService(IHubContext<PollHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public async Task Vote(int pollId, int optionIndex)
    {
        // Update the vote count in your database
        // ...

        // Get the updated vote counts
        var voteCounts = GetVoteCounts(pollId);

        // Notify clients in the poll group about the updated vote counts
        await _hubContext.Clients.Group(pollId.ToString()).SendAsync("ReceiveUpdate", voteCounts);
    }
}