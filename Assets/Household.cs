using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Household : MonoBehaviour
{
	public int Money = 100;
    public float Hour = 0;

	public Text label;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Hour += Time.deltaTime / 2;
		label.text = $"${Money}";
        if (Hour > 24) Hour -= 24;
    }
}
