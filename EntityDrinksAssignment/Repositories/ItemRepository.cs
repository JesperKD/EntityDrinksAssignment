using EntityDrinksAssignment.DataAccess;
using EntityDrinksAssignment.POCOS;
using EntityDrinksAssignment.Repositories.Abstractions;
using System;
using System.Collections.Generic;

namespace EntityDrinksAssignment.Repositories
{
    /// <summary>
    /// Repository class that handles all database actions
    /// </summary>
    public class ItemRepository : IItemRepository
    {
        public ItemRepository()
        { }

        /// <summary>
        /// Adds a cocktail to the database
        /// </summary>
        /// <param name="cocktail"></param>
        public void Create(Cocktail createEntity)
        {
            try
            {
                using (var ctx = new DrinkContext())
                {
                    ctx.Cocktails.Add(createEntity);
                    ctx.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Adds an ingredient to the database
        /// </summary>
        /// <param name="ingredient"></param>
        public void Create(Ingredient createEntity)
        {
            try
            {
                using (var ctx = new DrinkContext())
                {
                    ctx.Ingredients.Add(createEntity);
                    ctx.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Deletes a cocktail from the database
        /// </summary>
        /// <param name="id"></param>
        public void Delete(Cocktail deleteEntity)
        {
            try
            {
                using (var ctx = new DrinkContext())
                {

                    foreach (var item in deleteEntity.Ingredients)
                    {
                        Delete(item);
                    }

                    foreach (var item in ctx.Cocktails)
                    {
                        if (deleteEntity.Id == item.Id)
                        {
                            ctx.Cocktails.Remove(item);
                        }
                    }

                    ctx.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ItemRepository: { ex.Message}");
            }
        }

        /// <summary>
        /// Deletes an Ingredient from the database
        /// </summary>
        /// <param name="id"></param>
        public void Delete(Ingredient deleteEntity)
        {
            try
            {
                using (var ctx = new DrinkContext())
                {
                    foreach (var item in ctx.Ingredients)
                    {
                        if (deleteEntity.Id == item.Id)
                        {
                            ctx.Ingredients.Remove(item);
                        }
                    }
                    ctx.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Returns a list of all Cocktails
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Cocktail> GetAllCocktails()
        {
            try
            {
                List<Cocktail> list = new List<Cocktail>();

                using (var ctx = new DrinkContext())
                {
                    foreach (var item in ctx.Cocktails)
                    {
                        //Goes through all the ingredients
                        foreach (var ingred in ctx.Ingredients)
                        {
                            //Matches ingredients on their foreign key
                            if (ingred.Cocktail_Id == item.Id)
                            {
                                //Adds matchin ingredients to the cocktail object
                                item.Ingredients.Add(ingred);
                            }
                        }
                        list.Add(item);
                    }
                }
                return list;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Returns a list of all Ingredients
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Ingredient> GetAllIngredients()
        {
            List<Ingredient> list = new List<Ingredient>();

            try
            {
                using (var ctx = new DrinkContext())
                {
                    foreach (var item in ctx.Ingredients)
                    {
                        list.Add(item);
                    }
                }
                return list;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Returns a bool to see if an Id is unused
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool CocktailIdIsValid(int id)
        {
            using (var ctx = new DrinkContext())
            {
                foreach (var cocktail in ctx.Cocktails)
                {
                    if (cocktail.Id == id)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// Returns a bool to see if an Id is unused
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool IngredientIdIsValid(int id)
        {
            using (var ctx = new DrinkContext())
            {
                foreach (var ingredient in ctx.Ingredients)
                {
                    if (ingredient.Id == id)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// Returns a boolean response depending on wether a string has already been used for a cocktail name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool CocktailNameIsNew(string name)
        {
            using (var ctx = new DrinkContext())
            {
                foreach (var cocktail in ctx.Cocktails)
                {
                    if (cocktail.Name.ToLower() == name.ToLower())
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
