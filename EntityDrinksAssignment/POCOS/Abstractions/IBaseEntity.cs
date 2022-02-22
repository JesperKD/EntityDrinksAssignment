namespace EntityDrinksAssignment.POCOS.Abstractions
{
    /// <summary>
    /// Interface for basic entity properties
    /// </summary>
    public interface IBaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
