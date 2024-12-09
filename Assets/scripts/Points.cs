using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Points : MonoBehaviour
{
    public TextMeshProUGUI pointsTXT;
    public int points;
    public Spawner spawner;
    private void Start()
    {
        points = 0;
        StartCoroutine(PointsNum());
    }

    IEnumerator PointsNum()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.9f);
            points++;
            if (points == 10 || points == 20 || points == 30 || points == 40 || points == 50)
            {
                spawner.cooldown -= 0.15f;
            }
            LeanTween.scale(pointsTXT.gameObject, new Vector3(1.2f, 1.2f, 1.2f), 0.05f);
            yield return new WaitForSeconds(0.1f);
            LeanTween.scale(pointsTXT.gameObject, Vector3.one, 0.05f);
            pointsTXT.text = points.ToString();
        }
    }
}
