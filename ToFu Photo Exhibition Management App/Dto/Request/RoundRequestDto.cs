using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToFu_Photo_Exhibition_Management_App.Shared.Dto.Request
{
	public class RoundRequestDto
	{
		public RoundRequestDto(int id, string name, int categoryId)
		{
			Id = id;
			Name = name;
			CategoryId = categoryId;
		}
		public int Id { get; }
		public string Name { get; }
		public int CategoryId { get; }
	}
}
