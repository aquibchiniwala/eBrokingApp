using Business.Interfaces;
using Repository.Entity;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Services
{
    public class EquityService : IEquityService
    {
        private readonly IEquityRepository equityRepository;

        public EquityService(IEquityRepository equityRepository)
        {
            this.equityRepository = equityRepository;
        }
        public List<Equity> GetAllEquities()
        {
            return equityRepository.GetAllEquities();
        }

        public Equity GetEquityById(int id)
        {
            return equityRepository.GetEquityById(id);
        }

        public Equity GetEquityByName(string name)
        {
            return equityRepository.GetEquityByName(name);
        }
    }
}
