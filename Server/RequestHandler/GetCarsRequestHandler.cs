using System.Linq;
using Shared.Requests;
using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.Logging;
using Shared.Response;
using Server.DAL;

namespace Server.Requesthandler
{
	public class GetCarsRequestHandler : IHandleMessages<GetCarsRequest>
	{
		readonly CarDataAccess _carDataAccess;

		public GetCarsRequestHandler()
		{
			_carDataAccess = new CarDataAccess();
		}

		static ILog log = LogManager.GetLogger<GetCarsRequestHandler>();

		public Task Handle(GetCarsRequest message, IMessageHandlerContext context)
		{
			log.Info("Received GetCarsRequest.");

			var response = new GetCarsResponse()
			{
				Cars = _carDataAccess.GetCars().ToList()
			};

			var reply = context.Reply(response);
			return reply;
		}
	}
}