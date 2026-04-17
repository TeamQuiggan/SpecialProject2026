using UnityEngine;

public class Ghost_Chase : Ghost_Behave
{
    private void OnDisable()
    {
        this.ghost.scatter.Enable();
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        Node node = col.GetComponent<Node>();
        if (node != null && this.enabled && !this.ghost.frighten.enabled)
        {
            Vector2 Direction = Vector2.zero;
            float minDistance = float.MaxValue;
            foreach (Vector2 availableDir in node.AvailableDir)
            {
                Vector3 newPosition = this.transform.position + new Vector3(availableDir.x, availableDir.y, 0);
                float distance = (this.ghost.target.position - newPosition).sqrMagnitude;
                if (distance < minDistance)
                {
                    Direction = availableDir;
                    minDistance = distance;
                }
            }
            this.ghost.movement.SetDirection(Direction);
        }
    }

}
