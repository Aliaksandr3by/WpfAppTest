using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfAppTest.Model;

namespace WpfAppTest.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged, IDataErrorInfo
    {
        public event PropertyChangedEventHandler PropertyChanged;
        User _userObj;
        public ObservableCollection<string> _country { get; private set; }

        public MainViewModel(User q1)
        {
            this._userObj = q1;
            _country = new ObservableCollection<string>() { "Россия", "Германия", "Италия" };
        }

        private void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public string this[string columnName]
        {
            get
            {
                string error = String.Empty;
                switch (columnName)
                {
                    case "Age":
                        if ((Age < 0) || (Age > 100))
                        {
                            error = "Возраст должен быть больше 0 и меньше 100";
                        }
                        break;
                    case "Name":
                        if (String.IsNullOrEmpty(Name))
                        {
                            error = "Не пустой";
                        }
                        //Обработка ошибок для свойства Name
                        break;
                    case "Сountry":
                        if (CountrySet != -1)
                        {
                            error = "Не пустой";
                        }
                        break;
                }
                return error;
            }
        }

        public string Error => throw new NotImplementedException();

        public string Name
        {
            get
            {
                return Properties.Settings.Default._name;
            }
            set
            {
                _userObj.Name = value;
                Properties.Settings.Default._name = value;
                OnPropertyChanged();
            }
        }

        public int Age
        {
            get => Properties.Settings.Default._age;
            set
            {
                _userObj.Age = value;
                Properties.Settings.Default._age = value;
                OnPropertyChanged();
            }

        }

        public int CountrySet
        {
            get
            {
                return Properties.Settings.Default._country = -1; //костыль
            }
            set
            {
                _userObj.Country = value;
                Properties.Settings.Default._country = value;
                OnPropertyChanged();
            }
        }

        public string FormatTmp
        {
            get => Properties.Settings.Default._format;
            set
            {
                Properties.Settings.Default._format = value;
                MessageBox.Show(Properties.Settings.Default._format);

                switch (value)
                {
                    case "JSON":

                        //Обработка ошибок для свойства 
                        break;
                    case "XML":

                        //Обработка ошибок для свойства 
                        break;
                    default:
                        break;
                }

                OnPropertyChanged();
            }

        }
    }
}
