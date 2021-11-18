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

            PowerTube powerTube = new PowerTube(output);

            Light light = new Light(output);

            Microwave.Classes.Boundary.Timer timer = new Microwave.Classes.Boundary.Timer();

            CookController cooker = new CookController(timer, display, powerTube);

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
