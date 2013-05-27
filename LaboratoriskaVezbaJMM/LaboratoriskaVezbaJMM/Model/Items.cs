using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalDatabaseSample.Model
{
    class Items
    {
        private string Description;

        public string Description1
        {
            get { return Description; }
            set { Description = value; }
        }

        private string Link;

        public string Link1
        {
            get { return Link; }
            set { Link = value; }
        }

        private string Title;

        public string Title1
        {
            get { return Title; }
            set { Title = value; }
        }
    }     
}
