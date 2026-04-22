using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed = 8;
    public float SpeedMultiplyer = 1f;
    public Vector2 InitialDir;
    public LayerMask obstacleLayer;
    public Vector2 Dir {  get; private set; }
    public Vector2 NextDir { get; private set; }
    public Vector3 StartingPos;
    public Rigidbody2D body { get; private set; }
    private Vector2 targetPosition;
    private bool isMoving = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        this.StartingPos = this.transform.position;
    }
    private void Start()
    {
        ResetState();
    }
    private void Update()
    {
        if (this.NextDir != Vector2.zero)
        {
            SetDirection(this.NextDir);
        }
    }
    public void ResetState()
    { 
        this.SpeedMultiplyer = 1f;
        this.Dir = this.InitialDir;
        this.NextDir = Vector2.zero;
        this.transform.position = this.StartingPos;
        //this.body.bodyType = RigidbodyType2D.Static;
        this.body.isKinematic = false;
        this.enabled = true;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 position = this.body.position;
        Vector2 Translation = this.Dir * this.speed * this.SpeedMultiplyer * Time.fixedDeltaTime;
        this.body.MovePosition(position + Translation);
        //this.body.position += this.Dir;
        //if (isMoving)
        //{

        //    Vector2 newPos = Vector2.MoveTowards(body.position, targetPosition, speed * Time.fixedDeltaTime * SpeedMultiplyer);
        //    body.MovePosition(newPos);
        //    if (Vector2.Distance(body.position, targetPosition) < 0.01f)
        //    {
        //        body.MovePosition(targetPosition);
        //        isMoving = false;
        //    }
        //}
        //else
        //{

        //    if (this.Dir != Vector2.zero)
        //    {

        //        if (!Occupied(this.NextDir))
        //        {
        //            targetPosition = body.position + this.Dir;
        //            isMoving = true;
        //        }

        //    }
        //}

    }
    public void SetDirection(Vector2 dir, bool forced = false)
    {
        if (forced || !Occupied(dir))
        {
            this.Dir = dir;
            this.NextDir = Vector2.zero;
        }
        else
        {
            this.NextDir = dir;
        }
    }
    public bool Occupied(Vector2 dir)
    {
        RaycastHit2D hit = Physics2D.BoxCast(this.transform.position, Vector2.one * 0.75f, 0.0f, dir, 0.5f, this.obstacleLayer);
        return hit.collider != null;
    }
}
