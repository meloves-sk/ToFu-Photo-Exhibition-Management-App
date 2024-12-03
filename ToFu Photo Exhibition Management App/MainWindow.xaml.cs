using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ToFu_Photo_Exhibition_Management_App.Services.CarService;
using ToFu_Photo_Exhibition_Management_App.Services.CategoryService;
using ToFu_Photo_Exhibition_Management_App.Services.ManufacturerService;
using ToFu_Photo_Exhibition_Management_App.Services.PhotoService;
using ToFu_Photo_Exhibition_Management_App.Services.RoundService;
using ToFu_Photo_Exhibition_Management_App.Services.TeamService;
using ToFu_Photo_Exhibition_Management_App.Shared.Dto.Response;

namespace ToFu_Photo_Exhibition_Management_App
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private readonly ICategoryService _categoryService;
		private readonly IRoundService _roundService;
		private readonly IManufacturerService _manufacturerService;
		private readonly ITeamService _teamService;
		private readonly ICarService _carService;
		private readonly IPhotoService _photoService;
		public MainWindow(ICategoryService categoryService, IRoundService roundService, IManufacturerService manufacturerService, ITeamService teamService, ICarService carService, IPhotoService photoService)
		{
			InitializeComponent();
			_categoryService = categoryService;
			_roundService = roundService;
			_manufacturerService = manufacturerService;
			_teamService = teamService;
			_carService = carService;
			_photoService = photoService;
			SetCategory();
		}

		private async void SetCategory()
		{
			var categories = await _categoryService.GetCategories();
			categoryComboBox.ItemsSource = categories.Data;
			categoryComboBox.SelectedItem = categories.Data.FirstOrDefault();
		}

		private async void categoryComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			var category = categoryComboBox.SelectedItem as CategoryResponseDto;
			if (category == null) return;
			var rounds = await _roundService.GetFilterRounds(category.Id);
			roundComboBox.ItemsSource = rounds.Data;
			roundComboBox.SelectedItem = rounds.Data.FirstOrDefault();
			var manufacturers = await _manufacturerService.GetFilterManufacturers(category.Id);
			manufacturerComboBox.ItemsSource = manufacturers.Data;
			manufacturerComboBox.SelectedItem = manufacturers.Data.FirstOrDefault();
		}

		private async void roundComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			await SetPhoto();
		}

		private async void manufacturerComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			var category = categoryComboBox.SelectedItem as CategoryResponseDto;
			var manufacturer = manufacturerComboBox.SelectedItem as ManufacturerResponseDto;
			if (category == null || manufacturer == null) return;
			var teams = await _teamService.GetFilterTeams(category.Id, manufacturer.Id);
			teamComboBox.ItemsSource = teams.Data;
			teamComboBox.SelectedItem = teams.Data.FirstOrDefault();
		}

		private async void teamComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			var category = categoryComboBox.SelectedItem as CategoryResponseDto;
			var manufacturer = manufacturerComboBox.SelectedItem as ManufacturerResponseDto;
			var team = teamComboBox.SelectedItem as TeamResponseDto;
			if (category == null || manufacturer == null || team == null) return;
			var cars = await _carService.GetFilterCars(category.Id, manufacturer.Id, team.Id);
			carComboBox.ItemsSource = cars.Data;
			carComboBox.SelectedItem = cars.Data.FirstOrDefault();
		}

		private async void carComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			await SetPhoto();
		}

		private async Task SetPhoto()
		{
			var category = categoryComboBox.SelectedItem as CategoryResponseDto;
			var round = roundComboBox.SelectedItem as RoundResponseDto;
			var manufacturer = manufacturerComboBox.SelectedItem as ManufacturerResponseDto;
			var team = teamComboBox.SelectedItem as TeamResponseDto;
			var car = carComboBox.SelectedItem as CarResponseDto;
			if (category == null || round == null || manufacturer == null || team == null || car == null) return;
			var photos = await _photoService.GetFilterPhotos(category.Id, round.Id, manufacturer.Id, team.Id, car.Id);
			listBox.ItemsSource = photos.Data.Select(a => new
			{
				a.Id,
				a.CarNo,
				a.Car,
				FilePath = $"https://www.meloves.net/tofu-photo-exhibition/{a.FilePath}"
			});
		}
	}
}