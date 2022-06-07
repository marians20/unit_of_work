using Microsoft.EntityFrameworkCore;

namespace Uow.Data
{
    public class UowContext : DbContext
    {
        public UowContext(DbContextOptions options) : base(options)
        {
            
        }
    }
}