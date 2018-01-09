using NServiceBus;
using System.Threading.Tasks;
using NServiceBus.Logging;
using Shared.Requests;
using System;
using Shared.Response;
using System.Linq;
using Server.DAL;

namespace Server.Requesthandler
{
	public class GetCompaniesRequestHandler : IHandleMessages<GetCompaniesRequest>
	{
		readonly CarDataAccess _carDataAccess;

		public GetCompaniesRequestHandler()
		{
			_carDataAccess = new CarDataAccess();
		}


		static ILog log = LogManager.GetLogger<GetCompaniesRequest>();

		public Task Handle(GetCompaniesRequest message, IMessageHandlerContext context)
		{
			log.Info("Received GetCompaniesRequest");

			var response = new GetCompaniesResponse
			{
				DataId = Guid.NewGuid(),
				Companies = _carDataAccess.GetCompanies().ToList()
			};

			var reply = context.Reply(response);
			return reply;

		}
	}
}