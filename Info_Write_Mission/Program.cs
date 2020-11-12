using FinchAPI;
using System;
using System.Threading;
using System.Runtime;
using System.Dynamic;

namespace Alarm_System
{
    class Program
    {
        private static (int[], int[]) sensorData;

        // *************************************************************
        // Application:     Finch Starter Solution
        // Author:          Velis, John E
        // Description:
        // Date Created:    5/20/2016
        // Date Revised:    
        // *************************************************************
        //Application Additions Talent Show, Sensors, and a few others
        //Author: Sam G.
        //Description
        //Date Edited addition.10/1/2020
        //***************************************************************
        static void Main(string[] args)
        {
            //
            // create a new Finch object
            //
            Finch myFinch;
            myFinch = new Finch();
            bool endNow = false;
            string caseSwitch;

            //
            // call the connect method
            //
            TestFinchConnection(myFinch);

            //
            // begin your code
            //

            //color change test
            myFinch.setLED(0, 0, 255);
            myFinch.wait(5000);
            myFinch.setLED(0, 255, 0);
            myFinch.wait(5000);
            myFinch.setLED(255, 0, 0);
            //run set up and explanation functions
            SetUpDisplay();
            DisplayWelcomeMessage();
            do
            {
                DisplayMainMenu();
                caseSwitch = GatherInput();
                Console.WriteLine($"Option {caseSwitch} was selected.");
                Console.ReadKey();
                switch (caseSwitch)
                {
                    case "a":
                        Console.Clear();
                        TalentShow(myFinch);
                        break;
                    case "b":
                        Console.Clear();
                        AngryFinch(myFinch);
                        Console.Clear();
                        break;
                    case "c":
                        Console.Clear();
                        DisplayDataRecorder(myFinch);
                        break;
                    case "d":
                        Console.Clear();
                        DisplayLightDataRecorder(myFinch);
                        break;
                    case "e":
                        Console.Clear();
                        AlarmSystemAlert(myFinch);
                        break;
                    case "f":
                        Console.Clear();
                        UserProgrammingDisplayMainMenuScreen(myFinch);
                        break;
                    case "quit":
                        Console.WriteLine("Thank you for using our robot!");
                        Thread.Sleep(5000);
                        endNow = true;
                        break;
                }
            } while (!endNow);
            #region testCode
            /**myFinch.setLED(0, 0, 255);
            myFinch.wait(1000);
            myFinch.setLED(0, 255, 0);
            myFinch.wait(1000);*/

            /**for (int i = 0; i<5; i++)
            {
                myFinch.setLED(0, 0, 255);
                myFinch.wait(100);
                myFinch.setLED(0, 255, 0);
                myFinch.wait(100);
                myFinch.setLED(255, 0, 0);
                myFinch.wait(100);
            }
            
            for (int  i = 0; i<255; i++)
            {
                myFinch.setLED(0, 0, i);
                myFinch.wait(100);
                myFinch.setLED(i, 0, 0);
                myFinch.wait(100);
            }

            for (int i = 255; i >0; i--)
            {
                myFinch.setLED(0, 0, i);
                myFinch.wait(100);
                myFinch.setLED(i, 0, 0);
                myFinch.wait(100);
            }
            
            for (int i = 0; i < 5; i++)
            {
                myFinch.noteOn(261);
                myFinch.wait(1000);
                myFinch.noteOff();
                myFinch.wait(100);
            }
            myFinch.setMotors(-255, 255);
            myFinch.wait(10000);
            */
            #endregion
            //
            //end of your code
            //

            //
            // call the disconnect method
            //
            myFinch.disConnect();
        }
        #region VOICE CONTROLLER TEST




        #endregion

        #region USER PROGRAMMING

        //
        //USER PROGRAMMING REGION
        //
        public enum Command
        {
            MOVEFORWARD,
            MOVEBACKWARD,
            STOP,
            WAIT,
            LEDON,
            LEDOFF,
            STOPMOTORS,
            TURNRIGHT,
            TURNLEFT,
            SCREAM,
            GETTEMPERATURE,
            DONE
        }

