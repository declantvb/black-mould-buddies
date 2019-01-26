using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeAtStart : MonoBehaviour
{
    private Image image;
    public float fullAlphaPoint;

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        var scale = 1 + (120 - Camera.main.fieldOfView) / 200;
        image.color = new Color(image.color.r, image.color.g, image.color.b, (Camera.main.fieldOfView - 60) / fullAlphaPoint);
        image.transform.localScale = new Vector3(scale, scale, 1);
    }
}
