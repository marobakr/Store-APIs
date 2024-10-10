using Microsoft.AspNetCore.Mvc;
using Store.S_02.APIs.Error;
using Store.S_02.Repository.Data.Contexts;

namespace Store.S_02.APIs.Controllers;


public class BuggyController : BaseApiController
{
    private readonly StoreDbContext _context;

    public BuggyController(StoreDbContext context)
    {
        _context = context;
    }

    [HttpGet("notfound")] /*Get: /api/Buggy/notfound*/
    public async Task<IActionResult> GetNotFoundRequestError()
    {
        var brand = await _context.Brands.FindAsync(100);
        if (brand is null)
            return NotFound(new
            {
                Message = "Brand not found", StatusCode =
                    StatusCodes.Status404NotFound
            });
        return Ok(brand);
    }


    [HttpGet("servererror")] /*Get: /api/Buggy/servererro*/
    public async Task<IActionResult> GetServerRequestError()
    {
        var brand = await _context.Brands.FindAsync(100);
        var brandToString = brand.ToString(); /* Will throw Exception [NullReferenceException] */
        return Ok(brand);
    }


    [HttpGet("badrequest")] /*Get: /api/Buggy/badrequest*/
    public async Task<IActionResult> GetbadRequestError()
    {
        return BadRequest(new  APiErrorResponse(400));
    }


    [HttpGet("badrequest/{id}")] /*Get: /api/Buggy/badrequest/ahmed*/
    public async Task<IActionResult> GetbadRequestError(int id) // validation error 
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new APiErrorResponse(400));
        }
        return Ok();
    }

    [HttpGet("unaithorized/{id}")] /*Get: /api/Buggy/unaithorized*/
    public async Task<IActionResult> GetError()
    {
        return Unauthorized(new  APiErrorResponse(401));
    }
}