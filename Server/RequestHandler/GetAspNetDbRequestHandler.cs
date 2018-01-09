using Shared.Requests;
using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.Logging;
using Shared.Response;

namespace Server.Requesthandler
{
	using System.IO;

	public class GetAspNetDbRequestHandler : IHandleMessages<GetAspNetDbRequest>
	{
		static ILog log = LogManager.GetLogger<GetAspNetDbRequestHandler>();

		public Task Handle(GetAspNetDbRequest message, IMessageHandlerContext context)
		{
			log.Info("Received GetAspNetDbRequest.");

			var response = new GetAspNetDbResponse()
			{
				AspNetDb = Directory.GetCurrentDirectory() + "\\App_Data\\AspNet.db"
			};

			var reply = context.Reply(response);
			return reply;
		}
	}
}