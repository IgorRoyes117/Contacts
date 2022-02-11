using ContactsAPI._1.Interface.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactsAPI._2.Application.Services.Interfaces
{
    public interface IContactService
    {
        Task<int> Add(ContactViewModel contactViewModel);
        Task<List<ContactViewModel>> Index();
        Task<ContactViewModel> Details(int id);
        Task<int> Disable(int id);
        Task<bool> Delete(int id);
    }
}
