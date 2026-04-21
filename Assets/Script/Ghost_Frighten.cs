using UnityEngine;
using System.Collections.Generic;
using System.Collections;
public class Ghost_Frighten : Ghost_Behave
{   

    public SpriteRenderer sprite;
    public bool eaten {  get; private set; }
    public AnimateSprite anim;
    public Sprite[] Deathanimation;
    public Sprite[] LivingAnimation;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnEnable()
    {
        this.ghost.movement.SpeedMultiplyer = 0.5f;
        anim = GetComponentInChildren<AnimateSprite>();
        this.eaten = false;
    }
    private void OnDisable()
    {
        
        if (this.gameObject.tag == "Snake")
        {
            anim.sprites = LivingAnimation;
            anim.loop = true;
        }
        this.ghost.movement.SpeedMultiplyer = 1f;
        this.eaten = false;
    }
    public override void Enable(global::System.Single duration)
    {
        base.Enable(duration);
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Pacman"))
        {
            if (this.enabled)
            {
                //FindAnyObjectByType<GameManager>().GhostEaten(this);
                if (this.gameObject.tag == "Snake")
                {
                    anim.sprites = Deathanimation;
                    anim.loop = false;
                }

                StartCoroutine(DeathAnimation());
            }
        }
    }
    private void Eaten()
    {
        this.eaten = true;
        Vector3 position = this.ghost.home.Inside.position;
        position.z = this.transform.position.z;
        this.ghost.transform.position = position; 
        this.ghost.home.Enable(this.duration);
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (this.gameObject.tag == "Boulder")
        {
            return;
        }
        else
        {
            Node node = col.GetComponent<Node>();
            if (node != null && this.enabled)
            {
                Vector2 Direction = Vector2.zero;
                float maxDistance = float.MinValue;
                foreach (Vector2 availableDir in node.AvailableDir)
                {
                    Vector3 newPosition = this.transform.position + new Vector3(availableDir.x, availableDir.y, 0);
                    float distance = (this.ghost.target.position - newPosition).sqrMagnitude;
                    if (distance > maxDistance)
                    {
                        Direction = availableDir;
                        maxDistance = distance;
                    }
                }
                this.ghost.movement.SetDirection(Direction);
            }
        }
        
    }
    IEnumerator DeathAnimation()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        Eaten();
    }
}
