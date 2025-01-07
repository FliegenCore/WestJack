namespace Core.World
{
    public interface IInteractable
    {
        bool IsConsumable { get; set; }
        void Interact(IInteractable intareactable);
    }
}
