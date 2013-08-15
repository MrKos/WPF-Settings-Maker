using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
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
        private List<ConfigElement> _path = new List<ConfigElement>();
        private string SelectedDepartment = "0";
        
        public MainWindow()
        {
            InitializeComponent();
            for (int i = 0; i < 5; i++)
            {
                _path.Add(new ConfigElement(){Key = i.ToString(),Value = ""});
            }
            LoadConfig();
            DepartmentSelect.SelectedIndex = 0;
            FillPathTextBox(SelectedDepartment);
            
        }

        private void LoadConfig()
        {
            if (File.Exists("ERZDCom.config"))
            {
                try
                {

                    var loadConfig = SimpleConfig.LoadConfig("ERZDCom.config");
                    if (loadConfig == null) return;
                    foreach (ConfigElement config in loadConfig.ConfigElements)
                    {
                        _path.Find(p => p.Key.Equals(config.Key)).Value = config.Value;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при откритии файла конфигурации: " + ex);
                }
            }
        }

        private void FillPathTextBox(string departNumber)
        {
            PathTextBox.Text = _path.Find(p=>p.Key.Equals(SelectedDepartment)).Value ;
        }

        private void Browse_Click(object sender, RoutedEventArgs e)
        {
            var folderDlg = new System.Windows.Forms.FolderBrowserDialog();
            folderDlg.ShowDialog();
            if (!string.IsNullOrEmpty(folderDlg.SelectedPath))
            {
                _path.Find(p=>p.Key.Equals(SelectedDepartment)).Value = folderDlg.SelectedPath;
            }
            PathTextBox.Text = _path.Find(p=>p.Key.Equals(SelectedDepartment)).Value;
        }

        private void ErrorResult()
        {
            MessageBox.Show(@"Файл ""ERZDCom.config"" не найден!", @"Ошибка чтения файла конфигурации", MessageBoxButton.OK, MessageBoxImage.Error);
            Application.Current.Shutdown();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (_path != null)
            {
                try
                {
                    SimpleConfig res = new SimpleConfig();
                    foreach (ConfigElement configElement in _path)
                    {
                        res.ConfigElements.Add(configElement);
                    }

                    SimpleConfig.SaveConfig(res, "ERZDCom.config");
                    MessageBox.Show("Конфигурация сохранена!");
                    //Application.Current.Shutdown();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при сохранении в файл конфигурации: " + ex);
                }
            }
        }

        private void DepartmentSelect_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            SelectedDepartment = DepartmentSelect.SelectedIndex.ToString(CultureInfo.InvariantCulture);
            FillPathTextBox(SelectedDepartment);
        }
    }
}
