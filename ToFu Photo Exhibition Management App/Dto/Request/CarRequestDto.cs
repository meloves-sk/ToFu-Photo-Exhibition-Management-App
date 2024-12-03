using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToFu_Photo_Exhibition_Management_App.Shared.Dto.Request
{
	public class CarRequestDto
	{
		public CarRequestDto(int id, string name, int carNo, int teamInformationId)
		{
			Id = id;
			Name = name;
			CarNo = carNo;
			TeamInformationId = teamInformationId;
		}
		public int Id { get; }
		public string Name { get; }
		public int CarNo { get; }
		public int TeamInformationId { get; }
	}
}
