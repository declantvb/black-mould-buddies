public interface IInteractible
{
	string Name { get; }

	Interaction[] GetInteractions();

	bool Interact(Interaction type, float workAmount);
	void Break();
}

public class ObjectState
{
	public string Name { get; set; }
}