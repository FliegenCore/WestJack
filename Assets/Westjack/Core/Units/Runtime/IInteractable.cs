namespace Core.World
{
    public interface IInteractable
    {
        bool IsEnemy { get; set; }
        void Interact(IInteractable intareactable);
    }
}
