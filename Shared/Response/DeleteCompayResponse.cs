using NServiceBus;
using System;

namespace Shared.Response
{
	[Serializable]
	public class DeleteCompanyResponse : IMessage
	{
		public DeleteCompanyResponse()
		{
			DataId = Guid.NewGuid();
		}
		public Guid DataId { get; set; }
	}
}

