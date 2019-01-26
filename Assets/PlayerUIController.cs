using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIController : MonoBehaviour
{
    public readonly static Color lowColor = new Color(0.8f, 0.8f, 0.8f, 0.2f);

    public PlayerStatus playerStatus;

    public Image stressBar;
    public Image face;
    public Image toiletNeedImage;
    public Image foodNeedImage;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (playerStatus != null)
        {
            var amt = 0.8f + Mathf.Sin(Time.timeSinceLevelLoad * 10) * 0.2f;
            var highColor = new Color(1, amt, amt);

            var stressProportion = playerStatus.stress / (float)PlayerStatus.MAX_STRESS;
            stressBar.fillAmount = stressProportion;
            stressBar.color = new Color(stressProportion * 0.8f + 0.2f, (1 - stressProportion) * 0.8f + 0.2f, 0.2f);

            toiletNeedImage.color = playerStatus.needToilet >= PlayerStatus.NEED_TOILET_MAX ? highColor : lowColor;
            foodNeedImage.color = playerStatus.needFood >= PlayerStatus.NEED_FOOD_MAX ? highColor : lowColor;
        }
    }
}
