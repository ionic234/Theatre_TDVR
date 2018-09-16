
#if UNITY_EDITOR
	using UnityEditor;
#endif


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


namespace CMGCO.Unity.Tests
{
	[ExecuteInEditMode]
	[DisallowMultipleComponent]   

	

    public class Test : MonoBehaviour
    {
		public string testString = "hello";

		[SerializeField]
		private string testStringGetSet = "";
		public string _testStringGetSet{
			get{
				return this.testStringGetSet;
			}
			set{
				this.testStringGetSet = value;
			}
		}
		
		[SerializeField]
		private Vector2 screenRatio = new Vector2(0,0);
		public Vector2 _screenRatio{
			get{
				return this.screenRatio;
			}
			set{
				this.screenRatio = value; 
				//this.setScreenRatio(value, true);
			}

		}




	}
}