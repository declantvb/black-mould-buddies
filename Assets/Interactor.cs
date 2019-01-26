using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Interactor : MonoBehaviour
{
	public RectTransform menuPanel;
	public GameObject optionPrefab;

	private Transform myTransform;
	private CharacterController myplayer;
	private PlayerStatus mystatus;
	private Interaction[] currentList;
	private bool clicked;
	private bool menuOpen;
	private BaseInteractible interactible;
	private bool selectChanged;
	private GameObject[] menuOptions;
	private int selectedItem = -1;
	private Vector3 oldPosition;
	private Quaternion oldRotation;

	// Start is called before the first frame update
	void Start()
	{
		myTransform = this.transform;
		myplayer = GetComponent<CharacterController>();
		mystatus = GetComponent<PlayerStatus>();
	}

	// Update is called once per frame
	void Update()
	{
		if (!menuOpen && selectedItem >= 0)
		{
			if (!currentList[selectedItem].CanWork(mystatus))
			{
				StopInteracting();
				return;
			}

			var complete = interactible.Interact(mystatus, currentList[selectedItem], Time.deltaTime);
			if (complete)
			{
				StopInteracting();
				return;
			}

			var inputX = Input.GetAxis("Horizontal" + myplayer.Player);
			var inputY = Input.GetAxis("Vertical" + myplayer.Player);

			if (inputX != 0 || inputY != 0)
			{
				StopInteracting();
			}
		}

		if (Input.GetAxis("Fire" + myplayer.Player) > 0)
		{
			if (!clicked)
			{
				clicked = handleclick();
			}
		}
		else
		{
			clicked = false;
		}

		if (menuOpen)
		{
			var inputY = Input.GetAxis("Vertical" + myplayer.Player);
			if (inputY > 0)
			{
				if (!selectChanged)
				{
					selectItem(selectedItem + 1);
					selectChanged = true;
				}
			}
			else if (inputY < 0)
			{
				if (!selectChanged)
				{
					selectItem(selectedItem - 1);
					selectChanged = true;
				}
			}
			else
			{
				selectChanged = false;
			}
		}
	}

	private void StopInteracting()
	{
		selectedItem = -1;
		interactible.StopInteracting(mystatus);
		if (interactible.lockPosition != null)
		{
			unlockplayer(); 
		}
	}

	private bool handleclick()
	{
		if (!menuOpen)
		{
			var colliders = Physics.OverlapSphere(myTransform.position + myTransform.forward, 2);

			foreach (var collider in colliders.OrderBy(c => (myTransform.position + myTransform.forward - c.transform.position).sqrMagnitude))
			{
				var tempInteractible = collider.GetComponentInParent<BaseInteractible>();
				if (tempInteractible != null)
				{
					currentList = tempInteractible.GetInteractions();

					if (currentList.Length == 0) {
						currentList = null;
						return true;
					}

					interactible = tempInteractible;
					buildmenu();
					return true;
				}
			}
		}
		else
		{
			destroymenu();
			if (interactible.lockPosition != null)
			{
				lockplayer(interactible.lockPosition); 
			}
			return true;
		}
		return false;
	}

	private void lockplayer(Transform target)
	{
		oldPosition = transform.position;
		oldRotation = transform.rotation;

		GetComponent<Rigidbody>().isKinematic = true;

		transform.position = target.position;
		transform.rotation = target.rotation;

		myplayer.Locked = true;
	}

	private void unlockplayer()
	{
		transform.position = oldPosition;
		transform.rotation = oldRotation;

		GetComponent<Rigidbody>().isKinematic = false;
		myplayer.Locked = false;
	}

	private void buildmenu()
	{
		myplayer.Locked = true;
		menuOpen = true;
		menuPanel.gameObject.SetActive(true);

		menuPanel.GetComponentInChildren<Text>().text = interactible.Name;
		var pos = 50;
		List<GameObject> newMenuOptions = new List<GameObject>();
		foreach (var item in currentList)
		{
			var newOption = Instantiate(optionPrefab, menuPanel);
			var transform1 = newOption.GetComponent<RectTransform>();
			transform1.localPosition = new Vector3(0, pos, 0);
			newMenuOptions.Add(newOption);
			pos -= 40;
		}

		menuOptions = newMenuOptions.ToArray();
		selectedItem = 0;
		selectItem(selectedItem);
	}

	private void updatemenu()
	{
		for (int i = 0; i < menuOptions.Length; i++)
		{
			var item = menuOptions[i];
			var interaction = currentList[i];
			var selected = selectedItem == i;

			if (interaction.CanWork(mystatus))
			{
				item.GetComponent<Image>().color = selected ? Color.cyan : Color.white;
				item.GetComponentInChildren<Text>().text = $"{interaction.Name}";
			}
			else
			{
				item.GetComponent<Image>().color = Color.red;
				item.GetComponentInChildren<Text>().text = $"{interaction.Name} (X)";
			}
		}
	}

	private void selectItem(int newSelection)
	{
		if (newSelection >= menuOptions.Length)
		{
			newSelection -= menuOptions.Length;
		}
		else if (newSelection < 0)
		{
			newSelection += menuOptions.Length;
		}
		
		selectedItem = newSelection;

		updatemenu();
	}

	private void destroymenu()
	{
		foreach (Transform child in menuPanel)
		{
			if (child.GetComponent<Image>() != null)
			{
				Destroy(child.gameObject);
			}
		}
		menuPanel.gameObject.SetActive(false);
		menuOpen = false;
		myplayer.Locked = false;
	}
}
