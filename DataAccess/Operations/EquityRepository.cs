using Repository.Entity;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository.Operations
{
    public class EquityRepository : IEquityRepository
    {
        private readonly DBContext context;

        public EquityRepository(DBContext context)
        {
            this.context = context;
        }

        public List<Equity> GetAllEquities()
        {
            return context.Equity.ToList();
        }

        public Equity GetEquityById(int id)
        {
            return context.Equity.Find(id);
        }

        public Equity GetEquityByName(string name)
        {
            return context.Equity.Where(e => e.EquityName == name.ToUpper()).Select(e=>e).FirstOrDefault<Equity>() ?? null;
        }
    }
}