        private static void UserProgrammingDisplayMainMenuScreen(Finch myFinch)
        {
            (int motorSpeed, int ledBrightness, double waitSeconds) commandPrompt;
            commandPrompt = (120, 60, 5);

            Command command = new Command();
            string optionSelect = null;
            string[] listOfCommands = null;
            bool quitMenu = false;
            Console.WriteLine("Opening option menu.");
            myFinch.wait(5000);
            Console.Clear();
            DisplayMenuUsuerProgramming();
            do
            {
                optionSelect = Console.ReadLine();
                switch (optionSelect)
                {
                    case "a":
                        Console.Clear();
                        commandPrompt = UserProgrammingDisplayGetCommandPrompt();
                        break;
                    case "b":
                        Console.Clear();
                        listOfCommands = EnterCommandSequence();
                        break;
                    case "c":
                        DisplayAllCommands(listOfCommands);
                        break;
                    case "d":
                        RunCommands(listOfCommands, commandPrompt, myFinch);
                        break;
                    case "quit":
                        Console.WriteLine("Heading back to main menu!");
                        myFinch.noteOn(500);
                        myFinch.wait(2000);
                        myFinch.noteOff();
                        quitMenu = true;
                        break;
                }
            } while (!quitMenu);
        }

        private static void RunCommands(string[] listOfCommands, (int motorSpeed, int ledBrightness, double waitSeconds) commandPrompt, Finch myFinch)
        {
            int i = 0;
            int motorSetting = commandPrompt.motorSpeed;
            int ledBright = commandPrompt.ledBrightness;
            double waitTime = commandPrompt.waitSeconds;
            do
            {
                switch (listOfCommands[i])
                {
                    case "MOVEFORWARD":
                        myFinch.setMotors(motorSetting, motorSetting);
                        break;
                    case "MOVEBACKWARD":
                        myFinch.setMotors(-motorSetting, -motorSetting);
                        break;
                    case "STOP":
                        myFinch.noteOff();
                        break;
                    case "WAIT":
                        myFinch.wait((int)(waitTime * 1000));
                        break;
                    case "LEDON":
                        myFinch.setLED(ledBright, 0, 0);
                        break;
                    case "LEDOFF":
                        myFinch.setLED(0, 0, 0);
                        break;
                    case "STOPMOTORS":
                        myFinch.setMotors(0, 0);
                        break;
                    case "TURNLEFT":
                        myFinch.setMotors(-motorSetting, motorSetting);
                        break;
                    case "TURNRIGHT":
                        myFinch.setMotors(motorSetting, -motorSetting);
                        break;
                    case "SCREAM":
                        myFinch.noteOn(2000);
                        break;
                    case "GETTEMPERATURE":
                        Console.WriteLine($"{myFinch.getTemperature()} is the current temperature in Celsius.");
                        break;
                }
            } while (i < listOfCommands.Length);
        }

        private static void DisplayAllCommands(string[] listOfCommands)
        {
            DisplayHeader("Display Commands");
            for (int i = 0; i < listOfCommands.Length; i++)
            {
                Console.WriteLine($"Command number {i + 1} is: {listOfCommands[i]}");
            }
            Console.WriteLine($"Total Commands {listOfCommands.Length}");
        }

        private static string[] EnterCommandSequence()
        {
            bool goOn = false;
            string[] commands;
            commands = null;
            int i = 0;
            Console.WriteLine("Please enter in the command sections.");
            Console.WriteLine("Here are the commnads below you can pick");
            foreach (string name in Enum.GetNames(typeof(Command)))
            {
                Console.WriteLine($"Command Number {i}:{name}");
            }
            Console.WriteLine("Enter in the commands until you are finished.");
            do
            {
                string userPrompt = Console.ReadLine().ToUpper();
                if (Enum.IsDefined(typeof(Command), userPrompt))
                {
                    if (userPrompt == "DONE")
                    {
                        goOn = true;
                    }
                    else
                    {
                        commands.SetValue(userPrompt, i);
                        i++;
                    }
                }
            } while (!goOn);
            return commands;
        }

