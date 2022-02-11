using ContactsAPI._1.Interface.Enums;
using ContactsAPI._1.Interface.ViewModels;
using ContactsAPI._2.Application.Services.Interfaces;
using ContactsAPI._3.Domain.Model;
using ContactsAPI._3.Domain.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactsAPI._2.Application.Services
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository _contactRepository;

        public ContactService(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }

        public async Task<int> Add(ContactViewModel contactViewModel)
        {
            try
            {
                if (DateTime.Today.AddYears(-18) >= contactViewModel.BirthDate &&
                    DateTime.Today > contactViewModel.BirthDate)
                {
                    Contact contact = new Contact();

                    contact = new Contact()
                    {
                        Id = contactViewModel.Id,
                        Name = contactViewModel.Name,
                        BirthDate = contactViewModel.BirthDate,
                        Sex = (int)contactViewModel.Sex,
                        Active = contactViewModel.Active
                    };
                     var res = await _contactRepository.Add(contact);
                    return res;
                }
                else
                {
                    throw new ApplicationException("Contato deve ser Maior de 18 anos.");
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<List<ContactViewModel>> Index()
        {
            try
            {
                List<ContactViewModel> contactsViewModel = new List<ContactViewModel>();

                List<Contact> contacts = await _contactRepository.Index();

                if (contacts != null && contacts.Count > 0)
                {
                    foreach (var item in contacts)
                    {
                        contactsViewModel.Add(new ContactViewModel()
                        {
                            Id = item.Id,
                            Name = item.Name,
                            BirthDate = item.BirthDate,
                            Sex = (SexEnum)item.Sex,
                            Active = item.Active
                        });
                    }
                }

                return contactsViewModel;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<ContactViewModel> Details(int id)
        {
            try
            {
                ContactViewModel contactViewModel = new ContactViewModel();

                Contact contact = await _contactRepository.Details(id);

                if (contact != null)
                {
                    contactViewModel = new ContactViewModel()
                    {
                        Id = contact.Id,
                        Name = contact.Name,
                        BirthDate = contact.BirthDate,
                        Sex = (SexEnum)contact.Sex,
                        Active = contact.Active
                    };
                }

                return contactViewModel;

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<int> Disable(int id)
        {
            try
            {
                Contact contact = await _contactRepository.Details(id);

                if (contact != null)
                    contact.Active = false;

                return await _contactRepository.Update(contact);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                return await _contactRepository.Delete(id);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
