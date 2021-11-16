using Microwave.Classes.Boundary;
using Microwave.Classes.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace Microwave.Test.Unit
{
    [TestFixture]
    public class BuzzerTest
    {
        private Buzzer uut;
        private IOutput output;

        [SetUp]
        public void Setup()
        {
            output = Substitute.For<IOutput>();

            uut = new Buzzer(output);
        }

        [Test]
        public void MakeSound_ZeroTimes_NoOutput()
        {
            uut.MakeSound(0);
            output.DidNotReceive().OutputLine(Arg.Any<string>());
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        public void MakeSound_XNumberOfTimes_CorrectOutput(int times)
        {
            uut.MakeSound(times);
            output.Received(times).OutputLine(Arg.Is<string>(str => str.Contains("sound")));
        }
    }
}