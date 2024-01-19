using JsonToDataTableTester.Models;
using Microsoft.Win32;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Windows;

namespace JsonToDataTableTester.ViewModels
{
    class MainViewModel : INotifyPropertyChanged
    {
        private int _selectedIndex;
        private List<string> _tableNames;
        private DataTable _selectedTable;
        private DataTable _dataTable;
        private string _jsonStringInTextBox;
        private List<DataTable> tables;
        private DataTable MainTable;
        private ObservableCollection<DataModel> _dataCollection;

        public int SelectedIndex
        {
            get => _selectedIndex;
            set
            {
                _selectedIndex = value;
                OnPropertyChanged(nameof(SelectedIndex));
            }
        }
        public List<string> TableNames
        {
            get => _tableNames;
            set
            {
                _tableNames = value;
                OnPropertyChanged(nameof(TableNames));
            }
        }
        public DataTable SelectedTable
        {
            get => _selectedTable;
            set
            {
                _selectedTable = value;
                OnPropertyChanged(nameof(SelectedTable));
            }
        }
        public DataTable DataTable
        {
            get => _dataTable;
            set
            {
                _dataTable = value;
                OnPropertyChanged(nameof(DataTable));
            }
        }
        public string JsonStringInTextBox
        {
            get => _jsonStringInTextBox;
            set
            {
                _jsonStringInTextBox = value;
                OnPropertyChanged(nameof(JsonStringInTextBox));
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ObservableCollection<DataModel> DataCollection
        {
            get => _dataCollection;
            set
            {
                if (_dataCollection != value)
                {
                    _dataCollection = value;
                    OnPropertyChanged(nameof(DataCollection));
                }
            }
        }

        public MainViewModel()
        {
            DataCollection = new();
            TableNames = new List<string>();
            tables = new();
        }

        public void LoadData()
        {
            JsonStringInTextBox = LoadJsonAsString();
            ProcessJsonString(JsonStringInTextBox);
        }

        public void GetDataFromDrop(string filePath)
        {
            if (Path.GetExtension(filePath).Equals(".json", StringComparison.OrdinalIgnoreCase))
            {
                JsonStringInTextBox = File.ReadAllText(filePath);
                ProcessJsonString(JsonStringInTextBox);
            }
        }

        //Use file dialog to open json file
        public string LoadJsonAsString()
        {
            string s = "";
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "JSON Dateien (*.json)|*.json";
            if (ofd.ShowDialog() == true)
            {
                s = File.ReadAllText(ofd.FileName);
            }
            return s;
        }

        public DataTable LoadTable(string filePath)
        {
            string json = File.ReadAllText(filePath);
            DataTable dataTable = ConvertJsonToDataTable(json);
            return dataTable;
        }

        private DataTable ConvertJsonToDataTable(string jsonString)
        {
            DataTable dt = new DataTable();
            JToken token = JToken.Parse(jsonString);

            if (token is JArray)
            {
                JArray? array = token as JArray;
                JObject? obj = array.First as JObject;
                foreach (JProperty property in obj.Properties())
                {
                    dt.Columns.Add(property.Name, typeof(string));
                }

                foreach (var item in array.Children<JObject>())
                {
                    DataRow row = dt.NewRow();
                    foreach (var property in item.Properties())
                    {
                        row[property.Name] = property.Value.ToString();
                    }
                    dt.Rows.Add(row);
                }
            }
            else if (token is JObject)
            {
                JObject? obj = token as JObject;
                foreach (var property in obj.Properties())
                {
                    dt.Columns.Add(property.Name, typeof(string));
                }

                DataRow row = dt.NewRow();
                foreach (var property in obj.Properties())
                {
                    row[property.Name] = property.Value.ToString();
                }
                dt.Rows.Add(row);
            }

            return dt;
        }

        bool ContainsJArray(JObject obj)
        {
            bool contains = false;

            foreach (var property in obj.Properties())
            {
                if (property.Value is JArray)
                {
                    contains = true;
                    break;
                }
            }
            return contains;
        }

        void ProcessJsonString(string json)
        {
            _tableNames.Clear();
            tables.Clear();

            try
            {
                // Deserialize JSON-Strings
                JObject jsonObject = JObject.Parse(json);

                //Handle when no array in json
                if (!ContainsJArray(jsonObject))
                {
                    MainTable = ConvertJsonToDataTable(json);
                    string mainTableName = MainTable.TableName;
                    if (string.IsNullOrEmpty(mainTableName))
                    {
                        mainTableName = "Tabelle 1";
                    }
                    TableNames.Add(mainTableName);
                    SelectedTable = MainTable;
                }

                else
                {
                    string mainTableName = "Tabelle 1";
                    // Check if there's only 1 array
                    if (jsonObject.Count == 1 && jsonObject.First.First is JArray jsonArray)
                    {
                        MainTable = CreateDataTable(jsonArray, mainTableName);

                        tables.Add(MainTable);
                        mainTableName = MainTable.TableName;
                        if (string.IsNullOrEmpty(mainTableName))
                        {
                            mainTableName = "Tabelle 1";
                        }
                        SelectedTable = tables[0];
                        _tableNames.Add(mainTableName);
                    }

                    else if (jsonObject.First is JObject jsonObj && jsonObject.First is not JArray)
                    {
                        MainTable = ConvertJsonToDataTable(json);
                        _tableNames.Add(MainTable.TableName);
                        SelectedTable = MainTable;
                    }

                    else
                    {
                        // multiple arrays
                        int tableCount = 1;
                        foreach (var property in jsonObject.Properties())
                        {
                            if (property.Value is JArray array)
                            {
                                // Create DataTable for array
                                string tableName = property.Name ?? $"Tabelle{tableCount}";
                                TableNames.Add(tableName);
                                DataTable dataTable = CreateDataTable(array, tableName);
                                tables.Add(dataTable);
                                SelectedTable = tables[0];
                                tableCount++;
                            }
                        }

                        if (tableCount == 1)
                        {
                            MainTable = ConvertJsonToDataTable(json);
                            SelectedTable = MainTable;
                        }
                    }

                }
            }

            catch (JsonReaderException ex)
            {
                MessageBox.Show("Fehler beim Parsen des JSON-Strings: " + ex.Message);
            }
        }

        private DataTable CreateDataTable(JArray jsonArray, string tableName)
        {
            DataTable dataTable = new DataTable(tableName);
            //does only work if all properties are the same
            //TODO: add handling for different properties
            if (jsonArray.Count > 0)
            {
                foreach (JProperty property in jsonArray[0].Children<JProperty>())
                {
                    dataTable.Columns.Add(property.Name, typeof(string));
                }

                foreach (JObject jsonRow in jsonArray)
                {
                    DataRow dataRow = dataTable.NewRow();
                    foreach (JProperty property in jsonRow.Children<JProperty>())
                    {
                        dataRow[property.Name] = property.Value.ToString();
                    }
                    dataTable.Rows.Add(dataRow);
                }
            }
            return dataTable;
        }



        private DataTable ConvertToDataTable(string jsonString)
        {
            return JsonConvert.DeserializeObject<DataTable>(jsonString);
        }

        public void CmbChanged(int index)
        {
            ProcessJsonString(JsonStringInTextBox);
            if (tables.Count > 0 && index < tables.Count && index > -1)
            {
                SelectedTable = tables[index];
            }
            else
            {
                SelectedTable = MainTable;
            }
        }


        public void Execute(int index = 0)
        {
            _jsonStringInTextBox = _jsonStringInTextBox;
            CmbChanged(index);
        }
    }
}
