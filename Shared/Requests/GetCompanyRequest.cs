using System;
using NServiceBus;

namespace Shared.Requests
{
	[Serializable]
	public class GetCompanyRequest : IMessage
	{
		public GetCompanyRequest(Guid id)
		{
			DataId = Guid.NewGuid();
			CompanyId = id;
		}
		public Guid DataId { get; set; }
		public Guid CompanyId;
	}
}
