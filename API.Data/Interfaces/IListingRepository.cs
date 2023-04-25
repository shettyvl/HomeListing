using System;
using API.Model;
using API.Model.Models;

namespace API.Data.Interfaces
{
	public interface IListingRepository
	{
		List<Listing> GetListings(string suburb, Enums.CategoryType categoryType, Enums.StatusType statusType, int skip, int take);
	}
}

