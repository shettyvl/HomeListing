using System;
using System.Linq;
using API.Core;
using API.Model.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using static API.Model.Models.Enums;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ListingsController : ControllerBase
    {
        private readonly ILogger<ListingsController> _logger;
        private IListManager _listManager;
        public ListingsController(ILogger<ListingsController> logger, IListManager listManager)
        {
            _logger = logger;
            _listManager = listManager;
        }

        [HttpGet("")]
        [Route("")]
        public IActionResult GetListings(string suburb, CategoryType categoryType = CategoryType.None, StatusType statusType = StatusType.None, int skip = 0, int take = 10)
        {
            if (string.IsNullOrEmpty(suburb))
                return BadRequest("No Suburb provided");

            try{
                PagedResult<Listing> listings = _listManager.GetListings(suburb, categoryType, statusType, skip, take);
                if (listings?.Results?.Any() == true)
                {
                    return Ok(JsonConvert.SerializeObject(listings));
                }
            }catch(Exception e)
            {
                _logger.LogError(e.ToString());
                // Can return custom error code server error
                return StatusCode(500, "An error occurred on the server.");
            }

            return NotFound();
        }
    }
}
