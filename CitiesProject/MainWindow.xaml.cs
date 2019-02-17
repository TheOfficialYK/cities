/*
 * Program : CitiesProject.exe
 * Coder: Abhinav Sahdev, Youssef Khochaba
 * Date: Feb 17, 2019
 * Purpose: To compare Canadian cities and province information
 */
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace CitiesProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<CityInfo> Cities { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            //  Get the data file from disk
            GetFile();
            lbCityList.ItemsSource = Cities.Select(c => c.CityName).ToList();

           
        }
        private void GetFile()
        {
            string FileName = null;
            string FileType = null;
            try
            {
                //  Get file path
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "CSV files (*.csv)|*.csv|JSON files (*.json)|*.json|XML files (*.xml)|*.xml|All files (*.*)|*.*";
                if (openFileDialog.ShowDialog() == true)
                {
                    FileName = openFileDialog.FileName;
                    lblFileName.Content = System.IO.Path.GetFileName(FileName);
                    FileType = FileName.Split('.')[1];
                }

                //  Call deserialize method from statistics class on async thread
                DeserializeFile(FileName, FileType);


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

        }

        private void OnBrowse(object sender, RoutedEventArgs e)
        {
            GetFile();
        }

        private async void DeserializeFile(string fname,string ftype)
        {
            Statistics stats = new Statistics(fname, ftype);
            stats.DeserializeFileToDictionary();           
            //  Add cities to List
            Cities = stats.CityCatalogue.Select(c => c.Value).ToList();
        }
    }
}
