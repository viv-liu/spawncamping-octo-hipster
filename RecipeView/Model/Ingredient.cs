using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeView
{
    public class Ingredient:INotifyPropertyChanged
    {
        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private string _foodgroup;

        public string Foodgroup
        {
            get { return _foodgroup; }
            set { _foodgroup = value; }
        }
 
        private int _quantity;

        public int Quantity
        {
            get { return _quantity; }
            set { _quantity = value; }
        }
        private string _units;

        public string Units
        {
            get { return _units; }
            set { _units = value; }
        }
        private int _points;

        public int Points
        {
            get { return _points; }
            set { _points = value; }
        }
        private Boolean _required;

        public Boolean Required
        {
            get { return _required; }
            set { _required = value; }
        }
        public Ingredient(string n, string fg, int q, string u, int p, Boolean r)
        {
            Name = n;
            Foodgroup = fg;
            Quantity = q;
            Units = u;
            Points = p;
            Required = r;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
