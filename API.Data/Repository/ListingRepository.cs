using System;
using API.Model.Models;
using API.Data.Interfaces;
using Dapper;
using static API.Model.Models.Enums;
using API.Data.Dapper;
using API.Data.Interfaces;
namespace API.Data.Repository
{
	public class ListingRepository : IListingRepository
	{
        private readonly IDbManager _dbManager;
        public ListingRepository(IDbManager dbManager)
		{
            _dbManager = dbManager;
        }

        public List<Listing> GetListings(string suburb, CategoryType categoryType, StatusType statusType, int skip, int take)
        {
            var listings = new List<Listing>();
            var total = 0;

            var filter = categoryType != CategoryType.None ? $" AND CategoryType = {(int)categoryType} " : string.Empty;
            filter = statusType != StatusType.None ? filter + $" AND StatusType = {(int)statusType} " : string.Empty;

            var sql = $@" SELECT count(ListingId) FROM [Backend-TakeHomeExercise].dbo.Listings WITH(NOLOCK)
                                WHERE Suburb = @suburb {filter} ;

                                SELECT ListingId, StreetNumber, Street, Suburb, State, Postcode, DisplayPrice, Title, CategoryType, StatusType
                                FROM [Backend-TakeHomeExercise].dbo.Listings WITH(NOLOCK)
                                WHERE Suburb = @suburb {filter} 
                                ORDER BY ListingId
                                OFFSET @skip ROWS FETCH NEXT @take ROWS ONLY ;
                            ";

            using (var db = _dbManager.GetOpenConnection())
            {
                var cmd = new CommandDefinition(sql, new { suburb, cattype = (int)categoryType, statusType = (int)statusType, skip, take });
                var multi = db.QueryMultiple(cmd);

                total = multi.Read<int>().FirstOrDefault();
                listings = multi.Read<Listing>().ToList();
            }
            return listings;
        }
    }
}

