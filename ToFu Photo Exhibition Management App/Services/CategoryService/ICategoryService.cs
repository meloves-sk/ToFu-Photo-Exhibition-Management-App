namespace ToFu_Photo_Exhibition_Management_App.Services.CategoryService
{
	public interface ICategoryService
	{
		public List<CategoryResponseDto> Categories { get; }
		public List<CategoryResponseDto> CategoriesWithAll { get; }
		public bool IsSearch { get; set; }
		Task GetCategories();
		Task GetCategoriesWithAll();
	}
}
