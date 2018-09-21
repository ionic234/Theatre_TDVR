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

        public void SetDestinationPortal(PortalGateway newDestinationPortalGateway, bool isPropigate = false, bool isValidate = false)
        {
            if (!isValidate || PortalGatewayValidator.Validate(newDestinationPortalGateway, this))
            {
                if (this.destinationPortalGateway != null)
                {
                    this.destinationPortalGateway.ClearDestinationPortal();
                }
                this.destinationPortalGateway = newDestinationPortalGateway;
                if (isPropigate)
                {
                    this.destinationPortalGateway.SetDestinationPortal(this, false);
                }
            }

        }

        public void PropagateSharedProperties()
        {
            //Debug.Log("PropagateSharedProperties");
        }

        public void InheritSharedProperties()
        {

            //Debug.Log("InheritSharedProperties");
        }

        public void ClearDestinationPortal(bool isPropigate = false)
        {
            // todo: Reset vars to default, is this manditory. 
            if (isPropigate)
            {
                this.destinationPortalGateway.ClearDestinationPortal();
            }
            this.destinationPortalGateway = null;

        }
    }
}

