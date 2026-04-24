using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;
using System.Collections;
public class TileExperience : MonoBehaviour
{
    public Tilemap targetTilemap;
    public List<Vector3> portalnode = new List<Vector3>();
    bool SpawnPortal = true;
    public Passage portal;
    int Random1;
    int Random2;
    public Passage Portal1;
    public Passage Portal2;
    public GameObject boulder;
    bool SpawnBoulder = true;
    public GameManager Gamemanager;
    public Tilemap targetTilemap2;
    public Tilemap targetTilemap3;
    public List<Vector3> BoulderNode;
    public List<Vector3> BoulderNode2;
    public List<Vector3> BoulderNode3;
    public List<Vector3> portalnode2 = new List<Vector3>();
    public List<Vector3> portalnode3 = new List<Vector3>();
    BoundsInt bounds;
    void Start()
    {
        if (Gamemanager.Level1)
        {
            bounds = targetTilemap.cellBounds;
        }
        else if (Gamemanager.Level2)
        {
            bounds = targetTilemap2.cellBounds;
        }
        else if (Gamemanager.Level3)
        {
            bounds = targetTilemap3.cellBounds;
        }
        

        foreach (var pos in bounds.allPositionsWithin)
        {
            if (Gamemanager.Level1)
            {
                if (targetTilemap.HasTile(pos))
                {

                    Vector3 worldPos = targetTilemap.GetCellCenterWorld(pos);
                    portalnode.Add(worldPos);
                    //Debug.Log($"Tile at {pos} is at World Position: {worldPos}");
                }
            }
            else if (Gamemanager.Level2)
            {
                if (targetTilemap2.HasTile(pos))
                {

                    Vector3 worldPos = targetTilemap2.GetCellCenterWorld(pos);
                    portalnode2.Add(worldPos);
                    //Debug.Log($"Tile at {pos} is at World Position: {worldPos}");
                }
            }
            else if (Gamemanager.Level3)
            {
                if (targetTilemap3.HasTile(pos))
                {

                    Vector3 worldPos = targetTilemap3.GetCellCenterWorld(pos);
                    portalnode3.Add(worldPos);
                    //Debug.Log($"Tile at {pos} is at World Position: {worldPos}");
                }
            }

        }
    }
    void Update()
    {
        if (Gamemanager.Level1)
        {
            bounds = targetTilemap.cellBounds;
        }
        else if (Gamemanager.Level2)
        {
            bounds = targetTilemap2.cellBounds;
        }
        else if (Gamemanager.Level3)
        {
            bounds = targetTilemap3.cellBounds;
        }


        foreach (var pos in bounds.allPositionsWithin)
        {
            if (Gamemanager.Level1)
            {
                if (targetTilemap.HasTile(pos))
                {

                    Vector3 worldPos = targetTilemap.GetCellCenterWorld(pos);
                    portalnode.Add(worldPos);
                    //Debug.Log($"Tile at {pos} is at World Position: {worldPos}");
                }
            }
            else if (Gamemanager.Level2)
            {
                if (targetTilemap2.HasTile(pos))
                {

                    Vector3 worldPos = targetTilemap2.GetCellCenterWorld(pos);
                    portalnode2.Add(worldPos);
                    //Debug.Log($"Tile at {pos} is at World Position: {worldPos}");
                }
            }
            else if (Gamemanager.Level3)
            {
                if (targetTilemap3.HasTile(pos))
                {

                    Vector3 worldPos = targetTilemap3.GetCellCenterWorld(pos);
                    portalnode3.Add(worldPos);
                    Debug.Log($"Tile at {pos} is at World Position: {worldPos}");
                }
            }

        }
        if (SpawnPortal)
        {
            SpawnPortal = false;
            StartCoroutine(spawnPortal());
        }
        if (SpawnBoulder)
        {
            SpawnBoulder = false;
            StartCoroutine(Spawnboulder());
        }
    }
    IEnumerator spawnPortal()
    {   
        if (Gamemanager.Level1)
        {
            Random1 = Random.Range(0, portalnode.Count);
            Random2 = Random.Range(0, portalnode.Count);
            if (Random1 == Random2)
            {
                Random2 = Random.Range(0, portalnode.Count);
            }
            else
            {
                if (!Portal1)
                {
                    Portal1 = Instantiate(portal, portalnode[Random1], Quaternion.identity);
                    Portal2 = Instantiate(portal, portalnode[Random2], Quaternion.identity);
                    Portal1.Connection = Portal2.transform;
                    Portal2.Connection = Portal1.transform;
                    //Portal1.Teleportable = Portal2.Teleportable;
                    //if (!Portal1.Teleportable)
                    //{
                    //    Portal2.enabled = false;
                    //    //yield return new WaitForSecondsRealtime(0.5f);

                    //}
                    //else
                    //{
                    //    Portal2.enabled = true;
                    //}
                    //if (!Portal2.Teleportable)
                    //{
                    //    Portal1.enabled = false;
                    //    //yield return new WaitForSecondsRealtime(0.5f);

                    //}
                    //else
                    //{
                    //    Portal1.enabled = true;
                    //}
                    yield return new WaitForSecondsRealtime(7f);
                    SpawnPortal = true;
                }



                //SpawnPortal = true;
                //StartCoroutine(DestroyPortal());
                //Destroy(Portal1);
                //Destroy(Portal2);
                //yield return new WaitForSecondsRealtime(2f);

            }
        }
        //yield return new WaitForSecondsRealtime(sec);
        else if (Gamemanager.Level2)
        {
            Random1 = Random.Range(0, portalnode2.Count);
            Random2 = Random.Range(0, portalnode2.Count);
            if (Random1 == Random2)
            {
                Random2 = Random.Range(0, portalnode2.Count);
            }
            else
            {
                if (!Portal1)
                {
                    Portal1 = Instantiate(portal, portalnode2[Random1], Quaternion.identity);
                    Portal2 = Instantiate(portal, portalnode2[Random2], Quaternion.identity);
                    Portal1.Connection = Portal2.transform;
                    Portal2.Connection = Portal1.transform;
                    //Portal1.Teleportable = Portal2.Teleportable;
                    //if (!Portal1.Teleportable)
                    //{
                    //    Portal2.enabled = false;
                    //    //yield return new WaitForSecondsRealtime(0.5f);

                    //}
                    //else
                    //{
                    //    Portal2.enabled = true;
                    //}
                    //if (!Portal2.Teleportable)
                    //{
                    //    Portal1.enabled = false;
                    //    //yield return new WaitForSecondsRealtime(0.5f);

                    //}
                    //else
                    //{
                    //    Portal1.enabled = true;
                    //}
                    yield return new WaitForSecondsRealtime(7f);
                    SpawnPortal = true;
                }



                //SpawnPortal = true;
                //StartCoroutine(DestroyPortal());
                //Destroy(Portal1);
                //Destroy(Portal2);
                //yield return new WaitForSecondsRealtime(2f);

            }
        }
        else if (Gamemanager.Level3)
        {
            Random1 = Random.Range(0, portalnode3.Count);
            Random2 = Random.Range(0, portalnode3.Count);
            if (Random1 == Random2)
            {
                Random2 = Random.Range(0, portalnode3.Count);
            }
            else
            {
                if (!Portal1)
                {
                    Portal1 = Instantiate(portal, portalnode3[Random1], Quaternion.identity);
                    Portal2 = Instantiate(portal, portalnode3[Random2], Quaternion.identity);
                    Portal1.Connection = Portal2.transform;
                    Portal2.Connection = Portal1.transform;
                    //Portal1.Teleportable = Portal2.Teleportable;
                    //if (!Portal1.Teleportable)
                    //{
                    //    Portal2.enabled = false;
                    //    //yield return new WaitForSecondsRealtime(0.5f);

                    //}
                    //else
                    //{
                    //    Portal2.enabled = true;
                    //}
                    //if (!Portal2.Teleportable)
                    //{
                    //    Portal1.enabled = false;
                    //    //yield return new WaitForSecondsRealtime(0.5f);

                    //}
                    //else
                    //{
                    //    Portal1.enabled = true;
                    //}
                    yield return new WaitForSecondsRealtime(7f);
                    SpawnPortal = true;
                }



                //SpawnPortal = true;
                //StartCoroutine(DestroyPortal());
                //Destroy(Portal1);
                //Destroy(Portal2);
                //yield return new WaitForSecondsRealtime(2f);

            }
        }
    
        
    }
    IEnumerator DestroyPortal()
    {
        yield return new WaitForSecondsRealtime(2f);
        Destroy(Portal1);
        Destroy(Portal2);
        SpawnPortal = true;
    }
    IEnumerator Spawnboulder()
    {   
        if (Gamemanager.Level1)
        {
            Random1 = Random.Range(0, BoulderNode.Count);
            Instantiate(boulder, BoulderNode[Random1], Quaternion.identity);
        }
        else if (Gamemanager.Level2)
        {
            Random1 = Random.Range(0, BoulderNode2.Count);
            Instantiate(boulder, BoulderNode2[Random1], Quaternion.identity);
        }
        else if (Gamemanager.Level3)
        {
            Random1 = Random.Range(0, BoulderNode3.Count);
            Instantiate(boulder, BoulderNode3[Random1], Quaternion.identity);
        }

        yield return new WaitForSecondsRealtime(7f);
        SpawnBoulder = true;

    }
}
