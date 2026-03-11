using ContactMangerAPI.Data;
using ContactMangerAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ContactMangerAPI.Repositories
{
    public class ContactInterface : IContactInterface
    {
        private readonly AppDbContext _context;
        public ContactInterface(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Contact> CreateAsync(Contact contact)
        {
            await _context.Contacts.AddAsync(contact);
            await _context.SaveChangesAsync();

            return contact;
        }

        public async Task<Contact?> DeleteAsync(Guid id)
        {
            var contact = await _context.Contacts.FindAsync(id);

            if (contact == null)
                return null;

            _context.Contacts.Remove(contact);
            await _context.SaveChangesAsync();

            return contact;
        }

        public async Task<List<Contact>> GetAllAsync()
        {
            return await _context.Contacts.ToListAsync();
        }

        public async Task<Contact?> GetByIdAsync(Guid id)
        {
            return await _context.Contacts.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Contact?> UpdateAsync(Guid id, Contact contact)
        {
            var model = await _context.Contacts.FindAsync(id);

            if (model == null)
                return null;

            model.Name = contact.Name;
            model.Email = contact.Email;
            model.Phone = contact.Phone;
            model.Address = contact.Address;

            await _context.SaveChangesAsync();

            return contact;
        }
    }
}
