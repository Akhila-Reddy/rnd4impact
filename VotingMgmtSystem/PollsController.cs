{\rtf1\ansi\ansicpg1252\cocoartf2638
\cocoatextscaling0\cocoaplatform0{\fonttbl\f0\fnil\fcharset0 HelveticaNeue;}
{\colortbl;\red255\green255\blue255;}
{\*\expandedcolortbl;;}
\paperw11900\paperh16840\margl1440\margr1440\vieww11520\viewh8400\viewkind0
\deftab560
\pard\pardeftab560\slleading20\pardirnatural\partightenfactor0

\f0\fs26 \cf0 // Controllers/PollsController.cs\
[ApiController]\
[Route("api/polls")]\
public class PollsController : ControllerBase\
\{\
    private readonly ApplicationDbContext _context;\
\
    public PollsController(ApplicationDbContext context)\
    \{\
        _context = context;\
    \}\
\
    // Create a new poll\
    [HttpPost]\
    public async Task<IActionResult> CreatePoll(Poll poll)\
    \{\
        _context.Polls.Add(poll);\
        await _context.SaveChangesAsync();\
        return Ok(poll);\
    \}\
\
    // Get all polls\
    [HttpGet]\
    public IActionResult GetAllPolls()\
    \{\
        var polls = _context.Polls.ToList();\
        return Ok(polls);\
    \}\
\
    // Get a specific poll by ID\
    [HttpGet("\{id\}")]\
    public IActionResult GetPollById(int id)\
    \{\
        var poll = _context.Polls.FirstOrDefault(p => p.Id == id);\
        if (poll == null)\
            return NotFound();\
        return Ok(poll);\
    \}\
\
    // Update a poll by ID\
    [HttpPut("\{id\}")]\
    public async Task<IActionResult> UpdatePoll(int id, Poll updatedPoll)\
    \{\
        var poll = _context.Polls.FirstOrDefault(p => p.Id == id);\
        if (poll == null)\
            return NotFound();\
\
        poll.Question = updatedPoll.Question;\
        poll.Options = updatedPoll.Options;\
\
        await _context.SaveChangesAsync();\
        return Ok(poll);\
    \}\
\
    // Delete a poll by ID\
    [HttpDelete("\{id\}")]\
    public async Task<IActionResult> DeletePoll(int id)\
    \{\
        var poll = _context.Polls.FirstOrDefault(p => p.Id == id);\
        if (poll == null)\
            return NotFound();\
\
        _context.Polls.Remove(poll);\
        await _context.SaveChangesAsync();\
        return Ok();\
    \}\
\}}