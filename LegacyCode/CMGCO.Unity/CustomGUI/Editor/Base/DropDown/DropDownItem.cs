using UnityEngine;

namespace CMGCO.Unity.CustomGUI.Base{
	
	public class DropDownItem<DataValueType> {
		public string itemLabel; 
		public DataValueType itemValue;

		public DropDownItem(string nItemLabel, DataValueType nItemValue){
			this.itemLabel  = nItemLabel;
			this.itemValue = nItemValue;
		}
	}
}