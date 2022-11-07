using DomainLayer.Models;
using FluentValidation.Results;
using ServiceLayer.ViewModels;
using ServiceLayer.ViewModels.Validators;
using System.Linq.Dynamic.Core;

namespace ServiceLayer.EmployeeService
{
    public class EmployeeService: IEmployeeService
    {
        private readonly ApplicationDbContext _context;

        public EmployeeService(ApplicationDbContext context)
        {
            _context = context;
        }
        public DTParameters GetAllEmployeeData(DTParameters dt)
        {
            int pageSize = dt.Length != null ? Convert.ToInt32(dt.Length) : 0;
            int skip = dt.Start != null ? Convert.ToInt32(dt.Start) : 0;
            int recordsTotal = 0;

            var employeeData = from person in _context.People
                               join emp in _context.Employees
                               on person.PeopleId equals emp.PeopleId
                               join position in _context.Positions
                               on emp.PositionId equals position.PositionId
                               where emp.IsDisabled == false

                               select new EmployeeViewModel
                               {
                                   EmployeeId = emp.EmployeeId,
                                   FullName = person.FirstName + " " + person.MiddleName + " " + person.LastName,
                                   Email = person.Email,
                                   Address = person.Address,
                                   EmployeeCode = emp.EmployeeCode,
                                   PositionName = position.Name,
                                   Salary = emp.Salary,
                                   StartDate = emp.StartDate,
                                   ViewStartDate = emp.StartDate.ToShortDateString(),
                               };
            if (!string.IsNullOrEmpty(dt.SearchValue))
            {
                employeeData = employeeData.Where(m => m.Email.Contains(dt.SearchValue) || m.EmployeeCode.Contains(dt.SearchValue));
            } 

            if (!string.IsNullOrEmpty(dt.SortColumn) && !string.IsNullOrEmpty(dt.SortColumnDirection))
            {
                employeeData = employeeData.OrderBy(dt.SortColumn + " " + dt.SortColumnDirection);
            }
            recordsTotal = employeeData.Count();
            
            var data = employeeData.Skip(skip).Take(pageSize).ToList();

            DTParameters toSend = new DTParameters()
            {
                Draw = dt.Draw,
                RecordsTotal= recordsTotal,
                EmployeeRecords = employeeData.ToList(),
            };

            return toSend;
        }
        public void AddEmployee(EmployeeViewModel employee)
        {
            People people = new People()
            {
                PeopleId = employee.PeopleId,
                FirstName = employee.FirstName,
                MiddleName = employee.MiddleName,
                LastName = employee.LastName,
                Email = employee.Email,
                Address = employee.Address
            };
            _context.Add(people);
            _context.SaveChanges();

            Employee emp = new Employee()
            {
                EmployeeId = employee.EmployeeId,
                PeopleId = people.PeopleId,
                PositionId = employee.PositionId,
                Salary = employee.Salary,
                StartDate = DateTime.Today,
                IsDisabled = false,
                EmployeeCode = employee.EmployeeCode,
            };
            _context.Add(emp);
            _context.SaveChanges();

            EmployeeHistory history = new EmployeeHistory()
            {
                EmployeeHistoryId = employee.EmployeeHistoryId,
                EmployeeId = emp.EmployeeId,
                PositionId = employee.PositionId,
                StartDate = DateTime.Today,
            };
            _context.Add(history);
            _context.SaveChanges();
        }
        public void DeleteEmployeeData(int employeeId)
        {
            var employee = GetEmployeeById(employeeId);
            employee.IsDisabled = true;

            _context.Employees?.Update(employee);
            _context.SaveChanges();
        }

        public void UpdateEmployee(EmployeeViewModel employee)
        {
            People people = new People()
            {
                PeopleId = employee.PeopleId,
                FirstName = employee.FirstName,
                MiddleName = employee.MiddleName,
                LastName = employee.LastName,
                Email = employee.Email,
                Address = employee.Address
            };
            _context.Update(people);
            _context.SaveChanges();
        }

