using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeScript : MonoBehaviour
{
    public float duration = 5;
    private float ratio;

    void Update()
    {
        if (Time.time > duration)
        {
            Destroy(gameObject);
        }
        Color myColor = GetComponent<Text>().color;
        ratio = Time.time / duration;
        myColor.a = Mathf.Lerp(1, 0, ratio);
        GetComponent<Text>().color = myColor;
        
    }
}
