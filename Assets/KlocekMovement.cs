using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class KlocekMovement : MonoBehaviour
{
    float vertical = -1;
    float speed = 4;

    void FixedUpdate()
    {
        Vector3 direction = new Vector3(0, vertical, 0).normalized;
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(collision.gameObject);
        SceneManager.LoadScene(0);
    }
    IEnumerator Disappear()
    {
        LeanTween.scale(this.gameObject, Vector3.zero, 0.2f);
        yield return new WaitForSeconds(0.3f);
        Destroy(this.gameObject);
    }
}