        static (int motorSpeed, int ledBrightness, double waitSeconds) UserProgrammingDisplayGetCommandPrompt()
        {
            (int motorSpeed, int ledBrightness, double waitSeconds) commandPrompt;
            DisplayHeader("Command Parameters");
            Console.Write("\tEnter in Motor Speed.");
            int.TryParse(Console.ReadLine(), out commandPrompt.motorSpeed);
            Console.Write("\tEnter in LED brightness.");
            int.TryParse(Console.ReadLine(), out commandPrompt.ledBrightness);
            Console.Write("\tEnter in the amount of time you wish the robot to wait.");
            double.TryParse(Console.ReadLine(), out commandPrompt.waitSeconds);
            return commandPrompt;
        }

        private static void DisplayMenuUsuerProgramming()
        {
            DisplayHeader("Main Menu: User Interface.");
            Console.WriteLine("a) Enter in motor speed, LED brightness, and wait time in seconds.");
            Console.WriteLine("If option a not selected then it will be defaulted to 120, 60, 5.");
            Console.WriteLine("b) Enter in command lists to use.");
            Console.WriteLine("c) Display all the commands you have entered.");
        }
        #endregion
        #region ALARM SYSTEM
        private static void AlarmSystemAlert(Finch myFinch)
        {
            string optionSelect = null;
            bool sentryMode = true;
            int sentrySec = 0;
            int sentryFrequency = 0;
            int userChoice = 0;
            int minMax = 0;
            int[] dataAmountSentryTakes = new int[userChoice];
            Console.WriteLine("Alarm System being prepped for arming.");
            myFinch.wait(5000);
            do
            {
                Console.Clear();
                DisplayAlarm();
                optionSelect = Console.ReadLine().ToLower();
                switch (optionSelect)
                {
                    case "a":
                        sentrySec = SentryTimerSettings();
                        Console.WriteLine($"{sentrySec} of how many seconds there are.");
                        Console.ReadKey();
                        break;
                    case "b":
                        sentryFrequency = SentryDataTakenFrequencySettings();
                        Console.WriteLine($"{sentryFrequency} of how many datapoints will be taken there are.");
                        Console.ReadKey();
                        break;
                    case "c":
                        userChoice = SentryDataTakenAmount();
                        Console.WriteLine($"{userChoice} how long the list will be.");
                        break;
                    case "d":
                        minMax = SentryAlarmAlert();
                        break;
                    case "e":
                        if (sentrySec <= 0 || sentryFrequency <= 0 || userChoice <= 0)
                        {
                            Console.WriteLine("Error you have entered in a null amount, sentry mode cannot be zero.");
                        }
                        else
                        {
                            (int[], int[]) sensorData = ExecuteSentryMode(sentrySec, sentryFrequency, dataAmountSentryTakes, myFinch, minMax);
                        }
                        break;
                    case "quit":
                        Console.WriteLine("Sentry Mode: Disengaging!");
                        myFinch.noteOn(500);
                        myFinch.wait(2000);
                        myFinch.noteOff();
                        sentryMode = false;
                        break;
                }
            } while (sentryMode);
        }

        private static int SentryAlarmAlert()
        {
            int i;
            Console.WriteLine("Enter in the alert range for the sensors.");
            int.TryParse(Console.ReadLine(), out i);
            return i;
        }

