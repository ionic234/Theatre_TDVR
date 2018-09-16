using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace CMGCO.Unity.EinsteinRosenBridge
{

    [ExecuteInEditMode]
    [DisallowMultipleComponent]
    [RequireComponent(typeof(BoxCollider))]

    public class PortalGateway : MonoBehaviour
    {

        public int testNumber = 0;

        [SerializeField]
        private PortalGateway destinationPortalGateway;
        public PortalGateway DestinationPortalGateway
        {
            get
            {
                return this.destinationPortalGateway;
            }
            set
            {
                this.SetDestinationPortal(value, true, false);
            }
        }

        public void SetDestinationPortal(PortalGateway newDestinationPortalGateway, bool isPropigate = false, bool isValidate = true)
        {
            if (PortalGatewayValidator.Validate(DestinationPortalGateway, this))
            {
                if (this.DestinationPortalGateway != null)
                {
                    // Clear the old destination portal. 
                    this.DestinationPortalGateway.ClearDestinationPortal();
                }

                this.DestinationPortalGateway = newDestinationPortalGateway;
                if (isPropigate)
                {
                    this.DestinationPortalGateway.SetDestinationPortal(this.DestinationPortalGateway);
                }
            }

        }


        public void PropagateSharedProperties()
        {

        }

        public void ClearDestinationPortal()
        {

        }
    }
}

