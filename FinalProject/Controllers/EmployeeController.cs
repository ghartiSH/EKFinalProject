using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ServiceLayer.EmployeeService;
using ServiceLayer.ViewModels;

namespace FinalProject.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService employeeService;

        public EmployeeController(IEmployeeService _employeeService)
        {
            employeeService = _employeeService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult LoadEmployeeData()
        {
            var draw = HttpContext.Request.Form["draw"].FirstOrDefault();
            var start = Request.Form["start"].FirstOrDefault();
            var length = Request.Form["length"].FirstOrDefault();
            var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();  
            var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
            var searchValue = Request.Form["search[value]"].FirstOrDefault();
            DTParameters dtParameters = new DTParameters()
            {
                Draw = draw,
                Start = start,
                Length = length,
                SortColumn = sortColumn,
                SortColumnDirection = sortColumnDirection,
                SearchValue = searchValue,
               
            };
            var employeeData = employeeService.GetAllEmployeeData(dtParameters);

            return Json(new { draw = draw, recordsFiltered = employeeData.RecordsTotal, recordsTotal = employeeData.RecordsTotal, data = employeeData.EmployeeRecords });
        }

        public IActionResult AddEmployee()
        {
            var allPositions = employeeService.GetAllPositions();
            ViewData["PositionName"] = new SelectList(allPositions, "PositionId", "Name");
            return View();
        }

        [HttpPost]
        public IActionResult AddEmployee(EmployeeViewModel employee)
        {
            var allPositions = employeeService.GetAllPositions();
            ViewData["PositionName"] = new SelectList(allPositions, "PositionId", "Name");

            var result = employeeService.Validate(employee);
            if (result.IsValid)
            {
                employee.PositionId = int.Parse(employee.PositionName);

                employeeService.AddEmployee(employee);
                return RedirectToAction("Index");
            }
            foreach (var x in result.Errors)
            {
                ModelState.AddModelError(x.PropertyName, x.ErrorMessage);
            }
            return View();
        }

        [HttpPost]
        public IActionResult DeleteEmployeeData(int employeeId)
        {
            employeeService.DeleteEmployeeData(employeeId);
            return Ok("Employee Deleted successfully");
        }

        public IActionResult AddPosition()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddPosition(PositionModel position)
        {
            if (ModelState.IsValid)
            {
                 employeeService.AddPosition(position);
                return RedirectToAction("Index");
            }
            return View();

        }

        public IActionResult UpdateEmployee(int employeeId)
        {

            var employeeData = employeeService.GetEmployeeViewModelById(employeeId);
            return View(employeeData);
        }

        [HttpPost]
        public IActionResult UpdateEmployee(EmployeeViewModel employee)
        {
            if (ModelState.IsValid)
            {
                employeeService.UpdateEmployee(employee);
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult ViewJobHistory(int employeeId)
        {
            ViewData["EmployeeId"] = employeeId;
            var employeeData = employeeService.GetAllHistories(employeeId);
            return View(employeeData);
        }

        public IActionResult AddNewTitle(int employeeId)
        {
            var allPositions = employeeService.GetAllPositions();
            ViewData["PositionName"] = new SelectList(allPositions, "PositionId", "Name");

            EmployeeHistoryViewModel history = new EmployeeHistoryViewModel
            {
                EmployeeId = employeeId
            };
            return View(history);

        }

        [HttpPost]
        public IActionResult AddNewTitle(EmployeeHistoryViewModel employeeHistory)
        {
            if (ModelState.IsValid)
            {
                employeeService.UpdateOldJob(employeeHistory);

                employeeHistory.PositionId = int.Parse(employeeHistory.PositionName);

                employeeService.AddNewJob(employeeHistory);
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult UpdateEmployeeHistory(int employeeHistoryId, int employeeId)
        {
            ViewData["EmployeeId"] = employeeId;
            var history = employeeService.GetHistoryViewModelById(employeeHistoryId);
            return View(history);
        }

        [HttpPost]
        public IActionResult UpdateEmployeeHistory(EmployeeHistoryViewModel employeeHistory, int empId)
        {
            if (ModelState.IsValid)
            {
                employeeService.UpdateEmployeeHistory(employeeHistory);
                return RedirectToAction("ViewJobHistory", new { employeeId = empId });

            }
            return View();
        }
    }
}
