using Business.Services;
using Repository.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using xUnitTesting.Mocks.Repositories;

namespace xUnitTesting.Services
{
    public class EquityServiceTests
    {
        // Test Scenario case 1: Invalid Equity ID
        [Fact]
        public void EquityService_GetEquityById_InvalidEquityId()
        {
            // Arrange
            var mockEquityRepo = new MockEquityRepository();

            // Create Service
            EquityService equityService = new EquityService(mockEquityRepo.Object);

            // Act
            var resultEquity = equityService.GetEquityById(2);

            //Assert 
            // Equity list is null
            Assert.Null(resultEquity);

        }

        // Test Scenario case 2: Invalid Equity Name
        [Fact]
        public void EquityService_GetEquityById_InvalidEquityName()
        {
            // Arrange
            var mockEquityRepo = new MockEquityRepository();

            // Create Service
            EquityService equityService = new EquityService(mockEquityRepo.Object);

            // Act
            var equity = equityService.GetEquityByName("TCS");

            //Assert 
            // Equity list is null
            Assert.Null(equity);

        }

        // Test Scenario case 3: Valid Equity Name
        [Fact]
        public void EquityService_GetEquityById_ValidEquityName()
        {
            // Arrange
            var mockEquityRepo = new MockEquityRepository();

            // Create Service
            EquityService equityService = new EquityService(mockEquityRepo.Object);

            // Act
            var equity = equityService.GetEquityByName("TCS");

            //Assert 
            // Equity list is null
            Assert.Null(equity);

        }

        // Test Scenario case 4: Valid Equity ID
        [Fact]
        public void EquityService_GetEquityById_ValidEquityId()
        {
            var equity = new Equity { EquityId = 1, EquityName = "TCS".ToUpper(), CMP = 2000 };
            // Arrange
            var mockEquityRepo = new MockEquityRepository().MockGetEquityById(equity);

            // Create Service
            EquityService equityService = new EquityService(mockEquityRepo.Object);

            // Act
            var resultEquity = equityService.GetEquityById(1);

            //Assert 
            // Equity list is not null
            Assert.NotNull(resultEquity);

        }
    }
}
