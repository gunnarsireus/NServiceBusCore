using System;
using NServiceBus;

namespace Shared.Requests
{
	[Serializable]
	public class GetAspNetDbRequest : IMessage
	{
		public GetAspNetDbRequest()
		{
			DataId = Guid.NewGuid();
		}
		public Guid DataId { get; set; }
	}
}
