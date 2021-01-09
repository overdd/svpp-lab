using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Xml.Serialization;

namespace LAB_3._2
{
    class FileHandler
    {
        readonly string path = @"storage.txt";
        public ObservableCollection<Employee> colEmployees;
        public FileHandler()
        {
            this.colEmployees = new ObservableCollection<Employee>();
            ObservableCollection<String> colSurname = new ObservableCollection<String>();
            ObservableCollection<Double> colSalary = new ObservableCollection<Double>();
            ObservableCollection<String> colPositions = new ObservableCollection<String>();
            ObservableCollection<String> colCity = new ObservableCollection<String>();
            ObservableCollection<String> colStreet = new ObservableCollection<String>();
            ObservableCollection<String> colHouse = new ObservableCollection<String>();

            foreach (Employee emp in colEmployees)
            {
                colSurname.Add(emp.Surname);
                colSalary.Add(emp.Salary);
                colPositions.Add(emp.Position);
                colCity.Add(emp.City);
                colStreet.Add(emp.Street);
                colHouse.Add(emp.House);
            }

            colSurname.Add("John Smith Jr.");
            colSurname.Add("Johnny Smith II");
            colSurname.Add("Tom W. Cruise");

            colSalary.Add(20000.50);
            colSalary.Add(15000.50);
            colSalary.Add(10000.50);

            colPositions.Add("CEO");
            colPositions.Add("Secretary");
            colPositions.Add("Data science engineer");

            colCity.Add("Atlanta");
            colCity.Add("Winniepeg");
            colCity.Add("Osaka");

            colStreet.Add("Independence Square");
            colStreet.Add("White-Red-White Street");
            colStreet.Add("Baker Street");

            colHouse.Add("456B");
            colHouse.Add("12");
            colHouse.Add("4905");

            for (int i = 0; i < 3; i++)
            {
                colEmployees.Add(new Employee());
                colEmployees[i].Surname = colSurname[i];
                colEmployees[i].Salary = colSalary[i];
                colEmployees[i].Position = colPositions[i];
                colEmployees[i].City = colCity[i];
                colEmployees[i].Street = colStreet[i];
                colEmployees[i].House = colHouse[i];
            }
            FileSave(colEmployees);
        }


        public void FileSave(ObservableCollection<Employee> colEmployees)
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
                MessageBox.Show($"{e.Message}", "Warning!", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            if (file != null)
            {
                file.Close();
            }
        }

        public ObservableCollection<Employee> FileRead()
        {
            XmlSerializer ser = new XmlSerializer(typeof(ObservableCollection<Employee>));
            FileStream file = null;
            ObservableCollection<Employee> colEmployees = new ObservableCollection<Employee>();
            try
            {
                file = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.None);
                colEmployees = (ObservableCollection<Employee>)ser.Deserialize(file);
            }
            catch (Exception e)
            {
                MessageBox.Show($"{e.Message}", "Warning!", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            if (file != null)
            {
                file.Close();
            }
            return colEmployees;
        }
    }
}
