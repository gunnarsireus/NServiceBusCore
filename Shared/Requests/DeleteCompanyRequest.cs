using System;
using NServiceBus;

namespace Shared.Requests
{
	[Serializable]
	public class DeleteCompanyRequest : IMessage
	{
		public DeleteCompanyRequest(Guid id)
		{
			DataId = Guid.NewGuid();
			CompanyId = id;
		}
		public Guid DataId { get; set; }
		public Guid CompanyId;
	}
}
