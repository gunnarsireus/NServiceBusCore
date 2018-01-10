using Shared.Requests;
using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.Logging;
using Shared.Response;
using Server.DAL;

namespace Server.Requesthandler
{
	public class SetCarOnlineOfflineRequestHandler : IHandleMessages<SetCarOnlineOfflineRequest>
	{
		readonly CarDataAccess _carDataAccess;
		public SetCarOnlineOfflineRequestHandler()
		{
			_carDataAccess = new CarDataAccess();
		}

		static ILog log = LogManager.GetLogger<SetCarOnlineOfflineRequestHandler>();

		public Task Handle(SetCarOnlineOfflineRequest message, IMessageHandlerContext context)
		{
			log.Info("Received SetCarOnlineOfflineRequest.");

			var response = new SetCarOnlineOfflineResponse()
			{
				Car = message.Car
			};

			_carDataAccess.UpdateCar(message.Car);

			var reply = context.Reply(response);
			return reply;
		}
	}
}