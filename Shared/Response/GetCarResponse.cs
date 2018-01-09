using NServiceBus;
using System;
using Shared.Models;

namespace Shared.Response
{
	[Serializable]
	public class GetCarResponse : IMessage
	{
		public GetCarResponse()
		{
			DataId = Guid.NewGuid();
		}
		public Guid DataId { get; set; }
		public Car Car { get; set; }
	}
}

