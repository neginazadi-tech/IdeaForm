using IdeaSystem.Application.Contracts;
using IdeaSystem.Application.Contracts.Dtos;
using Microsoft.AspNetCore.Mvc;
using SimpleForm.Models;
using System.Diagnostics;

namespace SimpleForm.Controllers;

public class HomeController(IIdeaService ideaService) : Controller
{
    private readonly IIdeaService _ideaService = ideaService;

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult SubmitIdea()
    {
        return View(new IdeaDto());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> SubmitIdea(IdeaDto idea)
    {
        if (ModelState.IsValid)
        {
            await _ideaService.AddAsync(idea);
            return RedirectToAction(nameof(IdeaGrid));
        }
        return View(idea);
    }

    [HttpGet]
    public async Task<IActionResult> IdeaGrid(int pageIndex = 1)
    {
        int pageSize = 3;
        var ideas = await _ideaService.GetAllAsync();
        var paginatedIdeas = PaginatedList<IdeaDto>.Create(ideas.AsQueryable(), pageIndex, pageSize);

        return View(paginatedIdeas);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
