using Microsoft.Extensions.Logging;
using SGBD.Interfaces;
using SGBD.Models;
using SGBD.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGBD.Services {
    public class StudentsService : IStudentsService {

        private IRepo _repo;
        private readonly ILogger<StudentsService> _logger;

        public StudentsService(ILogger<StudentsService> logger, IRepo repo) {
            _logger = logger;
            _repo = repo;
        }

        public List<Student> GetAll() {
            _logger.LogDebug("entering GetAll() in StudentsService");
            List<Student> students = _repo.GetAll();
            return students;
        }

        public void Add(Student student) {
            _repo.Add(student);
            
        }

        public void Delete(int id) {
            _repo.Delete(id);
        }
    }
}
