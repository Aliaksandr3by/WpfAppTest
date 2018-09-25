using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;

using WpfAppTest.Model;

namespace WpfAppTest.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged, IDataErrorInfo
    {
        public event PropertyChangedEventHandler PropertyChanged;
        User _userObj;
        private ObservableCollection<string> _country;

        public MainViewModel(User q1)
        {
            this._userObj = q1;
            CountryName = new ObservableCollection<string>() { "Россия", "Германия", "Италия" };
        }

        public ObservableCollection<string> CountryName { get => _country; set => _country = value; }

        private void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public string this[string columnName]
        {
            get
            {
                string error = default;
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
                            error = "Имя не может быть пустым";
                        }
                        break;
                    case "Country":
                        if (Country == -1)
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

        public int Country
        {
            get
            {
                return Properties.Settings.Default._country; //костыль
            }
            set
            {
                _userObj.Country = value;
                Properties.Settings.Default._country = value;
                OnPropertyChanged();
            }
        }

        public bool asdad { get; set; } = false;
    }
}
