// See https://aka.ms/new-console-template for more information

using SGBD.Services;
using SGBD.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using SGBD.Interfaces;
using SGBD.Repositories;

/*
var loggerFactory = LoggerFactory.Create(builder => {
    builder.AddConsole();
    builder.SetMinimumLevel(LogLevel.Debug); //Information, Warning, Error, Critical
});
ILogger logger = loggerFactory.CreateLogger<Program>();

try {

    ILogger studentLogger = loggerFactory.CreateLogger<StudentsService>();

    StudentsService studentsService = new StudentsService(studentLogger);
    logger.LogDebug("Fetching all students from the database...");
    List<Student> students = studentsService.GetAll();
    
     
    foreach (var student in students) {
        logger.LogInformation("{Matricule} - {FirstName} {LastName} - {email} " + student.Matricule);
    }

    Console.WriteLine("it works !");

} catch (Exception ex) { 
    logger.LogError(ex, "An error occurred");
}
*/

namespace SGBD {
    public class Program {
        public static void Main(string[] args) {

            using var serviceProvider = CreateService();

            var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
            var studentsService = serviceProvider.GetRequiredService<IStudentsService>();



            try {
                logger.LogInformation("Fetching all students from the database...");
                studentsService.GetAll();
                logger.LogInformation("it works !");

                Student newStudent = new Student {
                    Matricule = "123456",
                    FirstName = "John",
                    LastName = "Doe",
                    Email = "john.doe@gmail.com"
                };

                studentsService.Add(newStudent);
                

            } catch(Exception e) {
                logger.LogError(e, "An error as occurred while processing students.");
            }
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



