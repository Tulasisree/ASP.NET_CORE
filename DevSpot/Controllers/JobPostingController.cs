using System.Runtime.CompilerServices;
using DevSpot.Constants;
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
        if(User.IsInRole(Roles.EMPLOYER))
        {
            var filterdJobPostings = jobPostings.Where(x => x.UserId == _userManager.GetUserId(User));
            return View(filterdJobPostings);
        }
        return View(jobPostings);
    }
    [Authorize(Roles = "Admin,Employer")]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [Authorize(Roles = "Admin,Employer")]
    public async Task<IActionResult> Create(JobPostingsViewModel jobPostingVm)
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

//JobPosting/delete/5
    [HttpDelete]
    [Authorize(Roles = "Admin,Employer")]
    public async Task<IActionResult> Delete(int id)
    {
        var jobPosting = await _repository.GetByIdAsync(id);
        if(jobPosting == null)
        {
            return NotFound();
        }

        var userId = _userManager.GetUserId(User);
        if(User.IsInRole(Roles.ADMIN) == false && jobPosting.UserId != userId)
        {
            return Forbid();
        }

        await _repository.DeleteAsync(id);

        return Ok();
    }
}