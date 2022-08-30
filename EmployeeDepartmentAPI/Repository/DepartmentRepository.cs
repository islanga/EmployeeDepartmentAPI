using EmployeeDepartmentAPI.Data;
using EmployeeDepartmentAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeDepartmentAPI.Repository
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly APIDbContext _appDBContext;
        public DepartmentRepository(APIDbContext context) => _appDBContext = context ?? throw new ArgumentNullException(nameof(context));

        public bool DeleteDepartment(int ID)
        {
            bool result = false;
            var department = _appDBContext.Departments.Find(ID);
            if (department != null)
            {
                _appDBContext.Entry(department).State = EntityState.Deleted;
                _appDBContext.SaveChanges();
                result = true;
            }
            else
            {
                result = false;
            }

            return result;
        }

        public async Task<IEnumerable<Department>> GetDepartment() => await _appDBContext.Departments.ToListAsync();

        public async Task<Department> GetDepartmentById(int id) => await _appDBContext.Departments.FindAsync(id);

        public async Task<Department> InsertDepartment(Department objDepartment)
        {
            _appDBContext.Departments.Add(objDepartment);
            await _appDBContext.SaveChangesAsync();
            return objDepartment;
        }

        public async Task<Department> UpdateDepartment(Department objDepartment)
        {
            _appDBContext.Entry(objDepartment).State = EntityState.Modified;
            await _appDBContext.SaveChangesAsync();
            return objDepartment;
        }
    }
}
