any-value
=========

This can be used in a test to indicate that a particular value is not relevant to behaviour under test, i.e. it could be *any* value without affecting the test result.

For example:

    public class Person
    {
        private readonly DateTime dateOfBirth;
        private readonly string name;

        public Person(string name, DateTime dateOfBirth)
        {
            this.dateOfBirth = dateOfBirth;
            this.name = name;
        }

        public int CalculateAge()
        {
            return (DateTime.Today - dateOfBirth).TotalYears;
        }
    }

    public class PersonTests
    {
        public void TestAgeCalculation()
        {
            var person = new Person(
                name: Any.String(), /* not relevant to age calc */
                dateOfBirth: DateTime.Today.AddYears(-10));
            Assert.AreEqual(10, person.CalculateAge());
        }
    }
[![Build status](https://ci.appveyor.com/api/projects/status/7m55r6uwx45c8p1j)](https://ci.appveyor.com/project/KieranLynam/any-value)
