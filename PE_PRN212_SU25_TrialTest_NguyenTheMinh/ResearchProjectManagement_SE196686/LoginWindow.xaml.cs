using BLL.Services;
using CompanyMangement_SE196686;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ResearchProjectManagement_SE196686
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private UserAccountService _userAccountService;
        public LoginWindow()
        {
            InitializeComponent();
            this._userAccountService = new UserAccountService();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string email = txtEmail.Text;
            string password = txtPassword.Text;
            var account = _userAccountService.GetAccountByEmail(email, password);
            if (account != null)
            {
                if (account.Role != (int)DAL.Entities.UserRole.Member)
                {
                    ResearchProjectManagement researchWindow = new ResearchProjectManagement(account);
                    researchWindow.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("You have no permission to access this function!");
                }
            }
            else
            {
                MessageBox.Show("Invalid Email or Password!");
            }
        }
    }
}
