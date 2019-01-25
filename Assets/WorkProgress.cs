using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorkProgress : MonoBehaviour
{
	public GameObject barPrefab;
	private Image bar;
	public float FillAmount = 1f;

	// Start is called before the first frame update
	void Start()
    {
		bar = Instantiate(barPrefab, FindObjectOfType<WorldSpaceUi>().GetComponent<Canvas>().transform).GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
		bar.transform.position = Camera.main.WorldToScreenPoint(transform.position + Vector3.up * 1f);
		bar.fillAmount = FillAmount;
    }
}
