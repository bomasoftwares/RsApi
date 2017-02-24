using Boma.RedeSocial.Crosscut.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boma.RedeSocial.Crosscut.AssertConcern
{
    public class AssertConcern
    {
        public static void AssertArgumentEquals(object object1, object object2, string message)
            => AssertArgumentEquals<DomainException>(object1, object2, message);

        public static void AssertArgumentFalse(bool boolValue, string message)
            => AssertArgumentFalse<DomainException>(boolValue, message);

        public static void AssertArgumentNotEmpty(string stringValue, string message)
            => AssertArgumentNotEmpty<DomainException>(stringValue, message);

        public static void AssertArgumentNotEquals(object object1, object object2, string message)
            => AssertArgumentNotEquals<DomainException>(object1, object2, message);

        public static void AssertArgumentNotNull(object object1, string message)
            => AssertArgumentNotNull<DomainException>(object1, message);

        public static void AssertArgumentTrue(bool boolValue, string message)
            => AssertArgumentTrue<DomainException>(boolValue, message);

        public static void AssertArgumentNotGuidEmpty(Guid value, string message)
            => AssertArgumentNotGuidEmpty<DomainException>(value, message);

        public static void AssertArgumentEquals<TException>(object object1, object object2, string message)
            where TException : Exception
        {
            if (!object1.Equals(object2))
                throw (TException)Activator.CreateInstance(typeof(TException), message);
        }

        public static void AssertArgumentFalse<TException>(bool boolValue, string message) where TException : Exception
        {
            if (boolValue)
                throw (TException)Activator.CreateInstance(typeof(TException), message);
        }

        public static void AssertArgumentNotEmpty<TException>(string stringValue, string message)
            where TException : Exception
        {
            if (string.IsNullOrWhiteSpace(stringValue))
                throw (TException)Activator.CreateInstance(typeof(TException), message);
        }

        public static void AssertArgumentNotEquals<TException>(object object1, object object2, string message)
            where TException : Exception
        {
            if (object1.Equals(object2))
                throw (TException)Activator.CreateInstance(typeof(TException), message);
        }

        public static void AssertArgumentNotNull<TException>(object object1, string message)
            where TException : Exception
        {
            if (object1 == null)
                throw (TException)Activator.CreateInstance(typeof(TException), message);
        }

        public static void AssertArgumentTrue<TException>(bool boolValue, string message) where TException : Exception
        {
            if (!boolValue)
                throw (TException)Activator.CreateInstance(typeof(TException), message);
        }

        public static void AssertArgumentNotGuidEmpty<TException>(Guid value, string message) where TException: Exception
        {
            if (value == Guid.Empty) throw (TException)Activator.CreateInstance(typeof(TException), message);
        }
       
    }
}
