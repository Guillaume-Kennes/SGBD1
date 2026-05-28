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
            CheckMatricule(student.Matricule);
            _repo.Add(student);
            
        }

        public void Delete(int id) {
            _repo.Delete(id);
        }

        public void Update(Student student) {
            CheckMatricule(student.Matricule);
            _repo.Update(student);
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

    }
}
