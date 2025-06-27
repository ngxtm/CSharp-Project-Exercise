using BLL.Services;
using DAL.Entities;
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
    /// Interaction logic for ResearchProjectManagement.xaml
    /// </summary>
    public partial class ResearchProjectManagement : Window
    {
        private readonly ResearchProjectService _researchProjectService;
        private readonly ResearcherService _researcherService;
        private readonly UserAccount _currentUser;
        public ResearchProjectManagement(UserAccount user)
        {
            InitializeComponent();
            this._currentUser = user;
            this._researchProjectService = new ResearchProjectService();
            this._researcherService = new ResearcherService();
            LoadResearchProject();
            LoadResearcher();
        }

        private bool IsAdmin => _currentUser.Role == (int)UserRole.Administrator;
        private bool IsAdminOrManager => IsAdmin || _currentUser.Role == (int)UserRole.Manager;

        private bool HasReadAccess => IsAdminOrManager || _currentUser.Role == (int)UserRole.Staff;
        private void LoadResearchProject()
        {
            dgResearch.ItemsSource = _researchProjectService.GetResearchProjects();
        }

        private void LoadResearcher()
        {
            cbResearcher.ItemsSource = _researcherService.GetResearchers();
        }

        private void dgResearch_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            if (!HasReadAccess)
            {
                MessageBox.Show("You have no permission to access this function!");
            }
            string keyword = txtSearch.Text ?? string.Empty;
            dgResearch.ItemsSource = _researchProjectService.SearchProject(keyword);
        }

        private void btnView_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
