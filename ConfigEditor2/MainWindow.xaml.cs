using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows;
using System.Xml.Linq;
using ConfigEditor2.Classes;
using Application = System.Windows.Application;
using MessageBox = System.Windows.MessageBox;

namespace ConfigEditor2
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ConfigElement _path = new ConfigElement()
        {
            Key = "",
            Value = Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments)
        };
        private string SelectedDepartment = "0";
        
        public MainWindow()
        {
            InitializeComponent();
            GetSelectedClientPath(SelectedDepartment);
        }

        private void GetSelectedClientPath(string departNumber)
        {
            if (File.Exists("ERZDCom.config"))
            {
                var loadConfig = SimpleConfig.LoadConfig("ERZDCom.config");
                var findResult = loadConfig.ConfigElements.Find(element => element.Key.Equals(departNumber));
                if (findResult != null)
                {
                    _path.Value = findResult.Value;
                    PathTextBox.Text = _path.Value;
                }
                else
                {
                    PathTextBox.Text = string.Empty;
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var folderDlg = new System.Windows.Forms.FolderBrowserDialog();
            folderDlg.ShowDialog();
            if (!string.IsNullOrEmpty(folderDlg.SelectedPath))
            {
                _path.Value = folderDlg.SelectedPath;
            }
            PathTextBox.Text = _path.Value;
        }

        private void ErrorResult()
        {
            MessageBox.Show(@"Файл ""ERZDCom.config"" не найден!", @"Ошибка чтения файла конфигурации", MessageBoxButton.OK, MessageBoxImage.Error);
            Application.Current.Shutdown();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (_path != null)
            {
                SimpleConfig res = new SimpleConfig();
                _path.Key = SelectedDepartment;
                res.ConfigElements.Add(_path);
                SimpleConfig.SaveConfig(res, "ERZDCom.config");
                MessageBox.Show("Конфигурация сохранена!");
                //Application.Current.Shutdown();
            }
        }

        private void DepartmentSelect_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            SelectedDepartment = DepartmentSelect.SelectedIndex.ToString(CultureInfo.InvariantCulture);
            GetSelectedClientPath(SelectedDepartment);
        }

    }
}
