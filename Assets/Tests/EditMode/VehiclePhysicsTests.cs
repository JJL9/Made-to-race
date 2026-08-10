using MadeToRace.Vehicle;
using NUnit.Framework;

namespace MadeToRace.Tests.EditMode
{
    /// <summary>
    /// Pure-logic tests for the vehicle physics model (DECISIONS.md —
    /// Vehicle Physics Complexity): engine power curve, drag/downforce ∝ v²,
    /// traction limits, weight transfer, and part assembly.
    /// Reference kart: 15 kW, Cd 0.7, A 0.9 m², ~177 kg.
    /// </summary>
    public sealed class VehiclePhysicsTests
    {
        private const float Power = 15000f; // 15 kW kart engine
        private const float DragCoeff = 0.7f;
        private const float FrontalArea = 0.9f;

        [Test]
        public void DriveForce_AtLaunch_IsTractionLimited()
        {
            // P/v is infinite at v=0 — the tires set the limit.
            Assert.That(VehiclePhysics.DriveForce(Power, 0f, 500f), Is.EqualTo(500f));
        }

        [Test]
        public void DriveForce_AtSpeed_IsPowerLimited()
        {
            // F = P/v = 15000/20 = 750 N, below the 2000 N traction cap.
            Assert.That(VehiclePhysics.DriveForce(Power, 20f, 2000f), Is.EqualTo(750f));
        }

        [Test]
        public void DriveForce_CapsAtTraction()
        {
            // At low speed P/v exceeds grip — traction wins.
            Assert.That(VehiclePhysics.DriveForce(Power, 5f, 500f), Is.EqualTo(500f));
        }

        [Test]
        public void DragForce_ScalesWithSpeedSquared()
        {
            float at10 = VehiclePhysics.DragForce(10f, DragCoeff, FrontalArea);
            float at20 = VehiclePhysics.DragForce(20f, DragCoeff, FrontalArea);
            Assert.That(at20 / at10, Is.EqualTo(4f).Within(0.001f));
        }

        [Test]
        public void Downforce_ScalesWithSpeedSquared()
        {
            float at10 = VehiclePhysics.Downforce(10f, 0.5f, FrontalArea);
            float at20 = VehiclePhysics.Downforce(20f, 0.5f, FrontalArea);
            Assert.That(at20 / at10, Is.EqualTo(4f).Within(0.001f));
        }

        [Test]
        public void TopSpeed_MatchesPowerDragBalance()
        {
            // v = (2P/ρCdA)^(1/3); at that speed, drag force × v must equal P.
            float top = VehiclePhysics.TopSpeed(Power, DragCoeff, FrontalArea);
            float dragPower = VehiclePhysics.DragForce(top, DragCoeff, FrontalArea) * top;
            Assert.That(dragPower, Is.EqualTo(Power).Within(Power * 0.01f));
            Assert.That(top, Is.InRange(30f, 38f), "kart class: ~120 km/h ≈ 33 m/s");
        }

        [Test]
        public void GripLimit_IsMuTimesLoad()
        {
            Assert.That(VehiclePhysics.GripLimit(1.0f, 500f), Is.EqualTo(500f));
            Assert.That(VehiclePhysics.GripLimit(1.3f, 500f), Is.EqualTo(650f), "slick tires grip more");
        }

        [Test]
        public void LongitudinalTransfer_AccelShiftsLoadRearward()
        {
            float transfer = VehiclePhysics.LongitudinalTransfer(177f, 5f, 0.3f, 1.4f);
            Assert.That(transfer, Is.EqualTo(177f * 5f * 0.3f / 1.4f).Within(0.001f));
            Assert.That(transfer, Is.GreaterThan(0f));
        }

        [Test]
        public void Assembly_CombinesPartStats()
        {
            // Kart chassis (incl. driver 60kg) + engine (25kg) + 4 wheels (12kg) = 177 kg.
            AssembledVehicle spec = VehicleAssembly.Combine(
                chassisMassKg: 140f, chassisDragCoeff: 0.7f, chassisFrontalArea: 0.9f,
                centerHeight: 0.3f, wheelbase: 1.4f, trackWidth: 1.2f, frontWeightRatio: 0.45f,
                engineMassKg: 25f, enginePowerWatts: Power, wheelFrictionCoeff: 1.0f, wheelCount: 4);

            Assert.That(spec.MassKg, Is.EqualTo(177f));
            Assert.That(spec.PowerWatts, Is.EqualTo(Power));
            Assert.That(spec.FrictionCoeff, Is.EqualTo(1.0f));
            Assert.That(spec.DragCoeff, Is.EqualTo(0.7f));
        }
    }
}
