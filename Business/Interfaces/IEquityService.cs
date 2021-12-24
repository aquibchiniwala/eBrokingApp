using Repository.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Interfaces
{
    public interface IEquityService
    {
        public List<Equity> GetAllEquities();
        public Equity GetEquityById(int id);
        public Equity GetEquityByName(string name);
    }
}
