using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToFu_Photo_Exhibition_Management_App.Shared.Dto.Response;

namespace ToFu_Photo_Exhibition_Management_App.Services.TeamService
{
	class TeamService : ITeamService
	{
		public async Task<ServiceResponse<IEnumerable<TeamResponseDto>>> GetFilterTeams(int categoryId, int manufacturerId)
		{
			return await Api.Get<ServiceResponse<IEnumerable<TeamResponseDto>>>($"api/team/category/{categoryId}/manufacturer/{manufacturerId}");
		}
	}
}
