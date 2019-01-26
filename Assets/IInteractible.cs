public interface IInteractible
{
	Interaction[] GetInteractions();

	bool Interact(Interaction type, float workAmount);
	void Break();
}

public class ObjectState
{
	public string Name { get; set; }
}