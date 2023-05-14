namespace BidOneCodeTest.Controllers
{
    using BidOneCodeTest.Models;
    using BidOneCodeTest.Services;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Diagnostics;

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPersonService personService;
        private readonly List<Person> people;

        public HomeController(ILogger<HomeController> logger, IPersonService personService)
        {
            _logger = logger;
            this.personService = personService;
            people = this.personService.GetAll();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Index()
        {
            return View(people);
        }

        public IActionResult Create()
        {
            var person = new Person();

            return View(person);
        }

        [HttpPost]
        public IActionResult Create(Person person)
        {
            var index = people.FindIndex(p => p.Id == person.Id);

            if (index == -1)
            {
                person.Id = Guid.NewGuid();
                this.personService.AddPerson(person);
            }
            else
            {
                this.personService.UpdatePerson(person);
            }

            return RedirectToAction("Index");
        }

        public IActionResult Edit(Guid id)
        {
            var person = people.Find(p => p.Id == id);

            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        [HttpPost]
        public IActionResult Edit(Person person)
        {
            var index = people.FindIndex(p => p.Id == person.Id);

            if (index == -1)
            {
                this.personService.AddPerson(person);
            }
            else
            {
                this.personService.UpdatePerson(person);
            }

            return RedirectToAction("Index");
        }

        public IActionResult Delete(Guid id)
        {
            var person = people.Find(p => p.Id == id);

            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(Guid id)
        {
            var person = people.Find(p => p.Id == id);

            if (person == null)
            {
                return NotFound();
            }

            this.personService.DeletePerson(id);

            return RedirectToAction("Index");
        }
    }

    public class Person
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}