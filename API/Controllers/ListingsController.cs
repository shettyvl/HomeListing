using System;
using System.Linq;
using API.Core;
using API.Model.Models;
using Microsoft.AspNetCore.Mvc;
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
        private IListingsManager _listManager;
        public ListingsController(ILogger<ListingsController> logger, IListingsManager listManager)
        {
            _logger = logger;
            _listManager = listManager;
        }

        /// <summary>
        /// Get listings for the suburb.
        /// </summary>
        /// <param name="Suburb"></param>
        /// <param name="Category Type"></param>
        /// <param name="Status Type"></param>
        /// <param name="Skip"></param>
        /// <param name="Take"></param>
        /// <returns>List of listings for the given suburb.</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     suburb=Melbourne
        ///
        /// </remarks>
        /// <response code="200">Returns the listing list</response>
        /// <response code="400">when suburb name is empty</response>
        /// <response code="500">Internal server error</response>
        /// <response code="404">When list not found for the filter condition</response>

        [HttpGet]
        public IActionResult GetListings(string suburb, CategoryType categoryType = CategoryType.None, StatusType statusType = StatusType.None, int skip = 0, int take = 10)
        {
            try
            {
                return TryGetListing(suburb, categoryType, statusType, skip, take);
            }
            catch(Exception e)
            {
                _logger.LogError(e.ToString());
                // Can return custom error code server error
                return StatusCode(500, "An error occurred on the server.");
            }           
        }


        private IActionResult TryGetListing(string suburb, CategoryType categoryType, StatusType statusType, int skip, int take)
        {
            if (string.IsNullOrEmpty(suburb))
            {
                return BadRequest("No Suburb provided");
            }

            PagedResult<Listing> listings = _listManager.GetListings(suburb, categoryType, statusType, skip, take);
            var hasResult = listings?.Results?.Any() ?? false;

            if (hasResult)
            {
                return Ok(JsonConvert.SerializeObject(listings));
            }

            return NotFound();
        }
    }
}
