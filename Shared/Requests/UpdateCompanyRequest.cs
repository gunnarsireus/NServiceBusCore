using System;
using NServiceBus;
using Shared.Models;

namespace Shared.Requests
{
	[Serializable]
	public class UpdateCompanyRequest : IMessage
	{
		public UpdateCompanyRequest(Company company)
		{
			DataId = Guid.NewGuid();
			Company = company;
		}
		public Guid DataId { get; set; }
		public Company Company;
	}
}
