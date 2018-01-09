using NServiceBus;
using System;

namespace Shared.Response
{
	[Serializable]
	public class DeleteCarResponse : IMessage
	{
		public DeleteCarResponse()
		{
			DataId = Guid.NewGuid();
		}
		public Guid DataId { get; set; }
	}
}

