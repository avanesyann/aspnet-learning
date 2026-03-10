using ContactMangerAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace ContactMangerAPI.Repositories
{
    public interface IContactInterface
    {
        Task<List<Contact>> GetAllAsync();
        Task<Contact?> GetByIdAsync(Guid id);
        Task<Contact> CreateAsync(Contact contact);
        Task<Contact?> DeleteAsync(Guid id);
    }
}
