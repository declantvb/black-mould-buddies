public interface IInteractible
{
	string[] GetInteractions();

	bool Interact(string type, float workAmount);
	void Break();
}