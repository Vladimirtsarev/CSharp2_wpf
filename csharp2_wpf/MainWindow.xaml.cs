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
using System.IO;
using System.Xml;
using System.Xml.Linq;

namespace csharp2_wpf
{
    public class Employee
    {
        private int ID { get; set; }
        private string Name { get; set; }
        private int Salary { get; set; }
        private byte Age { get; set; }
        private int DepID { get; set; }

        public Employee(int Id, string empName, int empSalary, byte empAge, int depId)
        {
            this.ID = Id;
            this.Name = empName;
            this.Age = empAge;
            this.Salary = empSalary;
            this.DepID = depId;
        }
        public Employee(int Id, string empName, int empSalary, byte empAge)
        {
            this.ID = Id;
            this.Name = empName;
            this.Age = empAge;
            this.Salary = empSalary;
           
        }

        public override string ToString()
        {
            return this.Name;
        }
        public string GetName()
        {
            return Name;
        }
        public int GetSalary()
        {
            return Salary;
        }
        public byte GetAge()
        {
            return Age;
        }
        public int GetId()
        {
            return ID;
        }
        public int GetDepId()
        {
            return DepID;
        }
        public void SetDep(int depId) { DepID = depId; }

    }
    public class Department
    {
        private int ID { get; set; }
        private string Name { get; set; }
        public List<int> empList = new List<int>();

        public Department(string depName, int depId)
        {
            this.Name = depName;
            this.ID = depId;
        }

        public void AddEmployee(int empId)
        {
            empList.Add(empId);
        }
        public void AddEmployee(Employee emp)
        {
            empList.Add(emp.GetId());
        }

        public override string ToString()
        {
            return this.Name;
        }
    }
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static List<Department> _dep = new List<Department>();
        public static List<Employee> _emp = new List<Employee>();

        public MainWindow()
        {
            
            InitializeComponent();
            
            ReadFromFile();
            
        }

        private void ReadFromFile()
        {
            string[] dStr = File.ReadAllText("Departments.txt").Split('\n');
            int i = 0;
            foreach (string str in dStr)
            {               
                _dep.Add(new Department(str.Replace("\r", ""), ++i));
            }



            string[] eStr = File.ReadAllText("Employees.txt").Split('\n');
            int j = 0;
            foreach (string str in eStr)
            {
                if (!str.Contains("ФИО;") && !string.IsNullOrEmpty(str) && str != "\n")
                {
                    string[] str2 = new string[3];
                    str2 = str.Replace("\r", "").Split(';');
                    _emp.Add(new Employee(++j, str2[0], Convert.ToInt32(str2[1]), Convert.ToByte(str2[2]), Convert.ToInt32(str2[3])));
                }
            }

            //EmpToDep(1, 1);
            //EmpToDep(2, 1);
            //EmpToDep(3, 1);
            //EmpToDep(4, 2);
            //EmpToDep(5, 2);
            //EmpToDep(6, 2);
            //EmpToDep(7, 3);
            //EmpToDep(8, 3);
            //EmpToDep(9, 3);


            comboBox1.ItemsSource = _dep;
            comboBox1.SelectedIndex = 0;


           
        }

        private void ReadFromFile2()
        {
            _dep.Add(new Department("Отдел 1",1));
            _dep.Add(new Department("Отдел 2",2));
            _dep.Add(new Department("Отдел 3",3));

            _emp.Add(new Employee(1, "Сотрудник 1 1", 1100, 22));
            _emp.Add(new Employee(2, "Сотрудник 1 2", 1200, 23));
            _emp.Add(new Employee(3, "Сотрудник 1 3", 1300, 24));
            _emp.Add(new Employee(4, "Сотрудник 2 1", 2100, 25));
            _emp.Add(new Employee(5, "Сотрудник 2 2", 2200, 26));
            _emp.Add(new Employee(6, "Сотрудник 2 3", 2300, 27));
            _emp.Add(new Employee(7, "Сотрудник 3 1", 3100, 28));
            _emp.Add(new Employee(8, "Сотрудник 3 2", 3200, 29));
            _emp.Add(new Employee(9, "Сотрудник 3 3", 3300, 30));

            EmpToDep(1, 1);
            EmpToDep(2, 1);
            EmpToDep(3, 1);
            EmpToDep(4, 2);
            EmpToDep(5, 2);
            EmpToDep(6, 2);
            EmpToDep(7, 3);
            EmpToDep(8, 3);
            EmpToDep(9, 3);
            

            comboBox1.ItemsSource = _dep;
            comboBox1.SelectedIndex = 0;                      
            
        }

        /// <summary>
        /// Связывает отдел и сотрудника по ID
        /// </summary>
        /// <param name="empId">ID сотрудника</param>
        /// <param name="depId">ID отдела</param>
        private void EmpToDep(int empId, int depId)
        {
            _dep[depId-1].AddEmployee(empId);
            _emp[empId-1].SetDep(depId);
        }

        private void comboBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            List<Employee> eL = new List<Employee>();
            foreach (var item in _emp)// dep[comboBox1.SelectedIndex].empList)
            {
                if (item.GetDepId()== (comboBox1.SelectedIndex+1))
                eL.Add(item);
            }
            listView1.ItemsSource = eL;
        }

        private void listView1_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Employee e1=null;
            foreach (var empl in _emp)
            {
                if (empl.GetName() == listView1.SelectedItem.ToString())
                {
                    e1 = empl;
                }
            }
           
            
            EmployeeEditWindow eew = new EmployeeEditWindow(e1);
            eew.ShowDialog();

            
        }

        private string EmplToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("ФИО;Оклад;Возраст;Отдел");
            foreach (var item in _emp)
            {
                sb.Append("\n"+ item.GetName() + ";" + item.GetSalary() + ";" + item.GetAge() + ";" + item.GetDepId());
            }
            return sb.ToString();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            File.WriteAllText("Employees.txt", EmplToString());
        }
    }
}
