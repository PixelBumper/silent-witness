using UnityEngine;

public class Raycaster : MonoBehaviour 
{
	[SerializeField] private LayerMask _layer = -1;
	[SerializeField] private float _forwardOffset = 0.25f;
	[SerializeField] private float _length = 100;
	private RaycastHit _hitInfo;
	private int _lastUpdatedFrame = -1;
	
#if UNITY_EDITOR
	[SerializeField] private bool _debug;
#endif

	private Vector3 Origin { get; set; }
	

	public RaycastHit HitInfo
	{
		get
		{
			if (_lastUpdatedFrame != Time.frameCount)
			{
				_lastUpdatedFrame = Time.frameCount;
				Origin = transform.position + transform.forward * _forwardOffset;
				Physics.Raycast (Origin, transform.forward, out _hitInfo, _length, _layer);
			}

			return _hitInfo;
		}
	}
	
#if UNITY_EDITOR
	private void OnDrawGizmos()
	{
		if (_debug && HitInfo.transform != null)
		{
			Gizmos.color = Color.red;
			Gizmos.DrawLine(Origin, HitInfo.point);
			Gizmos.DrawSphere(HitInfo.point, 0.1f);
		}
	}
#endif
	
}
