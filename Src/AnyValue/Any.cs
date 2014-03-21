using System;
using System.Text;

namespace AnyValue
{
    /// <summary>
    /// Generates random values for use in tests.
    /// </summary>
    /// <remarks>
    /// Used to indicate that a particular value is not relevant to the behavior
    /// under test, i.e. it could be "any" value without affecting the test result.
    /// <example>
    /// In the following test, the person's name is not relevant to the behavior under test:
    /// <code>
    ///     public class Person
    ///     {
    ///         private readonly DateTime dateOfBirth;
    ///         private readonly string name;
    ///
    ///         public Person(string name, DateTime dateOfBirth)
    ///         {
    ///             this.dateOfBirth = dateOfBirth;
    ///             this.name = name;
    ///         }
    ///
    ///         public int CalculateAge()
    ///         {
    ///             return (DateTime.Today - dateOfBirth).TotalYears;
    ///         }
    ///     }
    ///
    ///     public class PersonTests
    ///     {
    ///         public void TestAgeCalculation()
    ///         {
    ///             var person = new Person(
    ///                 name: Any.String(), /* not relevant to age calc */
    ///                 dateOfBirth: DateTime.Today.AddYears(-10));
    ///             Assert.AreEqual(10, person.CalculateAge());
    ///         }
    ///     }
    /// </code>
    /// </example>
    /// </remarks>
    public static class Any
    {
        private const int DefaultStringLength = 10;

        private static readonly Random Rand = new Random();

        /// <summary>
        /// Gets a random string.
        /// </summary>
        /// <returns>The string.</returns>
        public static string String()
        {
            return String(DefaultStringLength);
        }

        /// <summary>
        /// Gets a random string with the given <paramref name="length"/>.
        /// </summary>
        /// <param name="length">The length of the string. Must be positive.</param>
        /// <returns>The string.</returns>
        public static string String(int length)
        {
            var builder = new StringBuilder();
            for (int i = 0; i < length; i++)
            {
                char ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * Rand.NextDouble() + 65)));
                builder.Append(ch);
            }

            return builder.ToString();
        }

        /// <summary>
        /// Gets a random string which is different from <paramref name="except"/>.
        /// </summary>
        /// <returns>The string.</returns>
        public static string StringExcept(string except)
        {
            return String(except.Length + 1);
        }

        /// <summary>
        /// Gets a random integer.
        /// </summary>
        /// <returns>The integer value. Can be negative, positive or zero.</returns>
        public static int Integer()
        {
            return Rand.Next(Int32.MinValue, Int32.MaxValue);
        }

        /// <summary>
        /// Gets a random positive integer.
        /// </summary>
        /// <returns>The positive (non-zero) integer.</returns>
        public static int PositiveInteger()
        {
            return PositiveInteger(Int32.MaxValue);
        }

        /// <summary>
        /// Gets a random positive integer up to the given inclusive <paramref name="maximum"/>.
        /// </summary>
        /// <returns>
        /// A positive (non-zero) integer, up to and including <paramref name="maximum"/>.
        /// </returns>
        public static int PositiveInteger(int maximum)
        {
            while (true)
            {
                var result = Rand.Next(maximum);

                if (result != 0)
                {
                    return result;
                }
            }
        }

        /// <summary>
        /// Gets a random boolean value.
        /// </summary>
        /// <returns>The boolean.</returns>
        public static bool Boolean()
        {
            int value = Rand.Next(2); // produces either 0 or 1
            return Convert.ToBoolean(value);
        }

        /// <summary>
        /// Gets a random date.
        /// </summary>
        /// <returns>The date (with blank time component).</returns>
        public static DateTime Date()
        {
            return System.DateTime.Today.AddDays(-5000 + PositiveInteger(10000));
        }

        /// <summary>
        /// Gets a random date and time.
        /// </summary>
        /// <returns>The date and time.</returns>
        public static DateTime DateTime()
        {
            return Date().AddMinutes(PositiveInteger(1440));
        }

        /// <summary>
        /// Gets a random enumeration value.
        /// </summary>
        /// <typeparam name="TEnum">The enumeration type.</typeparam>
        /// <returns>The enumeration value.</returns>
        public static TEnum Enumeration<TEnum>() where TEnum : struct, IConvertible
        {
            Array enumValues = Enum.GetValues(typeof(TEnum));
            return (TEnum)enumValues.GetValue(Rand.Next(0, enumValues.Length));
        }
    }
}
