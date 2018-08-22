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

namespace csharp2_wpf
{
    /// <summary>
    /// Логика взаимодействия для EmployeeEditWindow.xaml
    /// </summary>
    public partial class EmployeeEditWindow : Window
    {
        private Employee employee;
        private int id = 0;

        public EmployeeEditWindow(Employee e)
        {
            InitializeComponent();

            employee = e;
            id = e.GetId();
            DepartmentcomboBox.ItemsSource = MainWindow._dep;

            FIOtxtBox.Text = e.GetName();
            SalarytxtBox.Text = e.GetSalary().ToString();
            AgetxtBox.Text = e.GetAge().ToString();
            DepartmentcomboBox.SelectedIndex = e.GetDepId()-1;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            employee = new Employee(id, FIOtxtBox.Text, Convert.ToInt32(SalarytxtBox.Text), Convert.ToByte(AgetxtBox.Text), DepartmentcomboBox.SelectedIndex+1);
            for (int i=0; i<MainWindow._emp.Count;i++)
            {
                if (MainWindow._emp[i].GetId() == id)
                    MainWindow._emp[i] = employee;


            }
            this.Close();
            
        }
    }
}
