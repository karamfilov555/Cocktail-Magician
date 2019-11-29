using CM.Data;
using Microsoft.EntityFrameworkCore;

namespace CM.Services.Tests
{
    public static class TestUtils
    {
        public static DbContextOptions<CMContext> GetOptions(string databaseName)
        {
            return new DbContextOptionsBuilder<CMContext>()
                .UseInMemoryDatabase(databaseName)
                .Options;
        }
    }
}
