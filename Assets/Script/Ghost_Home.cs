using UnityEngine;
using System.Collections;

public class Ghost_Home : Ghost_Behave
{
    public Transform Inside;
    public Transform Outside;
    private void OnDisable()
    {
        if (gameObject.activeInHierarchy)
        {
            StartCoroutine(ExitTransition());
        }
    }
    private void OnEnable()
    {
        StopAllCoroutines();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    private IEnumerator ExitTransition()
    {
        this.ghost.movement.SetDirection(Vector2.up, true);
        this.ghost.movement.body.isKinematic = true;
        this.ghost.movement.enabled = false;
        Vector3 position = this.transform.position;
        float dura = 0.5f;
        float elapsed = 0.0f;
        while(elapsed < dura)
        {
            Vector3 newPosition = Vector3.Lerp(position, this.Inside.position, elapsed/dura);
            newPosition.z = position.z;
            this.ghost.transform.position = newPosition;
            elapsed += Time.deltaTime;
            yield return null;
        }
        elapsed = 0.0f;
        while (elapsed < dura)
        {
            Vector3 newPosition = Vector3.Lerp(this.Inside.position, this.Outside.position, elapsed / dura);
            newPosition.z = position.z;
            this.ghost.transform.position = newPosition;
            elapsed += Time.deltaTime;
            yield return null;
        }

        this.ghost.movement.SetDirection(new Vector2(Random.value < 0.5f ? -1.0f : 1.0f,0.0f), true);
        this.ghost.movement.body.isKinematic = false;
        this.ghost.movement.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (this.enabled && col.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
        {
            this.ghost.movement.SetDirection(-this.ghost.movement.Dir);
            
        }
    }
}
