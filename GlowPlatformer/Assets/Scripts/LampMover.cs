//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class LampMover : MonoBehaviour
//{
//    enum MoveMode { Mouse, Lerp, Drag}

//    [SerializeField] MoveMode m_MoveMode;

//    //[SerializeField] float inputMouseZ;
//    [SerializeField] float timeToLerpToTarget = 1;

//    // Start is called before the first frame update
//    void Start()
//    {
        
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        if(m_MoveMode == MoveMode.Mouse)
//        {
//            transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, inputMouseZ));
//        }
//        else if(m_MoveMode == MoveMode.Lerp)
//        {
//            if(Input.GetMouseButtonDown(0))
//            {
//                StartCoroutine(LerpToPos(Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, inputMouseZ))));
//            }
//        }
//        else if(m_MoveMode == MoveMode.Drag)
//        {

//        }

//    }

//    IEnumerator LerpToPos(Vector3 targetPos)
//    {
//        float elapsedTime = 0;
//        Vector3 startPos = transform.position;

//        while (elapsedTime < timeToLerpToTarget)
//        {
//            Vector3 newPos = Vector3.Lerp(startPos, targetPos, elapsedTime / timeToLerpToTarget);
//            transform.position = new Vector3(newPos.x, newPos.y, newPos.z);
//            elapsedTime += Time.deltaTime;
//            yield return null;
//        }
//    }
//}
