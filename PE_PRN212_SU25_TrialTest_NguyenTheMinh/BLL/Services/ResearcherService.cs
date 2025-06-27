using DAL.Entities;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class ResearcherService
    {
        private readonly ResearcherRepository _researcherRepository;
        public ResearcherService()
        {
            this._researcherRepository = new ResearcherRepository();
        }

        public List<Researcher> GetResearchers()
        {
            return _researcherRepository.GetReseachers();
        }
    }
}
