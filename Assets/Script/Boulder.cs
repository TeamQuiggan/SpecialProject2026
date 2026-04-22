using UnityEngine;
using System.Collections.Generic;
using System.Collections;
public class Boulder : MonoBehaviour
{
    public LayerMask obstacleLayer;
    public Movement movement;
    public GameManager gameManager;
    public Ghosts Boulderghost;
    AnimateSprite anim;
    public Sprite[] sprites;
    Sprite[] Instancesprites;
    int index;
    public List<Vector2> AvailableDir { get; private set; }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {   

        
    }
    void OnEnable()
    {   

        gameManager = FindFirstObjectByType<GameManager>();
        gameManager.GhostS.Add(Boulderghost);
        anim = GetComponentInChildren<AnimateSprite>();
        Instancesprites = anim.sprites;
        this.AvailableDir = new List<Vector2>();
        CheckAvailableDir1(Vector2.up);
        CheckAvailableDir1(Vector2.down);
        CheckAvailableDir1(Vector2.left);
        CheckAvailableDir1(Vector2.right);
        index = Random.Range(0, 2);


        //if (Boulderghost.scatter.Counter == 0)
        //{
        //    Boulderghost.scatter.Counter = 1;
        //}

    }
    // Update is called once per frame
    void FixedUpdate()
    {
        StartCoroutine(spareCheck());

    }
    //void OnCollisionEnter2D(Collision2D col)
    //{
    //    if (col.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
    //    {
    //        Destroy(gameObject, 5);
    //    }
    //}
    private void CheckAvailableDir(Vector2 dir)
    {   
        RaycastHit2D hit = Physics2D.BoxCast(this.transform.position, Vector2.one * 0.5f, 0.0f, dir, 1f, this.obstacleLayer);
        if (hit.collider != null)
        {
            gameManager.GhostS.Remove(Boulderghost);
            Instancesprites = (Sprite[])this.sprites.Clone();
            anim.sprites = Instancesprites;
            Destroy(gameObject, 1f);

        }
    }
    private void CheckAvailableDir1(Vector2 dir)
    {
        RaycastHit2D hit = Physics2D.BoxCast(this.transform.position, Vector2.one * 0.5f, 0.0f, dir, 1f, this.obstacleLayer);
        if (hit.collider == null)
        {
            this.AvailableDir.Add(dir);
        }
    }
    //IEnumerator Check()
    //{
    //    yield return new WaitForSecondsRealtime();
    //}
    IEnumerator spareCheck()
    {
        yield return new WaitForSecondsRealtime(1f);
        CheckAvailableDir(this.movement.Dir);
        this.movement.SetDirection(AvailableDir[index]);
    }
}
