
using UnityEngine;

namespace CMGCO.Unity.CustomGUI.AspectRatio{
	
	public class AspectRatioData {
		public string ratioDescription; 
		public Vector2 ratioValue;

		public AspectRatioData(string nRatioDescription, Vector2 nRatioValue){
			ratioDescription = nRatioDescription;
			ratioValue = nRatioValue;

		}
	}
}