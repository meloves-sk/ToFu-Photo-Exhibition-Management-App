namespace ToFu_Photo_Exhibition_Management_App.Windows
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
		private readonly IAbstractFactory<AddEditPhotoWindow, PhotoResponseDto> _addEditPhotoFactory;
		public MainWindow(
			ICategoryService categoryService,
			IRoundService roundService,
			IManufacturerService manufacturerService,
			ITeamService teamService,
			ICarService carService,
			IPhotoService photoService,
			IAbstractFactory<AddEditPhotoWindow, PhotoResponseDto> addEditPhotoFactory)
		{
			InitializeComponent();
			_categoryService = categoryService;
			_roundService = roundService;
			_manufacturerService = manufacturerService;
			_teamService = teamService;
			_carService = carService;
			_photoService = photoService;
			_addEditPhotoFactory = addEditPhotoFactory;
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

		private async void addPhotoBtn_Click(object sender, RoutedEventArgs e)
		{
			_addEditPhotoFactory.Argument = null;
			_addEditPhotoFactory.Create().ShowDialog();
			await SetPhotos();
		}

		private async void listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			var photo = listBox.SelectedItem as PhotoResponseDto;
			if (photo == null)
			{
				return;
			}
			_addEditPhotoFactory.Argument = photo;
			_addEditPhotoFactory.Create().ShowDialog();
			await SetPhotos();
		}

		private async Task SetCategories()
		{
			await _categoryService.GetCategoriesWithAll();
			categoryComboBox.ItemsSource = _categoryService.CategoriesWithAll;
			categoryComboBox.SelectedItem = _categoryService.CategoriesWithAll.FirstOrDefault();
			categoryComboBox.Items.Refresh();
		}

		private async Task SetRounds()
		{
			var category = categoryComboBox.SelectedItem as CategoryResponseDto;
			if (category == null)
			{
				return;
			}
			await _roundService.GetRoundsWithAll(category.Id);
			roundComboBox.ItemsSource = _roundService.RoundsWithAll;
			roundComboBox.SelectedItem = _roundService.RoundsWithAll.FirstOrDefault();
			roundComboBox.Items.Refresh();
		}
		private async Task SetManufacturers()
		{
			var category = categoryComboBox.SelectedItem as CategoryResponseDto;
			if (category == null)
			{
				return;
			}
			await _manufacturerService.GetManufacturersWithAll(category.Id);
			manufacturerComboBox.ItemsSource = _manufacturerService.ManufacturersWithAll;
			manufacturerComboBox.SelectedItem = _manufacturerService.ManufacturersWithAll.FirstOrDefault();
			manufacturerComboBox.Items.Refresh();
		}

		private async Task SetTeams()
		{
			var category = categoryComboBox.SelectedItem as CategoryResponseDto;
			var manufacturer = manufacturerComboBox.SelectedItem as ManufacturerResponseDto;
			if (category == null || manufacturer == null)
			{
				return;
			}
			await _teamService.GetTeamsWithAll(category.Id, manufacturer.Id);
			teamComboBox.ItemsSource = _teamService.TeamsWithAll;
			teamComboBox.SelectedItem = _teamService.TeamsWithAll.FirstOrDefault();
			teamComboBox.Items.Refresh();
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
			await _carService.GetCarsWithAll(category.Id, manufacturer.Id, team.Id);
			carComboBox.ItemsSource = _carService.CarsWithAll;
			carComboBox.SelectedItem = _carService.CarsWithAll.FirstOrDefault();
			carComboBox.Items.Refresh();
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
			await _photoService.GetPhotos(category.Id, round.Id, manufacturer.Id, team.Id, car.Id);
			listBox.ItemsSource = _photoService.Photos;
			listBox.Items.Refresh();
		}
	}
}