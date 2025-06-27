using BLL.Services;
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

namespace CompanyMangement_SE196686
{
    /// <summary>
    /// Interaction logic for ResearchProjectManagement.xaml
    /// </summary>
    public partial class ResearchProjectManagement : Window
    {
        private readonly ResearchProjectService _researchProjectService;
        public ResearchProjectManagement()
        {
            InitializeComponent();
            this._researchProjectService = new ResearchProjectService();
            LoadResearchProject();
        }

        private void LoadResearchProject()
        {
            dgResearch.ItemsSource = _researchProjectService.GetResearchProjects();
        }

        private void dgResearch_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