        private static (int[], int[]) ExecuteSentryMode(int sentrySec, int sentryFrequency, int[] dataAmountSentryTakes, Finch myFinch, int minMax)
        {
            bool finished = false;
            int[] dataAmountSentryTakesRight = new int[dataAmountSentryTakes.Length];
            (int[], int[]) sensoryData;

            DisplayHeader("Sentry Mode.");
            Console.WriteLine("sentry mode will begin, press any key to continue.");
            Console.ReadKey();
            do
            {
                int i = 0;
                if (i == sentryFrequency)
                {
                    finished = true;
                }
                else
                {
                    dataAmountSentryTakes[i] = myFinch.getLeftLightSensor();
                    dataAmountSentryTakesRight[i] = myFinch.getRightLightSensor();
                    myFinch.wait(sentrySec * 1000);
                    i++;
                }

            } while (!finished);
            sensoryData = (dataAmountSentryTakes, dataAmountSentryTakesRight);
            return sensoryData;
        }


        //
        //This method will return the size of the list that will be generated.
        //
        private static int SentryDataTakenAmount()
        {
            bool trueStatement = false;
            int listSize = 0;
            Console.Clear();
            DisplayHeader("\t\tData Amount Taken.");
            do
            {
                Console.WriteLine();
                Console.WriteLine("Enter in the amount of data you wish to gather.");
                Console.WriteLine();
                int.TryParse(Console.ReadLine(), out listSize);
                if (int.TryParse(Console.ReadLine(), out listSize) == false)
                {
                    Console.WriteLine("Error: Please try again.");
                }
                if (int.TryParse(Console.ReadLine(), out listSize) == true)
                {
                    trueStatement = true;
                }
            } while (!trueStatement);
            if (listSize <= 3)
            {
                Console.WriteLine("Setting list size to default value of 3.");
                listSize = 3;
            }
            Console.WriteLine($"List is {listSize} items long.");
            return listSize;
        }

        //
        //This will create the amount of time that people wish entered.
        //
        private static int SentryTimerSettings()
        {
            int sentrySec = 0;
            Console.WriteLine("Please select the number of seconds you would like the sentry to be operational.");
            int.TryParse(Console.ReadLine(), out sentrySec);
            return sentrySec;
        }


        //
        /// <summary>
        /// this will create the frequency of the data taken.
        /// </summary>
        /// <returns>Sentry Frequency</returns>
        private static int SentryDataTakenFrequencySettings()
        {
            int sentryFreq = 0;
            Console.WriteLine("Please select the number of data points you would like the sentry to take!");
            int.TryParse(Console.ReadLine(), out sentryFreq);
            return sentryFreq;
        }
        #endregion

        private static void TestFinchConnection(Finch myFinch)
        {
            if (myFinch.connect() == true)
            {
                Console.WriteLine("Connecting");
                for (int i = 0; i < 5; i++)
                {
                    Console.WriteLine(".");
                    Thread.Sleep(1000);
                }
            }
            else
            {
                do
                {
                    {
                        Console.WriteLine("Wait a minute...");
                        Thread.Sleep(2000);
                        Console.WriteLine("Why am I not awake?");
                        Console.WriteLine("Please check connection plz then hit any key to continue my boot up.");
                        Console.ReadKey();
                    }
                } while (!myFinch.connect());
            }

        }
        #region RANDOM FUNCTIONS
        //
        //Finch is angry
        //
        private static void AngryFinch(Finch myFinch)
        {
            myFinch.setLED(255, 0, 0);
            myFinch.noteOn(500);
            myFinch.wait(1000);
            myFinch.noteOff();
            myFinch.setLED(0, 0, 255);
        }

        private static void dataRecorder(Finch myFinch)
        {

        }


        #endregion


