using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGBD.Models;

namespace SGBD.Interfaces {
    public interface IRepo {
        List<Student> GetAll();

        void Add(Student sttudent);

        void Delete(int id);
    }
}
