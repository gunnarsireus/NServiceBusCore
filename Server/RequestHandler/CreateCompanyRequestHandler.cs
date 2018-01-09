using NServiceBus;
using System.Threading.Tasks;
using NServiceBus.Logging;
using Shared.Requests;
using Shared.Response;
using Server.DAL;

namespace Server.Requesthandler
{
	public class CreateCompanyRequestHandler : IHandleMessages<CreateCompanyRequest>
	{
		readonly CarDataAccess _carDataAccess;

		public CreateCompanyRequestHandler()
		{
			_carDataAccess = new CarDataAccess();
		}

		static ILog log = LogManager.GetLogger<CreateCompanyRequest>();

		public Task Handle(CreateCompanyRequest message, IMessageHandlerContext context)
		{
			log.Info("Received CreateCompanyRequest");

			var response = new CreateCompanyResponse()
			{
				Company = message.Company
			};

			_carDataAccess.AddCompany(message.Company);

			var reply = context.Reply(response);
			return reply;

		}
	}
}