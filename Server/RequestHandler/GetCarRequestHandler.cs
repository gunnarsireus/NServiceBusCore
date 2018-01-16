using Shared.Requests;
using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.Logging;
using Shared.Response;
using Server.DAL;
using Microsoft.EntityFrameworkCore;
using Server.Data;
using Shared.Models;

namespace Server.Requesthandler
{
	public class GetCarRequestHandler : IHandleMessages<GetCarRequest>
	{
		readonly DbContextOptionsBuilder<CarApiContext> _dbContextOptionsBuilder;
		public GetCarRequestHandler(DbContextOptionsBuilder<CarApiContext> dbContextOptionsBuilder)
		{
			_dbContextOptionsBuilder = dbContextOptionsBuilder;
		}

		static ILog log = LogManager.GetLogger<GetCarRequestHandler>();

		public Task Handle(GetCarRequest message, IMessageHandlerContext context)
		{
			log.Info("Received GetCarRequest.");
			Car car;
			using (var unitOfWork = new CarUnitOfWork(new CarApiContext(_dbContextOptionsBuilder.Options)))
			{
				car = unitOfWork.Cars.Get(message.CarId);
			}
			var response = new GetCarResponse()
			{
				Car = car
			};
			var reply = context.Reply(response);
			return reply;
		}
	}
}