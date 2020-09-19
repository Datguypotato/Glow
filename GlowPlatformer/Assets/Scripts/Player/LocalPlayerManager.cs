using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LocalPlayerManager : MonoBehaviour
{
    public static LocalPlayerManager instance;
    [SerializeField] GameObject m_AfterGlowPrefab;

    [SerializeField] Transform m_explosionSpriteTrans;

    
    [SerializeField] private GameObject m_localPlayerPrefab;
    [SerializeField] private Transform m_PlayerHolder;
    [SerializeField] private LocalPlayer m_CurrFocusedPlayer;

    [Header("Controls")]
    [SerializeField] private TMP_InputField up;
    [SerializeField] private TMP_InputField down;
    [SerializeField] private TMP_InputField left;
    [SerializeField] private TMP_InputField right;

    private float m_xMinMax = 3;
    private float m_yMinMax = 6;

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    // directional effect
    private void SpawnAfterGlow(Vector3 p1Pos, Vector3 p2Pos, Vector3 collPoint)
    {        
        Vector3 dirp1 = (p1Pos - p2Pos);
        Vector3 dirp2 = (p2Pos - p1Pos);

        Transform t1 = Instantiate(m_AfterGlowPrefab, collPoint, Quaternion.identity).GetComponent<Transform>();
        Transform t2 = Instantiate(m_AfterGlowPrefab, collPoint, Quaternion.identity).GetComponent<Transform>();

        Debug.Log(dirp1);

        t1.rotation = Quaternion.FromToRotation(Vector3.right, dirp1);
        t2.rotation = Quaternion.FromToRotation(Vector3.right, dirp2);
    }

    public void SpawnPlayer()
    {
        Debug.Log("spawned player");
        Instantiate(m_localPlayerPrefab, m_PlayerHolder);
    }

    public void UpdateInputField(LocalPlayer a_localPlayer)
    {
        m_CurrFocusedPlayer = a_localPlayer;

        up.text = a_localPlayer.up.ToString();
        down.text = a_localPlayer.down.ToString();
        left.text = a_localPlayer.left.ToString();
        right.text = a_localPlayer.right.ToString();
    }

    // Get called from inputfield event
    public void UpdatePlayerInputs()
    {
        m_CurrFocusedPlayer.up = (KeyCode)System.Enum.Parse(typeof(KeyCode), up.text.ToUpper());
        m_CurrFocusedPlayer.down = (KeyCode)System.Enum.Parse(typeof(KeyCode), down.text.ToUpper());
        m_CurrFocusedPlayer.left = (KeyCode)System.Enum.Parse(typeof(KeyCode), left.text.ToUpper());
        m_CurrFocusedPlayer.right = (KeyCode)System.Enum.Parse(typeof(KeyCode), right.text.ToUpper());

        Debug.Log("Updated " + m_CurrFocusedPlayer.name + " inputs");
    }

    // Couldn't start coroutine from the instance
    public void StartRespawn(LocalPlayer p1, LocalPlayer p2, Vector3 collPoint)
    {
        StartCoroutine(RespawnPairs(p1, p2, collPoint));
        SpawnAfterGlow(p1.transform.position, p2.transform.position, collPoint);
    }

    public IEnumerator RespawnPairs(LocalPlayer p1, LocalPlayer p2, Vector3 collPoint)
    {
        
        m_explosionSpriteTrans.position = collPoint;
        m_explosionSpriteTrans.gameObject.SetActive(true);
        p1.gameObject.SetActive(false);
        p2.gameObject.SetActive(false);

        yield return new WaitForSeconds(0.5f); // let explosion sprite play

        m_explosionSpriteTrans.gameObject.SetActive(false);

        yield return new WaitForSeconds(1); // respawn time
        // random respawn position
        p1.transform.position = new Vector3(Random.Range(-m_xMinMax, m_xMinMax), Random.Range(-m_yMinMax, m_yMinMax), 0);
        p2.transform.position = new Vector3(Random.Range(-m_xMinMax, m_xMinMax), Random.Range(-m_yMinMax, m_yMinMax), 0);
        p1.gameObject.SetActive(true);
        p2.gameObject.SetActive(true);
    }
}
