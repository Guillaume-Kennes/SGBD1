using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGBD.Models;

namespace SGBD.Interfaces {
    internal interface IStudentsService {

        List<Student> GetAll();

        void Add(Student student);
    }
}
