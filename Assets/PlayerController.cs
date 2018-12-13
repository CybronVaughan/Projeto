using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{

    private float MoveSpeed = 6;
    private float RotationSpeed = 180;
    public AudioClip Footsteps;
    public AudioClip WaterFootsteps;
    public AudioClip Death;
    private float CooldownRate = 0.3f;
    private float CooldownTimer = 0;
    public GameObject FootPos;
    public GameObject PlayerCamera;

    [HideInInspector] public string Ataque = "Ataque";
    [HideInInspector] public string Corre = "Corre";
    [HideInInspector] public string Anda = "Anda";
    [HideInInspector] public string Hit = "Hit";
    [HideInInspector] public string Parado = "Parado";
    [HideInInspector] public string Morte = "Morte";
    [HideInInspector] private string animacao;

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
            Anima(Morte);

            Destroy(gameObject);
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
        transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X") * RotationSpeed * Time.deltaTime, 0));
        if (!cc.isGrounded)
        {
            gravidade += Physics.gravity * Time.deltaTime;
        }
        move += gravidade;
        cc.Move(move * Time.deltaTime);

        if (CooldownTimer == 0f && cc.isGrounded == true && cc.velocity.magnitude > 1f)
        {
            CooldownTimer = CooldownRate;
            if (Water)
            {
                PlaySound(WaterFootsteps, FootPos);
            }
            else
            {
                PlaySound(Footsteps, FootPos);
            }
        }

        if (!Input.anyKey)
        {
            Anima(Parado);
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Anima(Ataque);
            }
            else if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) && ((!Input.GetKey(KeyCode.S) || !Input.GetKey(KeyCode.DownArrow))))
            {
                Anima(Corre);
                MoveSpeed = 6;
                CooldownRate = 0.3f;
            }
            else if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.RightArrow)) && (!Input.GetKey(KeyCode.W) || !Input.GetKey(KeyCode.UpArrow)))
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

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Water"))
        {
            Water = true;
        }
        else
        {
            Water = false;
        }
    }

    public void PlaySound(AudioClip clip, GameObject Object)
    {
        AudioSource.PlayClipAtPoint(clip, Object.transform.position);
    }
}