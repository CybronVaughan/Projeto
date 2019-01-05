using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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
        if (GolemHealth <= 0)
        {
            Player.GetComponent<PlayerController>().PlaySound(GolemDeath, gameObject);
            GolemAnima(death);
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
        if (other.gameObject == Player && other.GetComponent<PlayerController>().Health > 0)
        {
            GolemAnima(punch);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == Player)
        {
            GolemAnima(walk);
        }
    }
}
