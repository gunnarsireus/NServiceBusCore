using NServiceBus;
using System.Threading.Tasks;
using NServiceBus.Logging;
using Shared.Requests;
using Shared.Response;
using Server.DAL;
using Microsoft.EntityFrameworkCore;
using Server.Data;

namespace Server.Requesthandler
{
	public class CreateCompanyRequestHandler : IHandleMessages<CreateCompanyRequest>
	{
		readonly DbContextOptionsBuilder<CarApiContext> _dbContextOptionsBuilder;
		public CreateCompanyRequestHandler(DbContextOptionsBuilder<CarApiContext> dbContextOptionsBuilder)
		{
			_dbContextOptionsBuilder = dbContextOptionsBuilder;
		}

		static ILog log = LogManager.GetLogger<CreateCompanyRequest>();

		public Task Handle(CreateCompanyRequest message, IMessageHandlerContext context)
		{
			log.Info("Received CreateCompanyRequest");

			using (var unitOfWork = new CarUnitOfWork(new CarApiContext(_dbContextOptionsBuilder.Options)))
			{
				unitOfWork.Companies.Add(message.Company);
				unitOfWork.Complete();
			}

			var response = new CreateCompanyResponse()
			{
				Company = message.Company
			};

			var reply = context.Reply(response);
			return reply;
		}
	}
}