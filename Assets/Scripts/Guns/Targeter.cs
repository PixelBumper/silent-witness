using UnityEngine;

public class Targeter : MonoBehaviour
{
    [SerializeField] private float _range = 5;
    [SerializeField] private Raycaster _raycaster;

    public GameObject TargetedGameObject
    {
        get
        {
            var raycastHitInfo = _raycaster.HitInfo;
        
            if (raycastHitInfo.transform != null &&
                Vector3.SqrMagnitude(raycastHitInfo.point - transform.position) <= _range * _range)
            {
                return raycastHitInfo.transform.gameObject;
            }

            return null;
        }
    }
}
