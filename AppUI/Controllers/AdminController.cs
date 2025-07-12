using AppUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text.Json;

namespace AppUI.Controllers
{
    [Route("Admin")]
    public class AdminController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public AdminController(IHttpClientFactory httpClientFactory)
        {
            this._httpClientFactory = httpClientFactory;
        }

        [NonAction]
        public HttpClient GetClient()
        {
            var client = _httpClientFactory.CreateClient("ApiClient");

            var token = HttpContext.Session.GetString("jwttoken");
            if (!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            return client;
        }


        [HttpGet("")]
        [HttpGet("LoginPage")]
        public IActionResult LoginPage()
        {
            return View();
        }

        [HttpPost]
        [Route("ValidateUser", Name = "ValidateUser")]
        public async Task<IActionResult> LoginPage(User user)
        {
            var client = GetClient();
            var response = await client.PostAsJsonAsync($"api/v1.0/User/ValidateUser", user);

            if (response.IsSuccessStatusCode)
            {
                var tokenJson = await response.Content.ReadAsStringAsync();

                // Deserialize correctly based on API return
                var tokenObj = JsonSerializer.Deserialize<Dictionary<string, string>>(tokenJson);

                if (tokenObj != null && tokenObj.ContainsKey("token"))
                {
                    var token = tokenObj["token"];
                    HttpContext.Session.SetString("jwttoken", token);
                    return RedirectToAction("Dashboard");
                }
                else
                {
                    ViewBag.Error = "Token not found in response.";
                    return View();
                }
            }

            ViewBag.Error = "Invalid login attempt.";
            return View();
        }



        [HttpGet("Dashboard")]
        public async Task<IActionResult> Dashboard()
        {
            var client = GetClient();

            var employees = await client.GetFromJsonAsync<List<Employee>>("api/v8.0/Employee/GetAll");

            // This should match your working endpoint
            var payrollConfigs = await client.GetFromJsonAsync<List<AppUI.Models.PayrollConfig>>("api/v5.0/PayrollConfig/GetAll");


            ViewBag.PayrollCount = payrollConfigs?.Count ?? 0;
            ViewBag.PayrollConfigs = payrollConfigs;

            return View(employees); // This should be a strongly typed list of employees
        }

        // 🔍 View Employee
        [HttpGet("ViewUser/{id}")]
        public async Task<IActionResult> ViewUser(int id)
        {
            var client = GetClient();
            var response = await client.GetAsync($"api/v8.0/Employee/{id}/GetById");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var employee = JsonSerializer.Deserialize<Employee>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return View("ViewUser", employee);
            }

            return View("Error");
        }

        // ✏️ Edit Employee - GET
        [HttpGet("EditUser/{id}")]
        public async Task<IActionResult> EditUser(int id)
        {
            var client = GetClient();
            var response = await client.GetAsync($"api/v8.0/Employee/{id}/GetById");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var employee = JsonSerializer.Deserialize<Employee>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return View("EditUser", employee);
            }

            return View("Error");
        }

        [HttpPost("EditUser/{id}")]
        public async Task<IActionResult> EditUser(int id, Employee updatedEmp)
        {
            var client = GetClient();
            updatedEmp.EmployeeId = id;

            var response = await client.PutAsJsonAsync("api/v8.0/Employee/Update", updatedEmp);

            if (response.IsSuccessStatusCode)
                return RedirectToAction("Dashboard");

            var content = await response.Content.ReadAsStringAsync(); // 🔍 Log this if needed
            ModelState.AddModelError("", "Failed to update employee.");
            return View(updatedEmp);
        }



        [HttpGet("DeleteUser/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var client = GetClient();

            // Step 1: Get the employee details first
            var empRes = await client.GetAsync($"api/v8.0/Employee/{id}/GetById");
            if (!empRes.IsSuccessStatusCode) return View("Error");

            var json = await empRes.Content.ReadAsStringAsync();
            var employee = JsonSerializer.Deserialize<Employee>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            // Step 2: Now send full object to DELETE
            var deleteReq = new HttpRequestMessage(HttpMethod.Delete, "api/v8.0/Employee/Delete")
            {
                Content = JsonContent.Create(employee)
            };

            var deleteRes = await client.SendAsync(deleteReq);
            if (deleteRes.IsSuccessStatusCode)
                return RedirectToAction("Dashboard");

            ViewBag.Error = "Failed to delete employee.";
            return View("Error");
        }




        [HttpGet("Users")]
        public async Task<IActionResult> Users()
        {
            var client = GetClient();
            var users = await client.GetFromJsonAsync<List<User>>("api/v1.0/User/GetAll");
            return View(users);
        }

        [HttpGet("CreateUser")]
        public IActionResult CreateUser() => View();

        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateUser(User user)
        {
            var client = GetClient();
            var res = await client.PostAsJsonAsync("api/v1.0/User/Add", user);
            if (res.IsSuccessStatusCode)
                return RedirectToAction("Users");

            ModelState.AddModelError("", "Failed to create user.");
            return View(user);
        }
      


        [HttpGet("Employees")]
        public async Task<IActionResult> Employees()
        {
            var client = GetClient();
            var employees = await client.GetFromJsonAsync<List<Employee>>("api/v8.0/Employee/GetAll");
            return View(employees);
        }

        [HttpGet("CreateEmployee")]
        public IActionResult CreateEmployee() => View();

        [HttpPost("CreateEmployee")]
        public async Task<IActionResult> CreateEmployee(Employee emp)
        {
            var client = GetClient();
            var res = await client.PostAsJsonAsync("api/v8.0/Employee/Add", emp);
            if (res.IsSuccessStatusCode)
                return RedirectToAction("Employees");

            ModelState.AddModelError("", "Failed to create employee.");
            return View(emp);
        }

        [HttpGet("LogOut")]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("jwttoken");
            return RedirectToAction("LoginPage");
        }

        //Pay roll Conig
        [HttpGet("PayrollConfig")]
        public async Task<IActionResult> PayrollConfig()
        {
            var client = GetClient();
            var configs = await client.GetFromJsonAsync<List<PayrollConfig>>("api/v5.0/PayrollConfig/GetAll");
            return View(configs);
        }

        [HttpGet("CreatePayrollConfig")]
        public async Task<IActionResult> CreatePayrollConfig(int? id)
        {
            var client = GetClient();
            var employees = await client.GetFromJsonAsync<List<Employee>>("api/v8.0/Employee/GetAll");
            ViewBag.EmployeeList = new SelectList(employees, "EmployeeId", "Name");

            if (id.HasValue)
            {
                var response = await client.GetAsync($"api/v5.0/PayrollConfig/GetById/{id}");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var config = JsonSerializer.Deserialize<PayrollConfig>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    return View(config);
                }
            }

            return View(new PayrollConfig());
        }

        [HttpPost("CreatePayrollConfig")]
        public async Task<IActionResult> CreatePayrollConfig(PayrollConfig config)
        {
            var client = GetClient();
            var res = await client.PostAsJsonAsync("api/v5.0/PayrollConfig/AddorUpdate", config);

            if (res.IsSuccessStatusCode)
                return RedirectToAction("Dashboard");

            var employees = await client.GetFromJsonAsync<List<Employee>>("api/v8.0/Employee/GetAll");
            ViewBag.EmployeeList = new SelectList(employees, "EmployeeId", "Name");

            ModelState.AddModelError("", "Failed to save config.");
            return View(config);
        }

    }
}