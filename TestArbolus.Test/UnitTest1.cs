using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System.Threading.Tasks;
using Test.Arbolues.Repository;

namespace TestArbolus.Test
{
    public class Tests
    {
        EmployeeRepository _repository;
        [SetUp]
        public void Setup()
        {
            _repository = new EmployeeRepository();

        }

        [Test]
        [Order(4)]
        public async Task TestOlderEmployee()
        {
            var employee = await _repository.GetEmployee();
            Assert.IsNotNull(employee);
        }

        [Test]
        [Order(1)]
        public async Task TestGetEmployeeJson()
        {
            var response = await _repository.GetEmployeeJson();

            Assert.IsNotNull(response);
        }

        [Test]
        [Order(2)]
        public async Task TestLoadEmployeeJson()
        {
            var response = await _repository.GetEmployeeJson();

            JObject json = await _repository.LoadEmployeeJson(response);

            Assert.IsNotNull(json);
        }

        [Test]
        [Order(3)]
        public async Task TestGetEmployeeFromJson()
        {
            var response = await _repository.GetEmployeeJson();
            JObject json = await _repository.LoadEmployeeJson(response);
            var a_employees = await _repository.GetEmployeeFromJson(json);
            Assert.IsNotNull(a_employees);
        }
    }
}