using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Interactor : MonoBehaviour
{
	private Transform myTransform;

	private string[] currentList;
	private bool clicked;

	// Start is called before the first frame update
	void Start()
    {
		myTransform = this.transform;
    }

    // Update is called once per frame
    void Update()
	{
		if (Input.GetAxis("Fire1") > 0)
		{
			if (!clicked)
			{
				var colliders = Physics.OverlapSphere(myTransform.position + myTransform.forward, 1);

				foreach (var collider in colliders.OrderBy(c => (myTransform.position + myTransform.forward - c.transform.position).sqrMagnitude))
				{
					IInteractible interactible = collider.GetComponentInParent<IInteractible>();
					if (interactible != null)
					{
						//currentList = interactible.GetInteractions();
						interactible.Interact("test");
						clicked = true;
						break;
					}
				} 
			}
		}
		else
		{
			clicked = false;
		}

		//if (currentList != null)
		//{
		//	Debug.Log("current list : " + string.Join(",", currentList));
		//}
	}
}
