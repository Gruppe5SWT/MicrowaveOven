﻿using System;
using Microwave.Classes.Interfaces;

namespace Microwave.Classes.Boundary
{
    public class PowerTube : IPowerTube
    {
        private IOutput myOutput;

        public int MaximumPower { get; set; }

        private bool IsOn = false;

        public PowerTube(IOutput output, int maxPower)
        {
            myOutput = output;
            MaximumPower = maxPower;
        }

        public void TurnOn(int power)
        {
            if (power < 1 || MaximumPower < power)
            {
                throw new ArgumentOutOfRangeException("power", power, "Must be between 1 and 1000 (incl.)");
            }

            if (IsOn)
            {
                throw new ApplicationException("PowerTube.TurnOn: is already on");
            }

            myOutput.OutputLine($"PowerTube works with {power}");
            IsOn = true;
        }

        public void TurnOff()
        {
            if (IsOn)
            {
                myOutput.OutputLine($"PowerTube turned off");
            }

            IsOn = false;
        }
    }
}