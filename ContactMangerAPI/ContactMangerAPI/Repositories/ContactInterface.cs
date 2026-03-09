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

        public async Task<List<Contact>> GetAllAsync()
        {
            return await _context.Contacts.ToListAsync();
        }

        public async Task<Contact?> GetByIdAsync(Guid id)
        {
            return await _context.Contacts.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
