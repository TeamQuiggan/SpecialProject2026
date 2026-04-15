using UnityEngine;

public class Passage : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    public Transform Connection;
    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        Vector3 pos = col.transform.position;
        pos.x = this.Connection.position.x;
        pos.y = this.Connection.position.y;
        col.transform.position = pos;
    }
}