        #region TALENT SHOW
        //
        //Talent Show menu selection
        //
        private static void TalentShow(Finch myFinch)
        {
            string caseSwitch;
            bool endNowSequal;
            do
            {
                endNowSequal = false;
                DisplayTalentShow();
                Console.WriteLine("Please select an option.");
                caseSwitch = Console.ReadLine();
                switch (caseSwitch)
                {
                    case "a":
                        RoboticSinging(myFinch);
                        break;
                    case "b":
                        RoboticDancing(myFinch);
                        break;
                    case "c":
                        RoboticSCREAMING(myFinch);
                        break;
                    case "d":
                        RobotLightShow(myFinch);
                        break;
                    case "quit":
                        endNowSequal = true;
                        break;
                }
            } while (!endNowSequal);
        }
        //
        //Light show where the robot switches between the colors after glowing them brighter and brighter
        //
        private static void RobotLightShow(Finch myFinch)
        {
            myFinch.setLED(0, 0, 0);
            for (int i = 0; i < 255;)
            {
                i += 10;
                if (i > 255)
                {
                    i = 255;
                }
                myFinch.setLED(0, i, 0);
                myFinch.wait(1000);
                myFinch.setLED(i, 0, 0);
                myFinch.wait(1000);
                myFinch.setLED(0, 0, i);
                myFinch.wait(1000);
            }
        }
        //
        //Robot begins playing a series of notes
        //
        private static void RoboticSinging(Finch myFinch)
        {
            myFinch.setLED(255, 0, 0);
            myFinch.noteOn(233);
            myFinch.wait(2000);
            myFinch.noteOff();
            myFinch.wait(10);
            myFinch.noteOn(233);
            myFinch.wait(2000);
            myFinch.noteOff();
        }

        //
        //Finch begins dancing around a simple series of movements
        //
        private static void RoboticDancing(Finch myFinch)
        {
            for (int i = 0; i < 5; i++)
            {
                myFinch.setMotors(255, -255);
                myFinch.wait(1000);
                myFinch.setMotors(100, 100);
                myFinch.wait(3000);
                myFinch.setMotors(-255, -255);
                myFinch.wait(6000);
                myFinch.setMotors(255, -255);
                myFinch.wait(4000);
                myFinch.setMotors(-255, -255);
                myFinch.wait(2000);
                myFinch.setMotors(0, 0);
            }
        }

        //
        //Finch has no mind and must scream
        //
        private static void RoboticSCREAMING(Finch myFinch)
        {
            bool levelTrue = false;
            do
            {
                for (int i = 0; i <= 255; i++)
                {
                    myFinch.noteOn(1500 + i);
                    myFinch.setLED(0, 0 + i, 0);
                    if (i == 255)
                    {
                        levelTrue = true;
                        myFinch.noteOff();
                    }
                }
            }
            while (!levelTrue);
            myFinch.noteOff();
        }
        #endregion


        #region LIGHT DATA RECORDER
        private static void DisplayLightDataRecorder(Finch myFinch)
        {
            string caseSwitch;
            bool endNowData = false;
            double[] leftLightData, rightLightData, averageLightData;
            int numberOfDataPoints = 0;
            double frequencyOfDataPointsSeconds = 0;
            Tuple<double[], double[]> lightData = null;
            leftLightData = null;
            rightLightData = null;
            averageLightData = null;
            do
            {
                DisplayHeader("Data Recorder Menu");


                Console.WriteLine("\ta) Get number of data points.");
                Console.WriteLine("\tb) Get the freuquency of data points");
                Console.WriteLine("\tc) Get Light Measurements of data points.");
                Console.WriteLine("\td) Display Table of Light");
                Console.WriteLine("\te) ");
                Console.WriteLine("\t\tMan Menu");
                Console.WriteLine("Please select an option.");
                caseSwitch = Console.ReadLine();
                switch (caseSwitch)
                {
                    case "a":
                        numberOfDataPoints = LightDataRecorderDisplayGetNumberOfDataPoints();
                        break;

                    case "b":
                        frequencyOfDataPointsSeconds = LightDataRecorderDisplayGetFrequencyOfDataPoints();
                        break;

                    case "c":
                        if (numberOfDataPoints == 0 || frequencyOfDataPointsSeconds == 0)
                        {
                            Console.WriteLine();
                            Console.WriteLine("Please enter the number and frequency");
                        }
                        else
                        {
                            lightData = LightDataRecorderDisplayGetFrequencyOfDataSet(numberOfDataPoints, frequencyOfDataPointsSeconds, myFinch);
                        }
                        leftLightData = lightData.Item1;
                        rightLightData = lightData.Item2;
                        averageLightData = null;
                        for (int i = 0; i < leftLightData.Length; i++)
                        {
                            averageLightData[i] = (lightData.Item1[i] + lightData.Item2[i]) / 2;
                        }
                        break;
                    case "d":
                        LightDataRecorderDisplayGetFrequencyOfDataSet(leftLightData, rightLightData, averageLightData);
                        break;

                    case "quit":
                        Console.WriteLine("Back to main menu.");
                        endNowData = true;
                        break;
                }
            } while (!endNowData);
        }



