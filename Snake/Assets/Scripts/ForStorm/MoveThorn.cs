using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveThorn : MonoBehaviour
{
    private float speed = 5;

    private float lifetime = 0;

    // Update is called once per frame
    void Update()
    {
        lifetime += Time.deltaTime;
    }

    public void SetSpeed(float tspeed)
    {
        speed = tspeed;
        print(speed);
    }

    private void FixedUpdate()
    {
        transform.position += transform.up * Time.fixedDeltaTime * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if ((lifetime > 0.1f) && (collision.transform.tag == "DeathWall"))
        {

            Destroy(this.gameObject);

        }
    }
}
