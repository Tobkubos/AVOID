using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Points : MonoBehaviour
{
    public SpriteRenderer[] Borders;
    public SpriteRenderer player;
    public GameObject ALL_OBSTACLES;
    public Image BACKGROUND;
    public TextMeshProUGUI POINTS;
    public TextMeshProUGUI WARNINIG;
    public PlayerMovement pm;

    public Color[] ObjectsColor;
    public Color[] BackgroundColor;
    

    public TextMeshProUGUI pointsTXT;
    public int points;
    private int ColorSwitchCap = 10;
    private int LevelSwitchCap = 20;
    public int idx = 0;
    public Spawner spawner;
    public bool CountThePoints = true;
    private void Start()
    {
        idx = 0;
        points = 0;
        StartCoroutine(PointsNum());
    }

    private void FixedUpdate()
    {
        Debug.Log(idx);
        for (int i = 0; i < Borders.Length; i++)
        {
            if (Borders[i].color != ObjectsColor[idx])
            {
                Borders[i].color = ObjectsColor[idx];
                POINTS.color = new Color(ObjectsColor[idx].r, ObjectsColor[idx].g, ObjectsColor[idx].b, 0.3f);
            }
        }
        if (player.color != ObjectsColor[idx])
        {
            player.color = ObjectsColor[idx];
            player.GetComponentInChildren<TrailRenderer>().startColor = ObjectsColor[idx];
            player.GetComponentInChildren<TrailRenderer>().endColor = ObjectsColor[idx];
        }

        for (int i = 0; i < spawner.warningTOP.Length; i++)
        {
            if (spawner.warningTOP[i].color != spawner.inv && spawner.warningTOP[i].color != ObjectsColor[idx])
            {
                spawner.warningTOP[i].color = ObjectsColor[idx];
            }
        }

        for (int i = 0; i < spawner.warningLEFT.Length; i++)
        {
            if (spawner.warningLEFT[i].color != spawner.inv && spawner.warningLEFT[i].color != ObjectsColor[idx])
            {
                spawner.warningLEFT[i].color = ObjectsColor[idx];
            }
        }

        for (int i = 0; i < spawner.warningTOP.Length; i++)
        {
            if (spawner.warningRIGHT[i].color != spawner.inv && spawner.warningRIGHT[i].color != ObjectsColor[idx])
            {
                spawner.warningRIGHT[i].color = ObjectsColor[idx];
            }
        }

        BACKGROUND.color = BackgroundColor[idx];



        SpriteRenderer[] obstacles = ALL_OBSTACLES.GetComponentsInChildren<SpriteRenderer>();
        for (int i = 0; i < obstacles.Length; i++)
        {
            if (obstacles[i] != null && obstacles[i].color != ObjectsColor[idx])
            {
                obstacles[i].color = ObjectsColor[idx];
            }
        }
    }
    IEnumerator PointsNum()
    {
        while (CountThePoints)
        {
            yield return new WaitForSeconds(0.95f);
            points++;
            if (points == 10 || points == 20 || points == 30 || points == 40 || points == 50)
            {
                spawner.cooldown -= 0.15f;
            }
            LeanTween.scale(pointsTXT.gameObject, new Vector3(1.2f, 1.2f, 1.2f), 0.05f);
            yield return new WaitForSeconds(0.05f);
            LeanTween.scale(pointsTXT.gameObject, Vector3.one, 0.05f);
            pointsTXT.text = points.ToString();

            
            if(ColorSwitchCap == points)
            {
                ColorSwitchCap += 10;
                idx++;
            }

            if(LevelSwitchCap == points)
            {
                LevelSwitchCap += 20;
                CountThePoints = false;
                pm.enabled = false;

                foreach (Transform child in ALL_OBSTACLES.transform)
                {
                    Destroy(child.gameObject);
                }

                LeanTween.moveLocal(player.gameObject, new Vector3(0, 0, 90), 0.1f);

                LeanTween.rotateAround(Camera.main.gameObject, new Vector3(0,0,1),360f, 1f);
                LeanTween.value(5, 0.1f, 0.5f).setEase(LeanTweenType.easeInSine).setOnUpdate((float val) =>
                {
                    Camera.main.orthographicSize = val;

                });
                yield return new WaitForSeconds(0.52f);
                LeanTween.value(0.1f, 5f, 0.5f).setEase(LeanTweenType.easeOutSine).setOnUpdate((float val) =>
                {
                    Camera.main.orthographicSize = val;

                });
                yield return new WaitForSeconds(0.1f);

                pm.enabled = true;
                Camera.main.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);

                yield return new WaitForSeconds(3f);
                
                CountThePoints = true;
                StartCoroutine(spawner.SpawnObject());
            }
        }
    }
}
