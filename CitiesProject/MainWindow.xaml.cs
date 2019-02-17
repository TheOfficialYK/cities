/*
 * Program : CitiesProject.exe
 * Coder: Abhinav Sahdev, Youssef Khochaba
 * Date: Feb 17, 2019
 * Purpose: To compare Canadian cities and province information
 */

using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;

namespace CitiesProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<CityInfo> Cities { get; set; }
        Statistics stats;
        Dictionary<string, CityInfo> CityCatalog;
        public MainWindow()
        {
            InitializeComponent();
            cbSaveAs.ItemsSource = new List<string> { "JSON", "CSV", "XML" };
            cbSaveAs.SelectedIndex = 0;
        }
        private void GetFile()
        {
            string FileName = null;
            string FileType = null;
            try
            {
                //  Get file path
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "JSON files (*.json)|*.json|CSV files (*.csv)|*.csv|XML files (*.xml)|*.xml|All files (*.*)|*.*";
                if (openFileDialog.ShowDialog() == true)
                {
                    FileName = openFileDialog.FileName;
                    lblFileName.Content = System.IO.Path.GetFileName(FileName);
                    FileType = FileName.Split('.')[1];
                }

                if(openFileDialog.FileName != "")
                {
                    //  Call deserialize method from statistics class 
                    DeserializeFile(FileName, FileType);

                    //  Set ListBox to City Names
                    var tmplist = Cities.Select(c => c.CityName).ToList();
                    tmplist.Sort();
                    lbCityList.ItemsSource = tmplist;

                    //  Set total number of cities
                    lblNumberOfCities.Content = "Number of Cities: " + tmplist.Count;
                }
                

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

        private void DeserializeFile(string fname, string ftype)
        {
            stats = new Statistics(fname, ftype);
            stats.DeserializeFileToDictionary();
            //  copy catalog to global var
            CityCatalog = stats.CityCatalogue;
            //  Add cities to List
            Cities = CityCatalog.Select(c => c.Value).ToList();
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {

            if (cbSaveAs.SelectedIndex != -1)
            {
                if (stats != null)
                {
                    string fileType = cbSaveAs.SelectedItem as string;
                    SaveFileDialog saveFileDialog = new SaveFileDialog();
                    if (fileType == "JSON")
                        saveFileDialog.Filter = "JSON|*.json";
                    else if (fileType == "CSV")
                        saveFileDialog.Filter = "CSV|*.csv";
                    else if (fileType == "XML")
                        saveFileDialog.Filter = "  XML | *.xml";

                    saveFileDialog.Title = "Save File as " + fileType;
                    saveFileDialog.ShowDialog();

                    // If the file name is not an empty string open it for saving.  

                    if (saveFileDialog.FileName != "")
                    {
                        // Saves the file
                        string data = stats.Serialize(fileType);
                        if (data != null)
                        {
                            using (StreamWriter writer = new StreamWriter(saveFileDialog.FileName, false, Encoding.UTF8)) //using unicode is important to get the french chars right
                            {
                                writer.Write(data);
                            }
                            MessageBox.Show("File written!", "Operation complete", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        else return;

                    }
                }
                else
                {
                    MessageBox.Show("Nothing to write", "Warning", MessageBoxButton.OK, MessageBoxImage.Information);
                }

            }
            else
            {
                MessageBox.Show("Please select a save format!", "Warning", MessageBoxButton.OK, MessageBoxImage.Information);
            }


        }

        private void BtnCityOne_Click(object sender, RoutedEventArgs e)
        {
            if (lbCityList.SelectedIndex != -1)
            {
                CityInfo city = CityCatalog.Where(c => c.Key == (lbCityList.SelectedItem as string)).FirstOrDefault().Value;
                var prov = city.GetProvince();
                var pop = city.GetPopulation();
                var loc = city.GetLocation();
                tboxCityOne.Text = "City Name: " + city.CityName +
                                   "\nLocation:  " + loc +
                                   "\nPopulation: " + pop +
                                   "\nProvince:  " + prov;

                //  Get province info from stats class
                int provincePop = stats.CalculateProvincePopulation(prov);
                tboxProvinceOne.Text = "Province Name: " + prov +        
                                        "\nNumber of Cities: " + CityCatalog.Where(c => c.Value.Province == prov).ToList().Count +
                                        "\nPopulation " + provincePop; 
                
            }
            else
            {
                MessageBox.Show("Please select a City from the list!", "Warning", MessageBoxButton.OK, MessageBoxImage.Information);
            }

        }

        private void BtnCityTwo_Click(object sender, RoutedEventArgs e)
        {
            if (lbCityList.SelectedIndex != -1)
            {
                CityInfo city = CityCatalog.Where(c => c.Key == (lbCityList.SelectedItem as string)).FirstOrDefault().Value;
                var prov = city.GetProvince();
                var pop = city.GetPopulation();
                var loc = city.GetLocation();
                tboxCityTwo.Text = "City Name: " + city.CityName +
                                   "\nLocation:  " + loc +
                                   "\nPopulation: " + pop +
                                   "\nProvince:  " + prov;
                //  Get province info from stats class
                int provincePop = stats.CalculateProvincePopulation(prov);
                tboxProvinceTwo.Text = "Province Name: " + prov +
                                        "\nNumber of Cities: " + CityCatalog.Where(c => c.Value.Province == prov).ToList().Count +
                                        "\nPopulation " + provincePop;
            }
            else
            {
                MessageBox.Show("Please select a City from the list!", "Warning", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void TboxSearch_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            try
            {
                if (CityCatalog.Count > 0)
                {
                    if (tboxSearch.Text.Length != 0)
                    {
                        var tmplist = Cities.Where(c => c.CityName.ToLower()
                        .Contains(tboxSearch.Text.ToLower())).Select(c => c.CityName).ToList();
                        tmplist.Sort((a, b) => a.CompareTo(b));
                        lbCityList.ItemsSource = tmplist;
                    }
                    else
                    {
                        var tmplist = Cities.Select(c => c.CityName).ToList();
                        tmplist.Sort((a, b) => a.CompareTo(b));
                        lbCityList.ItemsSource = tmplist;
                    }
                }
            }
            catch(Exception)
            {
                return;
            }
                     
           
        }
    }
}
