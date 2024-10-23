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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.Entity;
using System.Collections.ObjectModel;
using System.Runtime.Remoting.Contexts;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DatabaseContext _dbContext = new DatabaseContext();
        private List<Employee> _employees;

        public MainWindow()
        {
            InitializeComponent();
            
            _dbContext = new DatabaseContext();
        }

        private void LoadData_Click(object sender, RoutedEventArgs e)
        {
            LoadEmployees();
        }

        private void LoadEmployees()
        {
            _employees = _dbContext.GetEmployees();
            dataGrid.ItemsSource = _employees;
        }

        
            

        private void SaveData_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SaveChanges_Click(object sender, RoutedEventArgs e)
        {
            if (_employees != null)
            {
                foreach (var employee in _employees)
                {
                    _dbContext.SaveEmployee(employee);
                }
                MessageBox.Show("Table saved successfully.");
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            {
                if (dataGrid.SelectedItem is Employee selectedEmployee)
                {
                    _dbContext.DeleteEmployee(selectedEmployee);
                    LoadEmployees();
                }
                else
                {
                    MessageBox.Show("Please select an employee to delete.");
                }
            }


        }
    }
}
