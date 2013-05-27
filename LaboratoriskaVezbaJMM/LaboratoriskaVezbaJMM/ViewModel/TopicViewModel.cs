using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using LocalDatabaseSample.Model;


namespace LocalDatabaseSample.ViewModel
{
    public class TopicViewModel : INotifyPropertyChanged
    {
        
        private TopicDataContext topicDB;

        public TopicViewModel(string ConnectionString)
        {
            topicDB = new TopicDataContext(ConnectionString);
        }

        
        private ObservableCollection<TopicItem> _topics;
        public ObservableCollection<TopicItem> Topics
        {
            get { return _topics; }
            set
            {
                _topics = value;
                NotifyPropertyChanged("Topics");
            }
        }

        public void SaveChangesToDB()
        {
            topicDB.SubmitChanges();
        }

    
        public void LoadCollectionsFromDatabase()
        {

            var TopicItemsInDB = from TopicItem topic in topicDB.Items select topic;

            Topics = new ObservableCollection<TopicItem>(TopicItemsInDB);

        }

        public void AddTopicItem(TopicItem newTopic)
        {
            topicDB.Items.InsertOnSubmit(newTopic);
            topicDB.SubmitChanges();
            Topics.Add(newTopic);
        }

        public void DeleteTopicItem(TopicItem topicDelete)
        {
            Topics.Remove(topicDelete);
            topicDB.Items.DeleteOnSubmit(topicDelete);
            topicDB.SubmitChanges();
        }
        

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        // Used to notify Silverlight that a property has changed.
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}
