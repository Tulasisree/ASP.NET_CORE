using System.Reflection.Metadata.Ecma335;
using DiaryApp.Models;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Tree;
using Microsoft.EntityFrameworkCore;
using WebDiaryAPI.Data;

namespace WebDiaryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiaryEntriesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DiaryEntriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DiaryEntry>>> GetDiaryEntries()
        {
            return await _context.DiaryEntries.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DiaryEntry>> GetDiaryEntry(int id)
        {
            var diaryEntry =  await _context.DiaryEntries.FindAsync(id);
            if( diaryEntry == null )
            {
                return NotFound();
            }

            return diaryEntry;
        }

        [HttpPost]
        public async Task<ActionResult<DiaryEntry>> PostDiaryEntry(DiaryEntry diaryEntry)
        {
            //Id shd not be assigned previously , if its does then we might end up creating a 
            //duplicate record so assign 0
            //Id is created when its added to db.
            diaryEntry.Id = 0;
            _context.DiaryEntries.Add(diaryEntry);

            await _context.SaveChangesAsync();

            //location at which item has been created
            //provides clear guidence to client on how to access the new resource created
            //facilitates followUp actions
            var resourceUrl = Url.Action(nameof(GetDiaryEntry),new {id=diaryEntry.Id});

            return Created(resourceUrl,diaryEntry);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutDiaryEntry(int id,[FromBody] DiaryEntry diaryEntry)
        {
            //[FromBody] binds request body to DiaryEntry type
            if(id != diaryEntry.Id)
            {
                return BadRequest();
            }

            //marks diaryEntry as modified in DbContext which tells the ef to update the existing 
            //record in db
            //UPDATE [DiaryEntries]
            // SET 
            //     [Title] = @p0,
            //     [Content] = @p1,
            //     [Created] = @p2
            // WHERE [Id] = @p3; (pppulated from object)
            _context.Entry(diaryEntry).State = EntityState.Modified; //doesnt execute update yet

            try
            {
                await _context.SaveChangesAsync();//executes
            }
            catch(DbUpdateConcurrencyException)
            {
                if(!DiaryEntryExists(id))
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }

        private bool DiaryEntryExists(int id)
        {
            return _context.DiaryEntries.Any(d => d.Id == id);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDiaryEntry(int id)
        {
            var diaryEntry = await _context.DiaryEntries.FindAsync(id);
            if(diaryEntry == null)
            {
                return NotFound();
            }

            _context.DiaryEntries.Remove(diaryEntry);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}