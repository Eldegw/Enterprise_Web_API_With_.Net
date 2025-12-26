using WebApi.Models;

namespace WebApi.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationContext context;

        public EmployeeRepository(ApplicationContext _context)
        {
            context = _context;
        }
        public Employee GetById(int id)
        {
            return context.Employee.FirstOrDefault(x => x.Id == id);
        }
    }
}
