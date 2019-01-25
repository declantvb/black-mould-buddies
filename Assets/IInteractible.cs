public interface IInteractible
{
	string[] GetInteractions();

	void Interact(string type);
	void Break();
}