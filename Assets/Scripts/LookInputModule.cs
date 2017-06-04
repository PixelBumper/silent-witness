using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Camera))]
public class LookInputModule: MonoBehaviour
{

	private Camera _camera;
	private Transform previouslyFocused;
	
	// Use this for initialization
	void Start ()
	{
		_camera = GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		RaycastHit hit;
		var ray = _camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
		Debug.Log("WTF");

		if (Physics.Raycast(ray, out hit))
		{
			Debug.Log("Whatching at "+ hit.transform.name);
			if (previouslyFocused == null)
			{
				previouslyFocused = hit.transform;
				var pointerEnterHandler = previouslyFocused.GetComponent<IPointerEnterHandler>();
				if (pointerEnterHandler != null)
				{
					pointerEnterHandler.OnPointerEnter(new PointerEventData(EventSystem.current));
				}
			}
			else if (previouslyFocused != hit.transform)
			{
				if (previouslyFocused != null)
				{
					var exitHandler = previouslyFocused.GetComponent<IPointerExitHandler>();
					if (exitHandler != null)
					{
						exitHandler.OnPointerExit(new PointerEventData(EventSystem.current));
					}
				}
				previouslyFocused = hit.transform;
				var pointerEnterHandler = previouslyFocused.GetComponent<IPointerEnterHandler>();
				if (pointerEnterHandler != null)
				{
					pointerEnterHandler.OnPointerEnter(new PointerEventData(EventSystem.current));
				}

			}
		}
		else
		{
			var exitHandler = previouslyFocused.GetComponent<IPointerExitHandler>();
			if (exitHandler != null)
			{
				exitHandler.OnPointerExit(new PointerEventData(EventSystem.current));
			}
		}
	}
}
