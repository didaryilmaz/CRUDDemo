using CRUDDemo.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRUDDemo.Abstract
{
    public interface IEmployeeService
    {
        Task<List<Employee>> GetAllEmployeesAsync();
        Task<Employee> GetEmployeeByIdAsync(int id);
        Task CreateEmployeeAsync(Employee employee);
        Task UpdateEmployeeAsync(int id, Employee employee);
        Task DeleteEmployeeAsync(int id);
    }
}
