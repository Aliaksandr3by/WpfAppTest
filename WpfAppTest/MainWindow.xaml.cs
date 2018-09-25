using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;
using System.Xml.Serialization;
using WpfAppTest.Model;
using WpfAppTest.ViewModel;

namespace WpfAppTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MainViewModel _mainViewModelObj;
        User _userObj;

        public MainWindow()
        {
            InitializeComponent();

            _userObj = new User();
            _mainViewModelObj = new MainViewModel(_userObj);

            try
            {
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.TargetSite.ToString(), MessageBoxButton.OK, MessageBoxImage.Error);
            }

            
            this.DataContext = _mainViewModelObj;

            CountryComboBox.ItemsSource = _mainViewModelObj._country;

            Properties.Settings.Default.PropertyChanged += (o, e) =>
            {
                Properties.Settings.Default.Save();
            };

        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            var xs = new XmlSerializer(typeof(User));
            using (var writer = XmlWriter.Create("export.xml"))
            {
                xs.Serialize(writer, _userObj);
            }
        }
    }
}
