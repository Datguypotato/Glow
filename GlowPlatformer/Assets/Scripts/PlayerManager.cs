using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] GameObject m_AfterGlowPrefab;

    [SerializeField] protected float m_yMinMax = 6;
    [SerializeField] protected float m_xMinMax = 3;

    // directional effect
    protected void SpawnAfterGlow(Vector3 p1Pos, Vector3 p2Pos, Vector3 collPoint)
    {
        Vector3 dirp1 = (p1Pos - p2Pos);
        Vector3 dirp2 = (p2Pos - p1Pos);

        Transform t1 = Instantiate(m_AfterGlowPrefab, collPoint, Quaternion.identity).GetComponent<Transform>();
        Transform t2 = Instantiate(m_AfterGlowPrefab, collPoint, Quaternion.identity).GetComponent<Transform>();

        //Debug.Log(dirp1);

        t1.rotation = Quaternion.FromToRotation(Vector3.right, dirp1);
        t2.rotation = Quaternion.FromToRotation(Vector3.right, dirp2);
    }

    // Couldn't start coroutine from the instance
    public void StartRespawn(BasePlayer p1, BasePlayer p2, Vector3 collPoint)
    {
        StartCoroutine(RespawnPairs(p1, p2, collPoint));
        SpawnAfterGlow(p1.transform.position, p2.transform.position, collPoint);
        MusicManager.instance.ChangeMusic();
    }

    public IEnumerator RespawnPairs(BasePlayer p1, BasePlayer p2, Vector3 collPoint)
    {
        p1.GetParticle().Stop();
        p2.GetParticle().Stop();

        p1.gameObject.SetActive(false);
        p2.gameObject.SetActive(false);

        yield return new WaitForSeconds(1); // respawn time

        // random respawn position
        p1.transform.position = new Vector3(Random.Range(-m_xMinMax, m_xMinMax), Random.Range(-m_yMinMax, m_yMinMax), 0);
        p2.transform.position = new Vector3(Random.Range(-m_xMinMax, m_xMinMax), Random.Range(-m_yMinMax, m_yMinMax), 0);
        p1.gameObject.SetActive(true);
        p2.gameObject.SetActive(true);
    }

    public float GetMinMaxX()
    {
        return m_xMinMax;
    }

    public float GetMinMaxy()
    {
        return m_yMinMax;
    }
}
