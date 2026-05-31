using Microsoft.Extensions.Logging;
using InterfacesDLL.Interfaces;
using ModelsDLL.Models;
using RepoDLL.Repositories;

namespace ServicesDLL.Services {
    public class StudentsService : IStudentsService {

        private IStudentRepo _repo;
        private readonly ILogger<StudentsService> _logger;

        public StudentsService(ILogger<StudentsService> logger, IStudentRepo repo) {
            _logger = logger;
            _repo = repo;
        }

        public List<Student> GetAll() {
            _logger.LogDebug("entering GetAll() in StudentsService");
            List<Student> students = _repo.GetAll();
            _logger.LogInformation("Fetched {Count} students from database (service).", students.Count);
            return students;
        }

        public void Add(Student student) {
            CheckMatricule(student.Matricule);
            CheckFirstName(student.FirstName);
            _repo.Add(student);
            
        }

        public void Delete(int id) {
            _repo.Delete(id);
        }

        public void Update(Student student) {
            CheckMatricule(student.Matricule);
            CheckFirstName(student.FirstName);
            _repo.Update(student);
        }

        public List<Student> FindStudentByLastname(string lastName) {
            if (string.IsNullOrEmpty(lastName)) {
                throw new ArgumentException("Last name cannot be null or empty.");
            }
            if (lastName.Length > 50) {
                throw new ArgumentException("Last name must be less than or equal to 50 characters long.");
            }
            return _repo.FindStudentByLastname(lastName);
        }

        private void CheckMatricule(string matricule) {
            if (string.IsNullOrEmpty(matricule)) {
                throw new ArgumentException("Matricule cannot be null or empty.");
            }
            if (matricule.Length != 4) {
                throw new ArgumentException("Matricule must be 4 characters long.");
            }
            string prefix = matricule.Substring(0, 2);
            if (prefix != "HE" && prefix != "PS") {
                throw new ArgumentException("Matricule must begin with HE or PS");
            }
        }

        private void CheckFirstName(string firstName) {
            if (string.IsNullOrEmpty(firstName)) {
                throw new ArgumentException("First name cannot be null or empty.");
            }
            if (firstName.Length > 50) {
                throw new ArgumentException("First name must be less than or equal to 50 characters long.");
            }
        }


    }
}
