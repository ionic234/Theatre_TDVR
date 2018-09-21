using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

using CMGCO.Unity.EinsteinRosenBridge;

public class PortalGatewayPropSyncTest
{
    private GameObject gameObject1;
    private GameObject gameObject2;
    private GameObject gameObject3;
    private GameObject gameObject4;
    private PortalGateway gateway1;
    private PortalGateway gateway2;
    private PortalGateway gateway3;
    private PortalGateway gateway4;



    [SetUp]
    public void SetUpFunc()
    {
        this.gameObject1 = new GameObject("gameObject1");
        this.gameObject2 = new GameObject("gameObject2");
        this.gameObject3 = new GameObject("gameObject3");
        this.gameObject4 = new GameObject("gameObject4");

        this.gateway1 = this.gameObject1.AddComponent(typeof(PortalGateway)) as PortalGateway;
        this.gateway2 = this.gameObject2.AddComponent(typeof(PortalGateway)) as PortalGateway;
        this.gateway3 = this.gameObject3.AddComponent(typeof(PortalGateway)) as PortalGateway;
        this.gateway4 = this.gameObject4.AddComponent(typeof(PortalGateway)) as PortalGateway;
    }

    [TearDown]
    public void TearDownFunc()
    {
        UnityEngine.Object.DestroyImmediate(this.gameObject1);
        UnityEngine.Object.DestroyImmediate(this.gameObject2);
        UnityEngine.Object.DestroyImmediate(this.gameObject3);
        UnityEngine.Object.DestroyImmediate(this.gameObject4);
    }

    [Test]
    public void linkPortalGateways_ADestinationSet()
    {
        PortalGatewayPropSync.LinkPortalGateways(this.gateway1, this.gateway2, false);
        Assert.That(this.gateway1.DestinationPortalGateway, Is.SameAs(gateway2), "PortalGatewayPropSync: Destination Of Portal A is not Set to Portal B ");
    }

    [Test]
    public void linkPortalGateways_BDestinationSet()
    {
        PortalGatewayPropSync.LinkPortalGateways(this.gateway1, this.gateway2, false);
        Assert.That(this.gateway2.DestinationPortalGateway, Is.SameAs(gateway1), "PortalGatewayPropSync: Destination Of Portal B is not Set to Portal A ");
    }

    // Copying variables test 

    [Test]
    public void linkPortalGateways_oldADestinationCleared()
    {
        PortalGatewayPropSync.LinkPortalGateways(this.gateway1, this.gateway2, false);
        PortalGatewayPropSync.LinkPortalGateways(this.gateway1, this.gateway3, false);
        Assert.That(this.gateway2.DestinationPortalGateway, Is.SameAs(null), "PortalGatewayPropSync: The Previous destination portal of A is still pointing at A");
    }

    [Test]
    public void linkPortalGateways_oldBDestinationCleared()
    {
        PortalGatewayPropSync.LinkPortalGateways(this.gateway3, this.gateway4, false);
        PortalGatewayPropSync.LinkPortalGateways(this.gateway1, this.gateway3, false);
        Assert.That(this.gateway4.DestinationPortalGateway, Is.SameAs(null), "PortalGatewayPropSync: The Previous destination Portal of B is Still pointing at B");
    }


    // Reseting variables test 


    [Test]
    public void UndoAAndBWhenNoDestinationsSet()
    {
        PortalGatewayPropSync.LinkPortalGateways(this.gateway1, this.gateway2, false);
        Undo.IncrementCurrentGroup();
        Undo.FlushUndoRecordObjects();
        Undo.PerformUndo();
        Assert.That(this.gateway1.DestinationPortalGateway, Is.SameAs(null), "PortalGatewayPropSync: Destination Of Gateway1 has not been undone to the origional value of null ");
        Assert.That(this.gateway2.DestinationPortalGateway, Is.SameAs(null), "PortalGatewayPropSync: Destination Of Gateway2 has not been undone to the origional value of null ");
    }

    [Test]
    public void UndoAAndBWhenADestinationIsSet()
    {
        PortalGatewayPropSync.LinkPortalGateways(this.gateway1, this.gateway3, false);
        Undo.IncrementCurrentGroup();
        Undo.FlushUndoRecordObjects();
        PortalGatewayPropSync.LinkPortalGateways(this.gateway1, this.gateway2, false);
        Undo.IncrementCurrentGroup();
        Undo.FlushUndoRecordObjects();
        Undo.PerformUndo();
        Assert.That(this.gateway1.DestinationPortalGateway, Is.SameAs(this.gateway3), "PortalGatewayPropSync: Destination Of Gateway1 has not been undone to the origional value of gateway 3 ");
        Assert.That(this.gateway2.DestinationPortalGateway, Is.SameAs(null), "PortalGatewayPropSync: Destination Of Gateway2 has not been undone to the origional value of null ");
        Assert.That(this.gateway3.DestinationPortalGateway, Is.SameAs(this.gateway1), "PortalGatewayPropSync: Destination Of Gateway3 has not been undone to the origional value of gateway 1 ");
    }

