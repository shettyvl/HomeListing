
using API.Model.Models;
using Microsoft.Extensions.Configuration;
using static API.Model.Models.Enums;
using API.Data.Interfaces;

namespace API.Core.Managers
{
    public class ListManager : IListManager
    {
        private readonly IListingRepository _listingRepository;

        public ListManager(IListingRepository listingRepository)
        { 
            _listingRepository = listingRepository;
        }
        public PagedResult<Listing> GetListings(string suburb, CategoryType categoryType, StatusType statusType, int skip, int take)
        {            
            var listings = _listingRepository.GetListings(suburb, categoryType, statusType, skip, take);
            return new PagedResult<Listing>(skip, listings.Count, listings);      
        }
    }
}
