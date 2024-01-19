using JsonToDataTableTester.ViewModels;
using System.IO;
using System.Windows;

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

        private void Execute_Button_Click(object sender, RoutedEventArgs e)
        {
            int index = SelectTableBox.SelectedIndex;
            vm.Execute(index);
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
                        vm.GetDataFromDrop(filePath);
                    }
                }
            }
        }
    }
}