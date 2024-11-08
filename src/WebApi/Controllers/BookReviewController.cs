using Domain;
using Domain.Repository;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class BookReviewController : ControllerBase
    {
        private readonly ILogger<BookReviewController> _logger;
        private readonly IRepository<BookReview> _repository;
        public BookReviewController(ILogger<BookReviewController> logger, IRepository<BookReview> repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpPost]
        public async Task<IActionResult> PostBookReview([FromBody] BookReview bookReview, CancellationToken cancellationToken )
        {
            await _repository.InsertAsync(bookReview, cancellationToken);
            return Ok();
        }
    }
}
