using UnityEngine;

public class PowerPellet : Pellet
{
    public float dura = 8f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    protected override void Eat()
    {
        FindAnyObjectByType<GameManager>().PowerPelletEaten(this);
    }
}
