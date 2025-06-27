using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
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

        public List<ResearchProject> GetResearchProject()
        {
            var researchList = _db.ResearchProjects.Include(x => x.LeadResearcher).ToList();
            return researchList;
        }
    }
}
