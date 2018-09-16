using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

using CMGCO.Unity.EinsteinRosenBridge;



public class PortalGatewayValidatorTests
{
    private GameObject gameObjectA = new GameObject("gameObjectA");
    private GameObject gameObjectB = new GameObject("gameObjectB");
    private PortalGateway gatewayA;
    private PortalGateway gatewayB;

    public PortalGatewayValidatorTests()
    {
        this.gatewayA = gameObjectA.AddComponent(typeof(PortalGateway)) as PortalGateway;
        this.gatewayB = gameObjectB.AddComponent(typeof(PortalGateway)) as PortalGateway;
    }

    [Test]
    public void ValidateNotSelfPassesCorrectly()
    {
        Assert.That(PortalGatewayValidator.Validate(gatewayA, gatewayB), Is.True, "validateNotSelf: Portal Gateway Validator returns true if both portals are different");
    }

    [Test]
    public void ValidateNotSelfFailsCorrectly()
    {
        // Use the Assert class to test conditions.
        Assert.That(PortalGatewayValidator.Validate(gatewayA, gatewayA), Is.False, "validateNotSelf: Portal Gateway Validator returns false if both portals are the same");
    }

}
