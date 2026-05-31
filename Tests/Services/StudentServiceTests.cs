using Moq;
using ServicesDLL.Services;
using InterfacesDLL.Interfaces;
using ModelsDLL.Models;
using Microsoft.Extensions.Logging.Abstractions;

namespace Tests.Services {
    public class StudentServiceTests {

        private Mock<IStudentRepo> _mockRepo;
        private NullLogger<StudentsService> _logger;
        private IStudentsService _service;

        public StudentServiceTests() {
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



        [Fact]
        public void Add_ValidStudent_CallsRepoAdd() {

            var student = new Student {
                Matricule = "HE01",
                FirstName = "John",
                LastName = "Doe",
                Email = "john@example.com"
            };

            _service.Add(student);

            _mockRepo.Verify(r => r.Add(It.Is<Student>(s =>
                s.Matricule == "HE01" &&
                s.FirstName == "John" &&
                s.LastName == "Doe" &&
                s.Email == "john@example.com"
            )), Times.Once);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("001")]
        public void Add_InvalidMatricule_ThrowsArgumentException_AndDoesNotCallRepo(string? matricule) {

            var student = new Student {
                Matricule = matricule,
                FirstName = "John",
                LastName = "Doe",
                Email = "john@example.com"
            };

            Assert.Throws<ArgumentException>(() => _service.Add(student));
            _mockRepo.Verify(r => r.Add(It.IsAny<Student>()), Times.Never);
        }


        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void AddWithInvalidFirstNameAndThrowsArgumentException(string? firstName) {
            var student = new Student {
                Matricule = "HE01",
                FirstName = firstName,
                LastName = "Doe",
                Email = "a@example.com"
            };
            Assert.Throws<ArgumentException>(() => _service.Add(student));
        }

        [Fact]
        public void Add_RepoThrows_ExceptionIsPropagated() {

            var student = new Student {
                Matricule = "HE01",
                FirstName = "John",
                LastName = "Doe",
                Email = "john@example.com"
            };

            _mockRepo.Setup(r => r.Add(It.IsAny<Student>()))
                    .Throws(new InvalidOperationException("DB failure"));

            var ex = Assert.Throws<InvalidOperationException>(() => _service.Add(student));
            Assert.Equal("DB failure", ex.Message);
            _mockRepo.Verify(r => r.Add(It.IsAny<Student>()), Times.Once);
        }


        [Fact]
        public void Update_ValidStudent_CallsRepoUpdate() {
            var student = new Student {
                Id = 42,
                Matricule = "HE01",
                FirstName = "John",
                LastName = "Updated",
                Email = "john.updated@example.com"
            };

            _service.Update(student);

            _mockRepo.Verify(r => r.Update(It.Is<Student>(s =>
                Equals(s.Id, student.Id) &&
                s.Matricule == "HE01" &&
                s.FirstName == "John" &&
                s.LastName == "Updated" &&
                s.Email == "john.updated@example.com"
            )), Times.Once);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("001")]
        public void Update_InvalidMatricule_ThrowsArgumentException(string? matricule) {
            var student = new Student {
                Id = 1,
                Matricule = matricule,
                FirstName = "John",
                LastName = "Doe",
                Email = "a@a.com"
            };

            Assert.Throws<ArgumentException>(() => _service.Update(student));
            _mockRepo.Verify(r => r.Update(It.IsAny<Student>()), Times.Never);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Update_InvalidFirstName_ThrowsArgumentException(string? firstName) {
            var student = new Student {
                Id = 1,
                Matricule = "HE01",
                FirstName = firstName,
                LastName = "Doe",
                Email = "a@a.com"
            };

            Assert.Throws<ArgumentException>(() => _service.Update(student));
            _mockRepo.Verify(r => r.Update(It.IsAny<Student>()), Times.Never);
        }

        [Fact]
        public void Update_RepoThrows_ExceptionIsPropagated() {
            var student = new Student {
                Id = 1,
                Matricule = "HE01",
                FirstName = "John",
                LastName = "Doe",
                Email = "john@example.com"
            };

            _mockRepo.Setup(r => r.Update(It.IsAny<Student>()))
                     .Throws(new InvalidOperationException("DB update failure"));

            var ex = Assert.Throws<InvalidOperationException>(() => _service.Update(student));
            Assert.Equal("DB update failure", ex.Message);
            _mockRepo.Verify(r => r.Update(It.IsAny<Student>()), Times.Once);
        }

        // --- Delete tests ---

        [Fact]
        public void Delete_CallsRepoDelete() {
            int id = 99;

            _service.Delete(id);

            _mockRepo.Verify(r => r.Delete(id), Times.Once);
        }

        [Fact]
        public void Delete_RepoThrows_ExceptionIsPropagated() {
            int id = 5;
            _mockRepo.Setup(r => r.Delete(id)).Throws(new InvalidOperationException("DB delete failure"));

            var ex = Assert.Throws<InvalidOperationException>(() => _service.Delete(id));
            Assert.Equal("DB delete failure", ex.Message);
            _mockRepo.Verify(r => r.Delete(id), Times.Once);
        }

        // --- FindStudentByLastname tests ---

        [Fact]
        public void FindStudentByLastname_ReturnsStudentsFromRepo() {
            var expected = new List<Student> {
                new Student { Id = 10, Matricule = "HE02", FirstName = "Alice", LastName = "Smith", Email = "a@a.com" }
            };
            _mockRepo.Setup(r => r.FindStudentByLastname("Smith")).Returns(expected);

            var result = _service.FindStudentByLastname("Smith");

            Assert.Same(expected, result);
            _mockRepo.Verify(r => r.FindStudentByLastname("Smith"), Times.Once);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void FindStudentByLastname_NullOrEmpty_ThrowsArgumentException(string? lastName) {
            Assert.Throws<ArgumentException>(() => _service.FindStudentByLastname(lastName!));
        }

        [Fact]
        public void FindStudentByLastname_TooLong_ThrowsArgumentException() {
            var longName = new string('x', 51);
            Assert.Throws<ArgumentException>(() => _service.FindStudentByLastname(longName));
        }

        [Fact]
        public void FindStudentByLastname_RepoThrows_ExceptionIsPropagated() {
            _mockRepo.Setup(r => r.FindStudentByLastname(It.IsAny<string>()))
                     .Throws(new InvalidOperationException("DB find failure"));

            var ex = Assert.Throws<InvalidOperationException>(() => _service.FindStudentByLastname("Smith"));
            Assert.Equal("DB find failure", ex.Message);
            _mockRepo.Verify(r => r.FindStudentByLastname("Smith"), Times.Once);
        }
    }
}
