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
	public class DeleteCompanyRequestHandler : IHandleMessages<DeleteCompanyRequest>
	{
		readonly DbContextOptionsBuilder<CarApiContext> _dbContextOptionsBuilder;
		public DeleteCompanyRequestHandler(DbContextOptionsBuilder<CarApiContext> dbContextOptionsBuilder)
		{
			_dbContextOptionsBuilder = dbContextOptionsBuilder;
		}

		static ILog log = LogManager.GetLogger<GetCompanyRequest>();

		public Task Handle(DeleteCompanyRequest message, IMessageHandlerContext context)
		{
			log.Info("Received DeleteCompanyRequest");
			using (var unitOfWork = new CarUnitOfWork(new CarApiContext(_dbContextOptionsBuilder.Options)))
			{
				unitOfWork.Companies.Remove(unitOfWork.Companies.Get(message.CompanyId));
				unitOfWork.Complete();
			}
			var response = new DeleteCompanyResponse()
			{
				DataId = Guid.NewGuid()
			};
	
			var reply = context.Reply(response);
			return reply;
		}
	}
}