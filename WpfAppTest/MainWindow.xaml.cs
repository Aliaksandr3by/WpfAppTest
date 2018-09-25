using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
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
        private string _formatSaveJsonXml = default;
        private bool _validForm = false;

        MainViewModel _mainViewModelObj;
        User _userObj;

        public MainWindow()
        {
           

#if DEBUG
            Properties.Settings.Default.Reset();
#else 

#endif

            InitializeComponent();

            _userObj = new User();
            _mainViewModelObj = new MainViewModel(_userObj);

            this.DataContext = _mainViewModelObj;

            //CountryComboBox.ItemsSource = _mainViewModelObj.CountryName;

            Properties.Settings.Default.PropertyChanged += (o, e) =>
            {
                Properties.Settings.Default.Save();
            };

        }

        private void TextBox_Error(object sender, ValidationErrorEventArgs e)
        {
            //SaveButton.IsEnabled = false;
            //MessageBox.Show(e.Error.ErrorContent.ToString());
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            FormatTmp(_formatSaveJsonXml);
        }

        public void FormatTmp(string value)
        {
            string fileName = "export";
            switch (value)
            {
                case nameof(Format_XML_RadioButton):
                    try
                    {
                        XmlWriterSettings settings = new XmlWriterSettings();
                        settings.Indent = true;
                        settings.Encoding = Encoding.UTF8;
                        settings.OmitXmlDeclaration = true;
                        settings.NewLineOnAttributes = true;

                        // Сохранить объект в файле в формате XML.
                        XmlSerializer xmlFormat = new XmlSerializer(typeof(User));

                        using (Stream fStream = new FileStream($"{ fileName }.xml", FileMode.Append, FileAccess.Write, FileShare.None))
                        {
                            xmlFormat.Serialize(fStream, _userObj);
                        }
                        MessageBox.Show(_formatSaveJsonXml);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString(), ex.TargetSite.ToString(), MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    break;

                case nameof(Format_JSON_RadioButton):

                    try
                    {
                        DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(User));

                        using (FileStream fs = new FileStream($"{ fileName }.json", FileMode.Append))
                        {
                            jsonFormatter.WriteObject(fs, _userObj);
                        }
                        MessageBox.Show(_formatSaveJsonXml);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString(), ex.TargetSite.ToString(), MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    break;
                default:
                    break;

            }
        }
        private void Format_RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if (sender is RadioButton)
            {
                _formatSaveJsonXml = (sender as RadioButton).Name ?? "JSON";
            }

        }
    }
}
