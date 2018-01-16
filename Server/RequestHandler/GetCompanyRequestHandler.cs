using NServiceBus;
using System.Threading.Tasks;
using NServiceBus.Logging;
using Shared.Requests;
using System;
using Shared.Response;
using Server.DAL;
using Microsoft.EntityFrameworkCore;
using Server.Data;
using Shared.Models;

namespace Server.Requesthandler
{
	public class GetCompanyRequestHandler : IHandleMessages<GetCompanyRequest>
	{
		readonly DbContextOptionsBuilder<CarApiContext> _dbContextOptionsBuilder;
		public GetCompanyRequestHandler(DbContextOptionsBuilder<CarApiContext> dbContextOptionsBuilder)
		{
			_dbContextOptionsBuilder = dbContextOptionsBuilder;
		}

		static ILog log = LogManager.GetLogger<GetCompanyRequest>();

		public Task Handle(GetCompanyRequest message, IMessageHandlerContext context)
		{
			log.Info("Received GetCompanyRequest");

			Company company;
			using (var unitOfWork = new CarUnitOfWork(new CarApiContext(_dbContextOptionsBuilder.Options)))
			{
				company = unitOfWork.Companies.Get(message.CompanyId);
			}
			var response = new GetCompanyResponse(message.CompanyId)
			{
				DataId = Guid.NewGuid(),
				Company = company
			};

			var reply = context.Reply(response);
			return reply;

		}
	}
}