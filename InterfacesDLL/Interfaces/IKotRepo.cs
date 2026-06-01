using ModelsDLL.DTO;

namespace InterfacesDLL.Interfaces {
    public interface IKotRepo {
        List<KotStudentDTO> GetAll();

        void Delete(int id);

    }
}
