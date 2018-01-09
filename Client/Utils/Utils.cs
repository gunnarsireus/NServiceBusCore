using System;
using System.Linq;
using System.Threading.Tasks;
using NServiceBus;
using Shared.Models;
using Shared.Requests;
using Shared.Response;


namespace Client.Utils
{

	public class Utils
    {
	    public static Task<GetCompaniesResponse> GetCompaniesResponseAsync()
	    {
		    var message = new GetCompaniesRequest();
		    var sendOptions = new SendOptions();
		    sendOptions.SetDestination("NServiceBusCore.Server");
		    var endpointInstance = Startup.EndpointInstance;
		    var responseTask = endpointInstance
			    .Request<GetCompaniesResponse>(message, sendOptions);
		    return responseTask;
	    }

	    public static Task<GetCompanyResponse> GetCompanyResponseAsync(Guid id)
	    {
		    var message = new GetCompanyRequest(id);
		    var sendOptions = new SendOptions();
		    sendOptions.SetDestination("NServiceBusCore.Server");
		    var endpointInstance = Startup.EndpointInstance;
		    var responseTask = endpointInstance
			    .Request<GetCompanyResponse>(message, sendOptions);
		    return responseTask;
	    }


	    public static Task<CreateCarResponse> CreateCarResponseAsync(Car car)
	    {
		    var message = new CreateCarRequest(car);
		    var sendOptions = new SendOptions();
		    sendOptions.SetDestination("NServiceBusCore.Server");
		    var endpointInstance = Startup.EndpointInstance;
		    var responseTask = endpointInstance
			    .Request<CreateCarResponse>(message, sendOptions);
		    return responseTask;
	    }

	    public static Task<GetCarResponse> GetCarResponseAsync(Guid id)
	    {
		    var message = new GetCarRequest(id);
		    var sendOptions = new SendOptions();
		    sendOptions.SetDestination("NServiceBusCore.Server");
		    var endpointInstance = Startup.EndpointInstance;
		    var responseTask = endpointInstance
			    .Request<GetCarResponse>(message, sendOptions);
		    return responseTask;
	    }

	    public static Task<DeleteCarResponse> DeleteCarResponseAsync(Guid id)
	    {
		    var message = new DeleteCarRequest(id);
		    var sendOptions = new SendOptions();
		    sendOptions.SetDestination("NServiceBusCore.Server");
		    var endpointInstance = Startup.EndpointInstance;
		    var responseTask = endpointInstance
			    .Request<DeleteCarResponse>(message, sendOptions);
		    return responseTask;
	    }

	    public static Task<UpdateCarResponse> UpdateCarResponseAsync(Car car)
	    {
		    var message = new UpdateCarRequest(car);
		    var sendOptions = new SendOptions();
		    sendOptions.SetDestination("NServiceBusCore.Server");
		    var endpointInstance = Startup.EndpointInstance;
		    var responseTask = endpointInstance
			    .Request<UpdateCarResponse>(message, sendOptions);
		    return responseTask;
	    }

	    public static Task<GetCarsResponse> GetCarsResponseAsync()
	    {
		    var message = new GetCarsRequest();
		    var sendOptions = new SendOptions();
		    sendOptions.SetDestination("NServiceBusCore.Server");
		    var endpointInstance = Startup.EndpointInstance;
		    var responseTask = endpointInstance
			    .Request<GetCarsResponse>(message, sendOptions);
		    return responseTask;
	    }
	    public async Task<bool> CompanyExistsAsync(Guid id)
	    {
		    var getCompaniesResponse = await GetCompaniesResponseAsync();
		    var companies = getCompaniesResponse.Companies;
		    return companies.Any(e => e.Id == id);
	    }

	    public static Task<CreateCompanyResponse> CreateCompanyResponseAsync(Company company)
	    {
		    var message = new CreateCompanyRequest(company);
		    var sendOptions = new SendOptions();
		    sendOptions.SetDestination("NServiceBusCore.Server");
		    var endpointInstance = Startup.EndpointInstance;
		    var responseTask = endpointInstance
			    .Request<CreateCompanyResponse>(message, sendOptions);
		    return responseTask;
	    }

	    public static Task<DeleteCompanyResponse> DeleteCompanyResponseAsync(Guid id)
	    {
		    var message = new DeleteCompanyRequest(id);
		    var sendOptions = new SendOptions();
		    sendOptions.SetDestination("NServiceBusCore.Server");
		    var endpointInstance = Startup.EndpointInstance;
		    var responseTask = endpointInstance
			    .Request<DeleteCompanyResponse>(message, sendOptions);
		    return responseTask;
	    }

	    public static Task<UpdateCompanyResponse> UpdateCompanyResponseAsync(Company company)
	    {
		    var message = new UpdateCompanyRequest(company);
		    var sendOptions = new SendOptions();
		    sendOptions.SetDestination("NServiceBusCore.Server");
		    var endpointInstance = Startup.EndpointInstance;
		    var responseTask = endpointInstance
			    .Request<UpdateCompanyResponse>(message, sendOptions);
		    return responseTask;
	    }
	}
}
