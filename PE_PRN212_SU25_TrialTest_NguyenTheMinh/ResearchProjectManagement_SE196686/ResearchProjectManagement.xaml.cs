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
            if (dgResearch.SelectedItem is ResearchProject project)
            {
                AutoFillInUpdateBox(project);
            }
            else
            {
                txtTitle.Clear();
                txtField.Clear();
                dpStart.SelectedDate = null;
                dpEnd.SelectedDate = null;
                cbResearcher.SelectedItem = null;
                txtBudget.Clear();
            }
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            if (!HasReadAccess)
            {
                MessageBox.Show("You have no permission to access this function!");
                return;
            }
            string keyword = txtSearch.Text ?? string.Empty;
            dgResearch.ItemsSource = _researchProjectService.SearchProject(keyword);
        }

        private void btnView_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (!IsAdminOrManager)
            {
                MessageBox.Show("You have no permission to access this function!");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtTitle.Text) ||
                string.IsNullOrWhiteSpace(txtField.Text) ||
                !dpStart.SelectedDate.HasValue ||
                !dpEnd.SelectedDate.HasValue ||
                cbResearcher.SelectedItem == null ||
                string.IsNullOrWhiteSpace(txtBudget.Text))
            {
                MessageBox.Show("All fields are required.");
                return;
            }
            if (dpStart.SelectedDate.Value >= dpEnd.SelectedDate.Value)
            {
                MessageBox.Show("StartDate must be earlier than EndDate.");
                return;
            }
            string title = txtTitle.Text.Trim();
            if (title.Length <= 5 || title.Length >= 100 ||
                !System.Text.RegularExpressions.Regex.IsMatch(title, @"^([A-Z1-9][A-Za-z0-9]*)( [A-Z1-9][A-Za-z0-9]*)*$"))
            {
                MessageBox.Show("ProjectTitle must be between 5 and 100 characters. Each word in the ProjectTitle must start with a capital letter or a digit (1-9). ProjectTitle cannot contain special characters such as $,%,^, @.");
                return;
            }
            var project = new ResearchProject
            {
                ProjectId = _researchProjectService.GetNextReSearchProjectId(),
                ProjectTitle = title,
                ResearchField = txtField.Text.Trim(),
                StartDate = DateOnly.FromDateTime(dpStart.SelectedDate.Value),
                EndDate = DateOnly.FromDateTime(dpEnd.SelectedDate.Value),
                LeadResearcherId = (int)cbResearcher.SelectedValue,
                Budget = decimal.TryParse(txtBudget.Text, out decimal budget) ? budget : 0
            };
            _researchProjectService.AddResearchProject(project);
            LoadResearchProject();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (!IsAdminOrManager)
            {
                MessageBox.Show("You have no permission to access this function!");
                return;
            }
            if (string.IsNullOrWhiteSpace(txtTitle.Text) ||
                string.IsNullOrWhiteSpace(txtField.Text) ||
                !dpStart.SelectedDate.HasValue ||
                !dpEnd.SelectedDate.HasValue ||
                cbResearcher.SelectedItem == null ||
                string.IsNullOrWhiteSpace(txtBudget.Text))
            {
                MessageBox.Show("All fields are required.");
                return;
            }
            if (dpStart.SelectedDate.Value >= dpEnd.SelectedDate.Value)
            {
                MessageBox.Show("StartDate must be earlier than EndDate.");
                return;
            }
            string title = txtTitle.Text.Trim();
            if (title.Length <= 5 || title.Length >= 100 ||
                !System.Text.RegularExpressions.Regex.IsMatch(title, @"^([A-Z1-9][A-Za-z0-9]*)( [A-Z1-9][A-Za-z0-9]*)*$"))
            {
                MessageBox.Show("ProjectTitle must be between 5 and 100 characters. Each word in the ProjectTitle must start with a capital letter or a digit (1-9). ProjectTitle cannot contain special characters such as $,%,^, @.");
                return;
            }
            if (dgResearch.SelectedItem is ResearchProject project)
            {
                try
                {
                    project.ProjectTitle = txtTitle.Text;
                    project.ResearchField = txtField.Text;
                    project.StartDate = DateOnly.FromDateTime(dpStart.SelectedDate ?? DateTime.Today);
                    project.EndDate = DateOnly.FromDateTime(dpEnd.SelectedDate ?? DateTime.Today);
                    project.LeadResearcherId = (int?)cbResearcher.SelectedValue;
                    project.Budget = decimal.Parse(txtBudget.Text);
                    _researchProjectService.UpdateResearchProject(project);
                    LoadResearchProject();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred while updating the project: {ex.Message}");
                    return;
                }
            }
        }

        private void AutoFillInUpdateBox(ResearchProject project)
        {
            txtTitle.Text = project.ProjectTitle;
            txtField.Text = project.ResearchField;
            dpStart.SelectedDate = project.StartDate.ToDateTime(TimeOnly.MinValue);
            dpEnd.SelectedDate = project.EndDate.ToDateTime(TimeOnly.MinValue);
            cbResearcher.SelectedValue = project.LeadResearcherId;
            txtBudget.Text = project.Budget.ToString();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (!IsAdmin)
            {
                MessageBox.Show("You have no permission to access this function!");
                return;
            }
            var result = MessageBox.Show("Are you sure you want to delete this project?", "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                if (dgResearch.SelectedItem is ResearchProject project)
                {
                    int id = project.ProjectId;
                    _researchProjectService.DeleteResearchProject(id);
                    LoadResearchProject();
                }
            }
            
        }
    }
}
