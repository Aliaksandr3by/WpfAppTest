using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppTest.Model
{
    public class User 
    {
        private string _name;
        private int     _age;
        private int _country;

        public User()
        {

        }
        public User(string nameUser, int ageUser, int countryUser)
        {
            this.Name = nameUser;
            this.Age = ageUser;
            this.Country = countryUser;
        }

        public string Name { get => _name; set => _name = value; }
        public int Age { get => _age; set => _age = value; }
        public int Country { get => _country; set => _country = value; }
    }
}
