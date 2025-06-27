using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class UserAccountRepositories
    {
        private readonly Su25researchDbContext _db;
        public UserAccountRepositories()
        {
            this._db = new Su25researchDbContext();
        }

        public UserAccount? GetAccountByEmail(string email, string password)
        {
            var account = _db.UserAccounts.FirstOrDefault(x => x.Email == email && x.Password == password);
            return account;
        }
    }
}
