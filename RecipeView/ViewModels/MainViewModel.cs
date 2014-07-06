// (c) Copyright Microsoft Corporation.
// This source is subject to the Microsoft Public License (Ms-PL).
// Please see http://go.microsoft.com/fwlink/?LinkID=131993 for details.
// All other rights reserved.

using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows;

namespace RecipeView
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private List<Ingredient> _unsortedBatch; 
        public static StringKeyGroup<Ingredient> produceIngs;
        public static StringKeyGroup<Ingredient> proteinsIngs;
        public static StringKeyGroup<Ingredient> dairyIngs;
        public static StringKeyGroup<Ingredient> grainsIngs;
        public static StringKeyGroup<Ingredient> condimentsIngs;
        public static StringKeyGroup<Ingredient> miscIngs;

        public MainViewModel()
        {
            this.UnsortedBatch = new List<Ingredient>();

            this.LoadData();
        }

        /// <summary>
        /// A collection for Ingredient objects.
        /// </summary>
        public List<Ingredient> UnsortedBatch
        {
            get
            {
                return _unsortedBatch;
            }
            private set
            {
                _unsortedBatch = value;
                NotifyPropertyChanged();
            }
        }

        public List<StringKeyGroup<Ingredient>> SortedBatch
        {
            get
            {
                return CreateGroups(UnsortedBatch);
            }
        }

        public bool IsDataLoaded
        {
            get;
            private set;
        }

        /// <summary>
        /// Creates and adds a few Ingredient objects into the Items collection.
        /// </summary>
        public void LoadData()
        {
            Ingredient i1 = new Ingredient("chicken", "proteins", 2, "pounds", 1, true);
            Ingredient i2 = new Ingredient("cashews", "proteins", 3, "cups", 1, true);
            Ingredient i3 = new Ingredient("cauliflower", "produce", 1, "head", 3, true);
            // Sample data; replace with real data
            this.UnsortedBatch.Add(i1);
            this.UnsortedBatch.Add(i2);
            this.UnsortedBatch.Add(i3);

            this.IsDataLoaded = true;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public class StringKeyGroup<T> : List<T>
        {
            /// <summary>
            /// The Key of this group.
            /// </summary>
            public string Key { get; private set; }

            public StringKeyGroup(string key)
            {
                Key = key;
            }
        }
        public static List<StringKeyGroup<Ingredient>> CreateGroups(IEnumerable<Ingredient> UnsortedBatch)
        {
            // Create List to hold final list
            List<StringKeyGroup<Ingredient>> SortedBatch = new List<StringKeyGroup<Ingredient>>();
            produceIngs = new StringKeyGroup<Ingredient>("produce");
            proteinsIngs = new StringKeyGroup<Ingredient>("proteins");
            dairyIngs = new StringKeyGroup<Ingredient>("dairy");
            grainsIngs = new StringKeyGroup<Ingredient>("grains");
            condimentsIngs = new StringKeyGroup<Ingredient>("condiments");
            miscIngs = new StringKeyGroup<Ingredient>("misc");

            // Fill each list with the appropriate Ingredients
            foreach (Ingredient i in UnsortedBatch)
            {
                if(i.Foodgroup == "produce") produceIngs.Add(i);
                else if(i.Foodgroup == "proteins") proteinsIngs.Add(i);
                else if(i.Foodgroup == "dairy") dairyIngs.Add(i);
                else if(i.Foodgroup == "grains") grainsIngs.Add(i);
                else if(i.Foodgroup == "condiments") condimentsIngs.Add(i);
                else if(i.Foodgroup == "misc") miscIngs.Add(i);
                else MessageBox.Show("I don't know what food group -"+ i.Name +"- belongs to. Give up buddy.");
            }

            // Add each TimeKeyGroup to the overall list
            SortedBatch.Add(produceIngs);
            SortedBatch.Add(proteinsIngs);
            SortedBatch.Add(dairyIngs);
            SortedBatch.Add(grainsIngs);
            SortedBatch.Add(condimentsIngs);
            SortedBatch.Add(miscIngs);
            //Debug.WriteLine(SortedBatch.Count);
            // In essence we're returning a list of lists. This is the format longlistselector expects.
            return SortedBatch;
        }
    }
}