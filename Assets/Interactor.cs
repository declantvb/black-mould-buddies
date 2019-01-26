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
	private Interaction[] currentList;
	private bool clicked;
	private bool menuOpen;
	private IInteractible interactible;
	private bool selectChanged;
	private GameObject[] menuOptions;
	private int selectedItem;

	// Start is called before the first frame update
	void Start()
	{
		myTransform = this.transform;
		myplayer = GetComponent<CharacterController>();
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetAxis("Fire1" + myplayer.Player) > 0)
		{
			if (!clicked)
			{
				if (!menuOpen)
				{
					var colliders = Physics.OverlapSphere(myTransform.position + myTransform.forward, 2);

					foreach (var collider in colliders.OrderBy(c => (myTransform.position + myTransform.forward - c.transform.position).sqrMagnitude))
					{
						interactible = collider.GetComponentInParent<IInteractible>();
						if (interactible != null)
						{
							currentList = interactible.GetInteractions();

							buildmenu();
							clicked = true;
						}
					}
				}
				else
				{
					var complete = interactible.Interact(currentList[selectedItem], Time.deltaTime);
					clicked = complete;
					destroymenu();
					clicked = true;
				}
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
			newOption.GetComponentInChildren<Text>().text = item.Name;
			newOption.GetComponent<Image>().color = Color.white;
			newMenuOptions.Add(newOption);
			pos -= 40;
		}

		menuOptions = newMenuOptions.ToArray();
		selectedItem = 0;
		selectItem(selectedItem);
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

		var oldItem = menuOptions[selectedItem];
		oldItem.GetComponent<Image>().color = Color.white;

		var item = menuOptions[newSelection];
		item.GetComponent<Image>().color = Color.cyan;

		selectedItem = newSelection;
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
