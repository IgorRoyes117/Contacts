using ContactsAPI._3.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactsAPI._3.Domain.Repositories.Interfaces
{
    public interface IContactRepository
    {
        Task<int> Add(Contact contact);
        Task<List<Contact>> Index();
        Task<Contact> Details(int id);
        Task<int> Update(Contact product);
        Task<bool> Delete(int id);
    }
}
