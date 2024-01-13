using SWAPI.ViewModels;

namespace SWAPI
{
    public partial class MainPage : ContentPage
    {

        MainViewModel _viewModel;
        public MainPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new MainViewModel();
        }

       
    }

}
