using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInteractible : MonoBehaviour, IInteractible
{
	public float WorkRequired = 3;

	public bool broken;
	public float work;
	private MeshRenderer myRenderer;
		
	void Start()
    {
		myRenderer = this.GetComponent<MeshRenderer>();        
    }

    void Update()
    {
		myRenderer.material.color = broken ? Color.red : Color.blue;
    }

	public string[] GetInteractions()
	{
		return new string[] { "test" };
	}

	public bool Interact(string type, float workAmount)
	{
		switch (type)
		{
			case "test":
				if (broken)
				{
					work -= workAmount;
					if (work <= 0)
					{
						Debug.Log("i got fixed :O");
						broken = false;
						return true;
					}
				}
				break;
			default:
				Debug.Log($"why do {type}");
				break;
		}

		return false;
	}

	public void Break()
	{
		broken = true;
		work = WorkRequired;
	}
}
