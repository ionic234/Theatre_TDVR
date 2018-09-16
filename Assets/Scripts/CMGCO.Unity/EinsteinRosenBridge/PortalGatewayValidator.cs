
using UnityEditor;
using UnityEngine;

namespace CMGCO.Unity.EinsteinRosenBridge
{
    [ExecuteInEditMode]
    public class PortalGatewayValidator
    {
        static public bool Validate(PortalGateway gatewayToValidate, PortalGateway targetGateway, bool showErrorDialog = false)
        {
            if (!validateNotSelf(gatewayToValidate, targetGateway))
            {
#if UNITY_EDITOR
                if (showErrorDialog)
                {
                    EditorUtility.DisplayDialog("Portal Gateway Picker Error", "A portal gateway cannot have its exit portal set to itself.", "OK");
                }
#endif
                return false;
            }
            else
            {
                return true;
            }
        }

        static private bool validateNotSelf(PortalGateway gatewayToValidate, PortalGateway targetGateway)
        {
            // Both can be null so use !=
            return targetGateway != gatewayToValidate;
        }
    }
}
