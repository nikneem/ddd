using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Threading;
using HexMaster.DomainDrivenDesign.ExtensionMethods;

namespace HexMaster.DomainDrivenDesign.ValueObjects
{

    public sealed class Iban
    {
        public string Number { get; }
        public string CondensedString => Number.ToCondensedString();

        private Iban(string number)
        {
            Number = number;
        }

        public static Iban FromNumber(string number)
        {
            if (!number.IbanChecksumCheck())
            {
                throw new ArgumentException($"The number '{number}' is not a valid IBAN number", nameof(number));
            }
            return new Iban(number);
        }
    }
}
