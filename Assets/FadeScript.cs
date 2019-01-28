using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeScript : MonoBehaviour
{
    private float duration = 10f;
    private float ratio;
    private bool starter = false;

    private void Start()
    {
        starter = true;
    }

    void Update()
    {
        if (Time.time > duration && starter)
        {
            Destroy(gameObject);
        }
        Color myColor = GetComponent<Text>().color;
        ratio = Time.time / duration;
        myColor.a = Mathf.Lerp(1, 0, ratio);
        GetComponent<Text>().color = myColor;
        
    }
}
