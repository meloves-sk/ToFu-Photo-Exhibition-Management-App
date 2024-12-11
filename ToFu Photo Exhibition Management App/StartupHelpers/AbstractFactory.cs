namespace ToFu_Photo_Exhibition_Management_App.StartupHelpers
{
	public class AbstractFactory<TWindow, TArgument> : IAbstractFactory<TWindow, TArgument> where TWindow : class where TArgument : class
	{
		private Func<TWindow> _factory;

		public TArgument? Argument { get; set; }
		public AbstractFactory(Func<TWindow> factory)
		{
			_factory = factory;
		}

		public TWindow Create()
		{
			return _factory();
		}
	}
}
