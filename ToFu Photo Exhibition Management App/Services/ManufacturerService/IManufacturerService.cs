﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToFu_Photo_Exhibition_Management_App.Shared.Dto.Response;

namespace ToFu_Photo_Exhibition_Management_App.Services.ManufacturerService
{
	public interface IManufacturerService
	{
		Task<ServiceResponse<IEnumerable<ManufacturerResponseDto>>> GetFilterManufacturers(int categoryId);
	}
}