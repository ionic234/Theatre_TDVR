// Static class to sync properties between linked PortalGateways. 
// This is here rather than part of the Editor so that the functionality can be tested. 

using UnityEditor;
using UnityEngine;

namespace CMGCO.Unity.EinsteinRosenBridge
{
    [ExecuteInEditMode]
    public class PortalGatewayPropSync
    {
        static public void LinkPortalGateways(PortalGateway gatewayA, PortalGateway gatewayB, bool showInheritDialog = true, bool inheritFromA = true)
        {
            int group = Undo.GetCurrentGroup();
            string undoName = "Set Exit Portal";
            Undo.SetCurrentGroupName(undoName);
            PortalGateway[] recordObject;

            if (gatewayA.DestinationPortalGateway == null)
            {
                recordObject = gatewayB.DestinationPortalGateway == null ?
                    new PortalGateway[] { gatewayA, gatewayB } :
                    new PortalGateway[] { gatewayA, gatewayB, gatewayB.DestinationPortalGateway };
            }
            else
            {
                recordObject = gatewayB == null || gatewayB.DestinationPortalGateway == null ?
                    new PortalGateway[] { gatewayA, gatewayA.DestinationPortalGateway, gatewayB } :
                    new PortalGateway[] { gatewayA, gatewayA.DestinationPortalGateway, gatewayB, gatewayB.DestinationPortalGateway };
            }

            Undo.RecordObjects(recordObject, undoName);
            gatewayA.DestinationPortalGateway = gatewayB;

            if (gatewayB.DestinationPortalGateway != null)
            {
                if (showInheritDialog)
                {
                    if (EditorUtility.DisplayDialog("Inherit Properties from?", "Which Portal Gateway would you like to inherit the linked properties from?", "This Portal Gateway", "Destination Portal Gateway"))
                    {
                        gatewayA.PropagateSharedProperties();
                    }
                    else
                    {
                        gatewayB.PropagateSharedProperties();
                    }
                }
                else
                {
                    if (inheritFromA)
                    {
                        gatewayA.PropagateSharedProperties();
                    }
                    else
                    {
                        gatewayB.PropagateSharedProperties();
                    }
                }
            }
            else
            {
                gatewayA.PropagateSharedProperties();
            }
            Undo.CollapseUndoOperations(group);
        }
        static public void deleteDestinationPortalGateway(PortalGateway gateway)
        {
            if (gateway.DestinationPortalGateway == null)
            {
                return;
            }

            int group = Undo.GetCurrentGroup();
            string undoName = "Clear Exit Portal";
            Undo.SetCurrentGroupName(undoName);
            PortalGateway[] recordObject = new PortalGateway[] { gateway, gateway.DestinationPortalGateway };
            Undo.RecordObjects(recordObject, undoName);
            gateway.ClearDestinationPortal(true);
            Undo.CollapseUndoOperations(group);

        }

    }
}
