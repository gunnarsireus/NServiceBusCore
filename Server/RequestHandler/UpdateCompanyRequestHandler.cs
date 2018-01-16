using NServiceBus;
using System.Threading.Tasks;
using NServiceBus.Logging;
using Shared.Requests;
using System;
using Shared.Response;
using Server.DAL;
using Microsoft.EntityFrameworkCore;
using Server.Data;

namespace Server.Requesthandler
{
	public class UpdateCompanyRequestHandler : IHandleMessages<UpdateCompanyRequest>
	{
		readonly DbContextOptionsBuilder<CarApiContext> _dbContextOptionsBuilder;
		public UpdateCompanyRequestHandler(DbContextOptionsBuilder<CarApiContext> dbContextOptionsBuilder)
		{
			_dbContextOptionsBuilder = dbContextOptionsBuilder;
		}
		static ILog log = LogManager.GetLogger<UpdateCompanyRequest>();

		public Task Handle(UpdateCompanyRequest message, IMessageHandlerContext context)
		{
			log.Info("Received UpdateCompanyRequest");
			using (var unitOfWork = new CarUnitOfWork(new CarApiContext(_dbContextOptionsBuilder.Options)))
			{
				unitOfWork.Companies.Update(message.Company);
				unitOfWork.Complete();
			}
			var response = new UpdateCompanyResponse()
			{
				DataId = Guid.NewGuid(),
				Company = message.Company
			};

			var reply = context.Reply(response);
			return reply;

		}
	}
}