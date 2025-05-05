using UnityEngine;

public class PlayerController : MonoBehaviour
{
    bool canJump;
    // Use this for initialization
    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
       //mover izquierda
       if (Input.GetKey("left"))
       {
           gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-1000f * Time.deltaTime, 0));
       }
       //mover derecha
       if (Input.GetKey("right"))
       {
           gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(1000f * Time.deltaTime, 0));
       }

       if (Input.GetKeyDown("up") && canJump)
       {
           canJump = false;
           gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 160f));
       }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "ground")
        {
            canJump = true;
        }
    }
}
