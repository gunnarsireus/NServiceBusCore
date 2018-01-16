using System;

namespace Server.Data
{
	internal interface ICarUnitOfWork: IDisposable
    {
	    int Complete();
    }
}
