using EntityDrinksAssignment.POCOS.Abstractions;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityDrinksAssignment.POCOS
{
    public class Cocktail : IBaseEntity
    {
        public Cocktail()
        { }

        [Key]
        public int Id { get; set; }

        [Column("Name")]
        public string Name { get; set; }

        public List<Ingredient> Ingredients { get; set; }

    }
}