        public EmployeeViewModel GetEmployeeViewModelById(int employeeId)
        {
            var employeeData = (from person in _context.People
                                join emp in _context.Employees
                                on person.PeopleId equals emp.PeopleId
                                join position in _context.Positions
                                on emp.PositionId equals position.PositionId
                                where emp.IsDisabled == false && emp.EmployeeId == employeeId

                                select new EmployeeViewModel
                                {
                                    EmployeeId = emp.EmployeeId,
                                    PeopleId = emp.PeopleId,
                                    PositionId = emp.PositionId,
                                    FirstName = person.FirstName,
                                    MiddleName = person.MiddleName,
                                    LastName = person.LastName,
                                    Email = person.Email,
                                    Address = person.Address,
                                    EmployeeCode = emp.EmployeeCode,
                                    PositionName = position.Name,
                                    Salary = emp.Salary,
                                    StartDate = emp.StartDate,
                                    EndDate = emp.EndDate,
                                }).FirstOrDefault();
            return employeeData;
        }

        public Employee GetEmployeeById(int employeeId)
        {
            var employee = _context.Employees?.FirstOrDefault(m => m.EmployeeId == employeeId);
            return employee;
        }

        public void AddPosition(PositionModel positionModel)
        {
            Position position = new Position()
            {
                PositionId = positionModel.PositionId,
                Name = positionModel.PositionName,
            };
            _context.Add(position);
            _context.SaveChanges();

        }
        public List<Position> GetAllPositions()
        {
            var positions = _context.Positions.ToList();
            return positions;
        }

        public List<EmployeeHistoryViewModel> GetAllHistories(int employeeId)
        {
            var employeehistory = (from emp in _context.Employees
                                join history in _context.EmployeeHistories on
                                emp.EmployeeId equals history.EmployeeId
                                join pos in _context.Positions on
                                history.PositionId equals pos.PositionId
                                where history.EmployeeId == employeeId
                                select new EmployeeHistoryViewModel
                                {
                                    EmployeeId = history.EmployeeId,
                                    PositionId = history.PositionId,
                                    EmployeeHistoryId = history.EmployeeHistoryId,
                                    PositionName = pos.Name,
                                    ViewStartDate = history.StartDate.ToShortDateString(),
                                    ViewEndDate = history.EndDate.ToShortDateString()
                                }).ToList();
            return (employeehistory);
        }


        public EmployeeHistory GetHistoryById(int historyId)
        {
            return (_context.EmployeeHistories.FirstOrDefault(h => h.EmployeeHistoryId == historyId));
        }

        public void UpdateEmployeeHistory(EmployeeHistoryViewModel employeeHistoryModel)
        {
            EmployeeHistory employeeHistory = new EmployeeHistory()
            {
                EmployeeHistoryId = employeeHistoryModel.EmployeeHistoryId,
                EmployeeId = employeeHistoryModel.EmployeeId,
                PositionId = employeeHistoryModel.PositionId,
                StartDate = employeeHistoryModel.StartDate,
                EndDate = employeeHistoryModel.EndDate,

            };
            _context.EmployeeHistories?.Update(employeeHistory);
            _context.SaveChanges();
        }
        public void AddNewJob(EmployeeHistoryViewModel employeeHistory)
        {
            EmployeeHistory history = new EmployeeHistory()
            {
                EmployeeHistoryId = employeeHistory.EmployeeHistoryId,
                EmployeeId = employeeHistory.EmployeeId,
                PositionId = employeeHistory.PositionId,
                StartDate = DateTime.Today,
            };
            _context.EmployeeHistories.Add(history);
            _context.SaveChanges();

            var employee = GetEmployeeById(employeeHistory.EmployeeId);

            employee.Salary = employeeHistory.Salary;
            employee.StartDate = DateTime.Today;
            employee.PositionId = employeeHistory.PositionId;
            _context.Update(employee);
            _context.SaveChanges();
        }

        public void UpdateOldJob(EmployeeHistoryViewModel history)
        {
            var employee = GetEmployeeById(history.EmployeeId);

            var oldHistory = _context.EmployeeHistories.FirstOrDefault(h => h.EndDate == employee.EndDate && h.EmployeeId == history.EmployeeId);

            oldHistory.EndDate = DateTime.Today;

            _context.Update(oldHistory);
            _context.SaveChanges();
        }

        public ValidationResult Validate(EmployeeViewModel employee)
        {
            EmployeeValidator ev = new EmployeeValidator(_context);
            ValidationResult result = ev.Validate(employee);
            return result;
        }
    }
}
