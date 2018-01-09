using NServiceBus;
using System;

namespace Shared.Response
{
	[Serializable]
	public class GetAspNetDbResponse : IMessage
	{
		public GetAspNetDbResponse()
		{
			DataId = Guid.NewGuid();
		}
		public Guid DataId { get; set; }
		public string AspNetDb { get; set; }

		public static implicit operator string(GetAspNetDbResponse v)
		{
			throw new NotImplementedException();
		}
	}
}

