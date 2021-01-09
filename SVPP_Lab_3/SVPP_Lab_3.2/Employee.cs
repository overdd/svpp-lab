using System;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace LAB_3
{
    public class Employee : IDataErrorInfo
    {
        public string Surname { get; set; }
        public double Salary { get; set; }
        public string Position { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string House { get; set; }

        [field: NonSerializedAttribute()]
        public bool ValidData { get; set; } = true;

        public string Error
        {
            get { throw new NotImplementedException(); }
        }

        string patternSurname = @"[^a-zA-Z -]";
        string patternSalary = @"[^0-9.,]";
        string patternPosition = @"[^a-zA-Z. -]";
        string patternCity = @"[^a-zA-Z. -]";
        string patternStreet = @"[^a-zA-Z0-9. -]";
        string patternHouse = @"[^a-zA-Z0-9. /-]";

        bool check_Surname = false;
        bool check_Salary = false;

        public Employee()
        {
            Surname = "";
            Salary = 0;
            Position = "";
            City = "";
            Street = "";
            House = "";
        }

        public Employee(string surname, double salary, string position, string city, string street, string house)
        {
            this.Surname = surname;
            this.Salary = salary;
            this.Position = position;
            this.City = city;
            this.Street = street;
            this.House = house;
        }

        public Employee(Employee w)
        {
            this.Surname = w.Surname;
            this.Salary = w.Salary;
            this.Position = w.Position;
            this.City = w.City;
            this.Street = w.Street;
            this.House = w.House;
        }

        public void CheckValue()
        {
            if (Regex.IsMatch(Position, patternPosition, RegexOptions.IgnoreCase) || Position == "")
                ValidData = false;
            else if (Regex.IsMatch(City, patternCity, RegexOptions.IgnoreCase) || City == "")
                ValidData = false;
            else if (Regex.IsMatch(Street, patternStreet, RegexOptions.IgnoreCase) || Street == "")
                ValidData = false;
            else if (Regex.IsMatch(House, patternHouse, RegexOptions.IgnoreCase) || House == "")
                ValidData = false;
            else
                ValidData = true;
        }

        string IDataErrorInfo.this[string columnName]
        {
            get
            {
                string error = String.Empty;

                switch (columnName)
                {
                    case "Surname":
                        if (check_Surname)   
                        {
                            if (Regex.IsMatch(Surname, patternSurname, RegexOptions.IgnoreCase))
                            {
                                error = "Full name should consist of letters only";
                                ValidData = false;
                            }
                        }
                        check_Surname = true;
                        break;

                    case "Salary":
                        if (Salary <= 0 && check_Salary) 
                        {
                            if (Regex.IsMatch(Salary.ToString(), patternSalary, RegexOptions.IgnoreCase))
                            {
                                error = "Salary should be posititve";
                                ValidData = false;
                            }
                        }
                        check_Salary = true;
                        break;
                }
                return error;
            }
        }

        public override string ToString()
        {
            return $"{ Surname};{Salary};{Position};{City};{Street};{House}";
        }
    }
}
