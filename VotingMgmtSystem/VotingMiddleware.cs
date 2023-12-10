{\rtf1\ansi\ansicpg1252\cocoartf2638
\cocoatextscaling0\cocoaplatform0{\fonttbl\f0\fnil\fcharset0 HelveticaNeue;}
{\colortbl;\red255\green255\blue255;}
{\*\expandedcolortbl;;}
\paperw11900\paperh16840\margl1440\margr1440\vieww11520\viewh8400\viewkind0
\deftab560
\pard\pardeftab560\slleading20\pardirnatural\partightenfactor0

\f0\fs26 \cf0 using System;\
using System.Linq;\
using System.Threading.Tasks;\
using Microsoft.AspNetCore.Http;\
using Microsoft.EntityFrameworkCore;\
using PollingApp.Data;\
\
public class VotingMiddleware\
\{\
    private readonly RequestDelegate _next;\
    private readonly ApplicationDbContext _context;\
\
    public VotingMiddleware(RequestDelegate next, ApplicationDbContext context)\
    \{\
        _next = next;\
        _context = context;\
    \}\
\
    public async Task InvokeAsync(HttpContext context)\
    \{\
        // Get the poll ID from the route parameter (assuming the route is /api/polls/\{id\}/vote)\
        if (context.Request.Path.Value.StartsWith("/api/polls/") &&\
            context.Request.Path.Value.EndsWith("/vote") &&\
            int.TryParse(context.Request.Path.Value.Split('/')[3], out int pollId))\
        \{\
            // Get the user ID from your authentication system (adjust this based on your authentication)\
            var userId = context.User.Identity.Name; // Change this to your actual user identifier\
\
            // Check if the user has already voted in this poll\
            var existingVote = await _context.Votes\
                .FirstOrDefaultAsync(v => v.PollId == pollId && v.UserId == userId);\
\
            if (existingVote != null)\
            \{\
                // User has already voted in this poll, return an error response\
                context.Response.StatusCode = StatusCodes.Status403Forbidden;\
                await context.Response.WriteAsync("You have already voted in this poll.");\
                return;\
            \}\
        \}\
\
        // If the user hasn't voted in this poll, continue to the next middleware\
        await _next(context);\
    \}\
\}}