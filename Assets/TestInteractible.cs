using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInteractible : MonoBehaviour, IInteractible
{
	private bool broken;
	private MeshRenderer myRenderer;

	// Start is called before the first frame update
	void Start()
    {
		myRenderer = this.GetComponent<MeshRenderer>();
        
    }

    // Update is called once per frame
    void Update()
    {
		myRenderer.material.color = broken ? Color.red : Color.blue;
    }

	string[] IInteractible.GetInteractions()
	{
		return new string[] { "test" };
	}

	public void Interact(string type)
	{
		switch (type)
		{
			case "test":
				if (broken)
				{
					Debug.Log("i got fixed :O");
					broken = false; 
				}
				break;
			default:
				break;
		}
	}

	public void Break()
	{
		broken = true;
	}
}
