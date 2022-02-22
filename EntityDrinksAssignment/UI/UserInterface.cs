using EntityDrinksAssignment.POCOS;
using EntityDrinksAssignment.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EntityDrinksAssignment.UI
{
    /// <summary>
    /// Manager class that handles all user interaction
    /// </summary>
    public static class UserInterface
    {
        public static void Start()
        {
            Console.Clear();
            Console.WriteLine("Would you like to:\n");
            Console.WriteLine("A. Get the recipe for a cocktail.\n");
            Console.WriteLine("B. Add a new Cocktail to the database.\n");
            Console.WriteLine("C. Delete a cocktail from the database.");
            var response = Console.ReadKey().Key.ToString().ToLower();
            if (response == "a")
            {
                Console.Clear();
                Console.WriteLine("Loading...");
                RecipeMenu();
            }
            else if (response == "b")
            {
                Console.Clear();
                Console.WriteLine("Loading...");
                AddCocktailMenu();
            }
            else if (response == "c")
            {
                Console.Clear();
                Console.WriteLine("Loading...");
                DeleteCocktailMenu();
            }
            else
            {
                Console.WriteLine("Not a valid input.\nPress any key to continue.");
            }
        }

        public static void RecipeMenu()
        {
            ItemRepository ItemRepository = new ItemRepository();
            IEnumerable<Cocktail> cocktails = ItemRepository.GetAllCocktails();

            Console.Clear();
            Console.WriteLine("List of available cocktails:\n");
            foreach (var item in cocktails)
            {
                Console.WriteLine(item.Name);
            }
            if (cocktails.Count() != 0)
            {
                Console.WriteLine("\nWhat cocktail recipe would you like to see?");
                var response = Console.ReadLine();
                foreach (var cocktail in ItemRepository.GetAllCocktails())
                {
                    if (cocktail.Name.ToLower() == response.ToLower())
                    {
                        Console.Clear();
                        Console.WriteLine($"Recipe for a {cocktail.Name}:");
                        foreach (var ingredient in cocktail.Ingredients)
                        {
                            Console.WriteLine(ingredient.Name);
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("There are no cocktails added to the database yet.");
            }
            Console.WriteLine("\nPress any key to continue.");
            Console.ReadKey();
        }

        public static void AddCocktailMenu()
        {
            ItemRepository ItemRepository = new ItemRepository();
            List<Ingredient> ingredients = new List<Ingredient>();
            string newCocktailName = string.Empty;
            var cocktailCount = 0;
            var ingredientCount = 0;

            if (ItemRepository.GetAllCocktails().Any())
            {
                cocktailCount = ItemRepository.GetAllCocktails().Count();
            }

            if (ItemRepository.GetAllIngredients().Any())
            {
                ingredientCount = ItemRepository.GetAllIngredients().Count();
            }

            Console.Clear();
            Console.WriteLine("What is the name of your cocktail?");
            try
            {
                newCocktailName = Console.ReadLine();
            }
            catch
            {
                Console.WriteLine("Not a valid input");
            }

            if (cocktailCount > 0)
            {
                if (!string.IsNullOrWhiteSpace(newCocktailName))
                {
                    //Checks database to see if name is already used
                    if (!ItemRepository.CocktailNameIsNew(newCocktailName))
                    {
                        Console.WriteLine("This cocktail name is already in our database.\nPlease start over.");
                    }
                }
                else
                {
                    Console.WriteLine("There was not given a name...");
                    Console.ReadLine();
                    return;
                }
            }

            Console.WriteLine("\nWhat are the ingredients in this cocktail? (Seperate with comma)");
            var ingredientString = Console.ReadLine();

            var splittedString = ingredientString.Split(',');

            int newCocktailId = cocktailCount;
            while (!ItemRepository.CocktailIdIsValid(newCocktailId))
            {
                newCocktailId++;
            }

            int newIngredientId = ingredientCount;
            foreach (var item in splittedString)
            {
                //Finds an available id for the new ingredient
                while (!ItemRepository.IngredientIdIsValid(newIngredientId))
                {
                    newIngredientId++;
                }

                Ingredient newIngredient = new Ingredient();
                newIngredient.Id = newIngredientId;
                newIngredient.Name = item.Replace(" ", "");
                newIngredient.Cocktail_Id = newCocktailId;

                ingredients.Add(newIngredient);
            }

            Cocktail newCocktail = new Cocktail();
            newCocktail.Id = newCocktailId;
            newCocktail.Name = newCocktailName;
            newCocktail.Ingredients = ingredients;
            ItemRepository.Create(newCocktail);

            Console.WriteLine("Your cocktail has now been added to the database.\nPress any key to continue.");
            Console.ReadKey();
        }

        public static void DeleteCocktailMenu()
        {
            ItemRepository ItemRepository = new ItemRepository();
            IEnumerable<Cocktail> cocktails = ItemRepository.GetAllCocktails();

            if (!cocktails.Any())
            {
                Console.WriteLine("There are no cocktails in the database...");
                Console.ReadLine();
                return;
            }

            Console.Clear();
            Console.WriteLine("List of available cocktails:\n");
            foreach (var cocktail in cocktails)
            {
                Console.WriteLine(cocktail.Name);
            }
            Console.WriteLine("\nWhat cocktail recipe would you like to remove?");
            var response = Console.ReadLine();

            foreach (var item in cocktails)
            {
                if (item.Name.ToLower() == response.ToLower())
                {
                    ItemRepository.Delete(item);
                }
            }
            Console.Clear();
            Console.WriteLine($"{response} has been removed from the database.\nPress any key to continue.");

            Console.ReadKey();
        }
    }
}
