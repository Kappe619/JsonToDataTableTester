using JsonToDataTableTester.ViewModels;
using Microsoft.Win32;
using System.Data;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace JsonToDataTableTester
{
    public partial class MainWindow : Window
    {
        MainViewModel vm;
        public MainWindow()
        {
            vm = new MainViewModel();
            DataContext = vm;
            InitializeComponent();
            

        }

        private void Select_Button_Click(object sender, RoutedEventArgs e)
        {
            vm.LoadData();
        }


        private void Execute_Button_Click(object sender, RoutedEventArgs e) {
        
            int index = SelectTableBox.SelectedIndex;
            vm.Execute(index);
//            vm.CmbChanged(index);
        
        
        }

        private void ComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
          //if (sender is ComboBox box)
          //  {
          //      vm.CmbChanged(box.SelectedIndex);
          //  }  
        }

        private void TextBox_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (files != null && files.Length > 0)
                {
                    string filePath = files[0];

                    if (Path.GetExtension(filePath).Equals(".json", StringComparison.OrdinalIgnoreCase))
                    {                        
                        vm.GetDataFromDragEnter(filePath);
                    }
                }
            }
        }
    }
}