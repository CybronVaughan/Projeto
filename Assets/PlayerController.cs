using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{

    private float MoveSpeed = 6;
    private float RotationSpeed = 180;
    public AudioClip Footsteps;
    public AudioClip Death;
    private float CooldownRate = 0.3f;
    private float CooldownTimer = 0;
    public GameObject FootPos;
    public GameObject PlayerCamera;
    private string Ataque = "Ataque";
    private string Corre = "Corre";
    private string Anda = "Anda";
    private string Hit = "Hit";
    private string Parado = "Parado";
    private string Morte = "Morte";
    private string animacao;
    CharacterController cc;
    private Animator anim;
    protected Vector3 gravidade = Vector3.zero;
    protected Vector3 move = Vector3.zero;
    private bool jump = false;
    private float Attack = 0f;
    public int Health = 3;
    private int h = 1;


    public void Damage()
    {
        Health--;
        if (Health <= 0)
        {
            PlayerCamera.transform.parent = null;
            PlaySound(Death, gameObject);
            Anima(Morte);
        }
    }

    void Start()
    {
        cc = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        animacao = Parado;
    }

    void Update()
    {
        if (Attack > 0f && Attack <= 0.5f)
        {
            Attack = Attack - Time.deltaTime;
        }
        else if (Attack <= 0f)
        {
            Attack = 0f;
            anim.SetBool(Ataque, false);
        }

        if (CooldownTimer < 0)
        {
            CooldownTimer = 0;
        }
        if (CooldownTimer > 0)
        {
            CooldownTimer -= Time.deltaTime;
        }

        if (!anim.GetBool(Ataque) && Attack == 0f)
        {
            h = 1;
        }

        Vector3 move = Input.GetAxis("Vertical") * transform.TransformDirection(Vector3.forward) * MoveSpeed * h;
        move += transform.right * Input.GetAxis("Horizontal") * Time.deltaTime * 150 * h;
        transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X") * RotationSpeed * Time.deltaTime * h, 0));
        if (!cc.isGrounded)
        {
            gravidade += Physics.gravity * Time.deltaTime;
        }
        move += gravidade;
        cc.Move(move * Time.deltaTime);

        if (CooldownTimer == 0f && cc.isGrounded == true && cc.velocity.magnitude > 1f)
        {
            CooldownTimer = CooldownRate;
            PlaySound(Footsteps, FootPos);
        }

        if (!Input.anyKey && !anim.GetBool(Ataque) && Attack <= 0f)
        {
            Anima(Parado);            
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space) && anim.GetBool(Parado))
            {
                Attack = 0.5f;
                h = 0;
                Anima(Ataque);
            }
            else if (!anim.GetBool(Ataque) && (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) && ((!Input.GetKey(KeyCode.S) || !Input.GetKey(KeyCode.DownArrow))))
            {
                Anima(Corre);
                MoveSpeed = 6;
                CooldownRate = 0.3f;
            }
            else if (!anim.GetBool(Ataque) && (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.RightArrow)) && (!Input.GetKey(KeyCode.W) || !Input.GetKey(KeyCode.UpArrow)))
            {
                Anima(Anda);
                MoveSpeed = 2;
                CooldownRate = 0.5f;
            }
        }
    }

    private void Anima(string anima)
    {
        anim.SetBool(animacao, false);
        anim.SetBool(anima, true);
        animacao = anima;
    }

    public void PlaySound(AudioClip clip, GameObject Object)
    {
        AudioSource.PlayClipAtPoint(clip, Object.transform.position);
    }
}