        //Data Recorder >Display data table
        static void LightDataRecorderDisplayDataTable(double[] leftLightData, double[] rightLightData, double[] averageLightData)
        {
            Console.WriteLine(
                "Reading #".PadLeft(15) +
                "Light Sensor Left".PadLeft(15) + "Light Sensor Right".PadLeft(15) +
                "Average: ".PadLeft(15)
        );
            for (int i = 0; i < leftLightData.Length; i++)
            {
                Console.WriteLine(
                    (i + 1).ToString().PadLeft(15) +
                   leftLightData[i].ToString("n2").PadLeft(15) + rightLightData[i].ToString("n2").PadLeft(15) +
                   averageLightData[i].ToString("n2").PadLeft(15)
                    );
            }
        }
        static void LightDataRecorderDisplayGetFrequencyOfDataSet(double[] leftLightData, double[] rightLightData, double[] averageLightData)
        {
            DisplayHeader("Data Set");

            LightDataRecorderDisplayDataTable(leftLightData, rightLightData, averageLightData);
        }



        /// <summary>
        /// Data Recorder > Get the Data Points
        /// </summary>
        /// <param name="numberOfDataPoints"></param>
        /// <param name="frequencyOfDataPointsSeconds"></param>
        /// <param name="myFinch"></param>
        /// <returns></returns>
        static Tuple<double[], double[]> LightDataRecorderDisplayGetFrequencyOfDataSet(int numberOfDataPoints, double frequencyOfDataPointsSeconds, Finch myFinch)
        {
            double[] lightReadingRight = new double[numberOfDataPoints];
            double[] lightReadingLeft = new double[numberOfDataPoints];
            DisplayHeader("Get Data Set");

            Console.WriteLine($"\tNumber of Data Points: {numberOfDataPoints}");
            Console.WriteLine($"\tFrequency of Data Points: {frequencyOfDataPointsSeconds}");
            Console.WriteLine();

            Console.WriteLine("\tFinch robot is ready to record light measurement data.");
            Console.WriteLine("\tPress any key to begin.");
            Console.ReadKey();

            double lightLeft, lightRight;
            int waitVariable;
            for (int index = 0; index < numberOfDataPoints; index++)
            {
                lightLeft = myFinch.getLeftLightSensor();
                lightRight = myFinch.getRightLightSensor();

                Console.WriteLine($"Left light reading at {index + 1}: {lightLeft}");
                Console.WriteLine($"Right light reading at {index + 1}: {lightRight}");
                lightReadingLeft[index] = lightLeft;
                lightReadingRight[index] = lightRight;
                waitVariable = (int)(frequencyOfDataPointsSeconds * 1000);
                myFinch.wait(waitVariable);
            }
            Tuple<double[], double[]> lightReadingVarable = new Tuple<double[], double[]>(lightReadingLeft, lightReadingRight);
            return lightReadingVarable;
        }

        /// <summary>
        /// Data Recorder > Get the Frequency of Data Points
        /// </summary>
        /// <returns></returns>
        static double LightDataRecorderDisplayGetFrequencyOfDataPoints()
        {
            double frequencyOfDataPoints;

            DisplayHeader("Frequency of Data Points");

            Console.Write("Enter the Number of Data Points");

            double.TryParse(Console.ReadLine(), out frequencyOfDataPoints);
            Console.WriteLine();
            Console.WriteLine($"\tNumber of Data Points: {frequencyOfDataPoints}");
            Console.ReadKey();


            return frequencyOfDataPoints;
        }

