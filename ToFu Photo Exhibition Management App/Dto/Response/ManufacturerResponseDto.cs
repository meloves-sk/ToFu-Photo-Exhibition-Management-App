﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToFu_Photo_Exhibition_Management_App.Shared.Dto.Response
{
	public class ManufacturerResponseDto
	{
		public ManufacturerResponseDto(int id, string name)
		{
			Id = id;
			Name = name;
		}
		public int Id { get; }

		public string Name { get; }

	}
}