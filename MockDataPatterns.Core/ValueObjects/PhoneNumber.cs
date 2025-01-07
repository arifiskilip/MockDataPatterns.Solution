using System;

namespace MockDataPatterns.Core.ValueObjects
{
    /// <summary>
    /// Telefon numarası bilgisini tutan ve formatlayan value object
    /// </summary>
    public class PhoneNumber
    {
        public string Value { get; }

        public PhoneNumber(string number)
        {
            if (string.IsNullOrWhiteSpace(number))
                throw new ArgumentException("Phone number cannot be empty");

            // Basit bir format doğrulama
            if (!number.StartsWith("+"))
                throw new ArgumentException("Phone number must start with +");

            Value = number;
        }

        public override string ToString() => Value;
    }
}
