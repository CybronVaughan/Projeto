using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class GolemAttackScript : MonoBehaviour
{

    [HideInInspector] public float lookRadius = 10f;
    Transform target;
    NavMeshAgent agent;
    public Animator animgolem;
    private string idle = "idle";
    private string walk = "walk";
    private string hit = "hit";
    private string punch = "punch";
    private string death = "death";
    private string anim = "idle";
    public GameObject Player;
    public AudioClip GolemHit;
    public AudioClip GolemDeath;
    public int GolemHealth = 3;
    public Image Heart1, Heart2, Heart3;

    // Use this for initialization
    void Start()
    {
        target = PlayerManager.instance.Player.transform;
        agent = GetComponent<NavMeshAgent>();
        animgolem = GetComponent<Animator>();
    }

    public void GolemDamage()
    {
        GolemHealth--;
        Player.GetComponent<PlayerController>().PlaySound(GolemHit, gameObject);
        GolemAnima(hit);
        switch (GolemHealth)
        {
            case 2:
                {
                    Heart1.enabled = false;
                    break;
                }
            case 1:
                {
                    Heart2.enabled = false;
                    break;
                }
            case 0:
                {
                    Heart3.enabled = false;
                    break;
                }
            default:
                {
                    break;
                }
        }
        if (GolemHealth <= 0)
        {
            Player.GetComponent<PlayerController>().PlaySound(GolemDeath, gameObject);
            GolemAnima(death);
            Player.GetComponent<PlayerController>().PlayerCamera.GetComponent<GameOverTimer>().timer = 3f;
            GetComponent<GolemAttackScript>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);
        if (distance <= lookRadius)
        {
            agent.SetDestination(target.position);
            if (distance <= agent.stoppingDistance)
            {
                FaceTarget();
            }
        }

        if (agent.velocity.magnitude >= 1f && !animgolem.GetBool(punch))
        {
            GolemAnima(walk);
        }
        else if (agent.velocity.magnitude < 1f && !animgolem.GetBool(punch) || agent.velocity.magnitude < 1f && Player.GetComponent<PlayerController>().Health <= 0)
        {
            GolemAnima(idle);
        }
        if (animgolem.GetBool(hit))
        {
            GolemAnima(punch);
        }

        if (Player.GetComponent<PlayerController>().isActiveAndEnabled == false && animgolem.GetBool(punch))
        {
            GolemAnima(idle);
        }
    }

    private void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 0.5f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }

    public void GolemAnima(string animus)
    {
        animgolem.SetBool(anim, false);
        animgolem.SetBool(animus, true);
        anim = animus;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == Player && other.GetComponent<PlayerController>().Health > 0 && other.GetComponent<PlayerController>().isActiveAndEnabled == true)
        {
            GolemAnima(punch);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == Player && other.GetComponent<PlayerController>().isActiveAndEnabled == true)
        {
            GolemAnima(walk);
        }
    }
}
