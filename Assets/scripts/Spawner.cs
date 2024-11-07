using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject klocek;
    public SpriteRenderer[] warningTOP;
    public SpriteRenderer[] warningLEFT;
    public SpriteRenderer[] warningRIGHT;
    private Color inv = new Color(0, 0, 0, 0);
    private Color white = new Color(1, 1, 1 , 1);
    public float cooldown;


    private void Start()
    {
        cooldown = 1f;
        for (int i = 0; i < warningTOP.Length; i++) 
        {
            warningTOP[i].color = inv;
        }

        for (int i = 0; i < warningLEFT.Length; i++)
        {
            warningLEFT[i].color = inv;
        }

        for (int i = 0; i < warningRIGHT.Length; i++)
        {
            warningRIGHT[i].color = inv;
        }
        StartCoroutine(SpawnObject());
    }
    IEnumerator Spwn(int t, SpriteRenderer[] warning, int pos)
    {
        yield return StartCoroutine(AnimateWarning(t, 0.4f, warning));
        
        GameObject kloc = Instantiate(klocek, warning[t].gameObject.transform.position, Quaternion.identity);
        kloc.GetComponent<KlocekMovement>().dir = pos;
        Vector3 size = kloc.transform.localScale;
        kloc.transform.localScale = Vector3.zero;
        LeanTween.scale(kloc, size, 0.1f).setEase(LeanTweenType.easeInOutSine);
    }

    IEnumerator AnimateWarning(int t, float cooldown, SpriteRenderer[] warning)
    {
        warning[t].color = white;
        LeanTween.scale(warning[t].gameObject, new Vector3(0.4f, 0.5f, 1f), 0.1f);
        yield return new WaitForSeconds(0.1f);
        LeanTween.scale(warning[t].gameObject, new Vector3(0.3f, 0.4f, 1f), 0.1f);
        yield return new WaitForSeconds(0.1f + cooldown);
        warning[t].color = inv;

    }
    IEnumerator SpawnObject()
    {
        while (true)
        {
            yield return new WaitForSeconds(cooldown);
            int pos = Random.Range(0, 3);
            int t = 0;

            if (pos == 0)
            {
                t = Random.Range(0, warningTOP.Length);
                StartCoroutine(Spwn(t, warningTOP, pos));
            }

            if (pos == 1)
            {
                t = Random.Range(0, warningLEFT.Length);
                StartCoroutine(Spwn(t, warningLEFT, pos));
            }

            if (pos == 2)
            {
                t = Random.Range(0, warningRIGHT.Length);
                StartCoroutine(Spwn(t, warningRIGHT, pos));
            }
        }    
    }
}
