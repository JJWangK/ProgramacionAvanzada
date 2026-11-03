using PAP.DataAccess;
using PAP.Repositories;
using System;
using System.Linq;

namespace PAP.Generators
{
    /// <summary>
    /// Genera empleados de prueba usando los roles existentes.
    /// </summary>
    public class EmployeesGenerator : GeneratorBase
    {
        private readonly EmployeesRepository _repository;
        private readonly RolesRepository _rolesRepository;

        public EmployeesGenerator()
        {
            _repository = new EmployeesRepository();
            _rolesRepository = new RolesRepository();
        }

        public override string TableName
        {
            get { return "dbo.Employees"; }
        }

        public override bool HasData()
        {
            return _repository.GetAll().Any();
        }

        protected override int GenerateCore()
        {
            var roles = RequireRows(_rolesRepository.GetAll(), "dbo.Roles");

            for (var i = 0; i < BatchSize; i++)
            {
                var employee = new Employee
                {
                    RoleId = roles[i % roles.Count].Id,
                    FirstName = GeneratorData.Pick(GeneratorData.FirstNames),
                    LastName = GeneratorData.Pick(GeneratorData.LastNames),
                    Email = GeneratorData.Email("empleado"),
                    PasswordHash = "hash-generado-para-pruebas",
                    IsActive = true,
                    CreatedAt = DateTime.Now
                };

                _repository.Add(employee);
            }

            return BatchSize;
        }
    }
}
