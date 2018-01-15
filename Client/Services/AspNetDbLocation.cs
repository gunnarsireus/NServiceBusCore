using System.Threading.Tasks;
using NServiceBus;
using Shared.Requests;
using Shared.Response;

namespace Client.Services
{
	// This class is used by the application to get the location of the AspNet.db used for authntication of users.
	public class AspNetDbLocation : IAspNetDbLocation
    {
	    public async Task<GetAspNetDbResponse> GetAspNetDbAsync(IEndpointInstance endpointInstance)
	    {
		    var message = new GetAspNetDbRequest();
		    var sendOptions = new SendOptions();
		    sendOptions.SetDestination("NServiceBusCore.Server");
		    var responseTask = await endpointInstance
			    .Request<GetAspNetDbResponse>(message, sendOptions);
		    return responseTask;
		}
	}
}
