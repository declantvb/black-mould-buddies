using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseInteractible : MonoBehaviour, IInteractible
{
	public static class States
	{
		public static ObjectState Broken = new ObjectState { Name = "Broken" };
		public static ObjectState Good = new ObjectState { Name = "Good" };
	}

	public float WorkDone;
	public Interaction CurrentInteraction;

	public ObjectState State = States.Good;

	private MeshRenderer myRenderer;
	private Interaction[] interactions;
	private WorkProgress ui;

	void Start()
	{
		myRenderer = GetComponent<MeshRenderer>();
		ui = GetComponent<WorkProgress>();
		interactions = GetComponents<Interaction>();
	}

	void Update()
	{
		myRenderer.material.color = State == States.Broken ? Color.red : Color.blue;
		ui.FillAmount = State != States.Good && CurrentInteraction != null
			? Mathf.Clamp(WorkDone / CurrentInteraction.Work, 0, 1)
			: 0;
	}

	public Interaction[] GetInteractions()
	{
		return interactions;
	}

	public bool Interact(Interaction type, float workAmount)
	{
		if (type != CurrentInteraction)
		{
			CurrentInteraction = type;
			WorkDone = 0;
		}

		switch (type.Name)
		{
			case "Fix":
				if (State == States.Broken)
				{
					WorkDone += workAmount;
					if (WorkDone >= CurrentInteraction.Work)
					{
						WorkDone = 0;
						State = States.Good;
						CurrentInteraction = null;
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
