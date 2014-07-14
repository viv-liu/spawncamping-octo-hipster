using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeView
{
    public class Instruction<T> : List<T>
    {
        public enum FoodAction
        {
            dice, cut, chop, mince, mix, bake, cook, broil, boil, destroy
        }

        public string Text { get; private set; }
        public List<FoodAction> Actions;

        public Instruction(string t, List<FoodAction> al)
        {
            Text = t;
            Actions = al;
        }
    }
}
