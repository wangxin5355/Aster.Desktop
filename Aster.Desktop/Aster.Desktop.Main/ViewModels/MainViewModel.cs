using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Aster.Desktop.Main.ViewModels
{
    public class MainViewModel : ViewModelBase
    {

        public ICommand ShowMessageCommand { get; private set; }
        public Data MyData { get; set; }
        public object SelectedItem { get; set; }
        public MainViewModel()
        {
            MyData = new Data();
            MyAsyncCommand = new AsyncCommand<string>(Calculate, CanCalculate, true);
            ShowMessageCommand = new DelegateCommand(ShowMessage);
        }

        public AsyncCommand<string> MyAsyncCommand { get; private set; }

        IMessageBoxService MessageBoxService { get { return GetService<IMessageBoxService>(ServiceSearchMode.PreferParents); } }

        public  bool CanCalculate(string arg)
        {
            return true;
        }

        public  Task Calculate(string parameter)
        {
            return Task.Factory.StartNew(ShowMessage);
        }

        public void ShowMessage()
        {
            MessageBoxService.ShowMessage("hehe");
        }
    }

    public class Data
    {
        public List<Category> Categories { get; set; }
        public Data()
        {
            Categories = new List<Category>();
            List<Item> subitems = new List<Item>();
            subitems.Add(new Item() { ItemName = "Chair", Description = "A red chair." });
            subitems.Add(new Item() { ItemName = "Table", Description = "An old table." });
            Categories.Add(new Category() { CategoryName = "Furniture", Items = subitems });
            List<Item> books = new List<Item>();
            books.Add(new Item() { ItemName = "Dictionary", Description = "My old French-English Dictionary" });
            Categories.Add(new Category() { CategoryName = "Books", Items = books });
        }
    }
    public class Category
    {
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public List<Item> Items { get; set; }
    }
    public class Item
    {
        public string ItemName { get; set; }
        public string Description { get; set; }

    }
}
