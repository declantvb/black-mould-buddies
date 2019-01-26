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
        var notification = Instantiate(notificationPrefab, transform);
        var textObj = notification.GetComponent<UnityEngine.UI.Text>();
        textObj.text = text;
        textObj.color = color;
        StartCoroutine(internalSpawn(notification, textObj));
    }

    IEnumerator internalSpawn(GameObject notification, UnityEngine.UI.Text textObj) 
    {
        var timeLeft = 3f;

        var color = textObj.color;

        while (timeLeft > 0) {
            yield return wait;
            timeLeft -= Time.deltaTime;
            var alpha = Mathf.Clamp(timeLeft, 0, 1);
            notification.transform.position = notification.transform.position + new Vector3(0, Time.deltaTime * 30);
            textObj.color = new Color(color.r, color.g, color.b, alpha);
        }

        Destroy(notification);
    }
}
