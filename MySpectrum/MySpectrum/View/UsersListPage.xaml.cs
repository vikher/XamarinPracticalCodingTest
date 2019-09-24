using MySpectrum.Helper;
using MySpectrum.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MySpectrum.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class UsersListPage : ContentPage
	{
        public UsersListPage(IUserRepository usersRepository)
        {
            InitializeComponent();
            BindingContext = new UsersViewModel(usersRepository);

        }
    }
}