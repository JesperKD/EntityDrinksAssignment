using EntityDrinksAssignment.POCOS;

namespace EntityDrinksAssignment.Repositories.Abstractions
{
    public interface IItemRepository
    {
        void Create(Cocktail createEntity);
        void Create(Ingredient createEntity);

        void Delete(Cocktail deleteEntity);
        void Delete(Ingredient deleteEntity);
    }
}
