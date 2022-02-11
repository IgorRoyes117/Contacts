using ContactsAPI._1.Interface.Enums;
using ContactsAPI._1.Interface.ViewModels;
using ContactsAPI._2.Application.Services;
using ContactsAPI._2.Application.Services.Interfaces;
using ContactsAPI._3.Domain.Model;
using ContactsAPI._3.Domain.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContactUnitTests
{
    [TestFixture(Category = "CategoryName")]
    public class Tests
    {
        private ContactService _contactService;

        private Mock<IContactRepository> _contactRepository = new Mock<IContactRepository>();

        List<Contact> contactList = new List<Contact>();
        Contact contactDetails = new Contact();

        [SetUp]
        public void Setup()
        {
            _contactService = new ContactService(_contactRepository.Object);

            contactDetails = new Contact()
            {
                Id = 1,
                Name = "DetailsBoy",
                Sex = (int)SexEnum.Masculine,
                BirthDate = DateTime.Today.AddYears(-20),
                Active = true
            };

            contactList.Add(new Contact { 
                Id = 1,
                Name = "First",
                Sex = (int)SexEnum.Feminine,
                BirthDate = DateTime.Today.AddYears(-20),
                Active = true
            });
            contactList.Add(new Contact
            {
                Id = 2,
                Name = "Second",
                Sex = (int)SexEnum.NotApplicable,
                BirthDate = DateTime.Today.AddYears(-28),
                Active = true
            });
            contactList.Add(new Contact
            {
                Id = 3,
                Name = "Third",
                Sex = (int)SexEnum.Masculine,
                BirthDate = DateTime.Today.AddYears(-19),
                Active = true
            });
            contactList.Add(new Contact
            {
                Id = 4,
                Name = "Fourth",
                Sex = (int)SexEnum.Feminine,
                BirthDate = DateTime.Today.AddYears(-22),
                Active = false
            });
        }

        [Test]
        public async Task TestAddingValidContact()
        {            
            ContactViewModel contactViewModel = new ContactViewModel();
            Contact contact = new Contact();

            contactViewModel.Name = "ValidContact";
            contactViewModel.Sex = SexEnum.Feminine;
            contactViewModel.BirthDate = DateTime.Today.AddYears(-20);
            contactViewModel.Active = true;

            contact.Name = "ValidContact";
            contact.Sex = (int)SexEnum.Feminine;
            contact.BirthDate = DateTime.Today.AddYears(-20);
            contact.Active = true;

            _contactRepository.Setup(s => s.Add(contact)).ReturnsAsync(1);

            var res = await _contactService.Add(contactViewModel);

            Assert.AreEqual(res, 1);
        }

        [Test]
        public async Task TestAddingUnderageContact()
        {
            ContactViewModel contactViewModel = new ContactViewModel();
            Contact contact = new Contact();

            contactViewModel.Name = "UnderageContact";
            contactViewModel.Sex = SexEnum.Feminine;
            contactViewModel.BirthDate = DateTime.Today.AddYears(-16);
            contactViewModel.Active = true;

            contact.Id = 1;
            contact.Name = "UnderageContact";
            contact.Sex = (int)SexEnum.Feminine;
            contact.BirthDate = DateTime.Today.AddYears(-16);
            contact.Active = true;

            _contactRepository.Setup(s => s.Add(contact)).ReturnsAsync(1);

            var res = await _contactService.Add(contactViewModel);

            Assert.AreEqual(res, 1);
        }

        [Test]
        public async Task TestAddingInvalidBirthdateContact()
        {
            ContactViewModel contactViewModel = new ContactViewModel();
            Contact contact = new Contact();

            contactViewModel.Name = "InvalidBirthdateContact";
            contactViewModel.Sex = SexEnum.Feminine;
            contactViewModel.BirthDate = DateTime.Today.AddYears(1);
            contactViewModel.Active = true;

            contact.Id = 1;
            contact.Name = "InvalidBirthdateContact";
            contact.Sex = (int)SexEnum.Feminine;
            contactViewModel.BirthDate = DateTime.Today.AddYears(1);
            contact.Active = true;

            _contactRepository.Setup(s => s.Add(contact)).ReturnsAsync(1);

            var res = await _contactService.Add(contactViewModel);

            Assert.AreEqual(res, 1);
        }


        [Test]
        public async Task TestListingActiveContacts()
        {
            var localcontactList = new List<Contact>();

            _contactRepository.Setup(s => s.Index()).ReturnsAsync(contactList);

            var res = await _contactService.Index();

            foreach (var item in res)
            {
                if (item.Active)
                {
                    localcontactList.Add(new Contact()
                    {
                        Id = item.Id,
                        Name = item.Name,
                        BirthDate = item.BirthDate,
                        Sex = (int)item.Sex,
                        Active = item.Active
                    });
                }
            }

            Assert.AreEqual(localcontactList, contactList);
        }

        [Test]
        public async Task TestSearchForContactDetails()
        {
            _contactRepository.Setup(s => s.Details(1)).ReturnsAsync(contactDetails);

            var res = await _contactService.Details(1);

            Assert.AreEqual(res, contactDetails);
        }

        [Test]
        public async Task TestDisablingContact()
        {
            Contact contact = new Contact();

            contact.Id = 1;
            contact.Name = "d";
            contact.Sex = (int)SexEnum.Feminine;
            contact.BirthDate = DateTime.Today.AddYears(-20);
            contact.Active = true;

            _contactRepository.Setup(s => s.Update(contact)).ReturnsAsync(1);

            var res = await _contactService.Disable(1);

            Assert.AreEqual(res, 1);
        }

        [Test]
        public async Task TestDeletingContacts()
        {
            _contactRepository.Setup(s => s.Delete(1)).ReturnsAsync(true);

            var res = await _contactService.Delete(1);

            Assert.AreEqual(res, 1);
        }
    }
}