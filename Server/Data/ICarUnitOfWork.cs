using System;

namespace CarAPI.Data
{
	internal interface ICarUnitOfWork: IDisposable
    {
	    int Complete();
    }
}
