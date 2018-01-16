using Shared.Requests;
using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.Logging;
using Shared.Response;
using Server.DAL;
using Server.Data;
using Microsoft.EntityFrameworkCore;

namespace Server.Requesthandler
{
	public class CreateCarRequestHandler : IHandleMessages<CreateCarRequest>
	{
		readonly DbContextOptionsBuilder<CarApiContext> _dbContextOptionsBuilder;
		public CreateCarRequestHandler(DbContextOptionsBuilder<CarApiContext> dbContextOptionsBuilder)
		{
			_dbContextOptionsBuilder = dbContextOptionsBuilder;
		}

		static ILog log = LogManager.GetLogger<CreateCarRequestHandler>();

		public Task Handle(CreateCarRequest message, IMessageHandlerContext context)
		{
			log.Info("Received CreateCarRequest.");

			var response = new CreateCarResponse()
			{
				Car = message.Car
			};

			using (var unitOfWork = new CarUnitOfWork(new CarApiContext(_dbContextOptionsBuilder.Options)))
			{
				unitOfWork.Cars.Add(message.Car);
				unitOfWork.Complete();
			}

			var reply = context.Reply(response);
			return reply;
		}
	}
}