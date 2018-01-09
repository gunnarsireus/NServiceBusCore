using NServiceBus;
using System.Threading.Tasks;
using NServiceBus.Logging;
using Shared.Requests;
using System;
using Shared.Response;
using Server.DAL;

namespace Server.Requesthandler
{
	public class UpdateCompanyRequestHandler : IHandleMessages<UpdateCompanyRequest>
	{
		readonly CarDataAccess _carDataAccess;
		public UpdateCompanyRequestHandler()
		{
			_carDataAccess = new CarDataAccess();
		}

		static ILog log = LogManager.GetLogger<UpdateCompanyRequest>();

		public Task Handle(UpdateCompanyRequest message, IMessageHandlerContext context)
		{
			log.Info("Received UpdateCompanyRequest");

			_carDataAccess.UpdateCompany(message.Company);

			var response = new UpdateCompanyResponse()
			{
				DataId = Guid.NewGuid(),
				Company = message.Company
			};
	
			var reply = context.Reply(response);
			return reply;

		}
	}
}