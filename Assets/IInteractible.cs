using System;

public interface IInteractible
{
	string Name { get; }

	Interaction[] GetInteractions();

	bool Interact(Interaction type, float workAmount);
	void Break();
}

[Serializable]
public class ObjectState
{
	public string Name;

	public override string ToString()
	{
		return Name;
	}
}