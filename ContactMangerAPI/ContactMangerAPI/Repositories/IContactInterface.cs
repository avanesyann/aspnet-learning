using ContactMangerAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace ContactMangerAPI.Repositories
{
    public interface IContactInterface
    {
        Task<List<Contact>> GetAllAsync(string? filterOn, string? filterQuery, string? sortBy, bool isAscending);
        Task<Contact?> GetByIdAsync(Guid id);
        Task<Contact> CreateAsync(Contact contact);
        Task<Contact?> UpdateAsync(Guid id, Contact contact);
        Task<Contact?> DeleteAsync(Guid id);
    }
}
