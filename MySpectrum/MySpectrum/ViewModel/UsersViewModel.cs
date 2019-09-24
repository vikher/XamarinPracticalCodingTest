using MySpectrum.Helper;
using MySpectrum.Model;
using MySpectrum.View;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace MySpectrum.ViewModel
{
    public class UsersViewModel : INotifyPropertyChanged
    {
        private readonly IUserRepository _usersRepository;

        private IEnumerable<User> _users;

        public IEnumerable<User> Users
        {
            get
            {
                return _users;
            }
            set
            {
                _users = value;
                OnPropertyChanged();
            }
        }
        public string UserName { get; set; }
        public string UserPassword   { get; set; }

        public ICommand RefreshCommand { get; private set; }

        public ICommand AddCommand { get; private set; }

        public async Task<bool> ValidatePassword(string pwd)
        {
            if (string.IsNullOrWhiteSpace(pwd))
            {
                await Application.Current.MainPage.DisplayAlert("Alert", " Password cannot be empty", "OK");
                return false;

            }

            if (string.IsNullOrEmpty(pwd))
            {
                await Application.Current.MainPage.DisplayAlert("Alert", " Password cannot be empty", "OK");
                return false;

            }

            var hasNumber = new Regex(@"[0-9]+");
            var hasChar = new Regex(@"[a-zA-Z]+");
            var sameSequenceRegex = new Regex(@"^(.+).*\1$");

            if (!hasNumber.IsMatch(pwd))
            {
                await Application.Current.MainPage.DisplayAlert("Alert", "Password should contain At least one numeric value", "OK");
                return false;

            }
            if (!hasChar.IsMatch(pwd))
            {
                await Application.Current.MainPage.DisplayAlert("Alert", "Password should contain At least one letter", "OK");
                return false;
            }


            if ((pwd.Length < 5) || (pwd.Length > 12))
            {
                await Application.Current.MainPage.DisplayAlert("Alert", "Password should be between 5 and 12 characters in length", "OK");
                return false;
            }

            if (sameSequenceRegex.IsMatch(pwd))
            {
                await Application.Current.MainPage.DisplayAlert("Alert", "Password should not contain any sequence of characters immediately followed by the same sequence", "OK");
                return false;
            }
            else
            {
                return true;

            }
        }

        public async Task<bool> ValidateUserName(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                await Application.Current.MainPage.DisplayAlert("Alert", " Username cannot be empty", "OK");
                return false;

            }
            if (string.IsNullOrEmpty(username))
            {
                await Application.Current.MainPage.DisplayAlert("Alert", " Username cannot be empty", "OK");
                return false;

            }
            else
            {
                return true;

            }
        }

        public async void SubmitPassword()
        {

            var validPassword = await ValidatePassword(UserPassword);
            var validUsername = await ValidateUserName(UserName);

            if (validUsername == true && validPassword == true)
            {
                var user = new User
                {
                    Name = UserName,
                    Password = UserPassword
                };
                await _usersRepository.AddUserAsync(user);
                await Application.Current.MainPage.Navigation.PushAsync(new UsersListPage(_usersRepository));

            }
            else
            {

                return;
            }
        }

        public async void RefreshList()
        {
           Users = await _usersRepository.GetUserAsyc();
        }

        public UsersViewModel(IUserRepository usersRepository)
        {
            _usersRepository = usersRepository;
            RefreshList();
            AddCommand = new Command(SubmitPassword);
            RefreshCommand = new Command(RefreshList);

        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
