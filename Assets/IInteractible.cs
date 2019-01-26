using System;

public interface IInteractible
{
	string Name { get; }

	Interaction[] GetInteractions();

	bool Interact(PlayerStatus status, Interaction type, float workAmount);
	void Break();
	void StopInteracting(PlayerStatus status);
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