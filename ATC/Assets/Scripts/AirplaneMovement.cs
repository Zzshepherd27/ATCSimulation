using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirplaneMovement : MonoBehaviour
{

    private bool active = false;
    private Rigidbody2D rb;
    private SpriteRenderer spr;
    
    public Animator animator;
    public Animation anim;
    public CircleCollider2D targetCollider;
    public float planeSpeed = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        spr = gameObject.GetComponent<SpriteRenderer>();
        anim = gameObject.GetComponent<Animation>();
        Vector2 target = new Vector2();
        target = Random.insideUnitCircle.normalized * targetCollider.radius * 2.0f;
        rb = this.GetComponent<Rigidbody2D>();
        Vector2 velocity = new Vector2(-(transform.position.x - target.x), -(transform.position.y - target.y)).normalized;
        rb.velocity = velocity;
        target.x = target.x - transform.position.x;
        target.y = target.y - transform.position.y;
        float angle = Mathf.Atan2(target.y, target.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Planes")
        {
            rb.velocity = new Vector2(0,0);
            animator.SetBool("dead", true);
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if(col.gameObject.tag == "Background" && col.usedByEffector)
        {
            Destroy(this.gameObject);
        }
        if(col.gameObject.tag == "Radar")
        {
            
        }
    }

    public void destroyPlane()
    {
        Destroy(this.gameObject);
    }
}
