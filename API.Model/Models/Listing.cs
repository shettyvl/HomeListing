using System.Linq;
using System.Text;
using Newtonsoft.Json;
using API.Model;
using static API.Model.Models.Enums;

namespace API.Model.Models
{
    public class Listing
    {
        public int ListingId { get; set; }
        [JsonIgnore]
        public string StreetNumber { get; set; }
        [JsonIgnore]
        public string Street { get; set; }
        [JsonIgnore]
        public string Suburb { get; set; }
        [JsonIgnore]
        public string State { get; set; }
        [JsonIgnore]
        public int Postcode { get; set; }
        public string Address 
        {
            get 
            {
                var addr = new StringBuilder();
                if (!string.IsNullOrEmpty(StreetNumber))
                    addr = addr.Append(StreetNumber + " ");

                if (!string.IsNullOrEmpty(Street))
                    addr = addr.Append(Street + ", ");

                if (!string.IsNullOrEmpty(Suburb))
                    addr = addr.Append(Suburb + " ");

                if (!string.IsNullOrEmpty(State))
                    addr = addr.Append(State + " ");

                if (Postcode != 0)
                    addr = addr.Append(Postcode.ToString());

                return addr.ToString(); 
            }
         }
        public CategoryType CategoryType { get; set; }
        public StatusType StatusType { get; set; }
        public string DisplayPrice { get; set; }
        public string ShortPrice 
        {
            get
            {
                var res = string.IsNullOrEmpty(DisplayPrice) ? null : string.Concat(DisplayPrice.Trim().Where(x => char.IsDigit(x) || char.IsWhiteSpace(x))).FormatNumber();
                return string.IsNullOrEmpty(res) ? string.Empty : "$" + res;
            } 
        }
        public string Title { get; set; }
    }
}
