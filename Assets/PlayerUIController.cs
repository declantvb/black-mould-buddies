using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIController : MonoBehaviour
{
    public PlayerStatus playerStatus;

    public Image stressBar;
    public Image face;

    // Start is called before the first frame update
    void Start()
    {
        stressBar = transform.Find("StressBar").GetComponent<Image>();
        face = transform.Find("Face").GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerStatus != null)
        {
            var stressProportion = playerStatus.stress / (float)PlayerStatus.MAX_STRESS;
            stressBar.fillAmount = stressProportion;
            stressBar.color = new Color(stressProportion * 0.8f + 0.2f, (1 - stressProportion) * 0.8f + 0.2f, 0.2f);
        }
    }
}
