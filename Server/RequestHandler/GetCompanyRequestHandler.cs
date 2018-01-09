using NServiceBus;
using System.Threading.Tasks;
using NServiceBus.Logging;
using Shared.Requests;
using System;
using Shared.Response;
using Server.DAL;

namespace Server.Requesthandler
{
	public class GetCompanyRequestHandler : IHandleMessages<GetCompanyRequest>
	{
		readonly CarDataAccess _carDataAccess;

		public GetCompanyRequestHandler()
		{
			_carDataAccess = new CarDataAccess();
		}

		static ILog log = LogManager.GetLogger<GetCompanyRequest>();

		public Task Handle(GetCompanyRequest message, IMessageHandlerContext context)
		{
			log.Info("Received GetCompanyRequest");

			var response = new GetCompanyResponse(message.CompanyId)
			{
				DataId = Guid.NewGuid(),
				Company = _carDataAccess.GetCompany(message.CompanyId)
			};

			var reply = context.Reply(response);
			return reply;

		}
	}
}