using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Groupproj.Models.Account
{
        public class IdentityDataContext : IdentityDbContext<IdentityUser>
        {
            public IdentityDataContext(DbContextOptions<IdentityDataContext> options)
                : base(options)
            {
                Database.EnsureCreated();
            }
        }
    }
