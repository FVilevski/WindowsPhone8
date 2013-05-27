
using System;
using System.ComponentModel;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace LocalDatabaseSample.Model
{

    public class TopicDataContext : DataContext
    {
        public TopicDataContext(string connectionString) : base(connectionString){ }
        public Table<TopicItem> Items;
    }

    [Table]
    public class TopicItem : INotifyPropertyChanged, INotifyPropertyChanging
    {
        private int _topicID;

        [Column(IsPrimaryKey = true, IsDbGenerated = true, DbType = "INT NOT NULL Identity", CanBeNull = false, AutoSync = AutoSync.OnInsert)]
        public int TopicID
        {
            get { return _topicID; }
            set
            {
                if (_topicID != value)
                {
                    NotifyPropertyChanging("TopicID");
                    _topicID = value;
                    NotifyPropertyChanged("TopicID");
                }
            }
        }

        private string _itemName;

        [Column]
        public string ItemName
        {
            get { return _itemName; }
            set
            {
                if (_itemName != value)
                {
                    NotifyPropertyChanging("ItemName");
                    _itemName = value;
                    NotifyPropertyChanged("ItemName");
                }
            }
        }


        [Column(IsVersion = true)]
        private Binary _version;

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public event PropertyChangingEventHandler PropertyChanging;
        private void NotifyPropertyChanging(string propertyName)
        {
            if (PropertyChanging != null)
            {
                PropertyChanging(this, new PropertyChangingEventArgs(propertyName));
            }
        }
    }
}
