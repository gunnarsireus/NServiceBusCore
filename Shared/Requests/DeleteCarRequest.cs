using System;
using NServiceBus;

namespace Shared.Requests
{
	[Serializable]
	public class DeleteCarRequest : IMessage
	{
		public DeleteCarRequest(Guid id)
		{
			DataId = Guid.NewGuid();
			CarId = id;
		}
		public Guid DataId { get; set; }
		public Guid CarId;
	}
}
