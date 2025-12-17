using Tudosa_Stefan_Lab11.Data;

namespace Tudosa_Stefan_Lab11
{   
    public partial class App : Application
    {
        public static ShoppingListDatabase Database { get; private set; }

        public App()
        {
            InitializeComponent();
            Database = new ShoppingListDatabase(new RestService());
            MainPage = new NavigationPage(new ListEntryPage());
        }
    }
}
