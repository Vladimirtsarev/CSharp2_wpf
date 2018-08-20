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
    public class Employee { }
    public class Department { }
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<Department> _dep = new List<Department>();

        public MainWindow()
        {
            InitializeComponent();
            ReadFromFile();
        }

        private void ReadFromFile()
        {
            
           string[] s= File.ReadAllText("Departments.txt").Split('\n');
            foreach  (string str in s)
            {
                comboBox1.Items.Add(str.Replace("\r",""));
            }
        }
        
    }
}
