using SWAPI.ViewModels;

namespace SWAPI.Views;

public partial class CreateView : ContentPage
{
	CreateViewModel _viewModel; 
	public CreateView()
	{
		InitializeComponent();
		BindingContext = _viewModel = new CreateViewModel();
	}
}