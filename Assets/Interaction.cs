using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
	public string Name;
	public int Cost = 1;
	public float WorkRequired = 1;
	public float WorkDone;

	public ResultType Type;
	
	public bool Done => WorkDone >= WorkRequired;

	public void DoWork(float amount)
	{
		WorkDone += amount;
	}

	public void ResetToDefaults()
	{
		WorkDone = 0;
	}

	public void ApplyResult(PlayerStatus status)
	{
		switch (Type)
		{
			case ResultType.Bladder:
				status.needToilet = 0;
				break;
			case ResultType.Food:
				status.needFood = 0;
				break;
			default:
				Debug.Log("unknoiwn");
				break;
		}
	}

	public enum ResultType
	{
		None,
		Bladder,
		Food
	}
}
