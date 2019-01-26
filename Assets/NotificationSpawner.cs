using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationSpawner : MonoBehaviour
{
    public GameObject notificationPrefab;

    WaitForEndOfFrame wait = new WaitForEndOfFrame();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnNotification(string text, Color color) 
    {
        StartCoroutine(internalSpawn(text, color));
    }

    IEnumerator internalSpawn(string text, Color color) 
    {
        var notification = Instantiate(notificationPrefab);
        var timeLeft = 3f;

        while (timeLeft > 0) {
            yield return wait;
            timeLeft -= Time.deltaTime;
            notification.transform.position = notification.transform.position + new Vector3(0, Time.deltaTime);
        }

        Destroy(notification);
    }
}
