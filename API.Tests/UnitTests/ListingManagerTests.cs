﻿using System;
using API.Core.Managers;
using API.Data.Interfaces;
using API.Core;
using API.Data.Repository;
using Moq;
using Microsoft.Extensions.Configuration;
using Xunit;
using System.Collections.Generic;

namespace API.Tests.UnitTests
{
	public class ListingManagerTests
	{
		private readonly Mock<IListingRepository> _mocklistRepo = new Mock<IListingRepository>();
		private ListManager _listManager;
        private Mock<IConfiguration> _configuration  = new Mock<IConfiguration>();

        public ListingManagerTests()
		{
            _listManager = new ListManager(_mocklistRepo.Object, _configuration.Object);

        }

        [Fact]
        public void GetListings_WhenSuburbHasListing_ReturnsListOfListings()
        {
            _mocklistRepo.Setup(a => a.GetListings("Melbourne", Model.Models.Enums.CategoryType.Residential, Model.Models.Enums.StatusType.Current, 0, 10))
                .Returns(new List<Model.Models.Listing>()
                {
                    new Model.Models.Listing()
                    {
                         ListingId = 1
                    },
                    new Model.Models.Listing()
                    {
                        ListingId = 2
                    }
                });

            var result = _listManager.GetListings("Melbourne", Model.Models.Enums.CategoryType.Residential, Model.Models.Enums.StatusType.Current, 0, 10);

            Assert.Equal(2, result.Count);
            Assert.Equal(2, result.Total);
            _mocklistRepo.Verify(a => a.GetListings("Melbourne", Model.Models.Enums.CategoryType.Residential, Model.Models.Enums.StatusType.Current, 0, 10), Times.Once);

        }

    }
}
