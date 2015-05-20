using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

namespace FileSearcher
{
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Drop(object sender, DragEventArgs e)
        {
            var paths = ((System.Array)e.Data.GetData(DataFormats.FileDrop));
            string path = paths.GetValue(0).ToString();
            tbPath.Text = path;
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string path = tbPath.Text;
            List<string> result = new List<string>();
            if (Directory.Exists(path))
            {
                DirectoryInfo folder = new DirectoryInfo(path);
                var data = folder.GetFileSystemInfos();
                for (int i = 0; i < data.Length; i++)
                {
                    if (data[i].Name.IndexOf(tbTarget.Text, StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        result.Add(data[i].FullName);
                    }
                    if (data[i].Extension == String.Empty)
                    {
                        SearchFolder(data[i].FullName, result);
                    }
                }
            }
            listResult.Items.Clear();
            for (int i = 0; i < result.Count; i++)
            {
                listResult.Items.Add(result[i]);
            }
        }
        void SearchFolder(string path, List<string> result)
        {
            if (Directory.Exists(path))
            {
                DirectoryInfo folder = new DirectoryInfo(path);
                var data = folder.GetFileSystemInfos();
                for (int i = 0; i < data.Length; i++)
                {
                    if (data[i].Name.IndexOf(tbTarget.Text, StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        result.Add(data[i].FullName);
                    }
                    if (data[i].Extension == String.Empty)
                    {
                        SearchFolder(data[i].FullName, result);
                    }
                }
            }
        }

        private void listResult_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            string path  = listResult.SelectedItem.ToString();
            FileInfo file = new FileInfo(path);
            System.Diagnostics.Process.Start(file.DirectoryName);
        }

    }
}
