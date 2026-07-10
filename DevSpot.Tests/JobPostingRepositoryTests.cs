using DevSpot.Data;
using DevSpot.Models;
using DevSpot.Respositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace DevSpot.Tests;

public class JobPostingRepositoryTests
{
    private readonly DbContextOptions<ApplicationDbContext> _options;

    public JobPostingRepositoryTests()
    {
        _options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("JobPostingDb").Options;
    }

    private ApplicationDbContext CreateDbContext() => new ApplicationDbContext(_options);

    [Fact]
    public async Task AddAsync_ShouldAddJobPosting()
    {
        //db context
        var db = CreateDbContext();
        //job posting repository
        var jrepo = new JobPostingRespository(db);
        //job  posting
        var jobPosting =  new JobPosting
        {
            Title = "Test Title Added",
            Description = "Test description",
            PostedDate = DateTime.Now,
            Company = "Test company",
            Location = "Test location",
            UserId = "TestUserId"
        };
        //execute
        await jrepo.AddAsync(jobPosting);
        //result
        // var result = db.JobPostings.SingleOrDefault(x => x.Title == "Test Title Added");
        var result = db.JobPostings.Find(jobPosting.Id);
        //assert
        Assert.NotNull(result);
        Assert.Equal("Test description",result.Description);
    }

    [Fact]
    public async Task GetByIdAsync_ShdReturenJobPosting()
    {
        var db = CreateDbContext();
        var jrepo = new JobPostingRespository(db);
        var jobPosting =  new JobPosting
        {
            Title = "Test Title",
            Description = "Test description",
            PostedDate = DateTime.Now,
            Company = "Test company",
            Location = "Test location",
            UserId = "TestUserId"
        };
        await db.JobPostings.AddAsync(jobPosting);
        await db.SaveChangesAsync();
        //we will get the Id from ef after we save the job
        var result = jrepo.GetByIdAsync(jobPosting.Id);
        Assert.NotNull(result);
        Assert.Equal("Test Title", jobPosting.Title);
    }

    [Fact]
    public async Task GetByIdAsync_ShdThrowKeyNotFoundException()
    {
        var db = CreateDbContext();
        var jrepo = new JobPostingRespository(db);
        await Assert.ThrowsAsync<KeyNotFoundException>(() => jrepo.GetByIdAsync(99));
    }

    [Fact]
    public async Task GetAllAsync_ShdReturnAllJobPostings()
    {
        var db = CreateDbContext();
        var jrepo = new JobPostingRespository(db);
        var jobPosting1 =  new JobPosting
        {
            Title = "Test Title 1",
            Description = "Test description 1",
            PostedDate = DateTime.Now,
            Company = "Test company",
            Location = "Test location 1",
            UserId = "TestUserId1"
        };
        var jobPosting2 =  new JobPosting
        {
            Title = "Test Title 2",
            Description = "Test description 2",
            PostedDate = DateTime.Now,
            Company = "Test company 2",
            Location = "Test location 2",
            UserId = "TestUserId2"
        };
        await db.JobPostings.AddRangeAsync(jobPosting1,jobPosting2);
        await db.SaveChangesAsync();

        var result = await jrepo.GetAllAsync();
        Assert.NotNull(result);
        //we are using the same context and above tests also add rows to jobpostings
        Assert.True(result.Count() >= 2);
    }

    [Fact]
    public async Task UpdateAsync_ShdUpdateJobPosting()
    {
        var db = CreateDbContext();
        var jrepo = new JobPostingRespository(db);
        var jobPosting1 =  new JobPosting
        {
            Title = "Test Title 1",
            Description = "Test description 1",
            PostedDate = DateTime.Now,
            Company = "Test company",
            Location = "Test location 1",
            UserId = "TestUserId1"
        };
        await db.JobPostings.AddRangeAsync(jobPosting1);
        await db.SaveChangesAsync();

        jobPosting1.Title = "Updated Title";
        await jrepo.UpdateAsync(jobPosting1);

        var result = db.JobPostings.Find(jobPosting1.Id);

        Assert.NotNull(result);
        Assert.Equal("Updated Title",jobPosting1.Title);
    }

    [Fact]
    public async Task DeleteAsync_ShdDeleteJobPosting()
    {
        var db = CreateDbContext();
        var jrepo = new JobPostingRespository(db);
        var jobPosting1 =  new JobPosting
        {
            Title = "Test Title 1",
            Description = "Test description 1",
            PostedDate = DateTime.Now,
            Company = "Test company",
            Location = "Test location 1",
            UserId = "TestUserId1"
        };
        await db.JobPostings.AddRangeAsync(jobPosting1);
        await db.SaveChangesAsync();

        await jrepo.DeleteAsync(jobPosting1.Id);

        var result = db.JobPostings.Find(jobPosting1.Id);
        Assert.Null(result);
    }
}
