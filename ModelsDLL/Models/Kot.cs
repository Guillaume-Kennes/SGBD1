using System.ComponentModel.DataAnnotations.Schema;

namespace ModelsDLL.Models {
    public class Kot {
        //[Column("KOT_ID")] commenté car on suppose que les propriétés de la classe correspondent déjà aux colonnes de la table, si jamais les noms sont différents, il faudrait les décommenter
        public int Id { get; set; }

        //[Column("KOT_NAME")]
        public string Name { get; set; }

        public Student? Student { get; set; } //? pour permettre null, un kot peut être vide

    }
}
