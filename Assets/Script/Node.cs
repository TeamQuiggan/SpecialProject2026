using UnityEngine;
using System.Collections.Generic;
public class Node : MonoBehaviour
{   
    public List<Vector2> AvailableDir {  get; private set; }
    public LayerMask obstacleLayer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.AvailableDir = new List<Vector2>();
        CheckAvailableDir(Vector2.up); 
        CheckAvailableDir(Vector2.down); 
        CheckAvailableDir(Vector2.left); 
        CheckAvailableDir(Vector2.right);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void CheckAvailableDir(Vector2 dir)
    {
        RaycastHit2D hit = Physics2D.BoxCast(this.transform.position, Vector2.one * 0.5f, 0.0f, dir, 1f, this.obstacleLayer);
        if (hit.collider == null)
        {
            this.AvailableDir.Add(dir);
        }
    }
}
