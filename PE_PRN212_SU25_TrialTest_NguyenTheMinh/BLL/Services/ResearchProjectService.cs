using DAL.Entities;
using DAL.Repositories;
using System;
using System.Collections;
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

        public void DeleteResearchProject(int id)
        {
            _researchProjectRepository.DeleteResearchProject(id);
        }

        public List<ResearchProject> GetResearchProjects()
        {
            return _researchProjectRepository.GetResearchProject();
        }

        public IEnumerable SearchProject(string keyword)
        {
            return _researchProjectRepository.SearchProject(keyword);
        }

        public int GetNextReSearchProjectId()
        {
            return _researchProjectRepository.GetNextResearchProjectId();
        }

        public void AddResearchProject(ResearchProject project)
        {
            _researchProjectRepository.AddResearchProject(project);
        }

        public void UpdateResearchProject(ResearchProject project)
        {
            _researchProjectRepository.UpdateResearchProject(project);
        }
    }
}
