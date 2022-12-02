using HabitTrainer.API.Data;
using HabitTrainer.API.DTOs;
using HabitTrainer.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HabitTrainer.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HabitController : ControllerBase
{
    private readonly AppDbContext _context;

    public HabitController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet("List")]
    public async Task<ActionResult<List<Habit>>> ListHabits()
    {
        var habits = await _context.Habits.AsNoTracking().ToListAsync();

        if (!habits.Any()) return NotFound();

        return Ok(habits);
    }

    [HttpGet("Get/{id}")]
    public async Task<ActionResult<Habit>> GetHabit(int id)
    {
        var habit = await _context.Habits.FindAsync(id);

        if (habit == null) return NotFound();

        return Ok(habit);
    }

    [HttpPost("Create")]
    public async Task<ActionResult> PostHabit(PostHabitDto habitDto)
    {
        var habit = new Habit
        {
            Name = habitDto.Name,
            Description = habitDto.Description,
            StartDate = DateOnly.FromDateTime(DateTime.Now),
            EndDate = DateOnly.FromDateTime(DateTime.Now.AddMonths(1))
        };

        _context.Habits.Add(habit);

        await _context.SaveChangesAsync();

        return Ok();
    }

    [HttpDelete("Delete/{id}")]
    public async Task<ActionResult> DeleteHabit(int id)
    {
        var habit = await _context.Habits.FindAsync(id);

        if (habit == null) return NotFound();

        _context.Remove(habit);

        var result = await _context.SaveChangesAsync() > 0;

        if (result) return Ok();

        return BadRequest(new ProblemDetails { Title = "Problem deleting product" });
    }
}
