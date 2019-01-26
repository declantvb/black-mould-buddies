using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Household : MonoBehaviour
{
	public int Money = 100;
    public float Hour = 0;

	public Text label;
    public Text timeLabel;
    public NotificationSpawner notifications;
    private Light[] lights;

    // Start is called before the first frame update
    void Start()
    {
        lights = FindObjectsOfType<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        Hour += Time.deltaTime * 10;
		label.text = $"${Money}";
        timeLabel.text = $"{Mathf.FloorToInt(Hour * 2)}";
        if (Hour > 50)
        {
            Hour -= 50;
            DayPasses();
        }

        var lightsOut = Hour > 35 && Hour < 45;

        foreach (var light in lights) {
            light.enabled = !lightsOut;
        } 
    }

    private void DayPasses()
    {
        notifications.SpawnNotification("Rent due! $200", Color.red);
    }
}
