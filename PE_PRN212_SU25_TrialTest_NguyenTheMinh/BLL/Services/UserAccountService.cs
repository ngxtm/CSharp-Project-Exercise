using DAL.Entities;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class UserAccountService
    {
        private readonly UserAccountRepositories _userAccountRepository;
        public UserAccountService()
        {
            this._userAccountRepository = new UserAccountRepositories();
        }

        public UserAccount? GetAccountByEmail(string email, string password)
        {
            return _userAccountRepository.GetAccountByEmail(email, password);
        }
    }
}
