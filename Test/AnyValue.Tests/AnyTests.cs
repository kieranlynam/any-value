using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AnyValue.Tests
{
    [TestClass]
    public class AnyTests
    {
        [TestMethod]
        public void StringReturnsNonEmptyValue()
        {
            var result = Any.String();

            Assert.IsFalse(string.IsNullOrEmpty(result));
        }

        [TestMethod]
        public void StringReturnsValueWithProvidedLength()
        {
            var result = Any.String(1000);

            Assert.AreEqual(1000, result.Length);
        }

        [ExpectedException(typeof(ArgumentException))]
        public void ExceptionIfProvidedZeroLength()
        {
            Any.String(0);
        }

        [ExpectedException(typeof(ArgumentException))]
        public void ExceptionIfProvidedNegativeLength(int length)
        {
            Any.String(-1);
        }

        [TestMethod]
        public void StringExceptReturnsDifferentValueFromProvided()
        {
            for (int i = 0; i < 1000; i++)
            {
                var result = Any.StringExcept("X");

                Assert.AreNotEqual("X", result);
            }
        }

        [TestMethod]
        public void PositiveIntegerReturnsPositiveValue()
        {
            for (int i = 0; i < 1000; i++)
            {
                var result = Any.PositiveInteger();

                Assert.IsTrue(result > 0);
            }
        }

        [TestMethod]
        public void PositiveIntegerReturnsPositiveValueUpToProvidedMaximum()
        {
            for (int i = 0; i < 1000; i++)
            {
                var result = Any.PositiveInteger(5);

                Assert.IsTrue(result > 0);
                Assert.IsTrue(result <= 5);
            }
        }

        [TestMethod]
        public void DateTimeReturnsNonEmptyValue()
        {
            var result = Any.DateTime();

            Assert.AreNotEqual(default(DateTime), result);
        }

        [TestMethod]
        public void DateReturnsNonEmptyValue()
        {
            var result = Any.Date();

            Assert.AreNotEqual(default(DateTime).Date, result);
        }

        [TestMethod]
        public void DateHasNoTimeComponent()
        {
            var result = Any.Date();

            Assert.AreEqual(0, result.TimeOfDay.TotalMilliseconds);
        }

        [TestMethod]
        public void EnumerationReturnsValidValue()
        {
            var result = Any.Of<SampleEnumeration>();

            Assert.IsTrue(Enum.IsDefined(typeof(SampleEnumeration), result));
        }

        [TestMethod]
        public void EnumerationDoesNotReturnExcludedValue()
        {
            for (int i = 0; i < 1000; i++)
            {
                var result = Any.Of<SampleEnumeration>(except: SampleEnumeration.Red);

                Assert.IsTrue(Enum.IsDefined(typeof(SampleEnumeration), result));
                Assert.AreNotEqual(SampleEnumeration.Red, result);
            }
        }

        [TestMethod]
        public void EnumerationDoesNotReturnAnyOfGivenExceptionCollection()
        {
            for (int i = 0; i < 1000; i++)
            {
                var result = Any.Of<SampleEnumeration>(except: new[] { SampleEnumeration.Red, SampleEnumeration.Blue });

                Assert.IsTrue(Enum.IsDefined(typeof(SampleEnumeration), result));
                Assert.AreNotEqual(SampleEnumeration.Red, result);
                Assert.AreNotEqual(SampleEnumeration.Blue, result);
            }
        }

        private enum SampleEnumeration
        {
            Red,
            Green,
            Blue
        }
    }
}