using SWAPI.Models;
using SWAPI.ViewModels;

namespace SWAPI.Views;

public partial class PlanetView : ContentPage
{
	PlanetViewModel _viewModel;
	public PlanetView(Planet planet)
	{
		InitializeComponent();
		BindingContext = _viewModel = new PlanetViewModel(planet);
	}
}