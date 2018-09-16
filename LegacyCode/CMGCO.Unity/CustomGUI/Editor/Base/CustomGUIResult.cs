
using UnityEngine;

namespace CMGCO.Unity.CustomGUI.Base{
	
	public class CustomGUIResult<ResultIDType, ResulltValueType> {
		public ResultIDType resultID; 
		public ResulltValueType resultValue;

		public CustomGUIResult( ResultIDType nResultID,  ResulltValueType nResultValue ){
			this.resultID = nResultID;
			this.resultValue = nResultValue;
		}
	}
}