using System;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace SVPP_Lab_3._2
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

        string patternSurname = @"[^a-яА-ЯёЁa-zA-Z -]";
        string patternSalary = @"[^0-9.,]";
        string patternPosition = @"[^a-яА-ЯёЁa-zA-Z. -]";
        string patternCity = @"[^a-яА-ЯёЁa-zA-Z. -]";
        string patternStreet = @"[^a-яА-ЯёЁa-zA-Z0-9. -]";
        string patternHouse = @"[^a-яА-ЯёЁa-zA-Z0-9. /-]";

        bool check_Surname = false;
        bool check_Salary = false;

        #region //Пустой конструктор
        public Employee()
        {
            Surname = "";
            Salary = 0;
            Position = "";
            City = "";
            Street = "";
            House = "";
        }
        #endregion

        #region //Конcтруктор со всеми полями
        public Employee(string surname, double salary, string position, string city, string street, string house)
        {
            this.Surname = surname;
            this.Salary = salary;
            this.Position = position;
            this.City = city;
            this.Street = street;
            this.House = house;
        }
        #endregion

        #region //Конструктор на основе экземпляра класса
        public Employee(Employee w)
        {
            this.Surname = w.Surname;
            this.Salary = w.Salary;
            this.Position = w.Position;
            this.City = w.City;
            this.Street = w.Street;
            this.House = w.House;
        }
        #endregion

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
                        if (check_Surname)   //Что бы во время инициализации не выбивало ошибку check_Surname
                        {
                            if (Regex.IsMatch(Surname, patternSurname, RegexOptions.IgnoreCase))
                            {
                                error = "Фамилия должна состоять из букв";
                                ValidData = false;
                            }
                        }
                        check_Surname = true;
                        break;

                    case "Salary":
                        if (Salary <= 0 && check_Salary)    //Что бы во время инициализации не выбивало ошибку check_Salary
                        {
                            if (Regex.IsMatch(Salary.ToString(), patternSalary, RegexOptions.IgnoreCase))
                            {
                                error = "Зарплата не может быть отрицательной";
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
