
using DomainLayer.Models;
using ServiceLayer.ViewModels;

namespace ServiceLayer.EmployeeService
{
    public interface IEmployeeService
    {
        public DTParameters GetAllEmployeeData(DTParameters dtParameters);
        public void AddEmployee(EmployeeViewModel employeeModel);
        public void DeleteEmployeeData(int employeeId);
        public void UpdateEmployee(EmployeeViewModel employeeModel);
        public EmployeeViewModel GetEmployeeViewModelById(int employeeId);
        public Employee GetEmployeeById(int employeeId);
        public void AddPosition(PositionModel positionModel);
        public List<Position> GetAllPositions();
        public List<EmployeeHistoryViewModel> GetAllHistories(int employeeId);
        public EmployeeHistory GetHistoryById(int historyId);
        public void UpdateEmployeeHistory(EmployeeHistoryViewModel employeeHistoryModel);
        public void AddNewJob(EmployeeHistoryViewModel employeeHistoryModel);
        public void UpdateOldJob(EmployeeHistoryViewModel employeeHistoryModel);


    }
}
