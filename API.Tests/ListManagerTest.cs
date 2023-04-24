using API.Core;
using Microsoft.Extensions.Configuration;
using Xunit;
using API.Model.Models;
using API.Model.Utils;
using API.Core;
using API.Core.Managers;
using API.Data.Interfaces;
using API.Data.Repository;
using API.Data.Dapper;
using API.Data.Interfaces;
using Microsoft.Extensions.Options;

namespace API.Tests
{
    public class ListManagerTest
    {
        private IConfiguration _configuration;
        private IListManager _listManager;
        private IListingRepository _repository;
        private IDbManager _dbManager;
        private IOptions<AppConfig> _options;

        public ListManagerTest()
        {
            _configuration = GetConfiguration();
            _options.Value.TESTRead = _configuration.GetConnectionString("TESTRead");
            _dbManager = new DbManager(_options);
            _repository = new ListingRepository(_dbManager);
            _listManager = new ListManager(_repository);
        }

        [Fact]
        public void GetResidentialListings()
        {

            string subsurb = "Southbank";
            var results = _listManager.GetListings(subsurb, Enums.CategoryType.Residential, Enums.StatusType.Current, 0, 50);
            Assert.NotNull(results);
        }

        [Fact]
        public void GetRentalListings()
        {
            string subsurb = "Kew";
            var results = _listManager.GetListings(subsurb, Enums.CategoryType.Rental, Enums.StatusType.Current, 0, 50);
            Assert.NotNull(results);
        }

        public IConfiguration GetConfiguration()
        {
            if (_configuration == null)
            {
                var builder = new ConfigurationBuilder().AddJsonFile($"testsettings.json", optional: false);
                _configuration = builder.Build();
            }

            return _configuration;
        }
    }
}
