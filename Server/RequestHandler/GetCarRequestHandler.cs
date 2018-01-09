using Shared.Requests;
using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.Logging;
using Shared.Response;
using Server.DAL;

namespace Server.Requesthandler
{
	public class GetCarRequestHandler : IHandleMessages<GetCarRequest>
	{
		readonly CarDataAccess _carDataAccess;

		public GetCarRequestHandler()
		{
			_carDataAccess = new CarDataAccess();
		}

		static ILog log = LogManager.GetLogger<GetCarRequestHandler>();

		public Task Handle(GetCarRequest message, IMessageHandlerContext context)
		{
			log.Info("Received GetCarRequest.");

			var response = new GetCarResponse()
			{
				Car = _carDataAccess.GetCar(message.CarId)
			};
			var reply = context.Reply(response);
			return reply;
		}
	}
}