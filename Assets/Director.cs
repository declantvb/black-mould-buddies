using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Director : MonoBehaviour
{
	public BaseInteractible[] Interactibles;
	public float TimeBetweenBreaks = 10;

	public float TimeUntilNextBreak = 0;

	void Start()
	{
		Interactibles = GameObject.FindObjectsOfType<MonoBehaviour>().OfType<BaseInteractible>().Where(x => x.Breakable).ToArray();
		//Time.timeScale = 0;
	}

	void FixedUpdate()
	{
		TimeUntilNextBreak -= Time.fixedDeltaTime;
		if (TimeUntilNextBreak <= 0)
		{
			TimeUntilNextBreak = TimeBetweenBreaks;

			var inter = Interactibles[Random.Range(0, Interactibles.Length - 1)];
			inter.Break();
			Debug.Log($"broke {inter.Name}");
		}
	}
}
