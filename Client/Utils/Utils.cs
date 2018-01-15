using System;
using System.Linq;
using System.Threading.Tasks;
using NServiceBus;
using Shared.Requests;
using Shared.Response;
using Shared.Models;

namespace Client.Utils
{
	public class Utils
    {
	    public static Task<GetCompaniesResponse> GetCompaniesResponseAsync(IEndpointInstance endpointInstance)
	    {
		    var message = new GetCompaniesRequest();
		    var sendOptions = new SendOptions();
		    sendOptions.SetDestination("NServiceBusCore.Server");
		    var responseTask = endpointInstance
			    .Request<GetCompaniesResponse>(message, sendOptions);
		    return responseTask;
	    }

	    public static Task<GetCompanyResponse> GetCompanyResponseAsync(Guid id, IEndpointInstance endpointInstance)
	    {
		    var message = new GetCompanyRequest(id);
		    var sendOptions = new SendOptions();
		    sendOptions.SetDestination("NServiceBusCore.Server");
		    var responseTask = endpointInstance
			    .Request<GetCompanyResponse>(message, sendOptions);
		    return responseTask;
	    }


	    public static Task<CreateCarResponse> CreateCarResponseAsync(Car car, IEndpointInstance endpointInstance)
	    {
		    var message = new CreateCarRequest(car);
		    var sendOptions = new SendOptions();
		    sendOptions.SetDestination("NServiceBusCore.Server");
		    var responseTask = endpointInstance
			    .Request<CreateCarResponse>(message, sendOptions);
		    return responseTask;
	    }

	    public static Task<GetCarResponse> GetCarResponseAsync(Guid id, IEndpointInstance endpointInstance)
	    {
		    var message = new GetCarRequest(id);
		    var sendOptions = new SendOptions();
		    sendOptions.SetDestination("NServiceBusCore.Server");
		    var responseTask = endpointInstance
			    .Request<GetCarResponse>(message, sendOptions);
		    return responseTask;
	    }

	    public static Task<DeleteCarResponse> DeleteCarResponseAsync(Guid id, IEndpointInstance endpointInstance)
		{
		    var message = new DeleteCarRequest(id);
		    var sendOptions = new SendOptions();
		    sendOptions.SetDestination("NServiceBusCore.Server");
		    var responseTask = endpointInstance
			    .Request<DeleteCarResponse>(message, sendOptions);
		    return responseTask;
	    }

	    public static Task<UpdateCarResponse> UpdateCarResponseAsync(Car car, IEndpointInstance endpointInstance)
		{
		    var message = new UpdateCarRequest(car);
		    var sendOptions = new SendOptions();
		    sendOptions.SetDestination("NServiceBusCore.Server");
		    var responseTask = endpointInstance
			    .Request<UpdateCarResponse>(message, sendOptions);
		    return responseTask;
	    }

	    public static Task<GetCarsResponse> GetCarsResponseAsync(IEndpointInstance endpointInstance)
	    {
		    var message = new GetCarsRequest();
		    var sendOptions = new SendOptions();
		    sendOptions.SetDestination("NServiceBusCore.Server");
		    var responseTask = endpointInstance
			    .Request<GetCarsResponse>(message, sendOptions);
		    return responseTask;
	    }
	    public async Task<bool> CompanyExistsAsync(Guid id, IEndpointInstance endpointInstance)
		{
		    var getCompaniesResponse = await GetCompaniesResponseAsync(endpointInstance);
		    var companies = getCompaniesResponse.Companies;
		    return companies.Any(e => e.Id == id);
	    }

	    public static Task<CreateCompanyResponse> CreateCompanyResponseAsync(Company company, IEndpointInstance endpointInstance)
	    {
		    var message = new CreateCompanyRequest(company);
		    var sendOptions = new SendOptions();
		    sendOptions.SetDestination("NServiceBusCore.Server");
		    var responseTask = endpointInstance
			    .Request<CreateCompanyResponse>(message, sendOptions);
		    return responseTask;
	    }

	    public static Task<DeleteCompanyResponse> DeleteCompanyResponseAsync(Guid id, IEndpointInstance endpointInstance)

		{
		    var message = new DeleteCompanyRequest(id);
		    var sendOptions = new SendOptions();
		    sendOptions.SetDestination("NServiceBusCore.Server");
		    var responseTask = endpointInstance
			    .Request<DeleteCompanyResponse>(message, sendOptions);
		    return responseTask;
	    }

	    public static Task<UpdateCompanyResponse> UpdateCompanyResponseAsync(Company company, IEndpointInstance endpointInstance)

		{
		    var message = new UpdateCompanyRequest(company);
		    var sendOptions = new SendOptions();
		    sendOptions.SetDestination("NServiceBusCore.Server");
		    var responseTask = endpointInstance
			    .Request<UpdateCompanyResponse>(message, sendOptions);
		    return responseTask;
	    }
	}
}
