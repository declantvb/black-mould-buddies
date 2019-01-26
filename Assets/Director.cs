using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Director : MonoBehaviour
{
	public BaseInteractible[] Interactibles;
	public float TimeBetweenBreaks = 10;

	public float TimeUntilNextBreak = 0;

	public bool playing;

	void Start()
	{
		Interactibles = GameObject.FindObjectsOfType<MonoBehaviour>().OfType<BaseInteractible>().Where(x => x.Breakable).ToArray();
		Time.timeScale = 0;
		Camera.main.fieldOfView = 120;
	}

	private void Update() {
		if (Input.GetButtonDown("Fire1") && Time.timeScale < 0.1f)
		{
			Time.timeScale = 1;
			playing = true;
		}
	}

	void FixedUpdate()
	{
		var targetFov = 120;
		if (playing)
		{
			targetFov = 60;
		}

		if (Mathf.Abs(Camera.main.fieldOfView - targetFov) < 0.1f)
		{
			Camera.main.fieldOfView = targetFov;
		}
		else
		{
			Camera.main.fieldOfView = (Camera.main.fieldOfView * 9 + targetFov) / 10;
		}

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
