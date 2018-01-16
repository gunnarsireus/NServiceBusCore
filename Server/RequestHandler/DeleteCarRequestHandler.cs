using Shared.Requests;
using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.Logging;
using Shared.Response;
using Server.Data;
using Server.DAL;
using Microsoft.EntityFrameworkCore;
using System;

namespace Server.Requesthandler
{
	public class DeleteCarRequestHandler : IHandleMessages<DeleteCarRequest>
	{
		readonly DbContextOptionsBuilder<CarApiContext> _dbContextOptionsBuilder;
		public DeleteCarRequestHandler(DbContextOptionsBuilder<CarApiContext> dbContextOptionsBuilder)
		{
			_dbContextOptionsBuilder = dbContextOptionsBuilder;
		}

		static ILog log = LogManager.GetLogger<DeleteCarRequestHandler>();

		public Task Handle(DeleteCarRequest message, IMessageHandlerContext context)
		{
			log.Info("Received DeleteCarRequest.");
			using (var unitOfWork = new CarUnitOfWork(new CarApiContext(_dbContextOptionsBuilder.Options)))
			{
				unitOfWork.Cars.Remove(unitOfWork.Cars.Get(message.CarId));
				unitOfWork.Complete();
			}
			var response = new DeleteCarResponse()
			{
				DataId = Guid.NewGuid()
			};
			var reply = context.Reply(response);
			return reply;
		}
	}
}