using ModelsDLL.Models;

namespace InterfacesDLL.Interfaces {
    public interface IStudentRepo {
        List<Student> GetAll();

        void Add(Student student);

        void Delete(int id);

        void Update(Student student);

        List<Student> FindStudentByLastname(string lastName);
    }
}
