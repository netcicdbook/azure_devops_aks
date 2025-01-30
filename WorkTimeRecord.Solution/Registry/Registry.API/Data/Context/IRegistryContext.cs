using Microsoft.EntityFrameworkCore;
using Registry.API.Domain;

namespace Registry.API.Data.Context
{
    public interface IRegistryContext
    {
        DbSet<UserWorkTimeRecord> UserWorkTimeRecords { get;}
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