        /// <summary>
        /// Data Recorder > Get the Number of Data Points
        /// </summary>
        /// <returns></returns>
        static int LightDataRecorderDisplayGetNumberOfDataPoints()
        {
            int numberOfDataPoints;

            DisplayHeader("Number Of Data Points");

            Console.Write("Enter the Number of Data Points");

            int.TryParse(Console.ReadLine(), out numberOfDataPoints);
            Console.WriteLine();
            Console.WriteLine($"Number of Data Points: {numberOfDataPoints}");
            Console.ReadKey();

            return numberOfDataPoints;
        }

        #endregion



        #region DATA RECORDER
        private static void DisplayDataRecorder(Finch myFinch)
        {
            string caseSwitch;
            bool endNowData = false;

            int numberOfDataPoints = 0;
            double frequencyOfDataPointsSeconds = 0;
            double[] temperaturesC = null;

            do
            {
                DisplayHeader("Data Recorder Menu");


                Console.WriteLine("\ta) Get number of data points.");
                Console.WriteLine("\tb) Get the freuquency of data points");
                Console.WriteLine("\tc) Get of data points.");
                Console.WriteLine("\td) Display data table.");
                Console.WriteLine("\te)");
                Console.WriteLine("\t\tMan Menu");
                Console.WriteLine("Please select an option.");
                caseSwitch = Console.ReadLine();
                switch (caseSwitch)
                {
                    case "a":
                        numberOfDataPoints = DataRecorderDisplayGetNumberOfDataPoints();
                        break;

                    case "b":
                        frequencyOfDataPointsSeconds = DataRecorderDisplayGetFrequencyOfDataPoints();
                        break;

                    case "c":
                        if (numberOfDataPoints == 0 || frequencyOfDataPointsSeconds == 0)
                        {
                            Console.WriteLine();
                            Console.WriteLine("Please enter the number and frequency");
                        }
                        else
                        {
                            temperaturesC = DataRecorderDisplayGetFrequencyOfDataSet(numberOfDataPoints, frequencyOfDataPointsSeconds, myFinch);
                        }
                        break;

                    case "d":
                        DataRecorderDisplayGetFrequencyOfDataSet(temperaturesC);
                        break;

                    case "quit":
                        Console.WriteLine("Back to main menu.");
                        endNowData = true;
                        break;
                }
            } while (!endNowData);
        }



        //Data Recorder >Display data table
        static void DataRecorderDisplayDataTable(double[] temperaturesC)
        {
            Console.WriteLine(
                "Reading #".PadLeft(15) +
                "Temperature".PadLeft(15)
        );

            for (int i = 0; i < temperaturesC.Length; i++)
            {
                Console.WriteLine(
                    (i + 1).ToString().PadLeft(15) +
                    temperaturesC[i].ToString("n2").PadLeft(15)
                    );
            }
        }
        static void DataRecorderDisplayGetFrequencyOfDataSet(double[] temperaturesC)
        {
            DisplayHeader("Data Set");
            DataRecorderDisplayDataTable(temperaturesC);
        }



        /// <summary>
        /// Data Recorder > Get the Data Points
        /// </summary>
        /// <param name="numberOfDataPoints"></param>
        /// <param name="frequencyOfDataPointsSeconds"></param>
        /// <param name="myFinch"></param>
        /// <returns></returns>
        static double[] DataRecorderDisplayGetFrequencyOfDataSet(int numberOfDataPoints, double frequencyOfDataPointsSeconds, Finch myFinch)
        {
            double[] temperatures = new double[numberOfDataPoints];

            DisplayHeader("Get Data Set");

            Console.WriteLine($"\tNumber of Data Points: {numberOfDataPoints}");
            Console.WriteLine($"\tFrequency of Data Points: {frequencyOfDataPointsSeconds}");
            Console.WriteLine();

            Console.WriteLine("\tFinch robot is ready to record temperature data.");
            Console.WriteLine("\tPress any key to begin.");
            Console.ReadKey();

            double temperature;
            int waitVariable;
            for (int index = 0; index < numberOfDataPoints; index++)
            {
                temperature = myFinch.getTemperature();
                Console.WriteLine($"Temperature Reading {index + 1}: {temperature} C");
                temperatures[index] = temperature;
                waitVariable = (int)(frequencyOfDataPointsSeconds * 1000);
                myFinch.wait(waitVariable);
            }

            return temperatures;
        }

