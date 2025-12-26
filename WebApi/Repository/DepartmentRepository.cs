using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace WebApi.Repository
{
    
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly ApplicationContext context;

        public DepartmentRepository(ApplicationContext _context)
        {
            context = _context;
        }

        public void Add(Department dept)
        {
           context.Department.Add(dept);
            context.SaveChanges();
        }

        public void Delete(int id)
        {
          Department dept = GetById(id);
            if (dept != null)
            {
                context.Department.Remove(dept);
                context.SaveChanges();
            }
        }

        public List<Department> GetAll()
        {
           return  context.Department.ToList();
        }

        public Department GetById(int id)
        {
          return context.Department.FirstOrDefault(x=>x.Id == id);
        }

       

        public void Update(Department dept, int id)
        {
            Department deptold = GetById(id);
            if (deptold != null)
            {
                context.Department.Update(deptold);
                context.SaveChanges();
            }
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }

        public Department GetByName(string nmae)
        {
          return context.Department.FirstOrDefault(c=>c.Name  == nmae);
        }

        public List<Department> GetDeptDetails()
        {
           return  context.Department.Include(d=>d.Emps).ToList();

        }
    }
}
