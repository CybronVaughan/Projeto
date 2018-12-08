using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{

    public float MoveSpeed;
    public float RotationSpeed;
    public float ForcaPulo;
    public AudioClip Footsteps;
    public AudioClip WaterFootsteps;
    public AudioClip Death;
    private float CooldownRate = 0.5f;
    private float CooldownTimer = 0;
    public GameObject FootPos;
    public GameObject PlayerCamera;

    [HideInInspector] public bool Water = false;

    CharacterController cc;
    private Animator anim;
    protected Vector3 gravidade = Vector3.zero;
    protected Vector3 move = Vector3.zero;
    private bool jump = false;

    public int Health = 3;


    public void Damage()
    {
        Health--;
        if (Health <= 0)
        {
            PlayerCamera.transform.parent = null;
            PlaySound(Death, gameObject);

            anim.SetBool("Parado", false);
            anim.SetBool("Anda", false);
            anim.SetBool("Corre", false);
            anim.SetTrigger("Morte");

            Destroy(gameObject);
        }
    }

    void Start()
    {
        cc = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        anim.SetTrigger("Parado");

        CooldownRate = 0.3f;
        MoveSpeed = MoveSpeed + 4;
        RotationSpeed = RotationSpeed + 100;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.K)) { Damage(); }

        if (CooldownTimer < 0)
        {
            CooldownTimer = 0;
        }
        if (CooldownTimer > 0)
        {
            CooldownTimer -= Time.deltaTime;
        }

        Vector3 move = Input.GetAxis("Vertical") * transform.TransformDirection(Vector3.forward) * MoveSpeed;
        move += transform.right * Input.GetAxis("Horizontal") * Time.deltaTime * 80;
        /*if (PowerUp == false)
        {
            move += transform.right * Input.GetAxis("Horizontal") * Time.deltaTime * 40;
        }
        else
        {
            move += transform.right * Input.GetAxis("Horizontal") * Time.deltaTime * 60;
        }*/
        transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X") * RotationSpeed * Time.deltaTime, 0));
        //transform.Rotate(new Vector3(0, Input.GetAxis("Mouse Y") * RotationSpeed * Time.deltaTime, 0));



        if (!cc.isGrounded)
        {
            gravidade += Physics.gravity * Time.deltaTime;
        }
        else
        {
            gravidade = Vector3.zero;
            if (jump)
            {
                gravidade.y = ForcaPulo;
                jump = false;
            }
        }
        move += gravidade;
        cc.Move(move * Time.deltaTime);

        if (CooldownTimer == 0f && cc.isGrounded == true && cc.velocity.magnitude > 2f)
        {
            CooldownTimer = CooldownRate;
            if (Water)
            {
                PlaySound(WaterFootsteps, FootPos);
            } else
            {
                PlaySound(Footsteps, FootPos);
            }
        }

        if (!Input.anyKey)
        {
            anim.SetBool("Parado", true);
            anim.SetBool("Anda", false);
            anim.SetBool("Corre", false);
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //anim.SetTrigger("Pula");
                //jump = true;
            }
            else if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
            {
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Water"))
        {
            Water = true;
        } else
        {
            Water = false;
        }
    }

    public void PlaySound(AudioClip clip, GameObject Object)
    {
        AudioSource.PlayClipAtPoint(clip, Object.transform.position);
    }
}