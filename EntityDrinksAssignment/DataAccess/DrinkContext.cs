using EntityDrinksAssignment.POCOS;
using System;
using System.Data.Entity;

namespace EntityDrinksAssignment.DataAccess
{
    /// <summary>
    /// Entity Database context class
    /// </summary>
    public class DrinkContext : DbContext
    {
        public DbSet<Cocktail> Cocktails { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }

        public DrinkContext() : base(nameOrConnectionString: "CocktailDB")
        {
            try
            {
                //Database.SetInitializer(new CreateDatabaseIfNotExists<DrinkContext>());
                //Database.SetInitializer<DrinkContext>(new DropCreateDatabaseIfModelChanges<DrinkContext>());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
