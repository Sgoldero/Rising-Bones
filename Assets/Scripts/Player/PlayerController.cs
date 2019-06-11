using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //[Range(0, 10)] public float speed = 5f;

    public float speed;
    public GameObject arrow;
    
    private float min = -1.0f, max = 1.0f;
    private float h, v;
    private Rigidbody2D rb2d;
    private Vector3 move;
    private Animator anim;
    private SpriteRenderer sprPlayer, sprShadow;

    Transform spawnPoint;

    private float maxCooldown = 1f;
    private float coolDown;


    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();

        spawnPoint = transform.Find("SpawnPointDown");

        speed = GameManager.instance.GetPlayerSpeed();

        sprPlayer = GetComponent<SpriteRenderer>();
        sprShadow = transform.Find("Shadow").gameObject.GetComponent<SpriteRenderer>();
        
    }

    // Update is called once per frame
    void Update()
    {
        //print(GameManager.instance.GetPlayerPoints());
        sprShadow.sprite = sprPlayer.sprite;

        speed = GameManager.instance.GetPlayerSpeed();

        if (!GameManager.instance.GetPlayerInputState())
        {
            move = Vector3.zero;

            v = Input.GetAxisRaw("Vertical");
            h = Input.GetAxisRaw("Horizontal");

            DirectionMovement();
            AnimateMovement();
            PlayerAttack();
        }

        PlayerDied();
        coolDown -= Time.deltaTime;

      //  print("VIDA ACTUAL: " + GameManager.instance.GetHealth());

    }//End Update()

    void DirectionMovement()
    {
        if (v != 0f)
        {
            move.y = Mathf.Clamp(v, min, max);
            if (v > 0)
            {
                spawnPoint = transform.Find("SpawnPointUp");
            }
            else
            {
                spawnPoint = transform.Find("SpawnPointDown");
            }
        }
        else if (h != 0f)
        {
            move.x = Mathf.Clamp(h, min, max);
            if (h > 0)
            {
                spawnPoint = transform.Find("SpawnPointRight");
            }
            else
            {
                spawnPoint = transform.Find("SpawnPointLeft");
            }
        }
    }//End directionMovement()

    void AnimateMovement()
    {
        if (move != Vector3.zero)
        {
            MoveCharacter();
         
            anim.SetFloat("moveX", move.x);
            anim.SetFloat("moveY", move.y);
            anim.SetBool("isMoving", true);
        }
        else
        {
            anim.SetBool("isMoving", false);

            FindObjectOfType<AudioManager>().Stop("GeneralWalking");

        }
    }//End AnimateMovement()

    void MoveCharacter()
    {
        rb2d.MovePosition(
            transform.position + move.normalized * speed * Time.deltaTime
        );
    }//End MoveCharacter()


    //========================================

    void PlayerAttack()
    {
        if (Input.GetButton("Fire1"))
        {
            FindObjectOfType<AudioManager>().Play("SwordSwing");
            anim.SetBool("isAttacking", true);
            anim.SetBool("isSword", true);
        }
        else if (Input.GetButton("Fire2"))
        {
            anim.SetBool("isAttacking", true);
            anim.SetBool("isBow", true);

            if (coolDown < 0.0f)
            {
                FindObjectOfType<AudioManager>().Play("Bow");
                Instantiate(arrow, spawnPoint.transform.position, spawnPoint.transform.rotation);
                coolDown = maxCooldown;
            }
        }
        else
        {
            ClearAttackAnimations();
        }
    }//End playerAttack()


    void ClearAttackAnimations()
    {
        anim.SetBool("isAttacking", false);
        anim.SetBool("isSword", false);
        anim.SetBool("isBow", false);
    }

    void PlayerDied()
    {
        if (GameManager.instance.GetHealth() <= 0)
        {

            //TODO

            anim.SetBool("isDead", true);
            GameManager.instance.UpdatePlayerInput(true);
            GameManager.instance.GameOver();
        }
        //if (Input.GetKey(KeyCode.K))
        //{
        //    
        //}
        //else
        //{
        //    anim.SetBool("isDead", false);
        //}
    }//End playerAttack()

    public void PlayDeathSound()
    {
        FindObjectOfType<AudioManager>().Play("PlayerDeath");
    }

    public void PlayWalkingSound()
    {
        FindObjectOfType<AudioManager>().Play("GeneralWalking");
    }
}//End class
