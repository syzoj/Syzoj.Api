using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Syzoj.Api.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Syzoj.Api.Models.Requests;
using System.Net.Http;
using Syzoj.Api.Services;
using Syzoj.Api.Models.Data;

namespace Syzoj.Api.Controllers
{
    [Route("api/problem")]
    public class ProblemController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        private readonly ILegacySyzojImporter legacySyzojImporter;
        public ProblemController(ApplicationDbContext dbContext, ILegacySyzojImporter legacySyzojImporter)
        {
            this.dbContext = dbContext;
            this.legacySyzojImporter = legacySyzojImporter;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetProblemList()
        {
            var problemSet = dbContext.ProblemSetProblems
                .Where(psp => psp.ProblemSetId == 1)
                .Include(psp => psp.Problem)
                .Select(p => new {
                    Id = p.ProblemSetProblemId,
                    Title = p.Problem.Title,
                    Submissions = p.Problem.Submissions,
                    Accepts = p.Problem.Accepts,
                });
            return Ok(new {
                Status = "Success",
                ProblemSet = problemSet
            });
        }

        // TODO: Server side rendering
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProblem(string id)
        {
            var problem = await dbContext.ProblemSetProblems
                .Where(psp => psp.ProblemSetId == 1 && psp.ProblemSetProblemId == id)
                .Include(psp => psp.Problem)
                .Select(p => new {
                    Id = p.ProblemSetProblemId,
                    Title = p.Problem.Title,
                    Submissions = p.Problem.Submissions,
                    Accepts = p.Problem.Accepts,
                    DataType = p.Problem.DataType,
                    Data = p.Problem.GetData<object>(),
                })
                .SingleOrDefaultAsync();
            if(problem == null)
            {
                return NotFound(new {
                    Status = "Fail",
                    Message = "Problem not found"
                });
            }
            else
            {
                return Ok(new {
                    Status = "Success",
                    Problem = problem
                });
            }
        }

        [HttpPost("{id}/import/legacy-syzoj")]
        public async Task<IActionResult> ImportProblemFromLegacySyzoj(string id, [FromBody] ImportProblemFromLegacySyzojRequest req)
        {
            var problem = await legacySyzojImporter.ImportFromLegacySyzoj(req.ProblemURL);
            if(problem == null)
            {
                return Ok(new {
                    Status = "Fail",
                    Message = "Import failed"
                });
            }
            dbContext.Problems.Add(problem);
            var problemSetRelation = new ProblemSetProblem() {
                Problem = problem,
                ProblemSetId = 1,
                ProblemSetProblemId = id,
            };
            dbContext.ProblemSetProblems.Add(problemSetRelation);
            // TODO: Handle uniqueness violation
            await dbContext.SaveChangesAsync();
            return Ok(new {
                Status = "Success",
            });
        }
    }
}