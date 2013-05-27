using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using LocalDatabaseSample.Model;
using System.Xml.Linq;
using System.Text.RegularExpressions;

namespace sdkLocalDatabaseCS
{
    public partial class MainPage : PhoneApplicationPage
    {
        public MainPage()
        {
            InitializeComponent();
            this.DataContext = App.ViewModel;
        }

        private void newTaskAppBarButton_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Topics.xaml", UriKind.Relative));
        }
        private void getDataFromService_Click(object sender, RoutedEventArgs e)
        {
            WebClient webClient = new WebClient();
            webClient.DownloadStringCompleted += new DownloadStringCompletedEventHandler(downloadCompleted);
            webClient.DownloadStringAsync(new System.Uri("http://www.finki.ukim.mk/mk/rss/announcements"));
        }
        private void downloadCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                Deployment.Current.Dispatcher.BeginInvoke(() =>
                {
                    MessageBox.Show(e.Error.Message);
                });
            }
            else
            {
                this.State["feed"] = e.Result;
                UpdateList(e.Result);
            }
        }
        private string RemoveHtmlTags(string html)
        {
            return Regex.Replace(html, "<.+?>", string.Empty);
        }       
        private void UpdateList(string dataXML)
        {

            RemoveHtmlTags(dataXML);
            HttpUtility.HtmlDecode(dataXML);
            XElement xmlitems = XElement.Parse(dataXML);

            List<XElement> elements = xmlitems.Descendants("item").ToList();

            List<Items> aux = new List<Items>();



            foreach (XElement rssItem in elements)
            {
                Items rss = new Items();

                rss.Description1 = rssItem.Element("description").Value;
                rss.Link1 = rssItem.Element("link").Value;
                rss.Title1 = rssItem.Element("title").Value;
                aux.Add(rss);

                TextBlock tbTitle = new TextBlock();
                tbTitle.Text = rss.Title1 + "\n";
                dataListBox.Items.Add(tbTitle);

                TextBlock tbDescription = new TextBlock();
                tbDescription.Text = rss.Description1 + "\n";
                dataListBox.Items.Add(tbDescription);

            }
        }
        private void deleteTaskButton_Click(object sender, RoutedEventArgs e)
        {           
            var button = sender as Button;
            if (button != null)
            {    
                TopicItem itemDelete = button.DataContext as TopicItem;
                App.ViewModel.DeleteTopicItem(itemDelete);
            }
            this.Focus();
        }
        protected override void OnNavigatedFrom(System.Windows.Navigation.NavigationEventArgs e)
        {
            App.ViewModel.SaveChangesToDB();
        }
    }
}
