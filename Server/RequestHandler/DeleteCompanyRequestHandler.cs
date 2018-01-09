using NServiceBus;
using System.Threading.Tasks;
using NServiceBus.Logging;
using Shared.Requests;
using System;
using Shared.Response;
using Server.DAL;

namespace Server.Requesthandler
{
	public class DeleteCompanyRequestHandler : IHandleMessages<DeleteCompanyRequest>
	{
		readonly CarDataAccess _carDataAccess;

		public DeleteCompanyRequestHandler()
		{
			_carDataAccess = new CarDataAccess();
		}

		static ILog log = LogManager.GetLogger<GetCompanyRequest>();

		public Task Handle(DeleteCompanyRequest message, IMessageHandlerContext context)
		{
			log.Info("Received DeleteCompanyRequest");
			_carDataAccess.DeleteCompany(message.CompanyId);

			var response = new DeleteCompanyResponse()
			{
				DataId = Guid.NewGuid()
			};
	
			var reply = context.Reply(response);
			return reply;
		}
	}
}