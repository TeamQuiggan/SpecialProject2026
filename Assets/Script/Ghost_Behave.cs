using UnityEngine;

public class Ghost_Behave : MonoBehaviour
{
    public Ghosts ghost { get; private set; }
    public float duration;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        this.ghost = GetComponent<Ghosts>();
        this.enabled = false;
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Enable()
    {
        Enable(this.duration);
    }
    public virtual void Disable()
    {
        this.enabled = false;
        CancelInvoke();
    }
    public virtual void Enable(float duration)
    {
        this.enabled = true;
        CancelInvoke();
        Invoke(nameof(Disable), duration);
    }
}
