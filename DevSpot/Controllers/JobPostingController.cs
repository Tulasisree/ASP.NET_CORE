using System.Runtime.CompilerServices;
using DevSpot.Models;
using DevSpot.Respositories;
using DevSpot.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NuGet.Packaging.Core;

namespace Controllers.JobPostingController;
[Authorize]
public class JobPostingController : Controller
{
    private readonly IRespository<JobPosting> _repository;
    private readonly UserManager<IdentityUser> _userManager;

    public JobPostingController(IRespository<JobPosting> respository, UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
        _repository = respository;
    }
    [AllowAnonymous]
    public async Task<IActionResult> Index()
    {
        var jobPostings = await _repository.GetAllAsync();
        return View(jobPostings);
    }
    [Authorize(Roles = "Admin,Employer")]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [Authorize(Roles = "Admin,Employer")]
    public async Task<IActionResult> Create(JobPostingViewModel jobPostingVm)
    {
        if(ModelState.IsValid)
        {
            var jobPosting = new JobPosting{
                Title = jobPostingVm.Title,
                Description = jobPostingVm.Description,
                Company = jobPostingVm.Company,
                Location  = jobPostingVm.Location,
                UserId = _userManager.GetUserId(User)
            };

           await _repository.AddAsync(jobPosting);
           return RedirectToAction(nameof(Index));
        }

        return View(jobPostingVm);
    }
}