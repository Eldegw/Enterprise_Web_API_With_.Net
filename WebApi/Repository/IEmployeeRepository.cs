using WebApi.Models;

namespace WebApi.Repository
{
    public interface IEmployeeRepository
    {
        public Employee GetById(int id);
    }
}
