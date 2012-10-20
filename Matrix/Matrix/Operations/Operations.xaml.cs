using System.Collections.Generic;
using System.Windows.Media;
using Matrix.Pages;
using Microsoft.Phone.Shell;

namespace Matrix.Operations
{
    public abstract class Operations
    {
        public string Name { get; set; }
        public SolidColorBrush Color { get; set; }
        public OperatonMode Mode { get; set; }
        protected List<ApplicationBarMenuItem> MenuItems;
        public List<ApplicationBarMenuItem> ApplicationbarItems
        {
            get { return MenuItems; }
        }
        protected Dictionary<int, SelectionOptions> Matrices;
        protected Operations(Dictionary<int, SelectionOptions> matrices)
        {
            MenuItems = new List<ApplicationBarMenuItem>();
            Matrices = matrices;
        }
        public abstract Result.Result EvaluateResult();
    }
}