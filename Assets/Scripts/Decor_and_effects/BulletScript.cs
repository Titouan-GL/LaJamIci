using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float damage = 1f;
    float speed = 100f;
    Rigidbody2D rb;
    [SerializeField] GameObject Explosion;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponentInChildren<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = transform.up * speed;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.GetComponent<EnnemyFollow>() != null)
        {
            collision.collider.GetComponent<EnnemyFollow>().TakeDamage(damage);
        }
        GameObject go2 = Instantiate(Explosion, collision.contacts[0].point, Quaternion.identity); 
        Destroy(gameObject);
    }
}
