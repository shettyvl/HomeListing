using API.Model.Models;
using static API.Model.Models.Enums;

namespace API.Core
{
    public interface IListManager
    {
        PagedResult<Listing> GetListings(string suburb, CategoryType categoryType, StatusType statusType, int skip, int take);
    }
}
