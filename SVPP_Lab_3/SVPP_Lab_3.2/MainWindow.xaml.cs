using System;
using System.Collections.ObjectModel;
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
using System.Collections.Specialized;
using System.Text.RegularExpressions;
using System.ComponentModel;
using System.Xml.Serialization;
using System.IO;
using System.Globalization;

namespace SVPP_Lab_3._2
{
    public partial class MainWindow : Window
    {
        ObservableCollection<Employee> colEmployees = new ObservableCollection<Employee>();

        #region //Create collections of class fields
        ObservableCollection<String> colSurname = new ObservableCollection<String>();
        ObservableCollection<Double> colSalary = new ObservableCollection<Double>();
        ObservableCollection<String> colPositions = new ObservableCollection<String>();
        ObservableCollection<String> colCity = new ObservableCollection<String>();
        ObservableCollection<String> colStreet = new ObservableCollection<String>();
        ObservableCollection<String> colHouse = new ObservableCollection<String>();
        #endregion

        Employee employee;

        string path = Directory.GetCurrentDirectory() + @"\storage.txt";

        public MainWindow()
        {
            InitializeComponent();
            employee = new Employee();

            this.DataContext = employee;

            listBoxEmployee.DataContext = colEmployees;

            #region //Binding to collection combobox
            ComboBoxPosition.DataContext = colPositions;
            ComboBoxCity.DataContext = colCity;
            ComboBoxStreet.DataContext = colStreet;
            ComboBoxHouse.DataContext = colHouse;
            #endregion

            FileRead();

            foreach (Employee emp in colEmployees)
            {
                colSurname.Add(emp.Surname);
                colSalary.Add(emp.Salary);
                colPositions.Add(emp.Position);
                colCity.Add(emp.City);
                colStreet.Add(emp.Street);
                colHouse.Add(emp.House);
            }

            #region //Fill class field collections
            //colSurname.Add("Иванов Иван Иванович");
            //colSurname.Add("Петров Петр Петрович");
            //colSurname.Add("Николаев Николай Николаевич");

            //colSalary.Add(20000.50);
            //colSalary.Add(15000.50);
            //colSalary.Add(10000.50);

            //colPositions.Add("Директор");
            //colPositions.Add("Секретарь");
            //colPositions.Add("Инженер");

            //colCity.Add("г. Минск");
            //colCity.Add("г. Гродно");
            //colCity.Add("г. Брест");

            //colStreet.Add("ул. Советская");
            //colStreet.Add("ул. Ленина");
            //colStreet.Add("ул. Меньшикова");

            //colHouse.Add("д. 1");
            //colHouse.Add("д. 2");
            //colHouse.Add("д. 2а корп. 1");
            #endregion

            #region //Fill a class collection
            //for (int i = 0; i < 3; i++)
            //{
            //    //colEmployees.Add(new Employee());
            //    colEmployees[i].Surname = colSurname[i];
            //    colEmployees[i].Salary = colSalary[i];
            //    colEmployees[i].Position = colPositions[i];
            //    colEmployees[i].City = colCity[i];
            //    colEmployees[i].Street = colStreet[i];
            //    colEmployees[i].House = colHouse[i];
            //}
            #endregion

            listBoxEmployee.SelectionChanged += listBoxEmployee_Selected;
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bool existEmployee = false;

                Filleng_Field_Class(employee);

                employee.CheckValue(); //Check Value Field of a class Employee

                #region //Check exist
                foreach (Employee w in colEmployees)
                {
                    if (w.Surname.Equals(employee.Surname) && w.City.Equals(employee.City) && w.Street.Equals(employee.Street) && w.House.Equals(employee.House))
                    {
                        existEmployee = true;
                    }
                }
                #endregion

                if (!existEmployee && employee.ValidData) //If not exist this employee and valid data
                {
                    Updating_Class_Field_Collections();

                    existEmployee = false;

                    colEmployees.Add(new Employee(employee));
                }
                else
                {
                    MessageBoxResult messageBoxResult = MessageBox.Show("Сотрудник уже существует или введены не верные данные", "Внимание!!!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (ArgumentException exc)
            {
                MessageBox.Show($"Ошибка! {exc.Message}");
            }
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            FileSave();
        }

        private void ButtonRead_Click(object sender, RoutedEventArgs e)
        {
            FileRead();
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

                if (!existEmployee && employee.ValidData) //If not exist this employee and valid data
                {
                    Updating_Class_Field_Collections();
                    Filleng_Field_Class(colEmployees[iLb]);
                }
                else
                {
                    MessageBoxResult messageBoxResult = MessageBox.Show("Сотрудник уже существует или введены не верные данные Edit", "Внимание!!!", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                listBoxEmployee.Items.Refresh();
            }
            catch (ArgumentException exc)
            {
                MessageBox.Show($"Ошибка! {exc.Message}");
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
            string patternPosition = @"[^a-яА-ЯёЁa-zA-Z. -]";
            string patternCity = @"[^a-яА-ЯёЁa-zA-Z. -]";
            string patternStreet = @"[^a-яА-ЯёЁa-zA-Z0-9. -]";
            string patternHouse = @"[^a-яА-ЯёЁa-zA-Z0-9. /-]";

            Color colorRed = (Color)ColorConverter.ConvertFromString("#FF0000");

            ComboBox combobox = (ComboBox)sender;
            //Brush redBrush = new SolidColorBrush(Colors.Red);
            Brush redBrush = new SolidColorBrush(colorRed);
            Brush redDefault = new SolidColorBrush(Colors.Black);

            StringBuilder tt = new StringBuilder();
            var s = tt.ToString();

            switch ((string)combobox.Name)
            {
                case "ComboBoxPosition":
                    if (Regex.IsMatch(combobox.Text, patternPosition, RegexOptions.IgnoreCase))
                    {
                        combobox.Foreground = redBrush;
                        combobox.ToolTip = tt.Append("Наименование должности должно состоять из букв и '-'");
                    }
                    else
                    {
                        combobox.Foreground = redDefault;
                        combobox.ToolTip = string.IsNullOrEmpty(s) ? null : s;
                    }
                    break;
                case "ComboBoxCity":
                    if (Regex.IsMatch(combobox.Text, patternCity, RegexOptions.IgnoreCase))
                    {
                        combobox.Foreground = redBrush;
                        combobox.ToolTip = tt.Append("Название города должно состоять из букв и '-'");
                    }
                    else
                    {
                        combobox.Foreground = redDefault;
                        combobox.ToolTip = string.IsNullOrEmpty(s) ? null : s;
                    }
                    break;
                case "ComboBoxStreet":
                    if (Regex.IsMatch(combobox.Text, patternStreet, RegexOptions.IgnoreCase))
                    {
                        combobox.Foreground = redBrush;
                        combobox.ToolTip = tt.Append("Название улицы должно состоять из букв, цифр и '-'");
                    }
                    else
                    {
                        combobox.Foreground = redDefault;
                        combobox.ToolTip = string.IsNullOrEmpty(s) ? null : s;
                    }
                    break;
                case "ComboBoxHouse":
                    if (Regex.IsMatch(combobox.Text, patternHouse, RegexOptions.IgnoreCase))
                    {
                        combobox.Foreground = redBrush;
                        combobox.ToolTip = tt.Append("Номер дома должен состоять из букв, цифр, '-' и '/'");
                    }
                    else
                    {
                        combobox.Foreground = redDefault;
                        combobox.ToolTip = string.IsNullOrEmpty(s) ? null : s;
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
            emp.House = ComboBoxHouse.Text.ToString();
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

        private void TextBoxSalary_PreviewTextInput(object sender, TextCompositionEventArgs e) //Убираем возможность ввода запятой или двух десятичных разделителей
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
                    textBox.Text = textBox.Text + replacementSeparator; //вместо "," добавлем "."
                    textBox.SelectionStart = textBox.Text.Length; //Пеермещаем каретку в конец TextBox
                }
                e.Handled = true; //Не выводить
            }
        }

        public void FileSave()
        {
            XmlSerializer ser = new XmlSerializer(typeof(ObservableCollection<Employee>));
            FileStream file = null;

            try
            {
                file = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None);
                ser.Serialize(file, colEmployees);
            }
            catch (Exception e)
            {
                MessageBoxResult messageBoxResult = MessageBox.Show($"{e.Message}", "Внимание!!!", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            if (file != null)
            {
                file.Close();
            }
        }

        public void FileRead()
        {
            XmlSerializer ser = new XmlSerializer(typeof(ObservableCollection<Employee>));

            FileStream file = null;

            try
            {
                file = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.None);
                colEmployees = (ObservableCollection<Employee>)ser.Deserialize(file);
                listBoxEmployee.DataContext = colEmployees;
            }
            catch (Exception e)
            {
                MessageBoxResult messageBoxResult = MessageBox.Show($"{e.Message}", "Внимание!!!", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            if (file != null)
            {
                file.Close();
            }

        }

        private void listBoxEmployee_Selected(object sender, RoutedEventArgs e)
        {
            ListBox lb = (ListBox)sender;
            int n = 0;

            //CultureInfo inf = new CultureInfo(System.Threading.Thread.CurrentThread.CurrentCulture.Name);
            string replaceableSeparator = ",";//inf.NumberFormat.NumberDecimalSeparator;
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
                    ComboBoxHouse.SelectedItem = emp.House;
                }
                n++;
            }
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            int iLb = listBoxEmployee.SelectedIndex;
            colEmployees.Remove(colEmployees[iLb]);
        }
    }
}
