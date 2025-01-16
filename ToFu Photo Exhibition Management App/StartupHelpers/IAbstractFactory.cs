namespace ToFu_Photo_Exhibition_Management_App.StartupHelpers
{
	public interface IAbstractFactory<TWindow, TArgument> where TWindow : class where TArgument : class
	{
		TWindow Create();
		TArgument? Argument { get; set; }
	}
}