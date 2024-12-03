using System.Windows;
using System.Xml.Serialization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ToFu_Photo_Exhibition_Management_App.Services.CarService;
using ToFu_Photo_Exhibition_Management_App.Services.CategoryService;
using ToFu_Photo_Exhibition_Management_App.Services.ManufacturerService;
using ToFu_Photo_Exhibition_Management_App.Services.PhotoService;
using ToFu_Photo_Exhibition_Management_App.Services.RoundService;
using ToFu_Photo_Exhibition_Management_App.Services.TeamService;
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
