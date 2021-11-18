using System;
using Microwave.Classes.Boundary;
using Microwave.Classes.Controllers;

namespace Microwave.App
{
    class Program
    {
        static void Main(string[] args) //comment hello
        {
            Button startCancelButton = new Button();
            Button powerButton = new Button();
            Button timeButton = new Button();

            Door door = new Door();

            Output output = new Output();

            Display display = new Display(output);

            int maxPower = 700;

            PowerTube powerTube = new PowerTube(output, maxPower);

            Light light = new Light(output);

            Microwave.Classes.Boundary.Timer timer = new Timer();

            CookController cooker = new CookController(timer, display, powerTube);

            UserInterface ui = new UserInterface(powerButton, timeButton, startCancelButton, door, display, light, cooker);

            // Finish the double association
            cooker.UI = ui;

            // Simulate a simple sequence --

            powerButton.Press();

            timeButton.Press();

            startCancelButton.Press();

            // The simple sequence should now run

            // Sequence to demonstrate change of maximum power on powertube
            // ------------- START ---------------
            startCancelButton.Press();

            Console.WriteLine("Sets maxpower to 100. The power will return to 50 if it would otherwise go above maxPower. Observe:");
            powerTube.MaximumPower=100;
            powerButton.Press();
            powerButton.Press();
            powerButton.Press();

            Console.WriteLine("Sets maxpower to 150. The power will return to 50 if it would otherwise go above maxPower. Observe:");
            powerTube.MaximumPower=150;
            powerButton.Press();
            powerButton.Press();
            powerButton.Press();


            // Sequence to demonstrate change of maximum power on powertube
            // ------------- END ---------------



            System.Console.WriteLine("When you press enter, the program will stop");
            // Wait for input

            System.Console.ReadLine();
        }
    }
}
