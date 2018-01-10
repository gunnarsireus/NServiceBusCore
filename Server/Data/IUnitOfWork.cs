using System;

namespace Server.Data
{
    interface IUnitOfWork: IDisposable
    {
	    int Complete();
    }
}
