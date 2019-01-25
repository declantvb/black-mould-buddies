using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Director : MonoBehaviour
{
	public IInteractible[] Interactibles { get; set; }
	public float TimeUntilNextBreak { get; set; } = 0;

	void Start()
	{
		Interactibles = GameObject.FindObjectsOfType<MonoBehaviour>().OfType<IInteractible>().ToArray();
	}

	void FixedUpdate()
	{
		if (TimeUntilNextBreak == 0)
		{
			TimeUntilNextBreak = 15 * 1000;

			var inter = Interactibles[Random.Range(0, Interactibles.Length - 1)];
			inter.Break();
		}
	}
}
