using System;
using System.Threading;
using NUnit.Framework;
using Timer = Microwave.Classes.Boundary.Timer;

namespace Microwave.Test.Unit
{
    [TestFixture]
    public class TimerTest
    {
        private Timer uut;

        [SetUp]
        public void Setup()
        {
            uut = new Timer();
        }

        [Test]
        public void Start_TimerTick_ShortEnough()
        {
            ManualResetEvent pause = new ManualResetEvent(false);

            uut.TimerTick += (sender, args) => pause.Set();
            uut.Start(2);

            // wait for a tick, but no longer
            Assert.That(pause.WaitOne(1100));
        }

        [Test]
        public void Start_TimerTick_LongEnough()
        {
            ManualResetEvent pause = new ManualResetEvent(false);

            uut.TimerTick += (sender, args) => pause.Set();
            uut.Start(2);

            // wait shorter than a tick, shouldn't come
            Assert.That(!pause.WaitOne(900));
        }

        [Test]
        public void Start_TimerExpires_ShortEnough()
        {
            ManualResetEvent pause = new ManualResetEvent(false);

            uut.Expired += (sender, args) => pause.Set();
            uut.Start(2);

            // wait for expiration, but not much longer, should come
            Assert.That(pause.WaitOne(2100));
        }

        [Test]
        public void Start_TimerExpires_LongEnough()
        {
            ManualResetEvent pause = new ManualResetEvent(false);

            uut.Expired += (sender, args) => pause.Set();
            uut.Start(2);

            // wait shorter than expiration, shouldn't come
            Assert.That(!pause.WaitOne(1900));
        }

        [Test]
        public void Start_TimerTick_CorrectNumber()
        {
            ManualResetEvent pause = new ManualResetEvent(false);
            int notifications = 0;

            uut.Expired += (sender, args) => pause.Set();
            uut.TimerTick += (sender, args) => notifications++;

            uut.Start(2);

            // wait longer than expiration
            Assert.That(pause.WaitOne(2100));

            Assert.That(notifications, Is.EqualTo(2));
        }

        [Test]
        public void Stop_NotStarted_NoThrow()
        {
            Assert.That( () => uut.Stop(), Throws.Nothing);
        }

        [Test]
        public void Stop_Started_NoTickTriggered()
        {
            ManualResetEvent pause = new ManualResetEvent(false);

            uut.TimerTick += (sender, args) => pause.Set();

            uut.Start(2000);
            uut.Stop();

            Assert.That(!pause.WaitOne(1100));
        }

        [Test]
        public void Stop_Started_NoExpiredTriggered()
        {
            ManualResetEvent pause = new ManualResetEvent(false);

            uut.Expired += (sender, args) => pause.Set();

            uut.Start(2000);
            uut.Stop();

            Assert.That(!pause.WaitOne(2100));
        }

        [Test]
        public void Stop_StartedOneTick_NoExpiredTriggered()
        {
            ManualResetEvent pause = new ManualResetEvent(false);
            int notifications = 0;

            uut.Expired += (sender, args) => pause.Set();
            uut.TimerTick += (sender, args) => uut.Stop();

            uut.Start(2000);

            Assert.That(!pause.WaitOne(2100));
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        public void Tick_Started_TimeRemainingCorrect(int ticks)
        {
            ManualResetEvent pause = new ManualResetEvent(false);
            int ticksGone = 0;
            uut.TimerTick += (sender, args) =>
            {
                ticksGone++;
                if (ticksGone >= ticks)
                    pause.Set();
            };
            uut.Start(5);

            // wait for ticks, only a little longer
            pause.WaitOne(ticks * 1000 + 100);

            Assert.That(uut.TimeRemaining, Is.EqualTo(5-ticks*1));
        }

        [Test]
        public void AddTime_TimeRemainingIncrease20Seconds()
        {
            var CurrentTimeRemaining = uut.TimeRemaining;

            uut.AddTime();

            Assert.That(uut.TimeRemaining.Equals(CurrentTimeRemaining+20));
        }

        [Test]
        public void SubtractTime_TimeRemainingDecrease20Seconds()
        {
            var CurrentTimeRemaining = uut.TimeRemaining;

            uut.SubtractTime();

            Assert.That(uut.TimeRemaining.Equals(CurrentTimeRemaining-20));
        }

        [TestCase(20)]
        [TestCase(19)]
        [TestCase(1)]
        [TestCase(0)]
        public void SubtractTime_TimeRemainingLessOrEqual20Sec_Expire(int timeRemaining)
        {
            //arrange counter to be incremented
            //if event "Expire" has been fired
            int isExpiredEventFired = 0;
            uut.Expired += delegate(object sender, EventArgs e)
               {
                    isExpiredEventFired++;
               };

            //enables timer & sets TimeRemaining
            uut.Start(timeRemaining);
            //subtract 20 seconds
            uut.SubtractTime();

            
            Assert.That(isExpiredEventFired.Equals(1));
        }
    }
}