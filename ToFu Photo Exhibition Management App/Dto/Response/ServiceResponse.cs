using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToFu_Photo_Exhibition_Management_App.Dto.Response
{
	public class ServiceResponse<T>
	{
		public ServiceResponse(T data, bool success, string message)
		{
			Data = data;
			Success = success;
			Message = message;
		}
		public T Data { get; }
		public bool Success { get; }
		public string Message { get; }
	}
}
