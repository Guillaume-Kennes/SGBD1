using ModelsDLL.Models;

namespace InterfacesDLL.Interfaces {
    public interface IKotServices {

        List<Kot> GetAll();

        void Delete(int id);


    }
}
