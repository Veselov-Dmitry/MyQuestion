using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;

namespace mynamespace
{
    class ViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<User> Users_OASU{get; set;}                
        public ICommand AddUsers_OASUCommand{get; set;} 
        public ICommand DelUsers_OASUCommand{get; set;} 

        public ViewModel()
        {
            Users_OASU = new ObservableCollection<User>(GetUsers());
            
            AddUsers_OASUCommand = new Command<object>(arg => AddUsers_OASUMethod());
            DelUsers_OASUCommand = new Command<object>(arg => DelUsers_OASUMethod(arg));
        }

        private void DelUsers_OASUMethod(object arg)
        {
            User find = Users_OASU.Where(x => x.Login == (arg as User).Login).FirstOrDefault();
            Users_OASU.Remove(find);
        }

        private void AddUsers_OASUMethod()
        {
            Users_OASU.Add(new User("52221", "John X."));
        }

        private List<User> GetUsers()
        {
            List<User> list = new List<User>();
            list.Add(new User("52222", "John W."));
            list.Add(new User("52223", "John Z."));
            list.Add(new User("52224", "John A."));
            list.Add(new User("52225", "John M."));
            return list;
        }
        public virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
    public class User
    {
        public string Login {get; set;}
        public string Name {get; set;}
        public User(string Login, string Name)
        {
            this.Login = Login;
            this.Name = Name;
        }
    }
}
