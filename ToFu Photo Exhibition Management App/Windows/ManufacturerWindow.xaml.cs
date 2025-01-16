namespace ToFu_Photo_Exhibition_Management_App.Windows
{
	/// <summary>
	/// ManufacturerWindow.xaml の相互作用ロジック
	/// </summary>
	public partial class ManufacturerWindow : Window
	{
		private readonly IManufacturerService _manufacturerService;
		private ManufacturerResponseDto? _manufacturerResponse = null;
		public ManufacturerWindow(IManufacturerService manufacturerService)
		{
			InitializeComponent();
			_manufacturerService = manufacturerService;
		}
		private async void Window_Loaded(object sender, RoutedEventArgs e)
		{
			StartProgress();
			await SetManufacturers();
			SetStatus();
			EndProgress();
		}
		private async void saveButton_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				if (manufacturerTextBox.Text == string.Empty)
				{
					throw new Exception("メーカーを入力してください");
				}
				var request = new ManufacturerRequestDto(_manufacturerResponse?.Id ?? 0, manufacturerTextBox.Text);
				var result = _manufacturerResponse == null ?
					await _manufacturerService.AddManufacturer(request) :
					await _manufacturerService.UpdateManufacturer(request);
				manufacturerTextBox.Text = string.Empty;
				_manufacturerResponse = null;
				SetStatus();
				await SetManufacturers();
				MessageBox.Show(result.Message);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void resetLink_Click(object sender, RoutedEventArgs e)
		{
			manufacturerTextBox.Text = string.Empty;
			_manufacturerResponse = null;
			SetStatus();
		}

		private void editLink_Click(object sender, RoutedEventArgs e)
		{
			var manufacturer = (sender as Hyperlink)?.Tag as ManufacturerResponseDto;
			if (manufacturer == null)
			{
				return;
			}
			_manufacturerResponse = manufacturer;
			manufacturerTextBox.Text = _manufacturerResponse.Name;
			SetStatus();
		}

		private async void deleteLink_Click(object sender, RoutedEventArgs e)
		{
			var manufacturer = (sender as Hyperlink)?.Tag as ManufacturerResponseDto;
			if (manufacturer == null)
			{
				return;
			}
			var result = await _manufacturerService.DeleteManufacturer(manufacturer.Id);
			await SetManufacturers();
			MessageBox.Show(result.Message);
		}
		private void SetStatus()
		{
			if (_manufacturerResponse == null)
			{
				statusTextBlock.Text = "Unselected";
			}
			else
			{
				statusTextBlock.Text = $"Selected: {_manufacturerResponse.Name}";
			}
		}
		private async Task SetManufacturers()
		{
			await _manufacturerService.GetManufacturers(0);
			dataGrid.ItemsSource = _manufacturerService.Manufacturers;
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
