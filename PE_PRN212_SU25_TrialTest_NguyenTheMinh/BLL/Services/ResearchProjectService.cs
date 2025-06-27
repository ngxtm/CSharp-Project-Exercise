using DAL.Entities;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class ResearchProjectService
    {
        private readonly ResearchProjectRepository _researchProjectRepository;

        public ResearchProjectService()
        {
            this._researchProjectRepository = new ResearchProjectRepository();
        }
        public List<ResearchProject> GetResearchProjects()
        {
            return _researchProjectRepository.GetResearchProject();
        }
    }
}
