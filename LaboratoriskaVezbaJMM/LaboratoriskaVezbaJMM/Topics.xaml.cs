
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

namespace sdkLocalDatabaseCS
{
    public partial class Topics : PhoneApplicationPage
    {
        public Topics()
        {
            InitializeComponent();
            this.DataContext = App.ViewModel;
        }

        private void appBarOkButton_Click(object sender, EventArgs e)
        {
            if (newTopicTextBox.Text.Length > 0)
            {
                TopicItem newItem = new TopicItem
                {
                    ItemName = newTopicTextBox.Text,
                };

                App.ViewModel.AddTopicItem(newItem);

                if (NavigationService.CanGoBack)
                {
                    NavigationService.GoBack();
                }
            }
        }

        private void appBarCancelButton_Click(object sender, EventArgs e)
        {
            if (NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }
        }
    }
}
