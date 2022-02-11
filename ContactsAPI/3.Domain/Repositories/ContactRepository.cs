using ContactsAPI._3.Domain.Context;
using ContactsAPI._3.Domain.Model;
using ContactsAPI._3.Domain.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactsAPI._3.Domain.Repositories
{
    public class ContactRepository : IContactRepository
    {
        private readonly ContactContext _context;

        public ContactRepository(ContactContext context)
        {
            _context = context;
        }

        public async Task<int> Add(Contact product)
        {
            await _context.AddAsync(product);
            await _context.SaveChangesAsync();

            return product.Id;
        }

        public async Task<List<Contact>> Index()
        {
            return await _context.Contacts.ToListAsync();
        }

        public async Task<Contact> Details(int id)
        {
            return await _context.Contacts.Where(w => w.Id == id).FirstOrDefaultAsync();
        }
        
        public async Task<int> Update(Contact product)
        {
            _context.Update(product);
            await _context.SaveChangesAsync();

            return product.Id;
        }

        public async Task<bool> Delete(int id)
        {
            var product = await _context.Contacts.Where(w => w.Id == id).FirstOrDefaultAsync();
            _context.Contacts.Remove(product);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
