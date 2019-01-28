using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingTimer : MonoBehaviour
{
    private float t = 10f;

    void Start()
    {
        t = 5f;
    }

    void Update()
    {
        if (t > 0f && t <= 5f)
        {
            t = t - Time.deltaTime;
        }

        if (t <= 0f)
        {
            SceneManager.LoadScene("Menu");
        }
    }
}
