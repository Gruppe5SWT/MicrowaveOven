using Microwave.Classes.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Microwave.Classes.Boundary
{
    public class Buzzer : IBuzzer
    {
        private IOutput myOutput;

        public Buzzer(IOutput output)
        {
            myOutput = output;
        }

        public void MakeSound(int times)
        {
            for (int i = 0; i < times; i++)
            {
                myOutput.OutputLine("The buzzer is making a sound");
                if (i > 0 && i < times - 1)
                    Thread.Sleep(100);
            }
        }

    }
}
