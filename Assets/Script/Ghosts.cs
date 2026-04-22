using UnityEngine;

public class Ghosts : MonoBehaviour
{
    public int points = 200;
    
    public Movement movement {  get; private set; }
    public Ghost_Chase chase { get; private set; }
    public Ghost_Frighten frighten { get; private set; }
    public Ghost_Home home { get; private set; }
    public Ghost_Scatter scatter { get; private set; }
    public Ghost_Behave inibehavior;
    public Transform target;
    private void Awake()
    {

            this.movement = GetComponent<Movement>();
            this.chase = GetComponent<Ghost_Chase>();
            this.frighten = GetComponent<Ghost_Frighten>();
            this.home = GetComponent<Ghost_Home>();
            this.scatter = GetComponent<Ghost_Scatter>();

    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {   
            ResetSttate();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ResetSttate()
    {
        this.gameObject.SetActive(true);
        this.movement.ResetState();
        this.frighten.Disable();
        this.chase.Disable();
        
        this.scatter.Enable();
        if (this.home != this.inibehavior)
        {
            this.home.Disable();
        }
        if (this.inibehavior != null)
        {
            this.inibehavior.Enable();
        }
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Pacman"))
        {
            if (this.frighten.enabled)
            {
                FindAnyObjectByType<GameManager>().GhostEaten(this);
            }
            else
            {
                FindAnyObjectByType<GameManager>().PacmanEaten();
            }
        }
    }
}
