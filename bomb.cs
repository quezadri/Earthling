using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bomb : MonoBehaviour
{
    Rigidbody2D rb;
    public float speedX;
    public float speedY;
    public float speedZ;
    public bool movingLeft;
    public float radius;
    public int damage;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();   
        if(movingLeft){speedX = -speedX;}
        rb.AddForce(new Vector3(speedX,speedY,speedZ),ForceMode2D.Impulse);

        StartCoroutine(Detonate());
    }

    void Update()
    {
                //rb.AddForce(new Vector3(speedX,speedY,speedZ) * Time.deltaTime,ForceMode2D.Impulse);

    }

    private IEnumerator Detonate()
    {
        yield return new WaitForSeconds(1.3f);
        Destroy(this.gameObject);
    }

    public void Damage()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(new Vector2(transform.position.x,transform.position.y),radius);
        foreach(Collider2D hit in colliders)
        {
        
            if (hit.gameObject.tag == "Player")
            {
                Health health = hit.gameObject.GetComponent<Health>();
                health.TakeDamage(damage);
            }

        }
    }
}