        /// <summary>
        /// Data Recorder > Get the Frequency of Data Points
        /// </summary>
        /// <returns></returns>
        static double DataRecorderDisplayGetFrequencyOfDataPoints()
        {
            double frequencyOfDataPoints;

            DisplayHeader("Frequency of Data Points");

            Console.Write("Enter the Number of Data Points");

            double.TryParse(Console.ReadLine(), out frequencyOfDataPoints);
            Console.WriteLine();
            Console.WriteLine($"\tNumber of Data Points: {frequencyOfDataPoints}");
            Console.ReadKey();


            return frequencyOfDataPoints;
        }

        /// <summary>
        /// Data Recorder > Get the Number of Data Points
        /// </summary>
        /// <returns></returns>
        static int DataRecorderDisplayGetNumberOfDataPoints()
        {
            int numberOfDataPoints;

            DisplayHeader("Number Of Data Points");

            Console.Write("Enter the Number of Data Points");

            int.TryParse(Console.ReadLine(), out numberOfDataPoints);
            Console.WriteLine();
            Console.WriteLine($"Number of Data Points: {numberOfDataPoints}");
            Console.ReadKey();

            return numberOfDataPoints;
        }




        #endregion

        #region DISPLAY SETUP

        //
        //Set up the Terminal Display
        //
        private static void SetUpDisplay()
        {
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WindowHeight = 50;
            Console.WindowWidth = 100;
            Console.Clear();
        }

        //
        //Create welcome message and explanation
        //
        private static void DisplayWelcomeMessage()
        {
            DisplayHeader("Finch Commandment Console.");
            Console.WriteLine("Welcome to this our simple menu here!");
            Console.WriteLine("Please take a look at the menu and sample one of our many options.");
            Console.WriteLine("Enjoy.");
            Thread.Sleep(4000);
            Console.Clear();
        }

        //
        //Function to collect user input.
        //
        private static string GatherInput()
        {
            Console.WriteLine("Please enter a response below based on the options above.");
            string a = Console.ReadLine();
            return a;

        }
        //
        //Simple display header function
        //
        private static void DisplayHeader(string displayHeader)
        {
            Console.WriteLine($"\t\t{displayHeader}");
            Console.WriteLine();
        }

        //
        //Main Menu Display
        //
        private static void DisplayMainMenu()
        {
            Console.WriteLine("Welcome here are our options currently.");
            Console.WriteLine("a. TALENT SHOW.");
            Console.WriteLine("b. Make Finch angry.");
            Console.WriteLine("c. Data Collection.");
            Console.WriteLine("d. Light Data Collection.");
            Console.WriteLine("e. Finch Sentry Mode.");
            Console.WriteLine("quit to quit.");
        }
        private static void DisplayTalentShow()
        {
            DisplayHeader("Talent Show");
            Console.WriteLine("Welcome to Talent Show.");
            Console.WriteLine("We have five options to select from.");
            Console.WriteLine("a. will get the finch to sing");
            Console.WriteLine("b. will get the finch to dance for you.");
            Console.WriteLine("c. will get the finch to scream for it has no mouth.");
            Console.WriteLine("d will get the finch to perform a lightshow.");
            Console.WriteLine("quit to quit.");
        }

        private static void DisplayAlarm()
        {
            DisplayHeader("Alarm Menu");
            Console.WriteLine("a. set timer for amount of time.");
            Console.WriteLine("b. Set frequency.");
            Console.WriteLine("c. min/max values.");
            Console.WriteLine("d. START");
            Console.WriteLine("quit to quit");
        }
        #endregion
    }
}

