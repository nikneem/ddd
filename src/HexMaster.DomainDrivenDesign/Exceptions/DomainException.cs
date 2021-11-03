using System;

namespace HexMaster.DomainDrivenDesign.Exceptions
{
    public class DomainException : Exception
    {
        public DomainException(string message, Exception ex = null) : base(message, ex)
        {

        }
    }
}
