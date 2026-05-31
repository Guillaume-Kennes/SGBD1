// See https://aka.ms/new-console-template for more information

using ServicesDLL.Services;
using ModelsDLL.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using InterfacesDLL.Interfaces;
using RepoDLL.Repositories; //ca ne peur pas faire reference a Repositories, il faut ajouter le projet Repositories en reference du projet SGBD


namespace SGBD {
    public class Program {
        public static void Main(string[] args) {

            using var serviceProvider = CreateService();

            var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
            var studentsService = serviceProvider.GetRequiredService<IStudentsService>();


            while (true) {
                Console.WriteLine("Select an operation : ");
                Console.WriteLine("0 - Exit");
                Console.WriteLine("1 - Add");
                Console.WriteLine("2 - Delete");
                Console.WriteLine("3 - Update");
                Console.WriteLine("4 - Get All");
                Console.WriteLine("5 - Find by Last Name");
                Console.Write("Enter your choice: ");
                var input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input)) {
                    logger.LogWarning("No input provided.");
                    continue;
                }

                if (input.Trim().Equals("0", System.StringComparison.OrdinalIgnoreCase)
                    || input.Trim().Equals("q", System.StringComparison.OrdinalIgnoreCase)) {
                    logger.LogInformation("Exiting application.");
                    break;
                }

                if (!int.TryParse(input, out int choice)) {
                    logger.LogError("Invalid input. Please enter a valid choice.");
                    continue;
                }

                try {
                    switch (choice) {
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
                            logger.LogWarning("Invalid choice. Please select a valid option.");
                            break;
                    }
                } catch (Exception e) {
                    logger.LogError(e, "An error has occurred while processing students.");
                }

                Console.WriteLine();
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey(true);
                Console.Clear();
            }
        }


        private static void Add(IStudentsService studentsService) {
            Student newStudent = new Student {
                Matricule = "HE05",
                FirstName = "Johndd",
                LastName = "Ddd",
                Email = "jo@gmail.com"
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
                    Matricule = "HE05",
                    FirstName = "HH",
                    LastName = "Dafiduck",
                    Email = "HD@gmail.com"
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
                .AddSingleton<IStudentRepo, StudentDapperRepo>()
                .AddSingleton<IStudentsService, StudentsService>();

            return services.BuildServiceProvider();
        }
    }
}



