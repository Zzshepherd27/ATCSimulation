using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirplaneMovement : MonoBehaviour
{

    private Rigidbody2D rb;
    private bool invincible = true;

    public bool active = false;
    public float IFrames = 3.0f;
    public GameObject activePrefab;
    public Animator animator;
    public CircleCollider2D targetCollider;
    public float planeSpeed = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        Vector2 target = new Vector2();
        target = Random.insideUnitCircle.normalized * targetCollider.radius * 2.0f;
        rb = this.GetComponent<Rigidbody2D>();
        Vector2 velocity = new Vector2(-(transform.position.x - target.x), -(transform.position.y - target.y)).normalized;
        if(name == "AirPlanePrefab")
        {
            velocity = new Vector2(0,0);
        }
        rb.velocity = velocity;
        target.x = target.x - transform.position.x;
        target.y = target.y - transform.position.y;
        float angle = Mathf.Atan2(target.y, target.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
        StartCoroutine(IFramesFunc(IFrames));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Planes" && !invincible && !col.gameObject.GetComponent<AirplaneMovement>().getInvincible())
        {
            rb.velocity = new Vector2(0,0);
            animator.SetBool("dead", true);
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if(col.gameObject.tag == "Background" && col.usedByEffector)
        {
            destroyPlane();
        }
        if(col.gameObject.tag == "Radar")
        {
            animator.Play("Plane_Idle", -1, 0f);
        }
    }

    public void destroyPlane()
    {
        Destroy(this.gameObject);
    }

    void OnMouseDown()
    {
        GameObject prefab = Instantiate(activePrefab, this.transform);
        StartCoroutine(instanceDelete(prefab, 2.0f));
        active = true;
    }

    public IEnumerator instanceDelete(GameObject fab, float delay)
    {
        yield return new WaitForSeconds(delay);
        active = false;
        Destroy(fab);
    }

    public IEnumerator IFramesFunc(float delay)
    {
        GetComponent<Collider2D>().enabled = false;
        GetComponent<Animator>().enabled = false;
        yield return new WaitForSeconds(delay);
        invincible = false;
        GetComponent<Collider2D>().enabled = true;
        GetComponent<Animator>().enabled = true;
    }

    public Rigidbody2D getRigidBody()
    {
        return rb;
    }

    public bool getInvincible()
    {
        return invincible;
    }
}
