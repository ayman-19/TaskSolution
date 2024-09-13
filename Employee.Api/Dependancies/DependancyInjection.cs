using Employee.Api.Data;
using Employee.Api.Dtos;
using Employee.Api.Requests;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Employee.Api.Dependancies
{
    public static class DependancyInjection
    {
        public static IServiceCollection AddSevices(
            this IServiceCollection services,
            string strConnection
        )
        {
            services.AddDbContext<EmployeeDbContext>(opt =>
                opt.UseNpgsql(
                    strConnection,
                    history =>
                        history.MigrationsHistoryTable(
                            HistoryRepository.DefaultTableName,
                            "EmployeesMigration" + ""
                        )
                )
            );

            services.AddMediatR(cfg =>
                cfg.RegisterServicesFromAssembly(typeof(DependancyInjection).Assembly)
            );
            services.AddValidatorsFromAssembly(typeof(DependancyInjection).Assembly);

            return services;
        }

        public static void AddSEndpoints(this IEndpointRouteBuilder builder)
        {
            var map = builder.MapGroup("Employees");
            /* get all employee
               if (employees is exist return employee response)
               else return empty respone
            request => Empty Request
            response => [
                          {
                            "id": 0,
                            "name": "string",
                            "department": "string",
                            "salary": 0,
                            "message": "string"
                          }
                        ]
             */

            map.MapGet("GatAllAsync", async (ISender sender) => await sender.Send(new GetAll()));
            /*
             * create employee
             * request => {
                              "name": "string",
                              "department": "string",
                              "salary": 0
                           }
             * response =>  {
                              "id": 0,
                              "name": "string",
                              "department": "string",
                              "salary": 0,
                              "message": "string"
                            }
           
            */
            map.MapPost(
                "CreteAsync",
                async (Command command, ISender sender) =>
                    await sender.Send(new Add(command.name, command.department, command.salary))
            );
            /* Delete employee by id
               if (employee is exist return employee response and delete from database)
               else return empty respone with message
               
               request => id
               response => {
                              "id": 0,
                              "name": "string",
                              "department": "string",
                              "salary": 0,
                              "message": "string"
                            }
            */
            map.MapDelete(
                "DeleteAsync/{id}",
                async (int id, ISender sender) => await sender.Send(new Delete(id))
            );
            /* update employee by id
             if (employee is exist return employee response and update from database)
              else return empty respone with message

             request => id,{
                              "name": "string",
                              "department": "string",
                              "salary": 0
                           }
             response => {
                           "id": 0,
                           "name": "string",
                           "department": "string",
                           "salary": 0,
                           "message": "string"
                         }
             */
            map.MapPut(
                "UpdateAsync/{id}",
                async (int id, Command command, ISender sender) =>
                    await sender.Send(
                        new Update(id, command.name, command.department, command.salary)
                    )
            );
            /* get employee by id
             if (employee is exist return employee response)
             else return empty respone with message
            request => id
            response => {
                           "id": 0,
                           "name": "string",
                           "department": "string",
                           "salary": 0,
                           "message": "string"
                         }
           */
            map.MapGet(
                "GatByIdAsync/{id}",
                async (int id, ISender sender) => await sender.Send(new GetById(id))
            );
        }
    }
}
