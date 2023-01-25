using UnityEngine;

namespace Tools
{
	public class AlwaysFaceCamera : MonoBehaviour
	{
		private Camera _cam;
 
		private void Awake() => _cam = Camera.main; 
     
		private void Update() => transform.LookAt(_cam.transform);
	}
}