using System.Threading.Tasks;
using Shared.Response;

namespace Client.Services
{
	public interface IAspNetDbLocation
	{
		Task<GetAspNetDbResponse> GetAspNetDbAsync();

	}
}
