using EntityDrinksAssignment.POCOS.Abstractions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityDrinksAssignment.POCOS
{
    public class Ingredient : IBaseEntity
    {
        public Ingredient()
        { }

        [Key]
        public int Id { get; set; }

        [Column("Name")]
        public string Name { get; set; }

        public int Cocktail_Id { get; set; }
    }
}
