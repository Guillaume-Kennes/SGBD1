using Models;

namespace Interfaces {
    public interface IStudentsService {

        List<Student> GetAll();

        void Add(Student student);

        void Delete(int id);

        void Update(Student student);

        List<Student> FindStudentByLastname(string lastName);

    }
}
