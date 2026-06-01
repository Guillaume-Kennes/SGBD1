using System;
using System.Collections.Generic;
using ModelsDLL.Models;

namespace ModelsDLL.Models {
    public class Student {
        public int Id { get; set; }
        public string Matricule { get; set; }
        public string? FirstName { get; set; }
        public string LastName { get; set; }
        public string? Email { get; set; }

        public ICollection<Kot> Kots { get; set; } // Un étudiant peut avoir plusieurs kots

    }
}
