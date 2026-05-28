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


            Console.WriteLine("Select an operation : ");
            Console.WriteLine("1 - Add");
            Console.WriteLine("2 - Delete");
            Console.WriteLine("3 - Update");
            Console.WriteLine("4 - Get All");
            Console.WriteLine("5 - Find by Last Name");
            Console.Write("Enter your choice: ");
            var input = Console.ReadLine();
            int choice;
            if (!int.TryParse(input, out choice)) {
                logger.LogError("Invalid input. Please enter a valid choice.");
                return;
            }

            try {

                switch(choice) {
                    case 1: 
                        Add(studentsService);
                        break;
                    case 2: 
                        Delete(studentsService);
                        break;
                    case 3:
                        Update(studentsService);
                        break;
                    case 4:
                        GetAll(studentsService);
                        break;
                    case 5:
                        FindByLastName(studentsService);
                        break;

                    default: 
                        logger.LogWarning("Invalid choice. Please select 1 for Add, 2 for Delete, or 3 for Update. 4 for Get All");
                        break;
                }
            } catch(Exception e) {
                logger.LogError(e, "An error as occurred while processing students.");
            }
        }


        private static void Add(IStudentsService studentsService) {
            Student newStudent = new Student {
                Matricule = "HE01",
                FirstName = "John",
                LastName = "D",
                Email = "johnd@gmail.com"
            };

            studentsService.Add(newStudent);
        }

        private static void Delete(IStudentsService studentsService) {
            Console.Write("Enter the ID of the student to delete : ");
            var input = Console.ReadLine();
            if (int.TryParse(input, out int id) && id != 0) {
                studentsService.Delete(id);
                Console.WriteLine("Student with ID {id} has been deleted.");
            } else {
                Console.WriteLine("Invalid ID. Please enter a valid student ID.");
                return;
            }
        }

        private static void Update(IStudentsService studentsService) {

            Console.Write("Enter the ID of the student to update : ");
            var input = Console.ReadLine();
            if (int.TryParse(input, out int id) && id != 0) {
                Student updatedStudent = new Student {
                    Id = id,
                    Matricule = "HE01",
                    FirstName = "John",
                    LastName = "Dafiduck",
                    Email = "johndoe@gmail.com"
                };

                studentsService.Update(updatedStudent);
            } else {
                Console.WriteLine("Invalid ID. Please enter a valid student ID.");
                return;

            }
        }

        private static void GetAll(IStudentsService studentsService) {
            var students = studentsService.GetAll();
            foreach (var student in students) {
                Console.WriteLine($"ID: {student.Id}, Name: {student.FirstName} {student.LastName}, Matricule: {student.Matricule}, Email: {student.Email}");
            }
        }

        private static void FindByLastName(IStudentsService studentsService) {
            Console.Write("Enter the last name to search for: ");
            var lastName = Console.ReadLine();
            if (lastName != null) {

                var students = studentsService.FindStudentByLastname(lastName);
                try {
                    var student = studentsService.FindStudentByLastname(lastName);
                    foreach (var s in students) {
                        Console.WriteLine($"ID: {s.Id}, Name: {s.FirstName} {s.LastName}, Matricule: {s.Matricule}, Email: {s.Email}");
                    }
                } catch (Exception e) {
                    Console.WriteLine($"An error occurred while searching for students with last name '{lastName}': {e.Message}");
                }
            } else {
                Console.WriteLine("Invalid input. Please enter a valid last name.");
                return;
            }
        }

        private static ServiceProvider CreateService() {
            var services = new ServiceCollection();
            services.AddLogging(configure=> configure.AddConsole())
                .AddSingleton<IStudentRepo, StudentRepo>()
                .AddSingleton<IStudentsService, StudentsService>();

            return services.BuildServiceProvider();
        }
    }
}



