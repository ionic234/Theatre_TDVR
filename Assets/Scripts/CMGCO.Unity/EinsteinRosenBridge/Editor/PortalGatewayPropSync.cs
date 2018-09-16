// Static class to sync properties between linked PortalGateways. 
// This is here rather than part of the Editor so that the functionality can be tested. 

using UnityEditor;
using UnityEngine;

namespace CMGCO.Unity.EinsteinRosenBridge
{
    [ExecuteInEditMode]
    public class PortalGatewayPropSync
    {
        static public void linkPortalGateways(PortalGateway gatewayA, PortalGateway gatewayB)
        {

        }




    }
}

/*
 private void setDestinationPortalGateway(PortalGateway newDestinationPortalGateway)
        {
            int group = Undo.GetCurrentGroup();
            string undoName = "Set Exit Portal";
            Undo.SetCurrentGroupName(undoName);
            PortalGateway[] recordObject;
            // Need to do some tests to work out what goes in the array 
            if (this.editorTarget.DestinationPortalGateway == null)
            {
                recordObject = newDestinationPortalGateway.DestinationPortalGateway == null ?
                    new PortalGateway[] { this.editorTarget, newDestinationPortalGateway } :
                    new PortalGateway[] { this.editorTarget, newDestinationPortalGateway, newDestinationPortalGateway.DestinationPortalGateway };
            }
            else
            {
                recordObject = newDestinationPortalGateway.DestinationPortalGateway == null ?
                    new PortalGateway[] { this.editorTarget, this.editorTarget.DestinationPortalGateway, newDestinationPortalGateway } :
                    new PortalGateway[] { this.editorTarget, this.editorTarget.DestinationPortalGateway, newDestinationPortalGateway, newDestinationPortalGateway.DestinationPortalGateway };
            }
            Undo.RecordObjects(recordObject, undoName);
            this.editorTarget.DestinationPortalGateway = newDestinationPortalGateway;
            // If are new destination is linked then it will already have valid properties set.  
            if (newDestinationPortalGateway.DestinationPortalGateway == null
                || EditorUtility.DisplayDialog("Inherit Properties from?", "Which Portal Gateway would you like to inherit the linked properties from?", "This Portal Gateway", "Destination Portal Gateway"))
            {
                this.editorTarget.PropagateSharedProperties();
            }
            else
            {
                this.editorTarget.InheritSharedProperties();
            }
            Undo.CollapseUndoOperations(group);
        }
 */
