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
using ChatClient.ServiceChat;
using System.ServiceModel;

namespace ChatClient
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IServiceChatCallback
    {
        bool isConnected = false;
        ServiceChatClient client;
        int Id;
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            client = new ServiceChatClient(new InstanceContext(this));
        }
        void ConnectUser()
        {
            if (!isConnected)
            {
                client = new ServiceChatClient(new InstanceContext(this));
                Id = client.Connect(tbUserName.Text);
                tbUserName.IsEnabled = false;
                bConnDiscon.Content = "Disconnect";
                isConnected = true;
            }
        }

        void DisconnectUser()
        {
            client.Disconnect(Id);
            client = null;
            tbUserName.IsEnabled = true;
            bConnDiscon.Content = "Connect";
            isConnected = false;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (isConnected)
            {
                DisconnectUser();
            }
            else
            {
                ConnectUser();
            }
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DisconnectUser();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void lbChat_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void tbMessage_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        public void MsgCallback(string msg)
        {
            lbChat.Items.Add(msg);
            lbChat.ScrollIntoView(lbChat.Items[lbChat.Items.Count-1]);
        }

        private void tbMessage_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                if(client != null)
                {
                    client.SendMsg(tbMessage.Text, Id);
                    tbMessage.Text = string.Empty;
                }
            }
        }
    }
}
