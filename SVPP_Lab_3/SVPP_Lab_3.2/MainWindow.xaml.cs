using System;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Text.RegularExpressions;

namespace LAB_3._2
{
    public partial class MainWindow : Window
    {
        readonly Employee employee;
        readonly FileHandler fileHandler = new FileHandler();
        readonly ObservableCollection<Employee> colEmployees = new ObservableCollection<Employee>();

        readonly ObservableCollection<String> colSurname = new ObservableCollection<String>();
        readonly ObservableCollection<Double> colSalary = new ObservableCollection<Double>();
        readonly ObservableCollection<String> colPositions = new ObservableCollection<String>();
        readonly ObservableCollection<String> colCity = new ObservableCollection<String>();
        readonly ObservableCollection<String> colStreet = new ObservableCollection<String>();
        readonly ObservableCollection<String> colHouse = new ObservableCollection<String>();

        public MainWindow()
        {
            InitializeComponent();
            employee = new Employee();
            colEmployees = fileHandler.colEmployees;
            this.DataContext = employee;

            listBoxEmployee.DataContext = colEmployees;

            ComboBoxPosition.DataContext = colPositions;
            ComboBoxCity.DataContext = colCity;
            ComboBoxStreet.DataContext = colStreet;

            listBoxEmployee.DataContext = fileHandler.FileRead();

            foreach (Employee emp in colEmployees)
            {
                colSurname.Add(emp.Surname);
                colSalary.Add(emp.Salary);
                colPositions.Add(emp.Position);
                colCity.Add(emp.City);
                colStreet.Add(emp.Street);
                colHouse.Add(emp.House);
            }
            listBoxEmployee.SelectionChanged += ListBoxEmployee_Selected;
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bool isEmployeeExists = false;

                Filleng_Field_Class(employee);

                employee.CheckValue();

                foreach (Employee w in colEmployees)
                {
                    if (w.Surname.Equals(employee.Surname) && w.City.Equals(employee.City) && w.Street.Equals(employee.Street) && w.House.Equals(employee.House))
                    {
                        isEmployeeExists = true;
                    }
                }

                if (!isEmployeeExists && employee.ValidData)
                {
                    Updating_Class_Field_Collections();
                    isEmployeeExists = false;
                    colEmployees.Add(new Employee(employee));
                    MessageBox.Show("Record was added successfully!");
                }
                else
                {
                    MessageBox.Show("Employee alreasy exists OR you've entered wrong data", "Warning!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                listBoxEmployee.DataContext = colEmployees;
            }
            catch (ArgumentException exc)
            {
                MessageBox.Show($"Error happened while trying to add new employee! \n {exc.Message}");
            }
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            fileHandler.FileSave(colEmployees);
            MessageBox.Show("File was saved");
        }

        private void ButtonRead_Click(object sender, RoutedEventArgs e)
        {
            listBoxEmployee.DataContext = fileHandler.FileRead();
            MessageBox.Show("File was read");
        }

        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bool existEmployee = false;

                int iLb = listBoxEmployee.SelectedIndex;

                Filleng_Field_Class(employee);

                int i = 0;

                foreach (Employee w in colEmployees)
                {
                    if (i != iLb)
                    {
                        if (w.Surname.Equals(employee.Surname) && w.City.Equals(employee.City) && w.Street.Equals(employee.Street) && w.House.Equals(employee.House))
                        {
                            existEmployee = true;
                        }
                    }
                    i++;
                }

                if (!existEmployee && employee.ValidData)
                {
                    Updating_Class_Field_Collections();
                    Filleng_Field_Class(colEmployees[iLb]);
                }
                else
                {
                    MessageBoxResult messageBoxResult = MessageBox.Show("Employee already exist OR you've entered wrong data!", "Achtung!!!", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                listBoxEmployee.Items.Refresh();
            }
            catch (ArgumentException exc)
            {
                MessageBox.Show($"Some error with editing! {exc.Message}");
            }
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textbox = (TextBox)sender;
            textbox.SelectionStart = 0;
            textbox.SelectionLength = textbox.Text.Length;
        }

        private void ComboBox_GotFocus(object sender, RoutedEventArgs e)
        {
            string patternPosition = @"[^a-zA-Z. -]";
            string patternCity = @"[^a-zA-Z. -]";
            string patternStreet = @"[^a-zA-Z0-9. -]";
            string patternHouse = @"[^a-zA-Z0-9. /-]";

            Color colorRed = (Color)ColorConverter.ConvertFromString("#FF0000");

            ComboBox combobox = (ComboBox)sender;
            Brush redBrush = new SolidColorBrush(colorRed);
            Brush redDefault = new SolidColorBrush(Colors.Black);

            StringBuilder toolTipStringBuilder = new StringBuilder();
            var toolTipString = toolTipStringBuilder.ToString();

            switch ((string)combobox.Name)
            {
                case "ComboBoxPosition":
                    if (Regex.IsMatch(combobox.Text, patternPosition, RegexOptions.IgnoreCase))
                    {
                        combobox.Foreground = redBrush;
                        combobox.ToolTip = toolTipStringBuilder.Append("Position should consist letters and \"-\"");
                    }
                    else
                    {
                        combobox.Foreground = redDefault;
                        combobox.ToolTip = string.IsNullOrEmpty(toolTipString) ? null : toolTipString;
                    }
                    break;
                case "ComboBoxCity":
                    if (Regex.IsMatch(combobox.Text, patternCity, RegexOptions.IgnoreCase))
                    {
                        combobox.Foreground = redBrush;
                        combobox.ToolTip = toolTipStringBuilder.Append("City name should consist letters and \"-\"");
                    }
                    else
                    {
                        combobox.Foreground = redDefault;
                        combobox.ToolTip = string.IsNullOrEmpty(toolTipString) ? null : toolTipString;
                    }
                    break;
                case "ComboBoxStreet":
                    if (Regex.IsMatch(combobox.Text, patternStreet, RegexOptions.IgnoreCase))
                    {
                        combobox.Foreground = redBrush;
                        combobox.ToolTip = toolTipStringBuilder.Append("Street name should consist letters, numbers and \"-\"");
                    }
                    else
                    {
                        combobox.Foreground = redDefault;
                        combobox.ToolTip = string.IsNullOrEmpty(toolTipString) ? null : toolTipString;
                    }
                    break;
                case "TextBoxHouse":
                    if (Regex.IsMatch(combobox.Text, patternHouse, RegexOptions.IgnoreCase))
                    {
                        combobox.Foreground = redBrush;
                        combobox.ToolTip = toolTipStringBuilder.Append("House number should consist letters, numbers and \"-\"");
                    }
                    else
                    {
                        combobox.Foreground = redDefault;
                        combobox.ToolTip = string.IsNullOrEmpty(toolTipString) ? null : toolTipString;
                    }
                    break;
            }
        }

        public void Filleng_Field_Class(Employee emp)
        {
            double dSalary;

            emp.Surname = TextBoxSurname.Text.ToString();
            if (Double.TryParse(TextBoxSalary.Text, out dSalary))
            {
                employee.Salary = dSalary;
            }
            emp.Position = ComboBoxPosition.Text.ToString();
            emp.City = ComboBoxCity.Text.ToString();
            emp.Street = ComboBoxStreet.Text.ToString();
            emp.House = TextBoxHouse.Text.ToString();
        }

        public void Updating_Class_Field_Collections()
        {
            if (!colPositions.Contains(employee.Position))
            {
                colPositions.Add(employee.Position);
            }

            if (!colCity.Contains(employee.City))
            {
                colCity.Add(employee.City);
            }

            if (!colStreet.Contains(employee.Street))
            {
                colStreet.Add(employee.Street);
            }

            if (!colHouse.Contains(employee.House))
            {
                colHouse.Add(employee.House);
            }
        }

        private void TextBoxSalary_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            string replacementSeparator = ".";
            string replaceableSeparator = ",";

            if (e.Text == replacementSeparator)
            {
                if (textBox.Text.Contains(replacementSeparator))
                {
                    e.Handled = true; //Не выводить
                }
            }
            else if (e.Text == replaceableSeparator)
            {
                if (!textBox.Text.Contains(replacementSeparator))
                {
                    textBox.Text = textBox.Text + replacementSeparator; 
                    textBox.SelectionStart = textBox.Text.Length; 
                }
                e.Handled = true; 
            }
        }


        private void ListBoxEmployee_Selected(object sender, RoutedEventArgs e)
        {
            ListBox lb = (ListBox)sender;
            int n = 0;

            string replaceableSeparator = ",";
            string replacementSeparator = ".";

            foreach (Employee emp in colEmployees)
            {
                if (lb.SelectedIndex.Equals(n))
                {
                    TextBoxSurname.Text = emp.Surname;
                    TextBoxSalary.Text = emp.Salary.ToString().Replace(replaceableSeparator, replacementSeparator);
                    ComboBoxPosition.SelectedItem = emp.Position;
                    ComboBoxCity.SelectedItem = emp.City;
                    ComboBoxStreet.SelectedItem = emp.Street;
                    TextBoxHouse.Text = emp.House;
                }
                n++;
            }
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int iLb = listBoxEmployee.SelectedIndex;
                if (iLb != -1)
                {
                    colEmployees.Remove(colEmployees[iLb]);
                    listBoxEmployee.DataContext = colEmployees;
                }
                else
                {
                    MessageBox.Show($"Select employee record to delete!");
                }

            }
            catch (ArgumentException exc)
            {
                MessageBox.Show($"Some error with deleting! \n{exc.Message}");
            }
        }
    }
}