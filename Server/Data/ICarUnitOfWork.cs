using System;
using Server.Data;

namespace CarAPI.Data
{
	interface ICarUnitOfWork: IDisposable
    {
	    ICarRepository Cars { get; }
		int Complete();
    }
}
