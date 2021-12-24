using Moq;
using Repository.Entity;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace xUnitTesting.Mocks.Repositories
{
    public class MockEquityRepository :  Mock<IEquityRepository>
    {
        public MockEquityRepository MockGetAllEquities(List<Equity> result)
        {
            Setup(x => x.GetAllEquities()).Returns(result);
            return this;
        }

        public MockEquityRepository MockGetEquityById(Equity result)
        {
            Setup(x => x.GetEquityById(It.IsAny<int>())).Returns(result);
            return this;
        }

        public MockEquityRepository MockGetEquityByName(string equityName, Equity result)
        {
            Setup(x => x.GetEquityByName(equityName)).Returns(result);
            return this;
        }
    }
}
