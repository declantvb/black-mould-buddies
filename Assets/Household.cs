using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Household : MonoBehaviour
{
	public int Money = 100;
    public float Hour = 0;
    public int lastHour = 0;
    public int rentAmount = 200;

	public Text label;
    public Text timeLabel;
    public NotificationSpawner notifications;
    public Transform mailman;
    public Director director;
    private IEnumerable<PlayerStatus> players;
    public GameObject letterPrefab;
    private Light[] lights;
    public int paychecks = 0;
    private bool lightsOut;
    public AudioSource AudioLight;
    public AudioSource AudioLetter;

    // Start is called before the first frame update
    void Start()
    {
        lights = FindObjectsOfType<Light>();
        mailman = transform.Find("Mailman").transform;
        director = FindObjectsOfType<MonoBehaviour>().OfType<Director>().First();
        players = FindObjectsOfType<MonoBehaviour>().OfType<PlayerStatus>();
    }

    // Update is called once per frame
    void Update()
    {
        Hour += Time.deltaTime;
		label.text = $"${Money}";
        timeLabel.text = $"{Mathf.FloorToInt(Hour * 2)}";
        if (Hour > 50)
        {
            Hour -= 50;
            DayPasses();
        }

        if ((int)Hour > lastHour || Hour < lastHour - 2)
        {
            lastHour = (int)Hour;
            StartCoroutine(HourTick(lastHour));
        }

        var old = lightsOut;
        
        lightsOut = Hour > 35 && Hour < 45;

        if (old != lightsOut)
        {
            if (AudioLight != null)
                AudioLight.Play();
        }

        foreach (var light in lights) {
            light.enabled = !lightsOut;
        }

        var stressAvg = players.Average((a) => a.stress);
        Debug.Log(stressAvg);
        // JACK DO HERE
    }

    private IEnumerator HourTick(int lastHour)
    {
        switch (lastHour)
        {
            case 1:
                notifications.SpawnNotification($"Rent due! ${rentAmount}", Color.red);
                SpawnLetter(rentAmount, Letter.LetterType.Bill, "Rent", Color.red);
                break;

            case 12:
                var rentBump = Random.Range(2, 10);
                notifications.SpawnNotification($"Rent increased due to inflation! +${rentBump}", Color.red);
                rentAmount += rentBump;
                break;

            case 33:
                if (paychecks > 0)
                {
                    notifications.SpawnNotification($"$$$ Payday! $$$", Color.yellow);
                }
                else
                {
                    notifications.SpawnNotification($"No work, no pay...", Color.yellow);
                }
                var pc = paychecks;
                paychecks = 0;
                for (int i = 0; i < pc; i++) {
                    SpawnLetter(-50, Letter.LetterType.Paycheck, "Paycheck", Color.white);
                    yield return new WaitForSeconds(0.1f);
                }
                break;

            case 43:
                var rents = FindObjectsOfType<MonoBehaviour>().OfType<Letter>().Where(l => l.type == Letter.LetterType.Bill);
                var count = rents.Count();
                var sum = rents.Sum(r => r.Cost);
                if (count > 2)
                {
                    director.Evict();
                }
                else if (count > 0)
                {
                    notifications.SpawnNotification($"EVICTION WARNING! ${sum} OWED!", Color.red);
                }
                break;
        }

        yield break;
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
        
        if (AudioLetter != null)
            AudioLetter.Play();
    }

    private void DayPasses()
    {
    }
}
