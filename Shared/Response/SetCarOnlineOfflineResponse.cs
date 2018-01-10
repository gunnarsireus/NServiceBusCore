using NServiceBus;
using System;
using Shared.Models;

namespace Shared.Response
{
	[Serializable]
	public class SetCarOnlineOfflineResponse : IMessage
	{
		public SetCarOnlineOfflineResponse()
		{
			DataId = Guid.NewGuid();
		}
		public Guid DataId { get; set; }
		public Car Car { get; set; }
	}
}