    [Test]
    public void UndoAAndBWhenBDestinationIsSet()
    {
        PortalGatewayPropSync.LinkPortalGateways(this.gateway2, this.gateway3, false);
        Undo.IncrementCurrentGroup();
        Undo.FlushUndoRecordObjects();
        PortalGatewayPropSync.LinkPortalGateways(this.gateway1, this.gateway2, false);
        Undo.IncrementCurrentGroup();
        Undo.FlushUndoRecordObjects();
        Undo.PerformUndo();
        Assert.That(this.gateway1.DestinationPortalGateway, Is.SameAs(null), "PortalGatewayPropSync: Destination Of Gateway1 has not been undone to the origional value of null ");
        Assert.That(this.gateway2.DestinationPortalGateway, Is.SameAs(this.gateway3), "PortalGatewayPropSync: Destination Of Gateway2 has not been undone to the origional value of gateway 3 ");
        Assert.That(this.gateway3.DestinationPortalGateway, Is.SameAs(this.gateway2), "PortalGatewayPropSync: Destination Of Gateway3 has not been undone to the origional value of gateway 2 ");
    }

    [Test]
    public void UndoAAndBWhenBothDestinationsSet()
    {
        PortalGatewayPropSync.LinkPortalGateways(this.gateway1, this.gateway3, false);
        Undo.IncrementCurrentGroup();
        Undo.FlushUndoRecordObjects();
        PortalGatewayPropSync.LinkPortalGateways(this.gateway2, this.gateway4, false);
        Undo.IncrementCurrentGroup();
        Undo.FlushUndoRecordObjects();
        PortalGatewayPropSync.LinkPortalGateways(this.gateway1, this.gateway2, false);
        Undo.IncrementCurrentGroup();
        Undo.FlushUndoRecordObjects();
        Undo.PerformUndo();
        Assert.That(this.gateway1.DestinationPortalGateway, Is.SameAs(this.gateway3), "PortalGatewayPropSync: Destination Of Gateway1 has not been undone to the origional value of gateway 3 ");
        Assert.That(this.gateway2.DestinationPortalGateway, Is.SameAs(this.gateway4), "PortalGatewayPropSync: Destination Of Gateway2 has not been undone to the origional value of gateway 4 ");
        Assert.That(this.gateway3.DestinationPortalGateway, Is.SameAs(this.gateway1), "PortalGatewayPropSync: Destination Of Gateway3 has not been undone to the origional value of gateway 1 ");
        Assert.That(this.gateway4.DestinationPortalGateway, Is.SameAs(this.gateway2), "PortalGatewayPropSync: Destination Of Gateway4 has not been undone to the origional value of gateway 2 ");
    }


    [Test]
    public void delete()
    {
        PortalGatewayPropSync.LinkPortalGateways(this.gateway1, this.gateway2, false);
        PortalGatewayPropSync.deleteDestinationPortalGateway(this.gateway1);
        Assert.That(this.gateway1.DestinationPortalGateway, Is.SameAs(null), "PortalGatewayPropSync: Pressing Delete has not cleared the targets destination portal");
        Assert.That(this.gateway2.DestinationPortalGateway, Is.SameAs(null), "PortalGatewayPropSync: Pressing Delete has not propigated to the the destination portal ");
    }



    [Test]
    public void undoDelete()
    {
        PortalGatewayPropSync.LinkPortalGateways(this.gateway1, this.gateway2, false);
        Undo.IncrementCurrentGroup();
        Undo.FlushUndoRecordObjects();
        PortalGatewayPropSync.deleteDestinationPortalGateway(this.gateway1);
        Undo.IncrementCurrentGroup();
        Undo.FlushUndoRecordObjects();
        Undo.PerformUndo();
        Assert.That(this.gateway1.DestinationPortalGateway, Is.SameAs(this.gateway2), "PortalGatewayPropSync: Pressing Delete has not cleared the targets destination portal");
        Assert.That(this.gateway2.DestinationPortalGateway, Is.SameAs(this.gateway1), "PortalGatewayPropSync: Pressing Delete has not propigated to the the destination portal ");
    }


}
