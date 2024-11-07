using System.Collections;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KlocekMovement : MonoBehaviour
{

    float vertical = 1;
    float horizontal = 1;
    float speed = 4;
    public int dir { get; set; }
    void FixedUpdate()
    {
        if(dir == 0)
        {
            transform.rotation = Quaternion.Euler(0,0,0);
            Vector3 direction = new Vector3(0, -vertical, 0).normalized;
            transform.Translate(direction * speed * Time.deltaTime, Space.World);
        }

        if (dir == 1)
        {
            transform.rotation = Quaternion.Euler(0, 0, 90);
            Vector3 direction = new Vector3(horizontal,0 , 0).normalized;
            transform.Translate(direction * speed * Time.deltaTime, Space.World);
        }

        if (dir == 2)
        {
            transform.rotation = Quaternion.Euler(0, 0, -90);
            Vector3 direction = new Vector3(-horizontal, 0, 0).normalized;
            transform.Translate(direction * speed * Time.deltaTime, Space.World);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(collision.gameObject);
        SceneManager.LoadScene(0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("bottom"))
        {
            Dspr();
        }
    }

    IEnumerator Disappear()
    {
        LeanTween.scale(this.gameObject, Vector3.zero, 0.2f);
        yield return new WaitForSeconds(0.3f);
        Destroy(this.gameObject);
    }

    public void Dspr()
    {
        StartCoroutine(Disappear());
    }
}
