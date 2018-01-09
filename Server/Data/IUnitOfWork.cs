using System;

namespace Server.Data
{
    interface IUnitOfWork: IDisposable
    {
	    ICarRepository Cars { get; }
		int Complete();
    }
}
