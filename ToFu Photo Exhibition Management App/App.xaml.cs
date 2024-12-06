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
global using ToFu_Photo_Exhibition_Management_App.Dto.Response;
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
				services.AddSingleton<AddEditPhotoWindow>();
				services.AddTransient<ICategoryService, CategoryService>();
				services.AddTransient<IRoundService, RoundService>();
				services.AddTransient<IManufacturerService, ManufacturerService>();
				services.AddTransient<ITeamService, TeamService>();
				services.AddTransient<ICarService, CarService>();
				services.AddTransient<IPhotoService, PhotoService>();
			}).Build();
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
	}


}
