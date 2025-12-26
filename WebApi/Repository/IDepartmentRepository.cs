using WebApi.Models;

namespace WebApi.Repository
{
    public interface IDepartmentRepository
    {
        public void Add(Department dept);
        public void Update( Department dept, int id);
        public void Delete(int id);

        public List<Department> GetAll();

        public List<Department> GetDeptDetails();


        public Department GetById(int id);

        public Department GetByName(string  nmae);  

        public void SaveChanges();
    }

}
