using UnityEngine;

public class Ghost_Scatter : Ghost_Behave
{   
    private void OnDisable()
    {
        this.ghost.chase.Enable();
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        Node node = col.GetComponent<Node>();
        if (node != null && this.enabled && !this.ghost.frighten.enabled)
        {
            int index = Random.Range(0, node.AvailableDir.Count);
            if (node.AvailableDir[index] == -this.ghost.movement.Dir && node.AvailableDir.Count > 1)
            {
                index++;
                if (index >= node.AvailableDir.Count)
                {
                    index = 0;
                }
                
            }
            this.ghost.movement.SetDirection(node.AvailableDir[index]);
        }
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
