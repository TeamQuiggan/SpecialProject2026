using UnityEngine;

public class Ghost_Eyes : MonoBehaviour
{
    public Sprite front;
    public Sprite back;
    public Sprite side;
    public SpriteRenderer spriteRender {  get; private set; }
    public Movement movement {  get; private set; }
    public AnimateSprite anim;
    public bool Anim;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if (spriteRender == null)
        {
            spriteRender = GetComponentInChildren<SpriteRenderer>();
        }
        if (movement == null)
        {
            movement = GetComponent<Movement>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (this.movement.Dir == Vector2.up)
        {
            spriteRender.flipX = false;
            if (Anim)
            {
                anim.Cancel();
                anim.enabled = false;
            }
            
            //this.movement.SetDirection(Vector2.up);
            spriteRender.sprite = back;
        }
        if (this.movement.Dir == Vector2.down)
        {
            spriteRender.flipX = false;
            if (Anim)
            {
                anim.Cancel();
                anim.enabled = false;
            }
            spriteRender.sprite = front;
        }
        if (this.movement.Dir == Vector2.left)
        {   
            if (Anim)
            {
                spriteRender.flipX = false;
                anim.enabled = true;
                anim.Restart();
            }
            else
            {
                spriteRender.flipX = false;
                spriteRender.sprite = side;
            }
            
        }
        if (this.movement.Dir == Vector2.right)
        {   
            if (Anim)
            {
                spriteRender.flipX = true;
                anim.enabled = true;
                anim.Restart();
            }
            else
            {
                spriteRender.flipX = true;
                spriteRender.sprite = side;
            }
        }
    }
}
