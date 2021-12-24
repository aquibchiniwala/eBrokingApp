using Repository.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Interface
{
    public interface IEquityRepository
    {
        public List<Equity> GetAllEquities();
        public Equity GetEquityById(int id);
        public Equity GetEquityByName(string name);

    }
}
