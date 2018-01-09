using NServiceBus;
using System;
using Shared.Models;

namespace Shared.Response
{
	[Serializable]
	public class UpdateCarResponse : IMessage
	{
		public UpdateCarResponse()
		{
			DataId = Guid.NewGuid();
		}
		public Guid DataId { get; set; }
		public Car Car { get; set; }
	}
}

