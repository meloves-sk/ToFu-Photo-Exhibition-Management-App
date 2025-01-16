namespace ToFu_Photo_Exhibition_Management_App.Windows
{
	/// <summary>
	/// RoundWindow.xaml の相互作用ロジック
	/// </summary>
	public partial class RoundWindow : Window
	{
		private readonly ICategoryService _categoryService;
		private readonly IRoundService _roundService;
		private RoundResponseDto? _roundResponse = null;
		public RoundWindow(
			ICategoryService categoryService,
			IRoundService roundService)
		{
			InitializeComponent();
			_categoryService = categoryService;
			_roundService = roundService;
		}

		private async void Window_Loaded(object sender, RoutedEventArgs e)
		{
			StartProgress();
			await SetCategories();
			SetStatus();
			EndProgress();
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
				if (roundTextBox.Text == string.Empty)
				{
					throw new Exception("ラウンドを入力してください");
				}
				var request = new RoundRequestDto(_roundResponse?.Id ?? 0, roundTextBox.Text, category.Id);
				var result = _roundResponse == null ?
					await _roundService.AddRound(request) :
					await _roundService.UpdateRound(request);
				roundTextBox.Text = string.Empty;
				_roundResponse = null;
				SetStatus();
				await SetRounds();
				MessageBox.Show(result.Message);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private async void categoryComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			await SetRounds();
		}
		private void resetLink_Click(object sender, RoutedEventArgs e)
		{
			roundTextBox.Text = string.Empty;
			_roundResponse = null;
			SetStatus();
		}

		private void editLink_Click(object sender, RoutedEventArgs e)
		{
			var round = (sender as Hyperlink)?.Tag as RoundResponseDto;
			if (round == null)
			{
				return;
			}
			_roundResponse = round;
			roundTextBox.Text = _roundResponse.Name;
			SetStatus();
		}
		private async void deleteLink_Click(object sender, RoutedEventArgs e)
		{
			var round = (sender as Hyperlink)?.Tag as RoundResponseDto;
			if (round == null)
			{
				return;
			}
			var result = await _roundService.DeleteRound(round.Id);
			await SetRounds();
			MessageBox.Show(result.Message);
		}
		private void SetStatus()
		{
			if (_roundResponse == null)
			{
				statusTextBlock.Text = "Unselected";
			}
			else
			{
				statusTextBlock.Text = $"Selected: {_roundResponse.Name}";
			}
		}
		private async Task SetCategories()
		{
			await _categoryService.GetCategories();
			categoryComboBox.ItemsSource = _categoryService.Categories;
			categoryComboBox.SelectedItem = _categoryService.Categories.FirstOrDefault();
			SetStatus();
		}
		private async Task SetRounds()
		{
			var category = categoryComboBox.SelectedItem as CategoryResponseDto;
			if (category == null)
			{
				dataGrid.ItemsSource = null;
				return;
			}
			await _roundService.GetRounds(category.Id);
			dataGrid.ItemsSource = _roundService.Rounds;
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
