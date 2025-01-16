global using System.Windows;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Hosting;
global using System.Net.Http;
global using System.Net.Http.Json;
global using System.Windows.Controls;
global using ToFu_Photo_Exhibition_Management_App.Windows;
global using ToFu_Photo_Exhibition_Management_App.Services.CarService;
global using ToFu_Photo_Exhibition_Management_App.Services.CategoryService;
global using ToFu_Photo_Exhibition_Management_App.Services.ManufacturerService;
global using ToFu_Photo_Exhibition_Management_App.Services.PhotoService;
global using ToFu_Photo_Exhibition_Management_App.Services.RoundService;
global using ToFu_Photo_Exhibition_Management_App.Services.TeamService;
global using ToFu_Photo_Exhibition_Management_App.Services.TeamInformationService;
global using ToFu_Photo_Exhibition_Management_App.Dto.Response;
global using ToFu_Photo_Exhibition_Management_App.StartupHelpers;
global using ToFu_Photo_Exhibition_Management_App.Shared.Dto.Request;
global using ToFu_Photo_Exhibition_Management_App.Services.ApiService;
global using Microsoft.Win32;
global using System.IO;
global using System.Windows.Documents;
global using System.Web;
namespace ToFu_Photo_Exhibition_Management_App
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		public static IHost? AppHost { get; private set; }
		public App()
		{
			AppHost = Host.CreateDefaultBuilder().ConfigureServices((hostContext, services) =>
			{
				services.AddSingleton<MainWindow>();
				services.AddWindowFactory<AddEditPhotoWindow, PhotoResponseDto>();
				services.AddWindowFactory<RoundWindow, RoundResponseDto>();
				services.AddWindowFactory<ManufacturerWindow, ManufacturerResponseDto>();
				services.AddWindowFactory<TeamWindow, TeamResponseDto>();
				services.AddWindowFactory<TeamInformationWindow, TeamInformationResponseDto>();
				services.AddWindowFactory<CarWindow, CarResponseDto>();
				services.AddTransient<ICategoryService, CategoryService>();
				services.AddTransient<IRoundService, RoundService>();
				services.AddTransient<IManufacturerService, ManufacturerService>();
				services.AddTransient<ITeamService, TeamService>();
				services.AddTransient<ITeamInformationService, TeamInformationService>();
				services.AddTransient<ICarService, CarService>();
				services.AddTransient<IPhotoService, PhotoService>();
				services.AddTransient<IApiService,ApiService>();
				services.AddTransient(a => new HttpClient());
			}).Build();
			Application.Current.DispatcherUnhandledException += OnDispatcherUnhandledException;
		}
		protected override async void OnStartup(StartupEventArgs e)
		{
			await AppHost!.StartAsync();
			var startupForm = AppHost.Services.GetRequiredService<MainWindow>();
			startupForm.Show();
			base.OnStartup(e);
		}

		protected override async void OnExit(ExitEventArgs e)
		{
			await AppHost!.StopAsync();
			base.OnExit(e);
		}

		private void OnDispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
		{
			var message = e.Exception.InnerException != null ? e.Exception.InnerException.Message : e.Exception.Message;
			MessageBox.Show("申し訳ありません。\n" +
				"お使いのアプリケーションは異常を検知したため終了します\n" +
				"-- エラー内容 --\n" +
				message,
				"ToFu Photo Exhibition Management App 異常終了");
			Environment.Exit(1);
		}

	}


}
