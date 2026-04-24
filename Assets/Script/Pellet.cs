using UnityEngine;

public class Pellet : MonoBehaviour
{
    public int points = 10;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Pacman"))
        {   
            if (this.gameObject.tag == "Collectible")
            {
                FindAnyObjectByType<GameManager>().SpawnCollectible();
                Destroy(this.gameObject);
                Eat();
            }
            else
            {
                Eat();
            }
                
        }
    }
    protected virtual void Eat()
    {
        FindAnyObjectByType<GameManager>().PelletEaten(this);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
