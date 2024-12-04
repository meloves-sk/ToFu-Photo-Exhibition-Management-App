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
		}
		private async void Window_Loaded(object sender, RoutedEventArgs e)
		{
			await SetCategories();
		}

		private async void categoryComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			await Task.WhenAll(SetRounds(), SetManufacturers());
		}

		private async void roundComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			await SetPhotos();
		}

		private async void manufacturerComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			await SetTeams();
		}

		private async void teamComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			await SetCars();
		}

		private async void carComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			await SetPhotos();
		}
		private async Task SetCategories()
		{
			await _categoryService.GetCategories();
			categoryComboBox.ItemsSource = _categoryService.Categories;
			categoryComboBox.SelectedItem = _categoryService.Categories.FirstOrDefault();
		}

		private async Task SetRounds()
		{
			var category = categoryComboBox.SelectedItem as CategoryResponseDto;
			if (category == null)
			{
				return;
			}
			await _roundService.GetFilterRounds(category.Id);
			List<RoundResponseDto> rounds = new List<RoundResponseDto>();
			rounds.Add(new RoundResponseDto(0, "ALL"));
			rounds.AddRange(_roundService.Rounds);
			roundComboBox.ItemsSource = rounds;
			roundComboBox.SelectedItem = rounds.FirstOrDefault();
		}
		private async Task SetManufacturers()
		{
			var category = categoryComboBox.SelectedItem as CategoryResponseDto;
			if (category == null)
			{
				return;
			}
			await _manufacturerService.GetFilterManufacturers(category.Id);
			List<ManufacturerResponseDto> manufacturers = new List<ManufacturerResponseDto>();
			manufacturers.Add(new ManufacturerResponseDto(0, "ALL"));
			manufacturers.AddRange(_manufacturerService.Manufacturers);
			manufacturerComboBox.ItemsSource = manufacturers;
			manufacturerComboBox.SelectedItem = manufacturers.FirstOrDefault();
		}

		private async Task SetTeams()
		{
			var category = categoryComboBox.SelectedItem as CategoryResponseDto;
			var manufacturer = manufacturerComboBox.SelectedItem as ManufacturerResponseDto;
			if (category == null || manufacturer == null)
			{
				return;
			}
			await _teamService.GetFilterTeams(category.Id, manufacturer.Id);
			List<TeamResponseDto> teams = new List<TeamResponseDto>();
			teams.Add(new TeamResponseDto(0, "ALL"));
			teams.AddRange(_teamService.Teams);
			teamComboBox.ItemsSource = teams;
			teamComboBox.SelectedItem = teams.FirstOrDefault();
		}

		private async Task SetCars()
		{
			var category = categoryComboBox.SelectedItem as CategoryResponseDto;
			var manufacturer = manufacturerComboBox.SelectedItem as ManufacturerResponseDto;
			var team = teamComboBox.SelectedItem as TeamResponseDto;
			if (category == null || manufacturer == null || team == null)
			{
				return;
			}
			await _carService.GetFilterCars(category.Id, manufacturer.Id, team.Id);
			List<CarResponseDto> cars = new List<CarResponseDto>();
			cars.Add(new CarResponseDto(0, "ALL", 0, 0, string.Empty, string.Empty, string.Empty));
			cars.AddRange(_carService.Cars);
			carComboBox.ItemsSource = cars;
			carComboBox.SelectedItem = cars.FirstOrDefault();
		}

		private async Task SetPhotos()
		{
			var category = categoryComboBox.SelectedItem as CategoryResponseDto;
			var round = roundComboBox.SelectedItem as RoundResponseDto;
			var manufacturer = manufacturerComboBox.SelectedItem as ManufacturerResponseDto;
			var team = teamComboBox.SelectedItem as TeamResponseDto;
			var car = carComboBox.SelectedItem as CarResponseDto;
			if (category == null || round == null || manufacturer == null || team == null || car == null)
			{
				return;
			}
			await _photoService.GetFilterPhotos(category.Id, round.Id, manufacturer.Id, team.Id, car.Id);
			listBox.ItemsSource = _photoService.Photos.Select(a => new
			{
				a.Id,
				a.CarNo,
				a.Car,
				FilePath = $"https://www.meloves.net/tofu-photo-exhibition/{a.FilePath}"
			});
		}
	}
}