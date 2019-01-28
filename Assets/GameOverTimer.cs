using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverTimer : MonoBehaviour
{
    public float timer = 10f;

    void Update()
    {
        if (timer > 0 && timer <= 3)
        {
            timer = timer - Time.deltaTime;
        }

        if (timer <= 0f)
        {
            SceneManager.LoadScene("GameOver");
        }
    }
}
