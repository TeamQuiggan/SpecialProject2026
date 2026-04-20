using UnityEngine;
using System.Collections;


public class Pacman : MonoBehaviour
{   
    public Movement movement {  get; private set; }
    public SpriteRenderer spriteRenderer { get; private set; }
    public Sprite up;
    public Sprite down;
    public AnimateSprite anim;
    public bool Teleportable = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        movement = GetComponent<Movement>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<AnimateSprite>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            anim.Cancel();
            anim.enabled = false;
            this.movement.SetDirection(Vector2.up);
            spriteRenderer.sprite = up;
            
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            anim.Cancel();
            anim.enabled = false;
            this.movement.SetDirection(Vector2.down);
            spriteRenderer.sprite = down;
            
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {   
            anim.enabled = true;
            anim.Restart();
            this.movement.SetDirection(Vector2.right);
            this.spriteRenderer.flipX = true;
        }
        else if (Input.GetKeyDown (KeyCode.A))
        {
            anim.enabled = true;
            anim.Restart();
            this.movement.SetDirection(Vector2.left);
            this.spriteRenderer.flipX = false;
        }
        //float angle = Mathf.Atan2(this.movement.Dir.y, this.movement.Dir.x);
        //this.transform.rotation = Quaternion.AngleAxis(angle * Mathf.Rad2Deg, Vector3.forward);
    }
    public void ResetState()
    {
        this.gameObject.SetActive(true);
        this.movement.ResetState();
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Portal")
        {
            StartCoroutine(TeleportationCoolDown());
        }
    }
    public IEnumerator TeleportationCoolDown()
    {
        Teleportable = false;
        yield return new WaitForSecondsRealtime(0.2f);
        Teleportable = true;

    }
}
