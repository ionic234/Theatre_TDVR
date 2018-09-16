using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

using CMGCO.Unity.EinsteinRosenBridge;

public class PortalGatewayPropSyncTest
{
    private GameObject gameObjectA = new GameObject("gameObjectA");
    private GameObject gameObjectB = new GameObject("gameObjectB");
    private GameObject gameObjectC = new GameObject("gameObjectC");
    private GameObject gameObjectD = new GameObject("gameObjectD");
    private PortalGateway gatewayA;
    private PortalGateway gatewayB;
    private PortalGateway gatewayC;
    private PortalGateway gatewayD;

    public PortalGatewayPropSyncTest()
    {
        this.gatewayA = gameObjectA.AddComponent(typeof(PortalGateway)) as PortalGateway;
        this.gatewayB = gameObjectB.AddComponent(typeof(PortalGateway)) as PortalGateway;
        this.gatewayC = gameObjectC.AddComponent(typeof(PortalGateway)) as PortalGateway;
        this.gatewayD = gameObjectD.AddComponent(typeof(PortalGateway)) as PortalGateway;
    }





    /* 
    [Test]
    public void SetDestinationPortalGatewayCreatesAssosiations()
    {




        PortalGatewayEditor testEditor = ScriptableObject.CreateInstance(typeof(PortalGatewayEditor)) as PortalGatewayEditor;
        testEditor.target = this.gatewayA;

        //PortalGatewayEditor testEditor = new PortalGatewayEditor();
        //testEditor.target = this.gatewayA;




        this.gatewayA.SetDestinationPortal(this.gatewayB);
        Assert.That(this.gatewayB.DestinationPortalGateway, Is.EqualTo(gatewayA), "PortalGateway.SetDestinationPortal: Destination gateway does not set this portal as its destination ");


    }



    /* 
    [Test]
    public void PortalGatewayValidateNotSelfPassesCorrectly()
    {
        Assert.That(PortalGatewayValidator.Validate(gatewayA, gatewayB), Is.True, "validateNotSelf: Portal Gateway Validator returns true if both portals are different");
    }

    [Test]
    public void PortalGatewayValidateNotSelfFailsCorrectly()
    {
        // Use the Assert class to test conditions.
        Assert.That(PortalGatewayValidator.Validate(gatewayA, gatewayA), Is.False, "validateNotSelf: Portal Gateway Validator returns false if both portals are the same");
    }
    */

}
