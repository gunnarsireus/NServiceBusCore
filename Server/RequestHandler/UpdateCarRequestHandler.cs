using Shared.Requests;
using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.Logging;
using Shared.Response;
using Server.DAL;

namespace Server.Requesthandler
{
	public class UpdateCarRequestHandler : IHandleMessages<UpdateCarRequest>
	{
		readonly CarDataAccess _carDataAccess;

		public UpdateCarRequestHandler()
		{
			_carDataAccess = new CarDataAccess();
		}

		static ILog log = LogManager.GetLogger<UpdateCarRequestHandler>();

		public Task Handle(UpdateCarRequest message, IMessageHandlerContext context)
		{
			log.Info("Received UpdateCarRequest.");

			_carDataAccess.UpdateCar(message.Car);

			var response = new UpdateCarResponse
			{
				Car = message.Car
			};
			var reply = context.Reply(response);
			return reply;
		}
	}
}