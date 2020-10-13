using UnityEngine;
using System.Collections;

namespace Bolt.AdvancedTutorial
{
	public class LineFade : MonoBehaviour
	{
		[SerializeField] private Color color;

        [SerializeField] private float speed = 10f;

		LineRenderer lineR;

		void Start ()
		{
			lineR = GetComponent<LineRenderer> ();
		}

		void Update ()
		{
			
			color.a = Mathf.Lerp (color.a, 0, Time.deltaTime * speed);		
			lineR.startColor = color;
			lineR.endColor = color;
		}
	}
}
