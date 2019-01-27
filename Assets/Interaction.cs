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

	public GameObject ThrowPrefab;
	public float throwForceMultiplier;
	public float throwTorqueMultiplier;

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
				status.removeStress(15);
				Transform transform1 = status.GetComponent<Transform>();
				doThrowPrefab(
					transform1.position - transform1.right + Vector3.up * 3,
					-transform1.forward);
				break;
			case InteractionType.Work:
				status.household.paychecks++;
				break;
			case InteractionType.None:
				break;
			default:
				Debug.Log("unknoiwn");
				break;
		}
	}

	private void doThrowPrefab(Vector3 pos, Vector3 dir)
	{
		var newthrow = Instantiate(ThrowPrefab, pos, Quaternion.identity);

		var rb = newthrow.GetComponent<Rigidbody>();
		rb.isKinematic = false;
		rb.AddForce(dir * throwForceMultiplier);
		rb.AddTorque(new Vector3(UnityEngine.Random.Range(-throwTorqueMultiplier, throwTorqueMultiplier), UnityEngine.Random.Range(-throwTorqueMultiplier, throwTorqueMultiplier), UnityEngine.Random.Range(-throwTorqueMultiplier, throwTorqueMultiplier)));
	}

	public enum InteractionType
	{
		None,
		Bladder,
		Food,
		Chill,
		Sleep,
		Drink,
		Work
	}
}
