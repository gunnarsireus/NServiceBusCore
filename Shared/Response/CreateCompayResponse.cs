using NServiceBus;
using System;

using Shared.Models;

namespace Shared.Response
{
	[Serializable]
	public class CreateCompanyResponse : IMessage
	{
		public CreateCompanyResponse()
		{
			DataId = Guid.NewGuid();
		}
		public Guid DataId { get; set; }
		public Company Company { get; set; }
	}
}

