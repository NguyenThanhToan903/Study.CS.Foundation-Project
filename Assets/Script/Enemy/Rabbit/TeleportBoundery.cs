//using UnityEngine;

//public class TeleportBoundery : MonoBehaviour
//{
//    [SerializeField] private Boundery boundery;

//    private void FixedUpdate()
//    {
//        if (Mathf.Abs(transform.position.x) > boundery.XLimit)
//        {
//            if (transform.position.x > 0)
//            {
//                transform.position = new Vector3(-boundery.XLimit, transform.position.y, transform.position.z);
//            }
//            else
//            {
//                transform.position = new Vector3(boundery.XLimit, transform.position.y, transform.position.z);
//            }
//        }
//        if (Mathf.Abs(transform.position.y) > boundery.YLimit)
//        {
//            if (transform.position.y > 0)
//            {
//                transform.position = new Vector3(transform.position.x, -boundery.YLimit, transform.position.z);
//            }
//            else
//            {
//                transform.position = new Vector3(transform.position.x, boundery.YLimit, transform.position.z);
//            }
//        }
//    }
//}
