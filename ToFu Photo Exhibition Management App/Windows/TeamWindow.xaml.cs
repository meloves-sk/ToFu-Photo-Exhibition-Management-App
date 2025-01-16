namespace ToFu_Photo_Exhibition_Management_App.Windows
{
	/// <summary>
	/// TeamWindow.xaml の相互作用ロジック
	/// </summary>
	public partial class TeamWindow : Window
	{
		private readonly ITeamService _teamService;
		private TeamResponseDto? _teamResponse = null;
		public TeamWindow(ITeamService teamService)
		{
			InitializeComponent();
			_teamService = teamService;
		}
		private async void Window_Loaded(object sender, RoutedEventArgs e)
		{
			StartProgress();
			await SetTeams();
			SetStatus();
			EndProgress();
		}

		private async void saveButton_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				if (teamTextBox.Text == string.Empty)
				{
					throw new Exception("チームを入力してください");
				}
				var request = new TeamRequestDto(_teamResponse?.Id ?? 0, teamTextBox.Text);
				var result = _teamResponse == null ?
					await _teamService.AddTeam(request) :
					await _teamService.UpdateTeam(request);
				teamTextBox.Text = string.Empty;
				_teamResponse = null;
				SetStatus();
				await SetTeams();
				MessageBox.Show(result.Message);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void resetLink_Click(object sender, RoutedEventArgs e)
		{
			teamTextBox.Text = string.Empty;
			_teamResponse = null;
			SetStatus();
		}

		private void editLink_Click(object sender, RoutedEventArgs e)
		{
			var team = (sender as Hyperlink)?.Tag as TeamResponseDto;
			if (team == null)
			{
				return;
			}
			_teamResponse = team;
			teamTextBox.Text = _teamResponse.Name;
			SetStatus();
		}

		private async void deleteLink_Click(object sender, RoutedEventArgs e)
		{
			var team = (sender as Hyperlink)?.Tag as TeamResponseDto;
			if (team == null)
			{
				return;
			}
			var result = await _teamService.DeleteTeam(team.Id);
			await SetTeams();
			MessageBox.Show(result.Message);
		}
		private void SetStatus()
		{
			if (_teamResponse == null)
			{
				statusTextBlock.Text = "Unselected";
			}
			else
			{
				statusTextBlock.Text = $"Selected: {_teamResponse.Name}";
			}
		}
		private async Task SetTeams()
		{
			await _teamService.GetTeams(0, 0);
			dataGrid.ItemsSource = _teamService.Teams;
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
