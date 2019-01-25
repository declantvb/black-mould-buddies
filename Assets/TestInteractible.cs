using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInteractible : MonoBehaviour, IInteractible
{
	private bool state;
	private MeshRenderer myRenderer;

	// Start is called before the first frame update
	void Start()
    {
		myRenderer = this.GetComponent<MeshRenderer>();
        
    }

    // Update is called once per frame
    void Update()
    {
		myRenderer.material.color = state ? Color.red : Color.blue;
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
				//todo
				Debug.Log("i got touched :O");
				state = !state;
				break;
			default:
				break;
		}
	}
}
