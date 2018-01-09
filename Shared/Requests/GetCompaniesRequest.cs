using System;
using NServiceBus;

namespace Shared.Requests
{
	[Serializable]
	public class GetCompaniesRequest : IMessage
	{
		public GetCompaniesRequest()
		{
			DataId = Guid.NewGuid();
		}
		public Guid DataId { get; set; }
	}
}
