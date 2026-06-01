using Moq;
using ServicesDLL.Services;
using InterfacesDLL.Interfaces;
using ModelsDLL.Models;
using Microsoft.Extensions.Logging.Abstractions;

namespace Tests.Services {
    public class KotServiceTests {

        private Mock<IStudentRepo> _mockRepo;
        private NullLogger<StudentsService> _logger;
        private IStudentsService _service;

        public KotServiceTests() {
            _mockRepo = new Mock<IStudentRepo>();
            _logger = NullLogger<StudentsService>.Instance;
            _service = new StudentsService(_logger, _mockRepo.Object);

        }
         

        [Fact]
        public void GetAll_ReturnsStudentsFromRepo() {


            var students = new List<Student> {
                new Student { Id = 1, Matricule = "HE01", FirstName = "John", LastName = "Doe", Email = "john@example.com" },
                new Student { Id = 2, Matricule = "HE02", FirstName = "Jane", LastName = "Smith", Email = "jane@example.com" }
            };
            _mockRepo.Setup(r => r.GetAll()).Returns(students);

            var result = _service.GetAll();

            Assert.Same(students, result);
            _mockRepo.Verify(r => r.GetAll(), Times.Once);
        }

        [Fact]
        public void GetAll_ReturnNoStudentsFromRepo() {
            var students = new List<Student>();

            _mockRepo.Setup(r => r.GetAll()).Returns(students);

            var result = _service.GetAll();

            Assert.Equal(students.Count, result.Count);
            _mockRepo.Verify(r => r.GetAll(), Times.Once);
        }

    }
}
