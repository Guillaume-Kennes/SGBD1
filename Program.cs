// See https://aka.ms/new-console-template for more information

using SGBD.Services;
using SGBD.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using SGBD.Interfaces;
using SGBD.Repositories;


namespace SGBD {
    public class Program {
        public static void Main(string[] args) {

            using var serviceProvider = CreateService();

            var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
            var studentsService = serviceProvider.GetRequiredService<IStudentsService>();

            //int choice = int.Parse(args[0]);
            int choice = 1;

            try {

                switch(choice) {
                    case 1: 
                        Add(studentsService);
                        break;
                    case 2: 
                        Delete(studentsService);
                        break;

                }

                logger.LogInformation("Fetching all students from the database...");
                studentsService.GetAll();
                logger.LogInformation("it works !");


                

            } catch(Exception e) {
                logger.LogError(e, "An error as occurred while processing students.");
            }
        }


        private static void Add(IStudentsService studentsService) {
            Student newStudent = new Student {
                Matricule = "HE01",
                FirstName = "John",
                LastName = "D",
                Email = "johndoe@gmail.com"
            };

            studentsService.Add(newStudent);
        }

        private static void Delete(IStudentsService studentsService) {
            studentsService.Delete(1);
        }

        private static ServiceProvider CreateService() {
            var services = new ServiceCollection();
            services.AddLogging(configure=> configure.AddConsole())
                .AddSingleton<IRepo, Repo>()
                .AddSingleton<IStudentsService, StudentsService>();

            return services.BuildServiceProvider();
        }
    }
}



