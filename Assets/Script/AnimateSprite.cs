using Unity.VisualScripting;
using UnityEngine;

public class AnimateSprite : MonoBehaviour
{
    public SpriteRenderer spriteRenderer{get; private set;}
    public Sprite[] sprites;
    public float AnimationTime = 0.25f;
    public int AnimationFrame {  get; private set; }
    public bool loop = true;
    public GameManager manager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        this.spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        Restart();
    }
    // Update is called once per frame
    void Update()
    {
    
    }
    public void Advance()
    {   
        if (!this.spriteRenderer.enabled)
        {
            return;
        }
        this.AnimationFrame++;
        if (this.AnimationFrame >= this.sprites.Length && this.loop)
        {
            this.AnimationFrame = 0;
        }
        if (this.AnimationFrame >= 0 && this.AnimationFrame<this.sprites.Length)
        {
            this.spriteRenderer.sprite = this.sprites[this.AnimationFrame];
        }
    }
    private void RestartAnim()
    {
        this.AnimationFrame = -1;
        Advance();
    }
    public void Cancel()
    {
        CancelInvoke(nameof(Advance));
    }
    public void Restart()
    {
        InvokeRepeating(nameof(Advance), this.AnimationTime, this.AnimationTime);
    }
    
}
