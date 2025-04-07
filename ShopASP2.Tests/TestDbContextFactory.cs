using Microsoft.EntityFrameworkCore;
using ShopASP2.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopASP2.Tests
{
    public static class TestDbContextFactory
    {
        public static ShopASP2DBContext Create()
        {
            var options = new DbContextOptionsBuilder<ShopASP2DBContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            return new ShopASP2DBContext(options);
        }
    }
}
