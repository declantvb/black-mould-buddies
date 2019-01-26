using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseInteractible : MonoBehaviour, IInteractible
{
	public static class States
	{
		public static ObjectState Broken = new ObjectState { Name = "Broken" };
		public static ObjectState Good = new ObjectState { Name = "Broken" };
	}

	public float WorkRequired = 1;
	public float WorkDone;

	public ObjectState State;

	private MeshRenderer myRenderer;
	private WorkProgress ui;

	void Start()
	{
		myRenderer = this.GetComponent<MeshRenderer>();
		ui = GetComponent<WorkProgress>();
		WorkDone = WorkRequired;
	}

	void Update()
	{
		myRenderer.material.color = State == States.Broken ? Color.red : Color.blue;
		ui.FillAmount = State != States.Good ? Mathf.Clamp(WorkDone / WorkRequired, 0, 1) : 0;
	}

	public string[] GetInteractions()
	{
		return new string[] { "Fix" };
	}

	public bool Interact(string type, float workAmount)
	{
		switch (type)
		{
			case "Fix":
				if (State == States.Broken)
				{
					WorkDone += workAmount;
					if (WorkDone >= WorkRequired)
					{
						WorkDone = WorkRequired;
						State = States.Good;
						Debug.Log("i got fixed :O");
						return true;
					}
				}
				break;
			default:
				Debug.Log($"unhandled: {type}");
				break;
		}

		return false;
	}

	public virtual void Break()
	{
		State = States.Broken;
		WorkDone = 0;
	}
}

public class Interaction
{
	public string Name { get; set; }
	public int Cost { get; set; }
	public float Work { get; set; }
}

public class ObjectState
{
	public string Name { get; set; }
}
