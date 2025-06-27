using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class ResearcherRepository
    {
        private readonly Su25researchDbContext _db;
        public ResearcherRepository()
        {
            this._db = new Su25researchDbContext();
        }

        public List<Researcher> GetReseachers()
        {
            return _db.Researchers.ToList();
        }
    }
}
