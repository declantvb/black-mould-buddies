using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
	public string Name;
	public int Cost = 1;
	public int StressCost = 1;
	public float WorkRequired = 1;
	public float WorkDone;
	public bool Continuous;
	public bool Cancelable = true;
	public int MaxStress;

	public InteractionType Type;
	
	public bool Done => !Continuous && WorkDone >= WorkRequired;

	public bool CanWork(PlayerStatus status)
	{
		return status.stress <= MaxStress;
	}

	public void DoWork(PlayerStatus status, float amount)
	{
		if (Continuous)
		{
			switch (Type)
			{
				case InteractionType.Chill:
					status.chilling = true;
					break;
				case InteractionType.Sleep:
					status.sleeping = true;
					break;
			}
		}
		else
		{
			WorkDone += amount; 
		}
	}

	public void Cancel()
	{
		if (Continuous)
		{

		}
	}

	public void ResetToDefaults()
	{
		WorkDone = 0;
	}

	public virtual void FinishWork(PlayerStatus status)
	{
		switch (Type)
		{
			case InteractionType.Chill:
				status.chilling = false;
				break;
			case InteractionType.Sleep:
				status.sleeping = false;
				break;
			case InteractionType.Bladder:
				status.needToilet = 0;
				break;
			case InteractionType.Food:
				status.needFood = 0;
				break;
			case InteractionType.Drink:
				status.removeStress(20);
				break;
			case InteractionType.None:
				break;
			default:
				Debug.Log("unknoiwn");
				break;
		}
	}

	public enum InteractionType
	{
		None,
		Bladder,
		Food,
		Chill,
		Sleep,
		Drink
	}
}
