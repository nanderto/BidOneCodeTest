using Microsoft.VisualStudio.TestTools.UnitTesting;
using BidOneCodeTest.Services;
using BidOneCodeTest.Controllers;

namespace BidOneCodeTest.Services.Tests
{
    [TestClass]
    public class PersonServiceTests
    {

        [TestMethod]
        public void AddPersonTest()
        {
            var newPerson = new Person { FirstName = "tedx", LastName = "Washingtonx" };
            var sut = new PersonService();
            var result = sut.AddPerson(newPerson);
            Assert.IsInstanceOfType(result, typeof(Guid));
            var returnedPerson = sut.GetPerson(result);
            Assert.AreEqual(result, returnedPerson.Id);
            Assert.AreEqual(newPerson.FirstName, returnedPerson.FirstName);
            Assert.AreEqual(newPerson.LastName, returnedPerson.LastName);
        }

        [TestMethod]
        public void RemovePersonTest()
        {
            var newPerson = new Person { FirstName = "tedx", LastName = "Washingtonx" };
            var sut = new PersonService();
            var result = sut.AddPerson(newPerson);
            Assert.IsInstanceOfType(result, typeof(Guid));
            var returnedPerson = sut.GetPerson(result);
            Assert.AreEqual(result, returnedPerson.Id);
            Assert.AreEqual(newPerson.FirstName, returnedPerson.FirstName);
            Assert.AreEqual(newPerson.LastName, returnedPerson.LastName);

            var ret = sut.DeletePerson(result);

            var deletedPerson = sut.GetPerson(result);
            Assert.IsNull(deletedPerson);
        }

        [TestMethod]
        public void UpdatePersonTest()
        {
            var newPerson = new Person { FirstName = "tedx", LastName = "Washingtonx" };
            var sut = new PersonService();
            var result = sut.AddPerson(newPerson);
            Assert.IsInstanceOfType(result, typeof(Guid));
            var returnedPerson = sut.GetPerson(result);
            Assert.AreEqual(result, returnedPerson.Id);
            Assert.AreEqual(newPerson.FirstName, returnedPerson.FirstName);
            Assert.AreEqual(newPerson.LastName, returnedPerson.LastName);

            returnedPerson.FirstName = "XXXX";
            returnedPerson.LastName = "ZZZZ";
            var ret = sut.UpdatePerson(returnedPerson);

            var UpdatePerson = sut.GetPerson(result);
            Assert.AreEqual(result, returnedPerson.Id);
            Assert.AreEqual(UpdatePerson.FirstName, returnedPerson.FirstName);
            Assert.AreEqual(UpdatePerson.LastName, returnedPerson.LastName);
        }

        [TestMethod]
        public void TestGetAllPersons()
        {
            // Arrange
            //string filePath = @"C:\temp\myJsonFile.json";
            List<Person> expectedPersons = new List<Person>
            {
                new Person { Id = new Guid(), FirstName = "Alice",  LastName = "Bob" },
                new Person { Id = new Guid(), FirstName = "Alice", LastName = "Bob" },
                new Person { Id = new Guid(), FirstName = "Charlie",  LastName = "Bob" }
            };

            var sut = new PersonService();
            sut.Clear();
            foreach (var expectedPerson in expectedPersons)
            {
                sut.AddPerson(expectedPerson);
            }

            // Act
            List<Person> actualPersons = sut.GetAll();

            // Assert
            Assert.AreEqual(expectedPersons.Count, actualPersons.Count);

            //for (int i = 0; i < expectedPersons.Count; i++)
            //{
            //    Assert.AreEqual(expectedPersons[i].Id, actualPersons[i].Id);
            //    Assert.AreEqual(expectedPersons[i].FirstName, actualPersons[i].FirstName);
            //}
        }
    }
}