using Domain;
using Domain.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Controller : ControllerBase
    {
        private readonly ILogger<Controller> _logger;
        private readonly IRepository<Review> _repository;
        public Controller(ILogger<Controller> logger, IRepository<Review> repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Review review, CancellationToken cancellationToken )
        {
            await _repository.InsertAsync(review, cancellationToken);
            return Ok();
        }
    }
}
