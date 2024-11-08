using System;

namespace HexMaster.DomainDrivenDesign.Exceptions;
    public class DomainException(string message, Exception ex = null) : Exception(message, ex);
