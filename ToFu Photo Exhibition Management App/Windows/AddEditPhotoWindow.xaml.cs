namespace ToFu_Photo_Exhibition_Management_App.Windows
{
	/// <summary>
	/// AddEditPhotoWindow.xaml の相互作用ロジック
	/// </summary>
	public partial class AddEditPhotoWindow : Window
	{
		private readonly ICategoryService _categoryService;
		private readonly IRoundService _roundService;
		private readonly IManufacturerService _manufacturerService;
		private readonly ITeamService _teamService;
		private readonly ICarService _carService;
		private readonly IPhotoService _photoService;
		private readonly IAbstractFactory<AddEditPhotoWindow, PhotoResponseDto> _addEditPhotoFactory;
		private byte[]? _imageData = null;
		private bool _isInitialize = true;
		private int _initializeCount = 0;
		public AddEditPhotoWindow(
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
			StartProgress();
			if (_addEditPhotoFactory.Argument == null)
			{
				_isInitialize = false;
			}
			await SetCategories();
			if (_isInitialize)
			{
				Title = "Edit Photo";
				uploadButton.Visibility = Visibility.Collapsed;
				deleteButton.Visibility = Visibility.Visible;
				descriptionTextBox.Text = _addEditPhotoFactory.Argument!.Description;
				using (var client = new HttpClient())
				{
					_imageData = await client.GetByteArrayAsync(_addEditPhotoFactory.Argument.FilePath);
				}
				SetImage();
			}
			EndProgress();
		}
		private void uploadButton_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				OpenFileDialog ofd = new OpenFileDialog();
				ofd.Filter = "写真|*.jpg;*.jpeg;*.png";
				if (ofd.ShowDialog() == true)
				{
					//var info = new FileInfo(ofd.FileName);
					//if (info.Length > 10485760)
					//{
					//	throw new Exception("アップロード可能な写真は10MB以下です");
					//}
					_imageData = File.ReadAllBytes(ofd.FileName);
					SetImage();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private async void deleteButton_Click(object sender, RoutedEventArgs e)
		{
			var result = await _photoService.DeletePhoto(_addEditPhotoFactory.Argument.Id);
			MessageBox.Show(result.Message);
			Close();
		}
		private async void categoryComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			await Task.WhenAll(SetRounds(), SetManufacturers());
		}

		private async void manufacturerComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			await SetTeams();
		}

		private async void teamComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			await SetCars();
		}

		private async void saveButton_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				var round = roundComboBox.SelectedItem as RoundResponseDto;
				var car = carComboBox.SelectedItem as CarResponseDto;
				if (round == null)
				{
					throw new Exception("ラウンドを選択してください");
				}
				if (carComboBox.SelectedItem == null)
				{
					throw new Exception("車両を選択してください");
				}
				if (_imageData == null)
				{
					throw new Exception("写真を選択してください");
				}
				var request = new PhotoRequestDto(_addEditPhotoFactory.Argument?.Id ?? 0, descriptionTextBox.Text, round.Id, car.Id, _imageData);
				var result = _addEditPhotoFactory.Argument == null ?
					await _photoService.AddPhoto(request) :
					await _photoService.UpdatePhoto(request);
				MessageBox.Show(result.Message);
				Close();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private async Task SetCategories()
		{
			await _categoryService.GetCategories();
			categoryComboBox.ItemsSource = _categoryService.Categories;
			if (_isInitialize)
			{
				categoryComboBox.SelectedItem = _categoryService.Categories.First(a => a.Name == _addEditPhotoFactory.Argument!.Category);
				CheckInitialize();
			}
			else
			{
				categoryComboBox.SelectedItem = _categoryService.Categories.FirstOrDefault();
			}
			categoryComboBox.Items.Refresh();
		}

		private async Task SetRounds()
		{
			var category = categoryComboBox.SelectedItem as CategoryResponseDto;
			if (category == null)
			{
				roundComboBox.ItemsSource = null;
				return;
			}
			await _roundService.GetRounds(category.Id);
			roundComboBox.ItemsSource = _roundService.Rounds;
			if (_isInitialize)
			{
				roundComboBox.SelectedItem = _roundService.Rounds.First(a => a.Name == _addEditPhotoFactory.Argument!.Round);
				CheckInitialize();
			}
			else
			{
				roundComboBox.SelectedItem = _roundService.Rounds.FirstOrDefault();
			}
			roundComboBox.Items.Refresh();
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
			if (_isInitialize)
			{
				manufacturerComboBox.SelectedItem = _manufacturerService.Manufacturers.First(a => a.Name == _addEditPhotoFactory.Argument!.Manufacturer);
				CheckInitialize();
			}
			else
			{
				manufacturerComboBox.SelectedItem = _manufacturerService.Manufacturers.FirstOrDefault();
			}
			manufacturerComboBox.Items.Refresh();
		}

		private async Task SetTeams()
		{
			var category = categoryComboBox.SelectedItem as CategoryResponseDto;
			var manufacturer = manufacturerComboBox.SelectedItem as ManufacturerResponseDto;
			if (category == null || manufacturer == null)
			{
				teamComboBox.ItemsSource = null;
				return;
			}
			await _teamService.GetTeams(category.Id, manufacturer.Id);
			teamComboBox.ItemsSource = _teamService.Teams;
			if (_isInitialize)
			{
				teamComboBox.SelectedItem = _teamService.Teams.First(a => a.Name == _addEditPhotoFactory.Argument!.Team);
				CheckInitialize();
			}
			else
			{
				teamComboBox.SelectedItem = _teamService.Teams.FirstOrDefault();
			}
			teamComboBox.Items.Refresh();
		}

		private async Task SetCars()
		{
			var category = categoryComboBox.SelectedItem as CategoryResponseDto;
			var manufacturer = manufacturerComboBox.SelectedItem as ManufacturerResponseDto;
			var team = teamComboBox.SelectedItem as TeamResponseDto;
			if (category == null || manufacturer == null || team == null)
			{
				carComboBox.ItemsSource = null;
				return;
			}
			await _carService.GetCars(category.Id, manufacturer.Id, team.Id);
			carComboBox.ItemsSource = _carService.Cars;
			if (_isInitialize)
			{
				carComboBox.SelectedItem = _carService.Cars.First(a => a.Name == _addEditPhotoFactory.Argument!.Car);
				CheckInitialize();
			}
			else
			{
				carComboBox.SelectedItem = _carService.Cars.FirstOrDefault();
			}
			carComboBox.Items.Refresh();
		}

		private void CheckInitialize()
		{
			_initializeCount++;
			_isInitialize = _initializeCount < 5;
		}

		private void SetImage()
		{
			DataContext = new
			{
				Image = _imageData
			};
		}
		private void StartProgress()
		{
			mainGrid.Visibility = Visibility.Collapsed;
			progressGrid.Visibility = Visibility.Visible;
		}
		private void EndProgress()
		{
			mainGrid.Visibility = Visibility.Visible;
			progressGrid.Visibility = Visibility.Collapsed;
		}
	}
}
