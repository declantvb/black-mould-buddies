//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class TestInteractible : MonoBehaviour, IInteractible
//{
//	public float WorkRequired = 3;

//	public bool broken;
//	public float work;
//	private MeshRenderer myRenderer;
//	private WorkProgress ui;

//	void Start()
//    {
//		myRenderer = this.GetComponent<MeshRenderer>();
//		ui = GetComponent<WorkProgress>();
//		work = WorkRequired;
//    }

//    void Update()
//    {
//		myRenderer.material.color = broken ? Color.red : Color.blue;
//		ui.FillAmount = broken ? Mathf.Clamp(work / WorkRequired, 0, 1) : 0;
//    }

//	public string[] GetInteractions()
//	{
//		return new string[] { "test" };
//	}

//	public bool Interact(string type, float workAmount)
//	{
//		switch (type)
//		{
//			case "test":
//				if (broken)
//				{
//					work += workAmount;
//					if (work >= WorkRequired)
//					{
//						work = WorkRequired;
//						Debug.Log("i got fixed :O");
//						broken = false;
//						return true;
//					}
//				}
//				break;
//			default:
//				Debug.Log($"why do {type}");
//				break;
//		}

//		return false;
//	}

//	public void Break()
//	{
//		broken = true;
//		work = 0;
//	}
//}
