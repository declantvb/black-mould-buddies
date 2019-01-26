using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
	public string Name;
	public int Cost = 1;
	public float WorkRequired = 1;
	//public bool DefaultActive;

	//public bool Active;
	public float WorkDone;

	public bool Done => WorkDone >= WorkRequired;

	public void DoWork(float amount)
	{
		//if (Active)
		{
			WorkDone += amount;
			if (WorkDone >= WorkRequired)
			{
				//WorkDone = 0;
			}
		}
	}

	public void ResetToDefaults()
	{
		//Active = DefaultActive;
		WorkDone = 0;
	}
}
