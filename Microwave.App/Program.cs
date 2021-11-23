using System;
using System.Threading;
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
            Button AddTimeButton = new Button();
            Button subtractTimeButton = new Button();

            Door door = new Door();

            Output output = new Output();

            Display display = new Display(output);

            int maxPower = 700;

            PowerTube powerTube = new PowerTube(output, maxPower);

            Light light = new Light(output);

            Microwave.Classes.Boundary.Timer timer = new Microwave.Classes.Boundary.Timer();
            Buzzer buzzer = new Buzzer(output);


            CookController cooker = new CookController(timer, display, powerTube, buzzer);

            UserInterface ui = new UserInterface(powerButton, timeButton, startCancelButton, AddTimeButton, subtractTimeButton, door, display, light, cooker);

            // Finish the double association
            cooker.UI = ui;

            // Simulate a simple sequence --

            powerButton.Press();

            timeButton.Press();

            startCancelButton.Press();
            
            System.Console.WriteLine("When you press enter, the program will stop");
            // Wait for input

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
            /*
             *  Demonstrating Feature 3: Add or remove time during cooking
             *  Works in 20 second intervals.             * 
             */
            Console.WriteLine("-------");
            Console.WriteLine("Feature 3: Adding and removing time during cooking");
            Console.WriteLine("Pressing AddtimeButton twice to add 20 seconds");
            AddTimeButton.Press();
            AddTimeButton.Press();
            Thread.Sleep(3000); //sleep a bit to see the change
            Console.WriteLine("Pressing SubtractTimeButton once to remove 20 seconds");
            subtractTimeButton.Press();




            System.Console.ReadLine();
        }
    }
}
