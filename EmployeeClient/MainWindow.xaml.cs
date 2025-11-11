using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using EmployeeClient.Models;
using EmployeeClient.Services;
using System.Windows;

namespace EmployeeClient
{
    public partial class MainWindow : Window
    {
        private readonly EmployeeApiService _service = new();
        private Employee? _selected;

        public MainWindow()
        {
            InitializeComponent();
            _ = LoadData();
        }

        private async Task LoadData()
        {
            EmployeeGrid.ItemsSource = await _service.GetAllAsync();
        }

        private async void Add_Click(object sender, RoutedEventArgs e)
        {
            var emp = new Employee
            {
                Name = NameBox.Text,
                Department = DeptBox.Text,
                HireDate = HireDatePicker.SelectedDate ?? DateTime.Now
            };
            await _service.AddAsync(emp);
            await LoadData();
        }

        private async void Update_Click(object sender, RoutedEventArgs e)
        {
            if (_selected == null) return;
            _selected.Name = NameBox.Text;
            _selected.Department = DeptBox.Text;
            _selected.HireDate = HireDatePicker.SelectedDate ?? _selected.HireDate;
            await _service.UpdateAsync(_selected);
            await LoadData();
        }

        private async void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (_selected == null) return;
            await _service.DeleteAsync(_selected.Id);
            await LoadData();
        }

        private async void Reload_Click(object sender, RoutedEventArgs e)
        {
            await LoadData();
        }

        private void EmployeeGrid_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            _selected = EmployeeGrid.SelectedItem as Employee;
            if (_selected != null)
            {
                NameBox.Text = _selected.Name;
                DeptBox.Text = _selected.Department;
                HireDatePicker.SelectedDate = _selected.HireDate;
            }
        }
    }
}
