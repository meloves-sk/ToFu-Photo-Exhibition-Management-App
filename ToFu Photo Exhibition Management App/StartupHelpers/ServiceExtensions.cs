namespace ToFu_Photo_Exhibition_Management_App.StartupHelpers
{
	public static class ServiceExtensions
	{
		public static void AddWindowFactory<TWindow, TArgument>(this IServiceCollection services) where TWindow : class where TArgument : class
		{
			services.AddTransient<TWindow>();
			services.AddSingleton<Func<TWindow>>(a => () => a.GetService<TWindow>());
			services.AddSingleton<IAbstractFactory<TWindow, TArgument>, AbstractFactory<TWindow, TArgument>>();
		}
	}
}
