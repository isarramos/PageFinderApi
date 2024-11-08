using Domain;
using Domain.Repository;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly ILogger<UserController> _logger;
    private readonly IRepository<User> _repository;
    public UserController(ILogger<UserController> logger, IRepository<User> repository)
    {
        _logger = logger;
        _repository = repository;
    }

    [HttpPost]
    public async Task<IActionResult> PostCreateUser([FromBody] User user, CancellationToken cancellationToken )
    {
        await _repository.InsertAsync(user, cancellationToken);
        return Ok();
    }
}