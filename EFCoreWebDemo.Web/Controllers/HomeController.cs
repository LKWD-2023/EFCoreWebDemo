using EFCoreWebDemo.Data;
using EFCoreWebDemo.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Diagnostics;

namespace EFCoreWebDemo.Web.Controllers
{
    public class HomeController : Controller
    {
        private string _connectionString;

        public HomeController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConStr");
        }

        public IActionResult Index()
        {
            var repo = new PersonRepository(_connectionString);
            return View(new HomePageViewModel
            {
                People = repo.GetPeople()
            });
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Person person)
        {
            var repo = new PersonRepository(_connectionString);
            repo.Add(person);
            return Redirect("/");
        }


        public IActionResult Edit(int id)
        {
            var repo = new PersonRepository(_connectionString);
            Person person = repo.GetById(id);
            if (person == null)
            {
                return Redirect("/home/index");
            }

            EditPageViewModel vm = new EditPageViewModel
            {
                Person = person
            };

            return View(vm);
        }

        [HttpPost]
        public IActionResult Update(Person person)
        {
            var repo = new PersonRepository(_connectionString);
            repo.Update(person);
            return Redirect("/");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var repo = new PersonRepository(_connectionString);
            repo.Delete(id);
            return Redirect("/");
        }
    }
}