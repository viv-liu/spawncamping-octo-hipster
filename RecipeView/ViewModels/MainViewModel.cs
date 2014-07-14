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
        public static List<Ingredient> UnsortedIngredientsList;
        public bool IsDataLoaded { get; private set; }
        public MainViewModel()
        {
            UnsortedIngredientsList = new List<Ingredient>();
            this.LoadData();
        }

        /* LongListSelector binds to GroupedInstructionsList and GroupedIngredientsList
         * These are called upon from MainPage.xaml's LLS's
         */
        public List<Instruction<Ingredient>> GroupedInstructionsList
        {
            get
            {
                return CreateInstructions();
            }
        }

        public List<StringKeyGroup<Ingredient>> GroupedIngredientsList
        {
            get
            {
                return CreateGroups();
            }
        }

        // Load UnsortedIngredientsList with random fake foods for the first time
        public void LoadData()
        {
            Ingredient i1 = new Ingredient("chicken", "proteins", 2, "pounds", 1, true);
            Ingredient i2 = new Ingredient("cashews", "proteins", 3, "cups", 1, true);
            Ingredient i3 = new Ingredient("cauliflower", "produce", 1, "head", 3, true);

            UnsortedIngredientsList.Add(i1);
            UnsortedIngredientsList.Add(i2);
            UnsortedIngredientsList.Add(i3);

            this.IsDataLoaded = true;
        }

        public class StringKeyGroup<T> : List<T>
        {
            public string Key { get; private set; }

            public StringKeyGroup(string key)
            {
                Key = key;
            }
        }

        public static List<StringKeyGroup<Ingredient>> CreateGroups()
        {
            // Create List to hold final list
            List<StringKeyGroup<Ingredient>> groupedIngredientsList = new List<StringKeyGroup<Ingredient>>();
            StringKeyGroup<Ingredient> produceIngs = new StringKeyGroup<Ingredient>("produce");
            StringKeyGroup<Ingredient> proteinsIngs = new StringKeyGroup<Ingredient>("proteins");
            StringKeyGroup<Ingredient> dairyIngs = new StringKeyGroup<Ingredient>("dairy");
            StringKeyGroup<Ingredient> grainsIngs = new StringKeyGroup<Ingredient>("grains");
            StringKeyGroup<Ingredient> condimentsIngs = new StringKeyGroup<Ingredient>("condiments");
            StringKeyGroup<Ingredient> miscIngs = new StringKeyGroup<Ingredient>("misc");         
            
            // Fill each list with the appropriate Ingredients
            foreach (Ingredient i in UnsortedIngredientsList)
            {
                if(i.Foodgroup == "produce") produceIngs.Add(i);
                else if(i.Foodgroup == "proteins") proteinsIngs.Add(i);
                else if(i.Foodgroup == "dairy") dairyIngs.Add(i);
                else if(i.Foodgroup == "grains") grainsIngs.Add(i);
                else if(i.Foodgroup == "condiments") condimentsIngs.Add(i);
                else if(i.Foodgroup == "misc") miscIngs.Add(i);
                else MessageBox.Show("I don't know what food group -"+ i.Name +"- belongs to. Give up buddy.");
            }

            // Populate grouped list with each food group list
            groupedIngredientsList.Add(produceIngs);
            groupedIngredientsList.Add(proteinsIngs);
            groupedIngredientsList.Add(dairyIngs);
            groupedIngredientsList.Add(grainsIngs);
            groupedIngredientsList.Add(condimentsIngs);
            groupedIngredientsList.Add(miscIngs);

            // In essence we're returning a list of lists. This is the format longlistselector expects.
            return groupedIngredientsList;
        }
        public static List<Instruction<Ingredient>> CreateInstructions()
        {
            List<Instruction<Ingredient>> compiledInstructionsList = new List<Instruction<Ingredient>>();

            Ingredient i1 = new Ingredient("chicken", "proteins", 2, "pounds", 1, true);
            Ingredient i2 = new Ingredient("cashews", "proteins", 3, "cups", 1, true);
            Ingredient i3 = new Ingredient("cauliflower", "produce", 1, "head", 3, true);

            // Instruction ia
            List<Instruction<Ingredient>.FoodAction> foodActionList= new List<Instruction<Ingredient>.FoodAction>();
            foodActionList.Add(Instruction<Ingredient>.FoodAction.chop);
            foodActionList.Add(Instruction<Ingredient>.FoodAction.destroy);
            Instruction<Ingredient> instruction1 = new Instruction<Ingredient>("Chop that cauliflower and DESTROY those cashews!!!", foodActionList);
            instruction1.Add(i3);
            instruction1.Add(i2);

            // Instruction ib
            foodActionList.Clear();
            foodActionList.Add(Instruction<Ingredient>.FoodAction.boil);
            Instruction<Ingredient> instruction2 = new Instruction<Ingredient>("Calmly resume boiling the chicken.", foodActionList);
            instruction2.Add(i1);

            // Compile instruction1 and instruction2
            compiledInstructionsList.Add(instruction1);
            compiledInstructionsList.Add(instruction2);
            return compiledInstructionsList;
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
    }
}