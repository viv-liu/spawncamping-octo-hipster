using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace RecipeView
{
    public partial class MatchedRecipeList : PhoneApplicationPage
    {
        private List<Recipe> _recipeList;

        public List<Recipe> RecipeList
        {
            get { return _recipeList; }
            set { _recipeList = value; }
        }
        public MatchedRecipeList()
        {
            InitializeComponent();
            _recipeList = new List<Recipe>();
            Recipe r1 = new Recipe();
            r1.Name = "Chicken Quesadilla";
            r1.prepTime = 120;
            Recipe r2 = new Recipe();
            r2.Name = "Chicken Alfredo";
            r2.prepTime = 2;
            Recipe r3 = new Recipe();
            r3.Name = "Chicken Sprite";
            r3.prepTime = 390;

            RecipeList.Add(r1);
            RecipeList.Add(r2);
            RecipeList.Add(r3);

            matchedRecipelist.DataContext = this;
            matchedRecipelist.ItemsSource = RecipeList;
        }



        public sealed class Recipe
        {
            public string Id;
            private string name;
            public string Name
            {
                get { return name; }
                set { name = value; }
            }
            public string instructions;
            public int prepTime;
            private string prepTimeString;
            public string PrepTimeString
            {
                get
                {
                    int hours = prepTime / 60;
                    int mins = prepTime % 60;
                    if (hours == 0)
                    {
                        return mins.ToString() + "m";
                    }
                    else
                    {
                        return hours.ToString() + "h " + mins.ToString() + "m";
                    }
                }
              set { prepTimeString = value; }
            }

            public int nPoints;
            public int nRequired;
        }

        private void matchedRecipelist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // TODO: navigate to ViewRecipe page
        }
    }
}