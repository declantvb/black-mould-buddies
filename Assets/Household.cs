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
    public Transform mailman;
    public GameObject letterPrefab;
    private Light[] lights;

    // Start is called before the first frame update
    void Start()
    {
        lights = FindObjectsOfType<Light>();
        mailman = transform.Find("Mailman").transform;
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

    public void SpawnLetter(int cost, Letter.LetterType type, string name, Color color) 
    {
        var letter = Instantiate(letterPrefab, mailman.position, Random.rotation);
        var letterData = letter.GetComponent<Letter>();
        letterData.Cost = cost;
        letterData.letterName = name;
        letterData.Name = cost < 0 ? $"Open {name}" : $"Pay {name}";
        letterData.type = type;
        letterData.MaxStress = cost > 0 ? 90 : 200;

        letter.GetComponentInChildren<Renderer>().material.color = color;

        var rb = letter.GetComponent<Rigidbody>();
        rb.AddForceAtPosition(mailman.forward * 8 + Random.onUnitSphere * 2, rb.position + Random.onUnitSphere, ForceMode.VelocityChange);
    }

    private void DayPasses()
    {
        notifications.SpawnNotification("Rent due! $200", Color.red);
        SpawnLetter(200, Letter.LetterType.Bill, "Rent", Color.red);
    }
}
