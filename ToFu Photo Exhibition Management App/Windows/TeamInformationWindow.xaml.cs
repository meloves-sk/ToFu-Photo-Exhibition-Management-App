namespace ToFu_Photo_Exhibition_Management_App.Windows
{
	/// <summary>
	/// TeamInformationWindow.xaml の相互作用ロジック
	/// </summary>
	public partial class TeamInformationWindow : Window
	{
		private readonly ICategoryService _categoryService;
		private readonly IManufacturerService _manufacturerService;
		private readonly ITeamService _teamService;
		private readonly ITeamInformationService _teamInformationService;
		private TeamInformationResponseDto? _teamInformationResponse = null;
		public TeamInformationWindow(
			ICategoryService categoryService,
			IManufacturerService manufacturerService,
			ITeamService teamService,
			ITeamInformationService teamInformationService)
		{
			InitializeComponent();
			_categoryService = categoryService;
			_manufacturerService = manufacturerService;
			_teamService = teamService;
			_teamInformationService = teamInformationService;
		}
		private async void Window_Loaded(object sender, RoutedEventArgs e)
		{
			StartProgress();
			await Task.WhenAll(SetCategories(), SetManufacturers(), SetTeams());
			SetStatus();
			EndProgress();
		}

		private async void categoryComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			await SetTeamInformations();
		}

		private async void manufacturerComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			await SetTeamInformations();
		}

		private async void saveButton_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				var category = categoryComboBox.SelectedItem as CategoryResponseDto;
				if (category == null)
				{
					throw new Exception("カテゴリーを選択してください");
				}
				var manufacturer = manufacturerComboBox.SelectedItem as ManufacturerResponseDto;
				if (manufacturer == null)
				{
					throw new Exception("メーカーを選択してください");
				}
				var team = teamComboBox.SelectedItem as TeamResponseDto;
				if (team == null)
				{
					throw new Exception("チームを選択してください");
				}
				var request = new TeamInformationRequestDto(_teamInformationResponse?.Id ?? 0, team.Id, manufacturer.Id, category.Id);
				var result = _teamInformationResponse == null ?
					await _teamInformationService.AddTeamInformation(request) :
					await _teamInformationService.UpdateTeamInformation(request);
				_teamInformationResponse = null;
				SetStatus();
				await SetTeamInformations();
				MessageBox.Show(result.Message);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void resetLink_Click(object sender, RoutedEventArgs e)
		{
			_teamInformationResponse = null;
			SetStatus();
		}

		private async void editLink_Click(object sender, RoutedEventArgs e)
		{
			var teamInformation = (sender as Hyperlink)?.Tag as TeamInformationResponseDto;
			if (teamInformation == null)
			{
				return;
			}
			_teamInformationResponse = teamInformation;
			await Task.WhenAll(SetCategories(), SetManufacturers(), SetTeams());
			SetStatus();
		}
		private async void deleteLink_Click(object sender, RoutedEventArgs e)
		{
			var teamInformation = (sender as Hyperlink)?.Tag as TeamInformationResponseDto;
			if (teamInformation == null)
			{
				return;
			}
			var result = await _teamInformationService.DeleteTeamInformation(teamInformation.Id);
			await SetTeamInformations();
			MessageBox.Show(result.Message);
		}

		private void SetStatus()
		{
			if (_teamInformationResponse == null)
			{
				statusTextBlock.Text = "Unselected";
			}
			else
			{
				statusTextBlock.Text = $"Selected: {_teamInformationResponse.Category} {_teamInformationResponse.Manufacturer} {_teamInformationResponse.Team}";
			}
		}

		private async Task SetCategories()
		{
			await _categoryService.GetCategories();
			categoryComboBox.ItemsSource = _categoryService.Categories;
			if (_teamInformationResponse == null)
			{
				categoryComboBox.SelectedItem = _categoryService.Categories.FirstOrDefault();
			}
			else
			{
				categoryComboBox.SelectedItem = _categoryService.Categories.First(a => a.Name == _teamInformationResponse.Category);
			}
			categoryComboBox.Items.Refresh();
		}
		private async Task SetManufacturers()
		{
			await _manufacturerService.GetManufacturers(0);
			manufacturerComboBox.ItemsSource = _manufacturerService.Manufacturers;
			if (_teamInformationResponse == null)
			{
				manufacturerComboBox.SelectedItem = _manufacturerService.Manufacturers.FirstOrDefault();
			}
			else
			{
				manufacturerComboBox.SelectedItem = _manufacturerService.Manufacturers.First(a => a.Name == _teamInformationResponse.Manufacturer);
			}
			manufacturerComboBox.Items.Refresh();
		}

		private async Task SetTeams()
		{
			await _teamService.GetTeams(0, 0);
			teamComboBox.ItemsSource = _teamService.Teams;
			if (_teamInformationResponse == null)
			{
				teamComboBox.SelectedItem = _teamService.Teams.FirstOrDefault();
			}
			else
			{
				teamComboBox.SelectedItem = _teamService.Teams.First(a => a.Name == _teamInformationResponse.Team);
			}
			teamComboBox.Items.Refresh();
		}

		private async Task SetTeamInformations()
		{
			var category = categoryComboBox.SelectedItem as CategoryResponseDto;
			var manufacturer = manufacturerComboBox.SelectedItem as ManufacturerResponseDto;
			if (category == null || manufacturer == null)
			{
				dataGrid.ItemsSource = null;
				return;
			}
			await _teamInformationService.GetTeamInformations(category.Id, manufacturer.Id, 0);
			dataGrid.ItemsSource = _teamInformationService.TeamInformations;
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
