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
using MetroApi;
using Newtonsoft.Json;

namespace WpfApp1
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Dictionary<string, List<string>> stationsDict;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void TextBox_TextChanged( object sender, TextChangedEventArgs e )
        {
            // should check is values are OK 
        }

        private void Click_Me(object sender, RoutedEventArgs e)
        {
            info.Text = "Fetching datas";
            
            if( allUserInputValid() )
            {
                SetRequest();
                info.Text = "";
                foreach( KeyValuePair<string, List<string>> station in stationsDict )
                {
                    info.Text += ( station.Key ) + "\n";
                    foreach( string line in station.Value )
                    {
                        info.Text += line + "\n";
                    }
                }
            }
            else
            {
                info.Text = "Some input values are not valid";
            }

        }

        private void SetRequest()
        {
            // MetroReq LinesReq = new MetroReq("5.72792", "45.18549", "500");
            
            MetroReq LinesReq = new MetroReq(lat.Text, lon.Text, dist.Text);
            List<StationModel> stations = JsonConvert.DeserializeObject<List<StationModel>>(LinesReq.GetResponseAsString());

            stationsDict = new Dictionary<string, List<string>>();
            stationsDict = ToolBox.GetListNameWithoutDuplicateAsDictionnary(stations);
            
        }

        
        Boolean isValid(string n)
        {
            if( n.IndexOf(".") >= 0 )
            {
                StringBuilder sb = new StringBuilder(n);
                sb[n.IndexOf(".")] = ',';
                n = sb.ToString();
            }
            return float.TryParse(n, out float toto);
        }

        Boolean allUserInputValid()
        {
            return (isValid(lon.Text) && isValid(lat.Text) && isValid(dist.Text));
        }
        
    }
}
