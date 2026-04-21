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
    void Start()
    {
        // 1. Get the rectangular bounds of all painted tiles
        BoundsInt bounds = targetTilemap.cellBounds;

        // 2. Loop through every cell within those bounds
        foreach (var pos in bounds.allPositionsWithin)
        {
            // 3. Check if a tile actually exists at this cell
            if (targetTilemap.HasTile(pos))
            {
                // 4. Convert cell coordinate (Vector3Int) to world position (Vector3)
                Vector3 worldPos = targetTilemap.GetCellCenterWorld(pos);
                portalnode.Add(worldPos);
                //Debug.Log($"Tile at {pos} is at World Position: {worldPos}");
            }
        }
    }
    void Update()
    {
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
        //yield return new WaitForSecondsRealtime(sec);
        Random1 = Random.Range(0, portalnode.Count);
        Random2 = Random.Range(0, portalnode.Count);
        if (Random1 == Random2)
        {
            Random2 = Random.Range(0, portalnode.Count);
        }
        else
        {   if (!Portal1)
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
    IEnumerator DestroyPortal()
    {
        yield return new WaitForSecondsRealtime(2f);
        Destroy(Portal1);
        Destroy(Portal2);
        SpawnPortal = true;
    }
    IEnumerator Spawnboulder()
    {
        Random1 = Random.Range(0, portalnode.Count);
        Instantiate(boulder, portalnode[Random1], Quaternion.identity);
        yield return new WaitForSecondsRealtime(7f);
        SpawnBoulder = true;

    }
}
