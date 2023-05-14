namespace BidOneCodeTest.Services
{
    using BidOneCodeTest.Controllers;
    using System.IO;
    using System.Text.Json;

    public interface IPersonService
    {
        Guid AddPerson(Person person);
        void Clear();
        bool DeletePerson(Guid id);
        List<Person> GetAll();
        Person GetPerson(Guid id);
        Person UpdatePerson(Person person);
    }

    public class PersonService : IPersonService
    {
        //const string filePath = "C:\\dev\\github\\repos\\BidOne\\WebApplication1\\myJsonFile1.json";
        const string filePath = ".\\myJsonFile.json";

        public Person GetPerson(Guid id)
        {
            return GetPersonById(id, filePath);
        }

        public Guid AddPerson(Person person)
        {
            person.Id = Guid.NewGuid();
            AddPersonToCollectionAndSave(person, filePath);
            return person.Id;
        }

        public bool DeletePerson(Guid id)
        {
            try
            {
                DeletePersonById(id, filePath);
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Person UpdatePerson(Person updatedPerson)
        {
            List<Person> people = new List<Person>();
            string jsonString = string.Empty;


            if (File.Exists(filePath))
            {
                jsonString = File.ReadAllText(filePath);
            }

            if (!string.IsNullOrEmpty(jsonString))
            {
                // Deserialize the JSON string into a list of Person objects
                people = JsonSerializer.Deserialize<List<Person>>(jsonString);
            }
            
            Person personToUpdate = people.FirstOrDefault(p => p.Id == updatedPerson.Id);

            // If the person is not found, throw an exception
            if (personToUpdate == null)
            {
                throw new Exception("Person not found.");
            }

            // Update the person's data with the new data
            personToUpdate.FirstName = updatedPerson.FirstName;
            personToUpdate.LastName = updatedPerson.LastName;

            // Serialize the updated list to a JSON string
            string updatedJsonString = JsonSerializer.Serialize(people);

            // Write the JSON string to the file
            File.WriteAllText(filePath, updatedJsonString);
            return personToUpdate;
        }

        public List<Person> GetAll()
        {
            return GetAllPersons();
        }

        public void Clear()
        {
            DeleteAllPersons(filePath);
        }


        private static void AddPersonToCollectionAndSave(Person newPerson, string filePath)
        {
            List<Person> persons = new List<Person>();
            string jsonString = string.Empty;


            if (File.Exists(filePath))
            {
                jsonString = File.ReadAllText(filePath);
            }

            if (!string.IsNullOrEmpty(jsonString))
            {
                // Deserialize the JSON string into a list of Person objects
                persons = JsonSerializer.Deserialize<List<Person>>(jsonString);
            }
            // Insert the new person at the start of the list
            persons.Insert(0, newPerson);

            // Serialize the updated list to a JSON string
            string updatedJsonString = JsonSerializer.Serialize(persons);

            // Write the JSON string to the file
            File.WriteAllText(filePath, updatedJsonString);
        }

        private static Person GetPersonById(Guid id, string filePath)
        {
            // Read the existing JSON string from the file
            string jsonString = File.ReadAllText(filePath);

            // Deserialize the JSON string into a list of Person objects
            List<Person> persons = JsonSerializer.Deserialize<List<Person>>(jsonString);

            // Find the person object with the specified ID
            Person result = persons.Find(p => p.Id == id);

            return result;
        }



        private static void DeletePersonById(Guid id, string filePath)
        {
            // Read the existing JSON string from the file
            string jsonString = File.ReadAllText(filePath);

            // Deserialize the JSON string into a list of Person objects
            List<Person> persons = JsonSerializer.Deserialize<List<Person>>(jsonString);

            // Find the person object with the specified ID
            Person personToDelete = persons.Find(p => p.Id == id);

            // Remove the person object from the list
            persons.Remove(personToDelete);

            // Serialize the updated list to a JSON string
            string updatedJsonString = JsonSerializer.Serialize(persons);

            // Write the JSON string to the file
            File.WriteAllText(filePath, updatedJsonString);
        }

        private static List<Person> GetAllPersons()
        {
            string jsonString = string.Empty;
            List<Person> persons = new List<Person>();
            if (File.Exists(filePath))
            {
                jsonString = File.ReadAllText(filePath);
            }
            // Deserialize the JSON string into a list of Person objects
            if (!string.IsNullOrEmpty(jsonString))
            {
                persons = JsonSerializer.Deserialize<List<Person>>(jsonString);
            }

            return persons;
        }
       
        private static void DeleteAllPersons(string filePath)
        {
            // Create an empty list of Person objects
            List<Person> persons = new List<Person>();

            // Serialize the empty list to JSON
            string jsonString = JsonSerializer.Serialize(persons);

            // Write the JSON string to the file
            File.WriteAllText(filePath, jsonString);
        }
    }
}
