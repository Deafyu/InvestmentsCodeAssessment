using InvestmentsCodeAssessment.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InvestmentsCodeAssessment.Controllers;

[Route("[controller]")]
[ApiController]
public class InvestmentsController : ControllerBase
{
    public IInvestmentsService _investmentsService;

    public InvestmentsController(IInvestmentsService investmentsService)
    {
        _investmentsService = investmentsService;
    }

    // GET: api/<InvestmentsController>
    [HttpGet]
    public IActionResult GetAllShares()
    {
        try
        {
            return Ok(_investmentsService.GetAllShares());
        }
        catch (Exception e)
        {
            return BadRequest();
        }
    }

    // GET api/<InvestmentsController>/5
    [HttpGet("basis")]
    public IActionResult GetSharesBasis()
    {
        try
        {
            return Ok(_investmentsService.GetSharesBasis());
        }
        catch (Exception e)
        {
            return BadRequest();
        }
    }

    // POST api/<InvestmentsController>/buy
    [HttpPost("buy")]
    public IActionResult BuyStock(int stockAmount, int stockPrice)
    {
        if (stockAmount < 1|| stockPrice < 1)
        {
            return BadRequest();
        }

        try
        {
            _investmentsService.BuyShares(stockAmount, stockPrice);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest();
        }
    }

    // DELETE api/<InvestmentsController>/sell
    [HttpPost("sell")]
    public IActionResult SellStock(int stockAmount, int stockPrice)
    {
        try
        {
            return Ok(_investmentsService.SellShares(stockAmount, stockPrice));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}