using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject klocek;
    public SpriteRenderer[] warning;
    private Color inv = new Color(0, 0, 0, 0);
    private Color white = new Color(1, 1, 1 , 1);
    [SerializeField] float cooldown;

    private void Start()
    {
        for (int i = 0; i < warning.Length; i++) 
        {
            warning[i].color = inv;
        }

        StartCoroutine(SpawnObject());
    }
    IEnumerator Spwn(int t)
    {
        warning[t].color = white;
        yield return new WaitForSeconds(0.5f);
        warning[t].color = inv;
        GameObject kloc = Instantiate(klocek, warning[t].gameObject.transform.position, Quaternion.identity);
        Vector3 size = kloc.transform.localScale;
        kloc.transform.localScale = Vector3.zero;
        LeanTween.scale(kloc, size, 0.1f).setEase(LeanTweenType.easeInOutSine);
    }
    IEnumerator SpawnObject()
    {
        while (true)
        {
            yield return new WaitForSeconds(cooldown);
            int t = Random.Range(0, warning.Length);
            StartCoroutine(Spwn(t));
        }    
    }
}
