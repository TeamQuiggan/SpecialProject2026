using UnityEngine;
using System.Collections;

public class Passage : MonoBehaviour
{
    //public bool Teleportable = true;
    //public bool isSecondPortal;
    Pacman pacman;
    //public TileExperience tileExperience;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pacman = FindFirstObjectByType<Pacman>();
    }
    public Transform Connection;
    // Update is called once per frame
    void Update()
    {
        pacman = FindFirstObjectByType<Pacman>();
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        Vector3 pos = col.transform.position;
        
        if (this.gameObject.tag == "Portal")
        {
            if (pacman != null)
            {
                if (pacman.Teleportable && col.gameObject.layer == LayerMask.NameToLayer("Pacman"))
                {
                    //Teleportable = false;
                    pos.x = this.Connection.position.x;
                    pos.y = this.Connection.position.y;
                    col.transform.position = pos;
                    //StartCoroutine(TeleportationCoolDown());

                }
                else if (col.gameObject.layer == LayerMask.NameToLayer("Ghost"))
                {
                    return;
                }
            }
            
        }
        else
        {
            pos.x = this.Connection.position.x;
            pos.y = this.Connection.position.y;
            col.transform.position = pos;
        }
    }
    //public IEnumerator TeleportationCoolDown()
    //{
    //    Teleportable = false;
    //    yield return new WaitForSecondsRealtime(0.5f);
    //    Teleportable = true;

    //}

}
