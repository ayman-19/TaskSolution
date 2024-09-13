using Employee.Api.Data;
using Employee.Api.Dtos;
using Employee.Api.Requests;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Employee.Testing.Tests
{
    [TestFixture]
    public class EmployeeTest
    {
        private EmployeeDbContext _context;
        private ServiceProvider _serviceProvider;

        public EmployeeTest()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddDbContext<EmployeeDbContext>(options =>
                options.UseNpgsql(
                    "host=localhost;port=5432;user id=postgres;password=password;database=TaskSolution"
                )
            );

            _serviceProvider = serviceCollection.BuildServiceProvider();
            _context = _serviceProvider.GetRequiredService<EmployeeDbContext>();
        }

        [Test]
        public async Task TestGetAllEmployee()
        {
            // Arrange

            var request = new GetAll();

            // Act
            var response = await _context
                .Employees.Select(u => new Response(
                    u.EmployeeId,
                    u.Name,
                    u.Department,
                    u.Salary,
                    "OK"
                ))
                .ToListAsync();
            List<Response> compares = [new Response(2, "Ayman Roshdy", "CS", 15000, "OK")];

            // Assert
            Assert.AreEqual(response, compares);
        }

        [Test]
        public async Task TestGetEmployeeById()
        {
            // Arrange

            var request = new GetById(2);

            // Act
            var response = await _context
                .Employees.Where(e => e.EmployeeId == 2)
                .Select(u => new Response(u.EmployeeId, u.Name, u.Department, u.Salary, "OK"))
                .FirstAsync();

            var compare = new Response(2, "Ayman Roshdy", "CS", 15000, "OK");

            // Assert
            Assert.AreEqual(response, compare);
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            _serviceProvider?.Dispose();
        }
    }
}
