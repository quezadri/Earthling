using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
   
    public float bullet_speed;
    public int damage;
    public bool movingLeft;
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * bullet_speed * Time.deltaTime);
    }
    void Start()
    {
        if(movingLeft){bullet_speed = -bullet_speed;}
        StartCoroutine(Die());
    }
    IEnumerator Die()
    {
        yield return new WaitForSeconds(2f);
        Destroy(this.gameObject);
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag =="Player")
        {
            Health health = collider.gameObject.GetComponent<Health>();
            health.TakeDamage(damage);
        }
        Destroy(this.gameObject);
    }
}
