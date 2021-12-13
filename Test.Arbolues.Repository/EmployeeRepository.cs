using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Linq;
using System.Threading.Tasks;
using Test.Arbolus.Viewmodel;

namespace Test.Arbolues.Repository
{
    public class EmployeeRepository
    {
        public async Task<Employee> GetEmployee()
        {
            var response = await GetEmployeeJson();
            JObject json = await LoadEmployeeJson(response);
            Employee[] a_employees = await GetEmployeeFromJson(json);

            var older_employee = a_employees.OrderByDescending(eq => eq.employee_age).FirstOrDefault();

            return older_employee;
        }


        public async Task<string> GetEmployeeJson()
        {
            try
            {
                RestClient client = new RestClient("http://dummy.restapiexample.com/");
                var request_eq = new RestRequest("/api/v1/employees");

                IRestResponse response_eq = await client.ExecuteGetAsync(request_eq);

                if (response_eq.StatusCode == System.Net.HttpStatusCode.OK)
                    return response_eq.Content;
            }
            catch (Exception ex)
            {
                //Mensaje de error a log/consola/db/elastic
                Console.WriteLine(ex.ToString());
            }

            return string.Empty;
        }

        public async Task<JObject> LoadEmployeeJson(string api_response)
        {
            try
            {
                return JObject.Parse(api_response);
            }
            catch (Exception ex)
            {
                //Mensaje de error a log/consola/db/elastic
                Console.WriteLine(ex.ToString());
            }

            return null;
        }


        public async Task<Employee[]> GetEmployeeFromJson(JObject json)
        {
            try
            {
                var a_employees = json["data"].Select(eq => new Employee
                {
                    id = eq["id"].Value<int>(),
                    employee_name = eq["employee_name"].Value<string>(),
                    employee_age = eq["employee_age"].Value<int>(),
                    employee_salary = eq["employee_salary"].Value<int>(),
                    profile_image = eq["profile_image"].Value<string>(),
                }).ToArray();

                return a_employees;

            }
            catch (Exception ex)
            {
                //Mensaje de error a log/consola/db/elastic
                Console.WriteLine(ex.ToString());
            }

            return null;
        }
    }
}
