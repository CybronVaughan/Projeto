using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemHand : MonoBehaviour
{
    private float HitCount = 0.75f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (HitCount <= 0.75f && HitCount > 0f)
        {
            HitCount--;
        }
        else if (HitCount <= 0)
        {
            HitCount = 0.75f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && HitCount <= 0)
        {
            other.GetComponent<PlayerController>().Damage();
        }
    }
}
