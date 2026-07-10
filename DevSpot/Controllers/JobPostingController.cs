using System.Runtime.CompilerServices;
using DevSpot.Models;
using DevSpot.Respositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NuGet.Packaging.Core;

namespace Controllers.JobPostingController;

public class JobPostingController : Controller
{
    private readonly IRespository<JobPosting> _repository;
    private readonly UserManager<IdentityUser> _userManager;

    public JobPostingController(IRespository<JobPosting> respository, UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
        _repository = respository;
    }
    public async Task<IActionResult> Index()
    {
        var jobPostings = await _repository.GetAllAsync();
        return View(jobPostings);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(JobPosting jobPosting)
    {
        return RedirectToAction(nameof(Index));
    }
}