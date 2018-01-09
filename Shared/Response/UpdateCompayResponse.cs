using NServiceBus;
using System;

namespace Shared.Response
{
	using Shared.Models;

	[Serializable]
	public class UpdateCompanyResponse : IMessage
	{
		public UpdateCompanyResponse()
		{
			DataId = Guid.NewGuid();
		}
		public Guid DataId { get; set; }
		public Company Company { get; set; }
	}
}

