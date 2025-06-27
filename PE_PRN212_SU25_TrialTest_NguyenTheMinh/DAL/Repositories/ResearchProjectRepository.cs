using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class ResearchProjectRepository
    {
        private readonly Su25researchDbContext _db;
        public ResearchProjectRepository()
        {
            this._db = new Su25researchDbContext();
        }

        public void DeleteResearchProject(int id)
        {
            var project = _db.ResearchProjects.Where(x => x.ProjectId == id);
            if (project.Any())
            {
                _db.ResearchProjects.Remove(project.First());
                _db.SaveChanges();
            }
        }

        public List<ResearchProject> GetResearchProject()
        {
            var researchList = _db.ResearchProjects.Include(x => x.LeadResearcher).ToList();
            return researchList;
        }

        public IEnumerable SearchProject(string keyword)
        {
            return _db.ResearchProjects
                .Where(p => p.ProjectTitle.Contains(keyword) || p.ResearchField.Contains(keyword))
                .ToList();
        }

        public int GetNextResearchProjectId()
        {
            if (!_db.ResearchProjects.Any()) return 1;
            return _db.ResearchProjects.Max(p => p.ProjectId) + 1;
        }

        public void AddResearchProject(ResearchProject project)
        {
            _db.ResearchProjects.Add(project);
            _db.SaveChanges();
        }
    }
}
