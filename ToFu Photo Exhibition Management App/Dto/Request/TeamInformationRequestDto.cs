using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToFu_Photo_Exhibition_Management_App.Shared.Dto.Request
{
	public class TeamInformationRequestDto
	{
		public TeamInformationRequestDto(int id, int teamId, int manufacturerId, int categoryId)
		{
			Id = id;
			TeamId = teamId;
			ManufacturerId = manufacturerId;
			CategoryId = categoryId;
		}
		public int Id { get; }
		public int TeamId { get; }
		public int ManufacturerId { get; }
		public int CategoryId { get; }
	}
}
