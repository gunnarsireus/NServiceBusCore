using Shared.Requests;
using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.Logging;
using Shared.Response;
using Server.Data;
using Server.DAL;

namespace Server.Requesthandler
{
	using System;

	public class DeleteCarRequestHandler : IHandleMessages<DeleteCarRequest>
	{
		readonly CarDataAccess _carDataAccess;

		public DeleteCarRequestHandler()
		{
			_carDataAccess = new CarDataAccess();
		}

		static ILog log = LogManager.GetLogger<DeleteCarRequestHandler>();

		public Task Handle(DeleteCarRequest message, IMessageHandlerContext context)
		{
			log.Info("Received DeleteCarRequest.");
			_carDataAccess.DeleteCar(message.CarId);

			var response = new DeleteCarResponse()
			{
				DataId = Guid.NewGuid()
			};
			var reply = context.Reply(response);
			return reply;
		}
	}
}