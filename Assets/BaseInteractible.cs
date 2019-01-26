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

	public string ObjectName;
	public Interaction CurrentInteraction;
	public ObjectState State = States.Good;

	//private MeshRenderer[] myRenderers;
	private Interaction[] interactions;
	private Household household;
	private ParticleSystem ps;
	private WorkProgress ui;

	public string Name => ObjectName;

	void Start()
	{
		//myRenderers = GetComponentsInChildren<MeshRenderer>();
		ui = GetComponent<WorkProgress>();
		interactions = GetComponents<Interaction>();
		household = FindObjectOfType<Household>();
		ps = GetComponentInChildren<ParticleSystem>();
		ps?.Stop();
	}

	void Update()
	{
		//myRenderer.material.color = State == States.Broken ? Color.red : Color.blue;
		ui.FillAmount = State != States.Good && CurrentInteraction != null
			? Mathf.Clamp(CurrentInteraction.WorkDone / CurrentInteraction.WorkRequired, 0, 1)
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
			CurrentInteraction?.ResetToDefaults();
			if (household.Money >= type.Cost)
			{
				household.Money -= type.Cost;
				CurrentInteraction = type; 
			}
		}
		CurrentInteraction.DoWork(workAmount);

		if (CurrentInteraction.Done)
		{
			if (CurrentInteraction.Name == "Fix")
			{
				State = States.Good;
				ps?.Stop(); 
			}
			CurrentInteraction?.ResetToDefaults();
			CurrentInteraction = null;
			return true; 
		}

		return false;
	}

	public virtual void Break()
	{
		State = States.Broken;
		ps?.Play();
	}
}
