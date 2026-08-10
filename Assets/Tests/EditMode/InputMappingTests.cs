using MadeToRace.Vehicle;
using NUnit.Framework;
using UnityEngine;

namespace MadeToRace.Tests.EditMode
{
    /// <summary>
    /// Pure tests for the input mapping (PlayerInputDriver.ComputeInput):
    /// WASD/arrows → throttle/steer, opposites cancel, gamepad stick blends
    /// and clamps. No input devices, no frame loop — deterministic.
    /// (The InputTestFixture-based PlayMode tests were removed: simulating
    /// presses in batchmode corrupts the Input System's cached state.)
    /// </summary>
    public sealed class InputMappingTests
    {
        private static (float throttle, float steer) Map(
            bool w = false, bool s = false, bool a = false, bool d = false,
            bool up = false, bool down = false, bool left = false, bool right = false,
            Vector2 stick = default)
        {
            return PlayerInputDriver.ComputeInput(w, s, a, d, up, down, left, right, stick);
        }

        [Test]
        public void W_ThrottlesForward()
        {
            var (throttle, steer) = Map(w: true);
            Assert.That(throttle, Is.EqualTo(1f));
            Assert.That(steer, Is.EqualTo(0f));
        }

        [Test]
        public void S_ThrottlesReverse()
        {
            var (throttle, _) = Map(s: true);
            Assert.That(throttle, Is.EqualTo(-1f));
        }

        [Test]
        public void A_SteersLeft()
        {
            var (_, steer) = Map(a: true);
            Assert.That(steer, Is.EqualTo(-1f));
        }

        [Test]
        public void D_SteersRight()
        {
            var (_, steer) = Map(d: true);
            Assert.That(steer, Is.EqualTo(1f));
        }

        [Test]
        public void Arrows_MirrorWASD()
        {
            var (throttle, steer) = Map(up: true, right: true);
            Assert.That(throttle, Is.EqualTo(1f));
            Assert.That(steer, Is.EqualTo(1f));

            var (downThrottle, leftSteer) = Map(down: true, left: true);
            Assert.That(downThrottle, Is.EqualTo(-1f));
            Assert.That(leftSteer, Is.EqualTo(-1f));
        }

        [Test]
        public void OppositeKeys_Cancel()
        {
            var (throttle, steer) = Map(w: true, s: true, a: true, d: true);
            Assert.That(throttle, Is.EqualTo(0f));
            Assert.That(steer, Is.EqualTo(0f));
        }

        [Test]
        public void Stick_BlendsWithKeyboard()
        {
            var (throttle, steer) = Map(w: true, stick: new Vector2(0.5f, 0.5f));
            Assert.That(throttle, Is.EqualTo(1f));  // 1 + 0.5 clamped to 1
            Assert.That(steer, Is.EqualTo(0.5f));
        }

        [Test]
        public void Stick_Alone_Drives()
        {
            var (throttle, steer) = Map(stick: new Vector2(1f, -1f));
            Assert.That(throttle, Is.EqualTo(-1f));
            Assert.That(steer, Is.EqualTo(1f));
        }

        [Test]
        public void Values_AreClamped()
        {
            var (throttle, steer) = Map(w: true, a: true, stick: new Vector2(1f, 1f));
            Assert.That(throttle, Is.EqualTo(1f)); // 1 + 1 clamped to 1
            Assert.That(steer, Is.EqualTo(0f));    // -1 + 1 = 0
        }
    }
}
