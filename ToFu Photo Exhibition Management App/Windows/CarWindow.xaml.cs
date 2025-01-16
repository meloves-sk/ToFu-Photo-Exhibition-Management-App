namespace ToFu_Photo_Exhibition_Management_App.Windows
{
	/// <summary>
	/// CarWindow.xaml の相互作用ロジック
	/// </summary>
	public partial class CarWindow : Window
	{
		private readonly ICategoryService _categoryService;
		private readonly IManufacturerService _manufacturerService;
		private readonly ITeamInformationService _teamInformationService;
		private readonly ICarService _carService;
		private CarResponseDto? _carResponse = null;
		public CarWindow(
			ICategoryService categoryService,
			IManufacturerService manufacturerService,
			ITeamInformationService teamInformationService,
			ICarService carService)
		{
			InitializeComponent();
			_categoryService = categoryService;
			_manufacturerService = manufacturerService;
			_teamInformationService = teamInformationService;
			_carService = carService;
		}

		private async void Window_Loaded(object sender, RoutedEventArgs e)
		{
			StartProgress();
			await SetCategories();
			SetStatus();
			EndProgress();
		}

		private async void categoryComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			await SetManufacturers();
		}

		private async void manufacturerComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			await Task.WhenAll(SetTeamInformations(), SetCars());
		}

		private async void saveButton_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				var teamInformation = teamInformationComboBox.SelectedItem as TeamInformationResponseDto;
				if (teamInformation == null)
				{
					throw new Exception("チームを選択してください");
				}
				if (carTextBox.Text == string.Empty)
				{
					throw new Exception("車名を入力してください");
				}
				if (carNoTextBox.Text == string.Empty)
				{
					throw new Exception("番号を入力してください");
				}
				if (!int.TryParse(carNoTextBox.Text, out int carNo))
				{
					throw new Exception("番号は数字を入力してください");
				}
				var request = new CarRequestDto(_carResponse?.Id ?? 0, carTextBox.Text, carNo, teamInformation.Id);
				var result = _carResponse == null ?
					await _carService.AddCar(request) :
					await _carService.UpdateCar(request);
				SetDefault();
				await SetCars();
				MessageBox.Show(result.Message);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void resetLink_Click(object sender, RoutedEventArgs e)
		{
			SetDefault();
		}

		private async void editLink_Click(object sender, RoutedEventArgs e)
		{
			var car = (sender as Hyperlink)?.Tag as CarResponseDto;
			if (car == null)
			{
				return;
			}
			_carResponse = car;
			carTextBox.Text = _carResponse.Name;
			carNoTextBox.Text = _carResponse.CarNo.ToString();
			await SetTeamInformations();
			categoryComboBox.IsEnabled = false;
			manufacturerComboBox.IsEnabled = false;
			teamInformationComboBox.IsEnabled = false;
			SetStatus();
		}

		private async void deleteLink_Click(object sender, RoutedEventArgs e)
		{
			var car = (sender as Hyperlink)?.Tag as CarResponseDto;
			if (car == null)
			{
				return;
			}
			var result = await _teamInformationService.DeleteTeamInformation(car.Id);
			await SetTeamInformations();
			SetStatus();
			MessageBox.Show(result.Message);
		}
		private void SetDefault()
		{
			carTextBox.Text = string.Empty;
			carNoTextBox.Text = string.Empty;
			_carResponse = null;
			categoryComboBox.IsEnabled = true;
			manufacturerComboBox.IsEnabled = true;
			teamInformationComboBox.IsEnabled = true;
			SetStatus();
		}
		private void SetStatus()
		{
			if (_carResponse == null)
			{
				statusTextBlock.Text = "Unselected";
			}
			else
			{
				statusTextBlock.Text = $"Selected: CarNo.{_carResponse.CarNo} {_carResponse.Name}";
			}
		}

		private async Task SetCategories()
		{
			await _categoryService.GetCategories();
			categoryComboBox.ItemsSource = _categoryService.Categories;
			if (_carResponse == null)
			{
				categoryComboBox.SelectedItem = _categoryService.Categories.FirstOrDefault();
			}
			else
			{
				categoryComboBox.SelectedItem = _categoryService.Categories.First(a => a.Name == _carResponse.Category);
			}
			categoryComboBox.Items.Refresh();
		}
		private async Task SetManufacturers()
		{
			var category = categoryComboBox.SelectedItem as CategoryResponseDto;
			if (category == null)
			{
				manufacturerComboBox.ItemsSource = null;
				return;
			}
			await _manufacturerService.GetManufacturers(category.Id);
			manufacturerComboBox.ItemsSource = _manufacturerService.Manufacturers;
			if (_carResponse == null)
			{
				manufacturerComboBox.SelectedItem = _manufacturerService.Manufacturers.FirstOrDefault();
			}
			else
			{
				manufacturerComboBox.SelectedItem = _manufacturerService.Manufacturers.First(a => a.Name == _carResponse.Manufacturer);
			}
			manufacturerComboBox.Items.Refresh();
		}

		private async Task SetTeamInformations()
		{
			var category = categoryComboBox.SelectedItem as CategoryResponseDto;
			var manufacturer = manufacturerComboBox.SelectedItem as ManufacturerResponseDto;
			if (category == null || manufacturer == null)
			{
				teamInformationComboBox.ItemsSource = null;
				return;
			}
			await _teamInformationService.GetTeamInformations(category.Id, manufacturer.Id, 0);
			teamInformationComboBox.ItemsSource = _teamInformationService.TeamInformations;
			if (_carResponse == null)
			{
				teamInformationComboBox.SelectedItem = _teamInformationService.TeamInformations.FirstOrDefault();
			}
			else
			{
				teamInformationComboBox.SelectedItem = _teamInformationService.TeamInformations.First(a => a.Team == _carResponse.Team);
			}
			teamInformationComboBox.Items.Refresh();
		}

		private async Task SetCars()
		{
			var category = categoryComboBox.SelectedItem as CategoryResponseDto;
			var manufacturer = manufacturerComboBox.SelectedItem as ManufacturerResponseDto;
			if (category == null || manufacturer == null)
			{
				dataGrid.ItemsSource = null;
				return;
			}
			await _carService.GetCars(category.Id, manufacturer.Id, 0);
			dataGrid.ItemsSource = _carService.Cars;
			dataGrid.Items.Refresh();
		}
		private void StartProgress()
		{
			dataGrid.Visibility = Visibility.Collapsed;
			progressPanel.Visibility = Visibility.Visible;
		}
		private void EndProgress()
		{
			dataGrid.Visibility = Visibility.Visible;
			progressPanel.Visibility = Visibility.Collapsed;
		}
	}
}
