using System;
using UnityEngine;
using CMGCO.Unity.CustomGUI.Base;
using CMGCO.Unity.CustomGUI.Components;

namespace CMGCO.Unity.EinsteinRosenBridge
{
    public class PortalGatewayGUI : ObjectPickerGUI<PortalGateway>
    {

        // Implement using thread safe singleton pattern. 
        private static volatile PortalGatewayGUI instance;
        private static object syncRoot = new System.Object();

        private PortalGatewayGUI() { }

        public static PortalGatewayGUI Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new PortalGatewayGUI();
                    }
                }
                return instance;
            }
        }
    }
}