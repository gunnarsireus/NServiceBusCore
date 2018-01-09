using Shared.Requests;
using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.Logging;
using Shared.Response;
using Server.DAL;

namespace Server.Requesthandler
{
	public class CreateCarRequestHandler : IHandleMessages<CreateCarRequest>
	{
		readonly CarDataAccess _carDataAccess;
		public CreateCarRequestHandler()
		{
			_carDataAccess = new CarDataAccess();
		}

		static ILog log = LogManager.GetLogger<CreateCarRequestHandler>();

		public Task Handle(CreateCarRequest message, IMessageHandlerContext context)
		{
			log.Info("Received CreateCarRequest.");

			var response = new CreateCarResponse()
			{
				Car = message.Car
			};

			_carDataAccess.AddCar(message.Car);

			var reply = context.Reply(response);
			return reply;
		}
	}
}