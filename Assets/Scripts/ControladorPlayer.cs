using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ControladorPlayer : MonoBehaviour
{   
    [SerializeField] public float Speed;
    [SerializeField] public float AlturaSalto;
    [SerializeField] public float PotenciaSalto;
    [SerializeField] private float Gravedad;
    [SerializeField] private int Fase1;
    [SerializeField] private int Fase2;
    [SerializeField] public bool Saltando;
    [SerializeField] public float Fallen;
    [SerializeField] public Animator animator;
    [SerializeField] private float YPos;
    [SerializeField] private int sky;
    [SerializeField] private Vector3 posEnd;
    [SerializeField] public Rigidbody2D rPlayer;
    [SerializeField] public CapsuleCollider2D ccPlayer;

    [SerializeField] private float posX;
    [SerializeField] private float posY;
    [SerializeField] private float posZ;

    [SerializeField] private bool movimiento;

    //Detectar el piso
    [SerializeField] private RaycastHit2D hit;
    [SerializeField] public Vector3 v3;
    [SerializeField] public float distance;
    [SerializeField] public LayerMask layer;


    private Enemigo m_enemigo = null;
    bool isLeft = false;
    bool isRight = false;
    bool isJump = false;

    void Start()
    {
        rPlayer = GetComponent<Rigidbody2D>();
        ccPlayer = GetComponent<CapsuleCollider2D>();
        movimiento = true;
    }

    void Awake()
    {
        animator = GetComponent<Animator>();
        m_enemigo = GameObject.FindObjectOfType<Enemigo> ();
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position + v3, Vector3.up * -1 * distance);
    }

    public bool CheckCollision
    {
        get
        {
            hit = Physics2D.Raycast(transform.position + v3, transform.up * -1, distance, layer);
            return hit.collider != null;
        }
    }

    public void Detector_Plataforma()
    {
        if(CheckCollision)
        {
            animator.SetBool("jump", false);
            sky = 0;
            if(!Saltando)
            {
                Gravedad = 0;
                Fase1 = 0;
                Fase2 = 0;
            }
        }
        else
        {
            animator.SetBool("jump", true);
            if(!Saltando)
            {
                switch(Fase2)
                {
                    case 0:
                        Gravedad = 0;
                        Fase1 = 0;
                        break;
                    case 1:
                        if(Gravedad > -10)
                        {
                            Gravedad -= AlturaSalto / Fallen * Time.deltaTime;
                        }
                        break;
                }
            }
        }

        if(transform.position.y > YPos)
        {
            animator.SetFloat("gravedad", 1);
        }

        if(transform.position.y < YPos)
        {
            animator.SetFloat("gravedad", 0);
            switch(sky)
            {
                case 0:
                    animator.Play("Base Layer.Jump", 0, 0f);
                    sky++;
                    break;
            }
        }
        YPos = transform.position.y;
    }

    public void ClickLeft()
    {
        isLeft = true;
    }

    public void ReleaseLeft()
    {
        isLeft = false;
    }

    public void ClickRight()
    {
        isRight = true;
    }

    public void ReleaseRight()
    {
        isRight = false;
    }

    public void ClickJump()
    {
        isJump = true;
    }

    public void Move()
    {
        if(Input.GetKey(KeyCode.D) || isRight == true)
        {
            transform.Translate(Vector3.right * Speed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(0,0,0);
            animator.SetBool("run", true);
        }
        else
        {
            animator.SetBool("run", false);
        }

        if(Input.GetKey(KeyCode.A) || isLeft == true)
        {
            transform.Translate(Vector3.right * Speed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(0,180,0);
            animator.SetBool("run", true);
        }
    }

    public void Jump()
    {
        if(Input.GetKey(KeyCode.W) || isJump == true)
        {
            switch(Fase1)
            {
                case 0:
                    if(CheckCollision)
                    {
                        Gravedad = AlturaSalto;
                        Fase1 = 1;
                        Saltando = true;
                    }
                    break;
                case 1:
                    if(Gravedad > 0)
                    {
                        Gravedad -= PotenciaSalto * Time.deltaTime;
                    }
                    else
                    {
                        Fase1 = 2;
                    }
                    Saltando = true;
                    break;
                case 2:
                    Saltando = false;
                    isJump = false;
                    break;
            }
        }
        else
        {
            Saltando = false;
            isJump = false;
        }
    }

    private void GuardarPosicion()
    {   
        PlayerPrefs.SetFloat("posx", transform.position.x);
        PlayerPrefs.SetFloat("posy", transform.position.y);
        PlayerPrefs.SetFloat("posz", transform.position.z);
        Debug.Log("Posicion Guardado Correctamente");
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {

        switch (collider.gameObject.tag)
        {
            case "ArbolesReinicio":
                Debug.Log("Posicion Guardada");
                GuardarPosicion();
                break;

            case "Pinchos":
                Debug.Log("Daño del Jugador");
                movimiento = false;
                StartCoroutine ("Esperar");
                break;
            case "Enemigo":
                Debug.Log("Daño del Jugador");
                Gravedad = 0;
                movimiento = false;
                StartCoroutine("Esperar");
                break;

            case "Menemigo":
                Debug.Log("Muerte enemigo");
                collider.gameObject.SendMessage("Muere");
                break;
        }

    }

    

    private void Reaparecer()
    {
        rPlayer.velocity = Vector3.zero;
        posX = PlayerPrefs.GetFloat("posx");
        posY = PlayerPrefs.GetFloat("posy");
        posZ = PlayerPrefs.GetFloat("posz");

        posEnd.x = posX;
        posEnd.y = posY;
        posEnd.z = posZ;

        transform.position = posEnd;
        animator.SetBool("enemigo", false);
        movimiento = true;
    }


    private IEnumerator Esperar()
    {
        animator.SetBool("enemigo", true);
        yield return new WaitForSeconds(1);
        Reaparecer();
        
    }

    void FixedUpdate()
    {
        if(movimiento == true)
        {
            Move();
            Jump();
        }
        
    }

    void Update()
    {
        Detector_Plataforma();
        transform.Translate(Vector3.up * Gravedad * Time.deltaTime);
    }
}
