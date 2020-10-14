using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalObstacleManager : MonoBehaviour
{
    public static LocalObstacleManager instance;

    private float m_xMinMax = 3;
    private float m_yMinMax = 6;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }


    public void PlayerDie(GameObject p)
    {
        StartCoroutine(Respawn(p));
    }

    public IEnumerator Respawn(GameObject p)
    {
        p.gameObject.SetActive(false);

        yield return new WaitForSeconds(2); // respawn time
        // random respawn position
        p.transform.position = new Vector3(Random.Range(-m_xMinMax, m_xMinMax), Random.Range(-m_yMinMax, m_yMinMax), 0);
        p.gameObject.SetActive(true);
    }
}
