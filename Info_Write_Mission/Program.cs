using FinchAPI;
using System;
using System.Threading;
using System.Runtime;
using System.Dynamic;
using System.IO;
using System.Collections.Generic;
namespace Info_Write_Mission
{
    enum Command
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
    class Program
    {
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
        //Date Edited addition.12/2/2020
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
                    case "g":
                        Console.Clear();
                        UserInformationWrite(myFinch);
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
        #region USER INFORMATION WRITE
        private static void UserInformationWrite(Finch myFinch)
        {
            bool endNow = false;
            do {
            Console.Clear();
            DisplayUserInformationWriteMenu();
            string caseSwitch = GatherInput();
            switch (caseSwitch)
            {
                case "a":
                        Console.Clear();
                        WriteCilmateData(myFinch);
                        break;
                case "b":
                        Console.Clear();
                        ReadDocument();
                        break;
                case "quit":
                        Console.Clear();
                        endNow = true;
                    break;
                        
                }
        }while(!endNow);
        }

        private static void ReadDocument()
        {
            string dataPath = @"Data\TextFile1.txt";
            string fileText;
            string[] fileTextArray;

            //
            //Read Theme from Data File(Single Line)
            //
            fileText = File.ReadAllText(dataPath);

            fileTextArray = fileText.Split('.');
            for (int i = 0; i < fileTextArray.Length; i++)
            {
                Console.WriteLine(fileTextArray[i]);
            }
            Console.WriteLine("");
            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
        }
        
        private static void WriteCilmateData(Finch myFinch)
        {
            string dataPath = @"Data\TextFile1.txt";
            List<string>dataTaken = new List<string>();
            int dataPoint;
            double frequency, tempC;

            Console.WriteLine("Please enter in the amount of times you want data taken.");
            double.TryParse(Console.ReadLine(), out frequency);
            Console.WriteLine("How many data points do you want taken.");
            int.TryParse(Console.ReadLine(), out dataPoint);
            for (int i = 0; i < dataPoint; i++)
            {
                Console.Clear();
                int lightDataAvg = myFinch.getLeftLightSensor() + myFinch.getRightLightSensor() / 2;
                tempC = myFinch.getTemperature();
                string lineText = $"Data point {i+1}:  {DateTime.Now} The Temperature in Celsicus: {tempC}  The light avg: {lightDataAvg}.";
                Console.WriteLine(lineText);
                dataTaken.Add(lineText);
                myFinch.wait((int)(frequency * 1000));
            }
            string[] str = dataTaken.ToArray();
            File.WriteAllLines(dataPath, str);
        }

        private static void DisplayUserInformationWriteMenu()
        {
            DisplayHeader("Information Gathering and Retaining");
            Console.WriteLine("a)To active data collection.");
            Console.WriteLine("b) Review written data.");
            Console.WriteLine("quit to exit the program.");
        }
        #endregion
        #region USER PROGRAMMING

        //
        //USER PROGRAMMING REGION
        //

        private static void UserProgrammingDisplayMainMenuScreen(Finch myFinch)
        {
            //
            //Initalize the robotic programming varibles 
            //
            (int motorSpeed, int ledBrightness, double waitSeconds) commandPrompt;
            commandPrompt = (120, 60, 5);

            string optionSelect = null;
            List<string> listOfCommands = new List<string>();
            bool quitMenu = false;
            Console.WriteLine("Opening option menu.");
            myFinch.wait(5000);
            do
            {
                Console.Clear();
                DisplayMenuUsuerProgramming();
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
        //
        //Function to run all the commands from the list 
        //
        private static void RunCommands(List<string> listOfCommands, (int motorSpeed, int ledBrightness, double waitSeconds) commandPrompt, Finch myFinch)
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
                i++;
            } while (i < listOfCommands.Count);
        }
        //
        //Display all the commands that have been entered
        //
        private static void DisplayAllCommands(List<string> listOfCommands)
        {
            DisplayHeader("Display Commands");
            for (int i = 0; i < listOfCommands.Count; i++)
            {
                Console.WriteLine($"Command number {i + 1} is: {listOfCommands[i]}");
            }
            Console.WriteLine($"Total Commands {listOfCommands.Count}");
            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
        }
        //
        //Enter in the sequence of commands the user wishes to run
        //
        private static List<string> EnterCommandSequence()
        {
            bool goOn = false;
            List<string> commands = new List<string>();
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
                        commands.Add(userPrompt);
                        i++;
                    }
                }
            } while (!goOn);
            return commands;
        }
        //
        //Set in the parameters for the motor speed, led brightness and seconds waited between commands
        //
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
        //
        //Displaying the menu for the user as well as explaining the default system params
        //
        private static void DisplayMenuUsuerProgramming()
        {
            DisplayHeader("Main Menu: User Interface.");
            Console.WriteLine("a) Enter in motor speed, LED brightness, and wait time in seconds.");
            Console.WriteLine("If option a not selected then it will be defaulted to 120, 60, 5.");
            Console.WriteLine("b) Enter in command lists to use.");
            Console.WriteLine("c) Display all the commands you have entered.");
            Console.WriteLine("d) Begin running robot.");
        }
        #endregion
        #region ALARM SYSTEM
        //
        //This is the main protocol of the Alarm System for the Finch Robot
        //
        private static void AlarmSystemAlert(Finch myFinch)
        {
            //
            //Initialize all the variables that are required for the finch robot
            //
            string optionSelect = null;
            bool sentryMode = true;
            int sentrySec = 0;
            int sentryFrequency = 0;
            int minimum = 0;
            int maximum = 0;
            do
            {
                Console.Clear();
                DisplayAlarm();
                optionSelect = Console.ReadLine().ToLower();
                (int[], int[]) sensorData;
                switch (optionSelect)
                {
                    case "a":
                        sentrySec = SentryTimerSettings();
                        break;
                    case "b":
                        sentryFrequency = SentryDataTakenFrequencySettings();
                        sensorData.Item1 = new int[sentryFrequency];
                        sensorData.Item2 = new int[sentryFrequency];
                        break;
                    case "c":
                        minimum = SentryMinAlarmAlert();
                        maximum = SentryMaxAlarmAlert();
                        break;
                    case "d":
                        if (sentrySec <= 0 || sentryFrequency <= 0)
                        {
                            Console.WriteLine("Error you have entered in a null amount, sentry mode cannot be zero.");
                        }
                        else
                        {
                            sensorData = ExecuteSentryMode(sentrySec, sentryFrequency, myFinch, minimum, maximum);
                        }
                        break;
                    //case "e":
                      //  DisplaySentryData(sensorData);
                        //break;
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

        private static void DisplaySentryData((int[], int[]) sensorData)
        {
            for (int i = 0; i < sensorData.Item1.Length; i++)
            {
                Console.WriteLine($"Data point {i} for the left sensor is {sensorData.Item1[i]} and right is {sensorData.Item2[i]}");
            }
        }

        //
        //Set up the minimum alarm system alert
        //
        private static int SentryMinAlarmAlert()
        {
            int i;
            Console.WriteLine("Enter in the minimum alert range for the sensors.");
            int.TryParse(Console.ReadLine(), out i);
            return i;
        }
        //
        //Set the maximum range
        //
        private static int SentryMaxAlarmAlert()
        {
            int i;
            Console.WriteLine("Enter in the maximum alert range for the sensors.");
            int.TryParse(Console.ReadLine(), out i);
            return i;
        }

        //
        //This function executes the main sentry protocols
        //It will return a Tuple function which can be displayed later on
        //
        private static (int[], int[]) ExecuteSentryMode(int sentrySec, int sentryFrequency, Finch myFinch, int min, int max)
        {
            bool finished = false;
            (int[], int[]) sensoryData;
            int left, right;
            int[] dataLeft, dataRight;
            dataRight = new int[sentryFrequency];
            dataLeft = new int[sentryFrequency];
            int i = 0;
            DisplayHeader("Sentry Mode.");
            //
            //Continuation prompt
            //
            Console.WriteLine("sentry mode will begin, press any key to continue.");
            Console.ReadKey();
            //
            //Function of the sentry begins here
            //
            do
            {
                if (i == sentryFrequency)
                {
                    finished = true;
                }
                else
                {
                    //
                    //Grabbing both of the variables in order to test them to make sure they aren't setting off the alarm
                    //if correct it will put the variables into a list, if not it will spit out a warning ending the process.
                    left = myFinch.getLeftLightSensor();
                    right = myFinch.getRightLightSensor();
                    if (max >= left && left >= min && max >= right && right >= min)
                    {
                        dataLeft[i] = left;
                        dataRight[i] = right;
                        Console.WriteLine(left + " " + right + "light level of the room.");
                    }
                    else
                    {
                        Console.WriteLine("WARNING ALARM TRIPPED!");
                        finished = true;
                    }
                    myFinch.wait(sentrySec * 1000);
                }
                Console.WriteLine(i);
                i++;
            } while (!finished);
            //
            //
            //
            sensoryData.Item1 = dataLeft;
            sensoryData.Item2 = dataRight;
            return sensoryData;
        }


        //
        //This will create the amount of time that people wish between datapoints.
        //
        private static int SentryTimerSettings()
        {
            int sentrySec = 0;
            Console.WriteLine("Please select the number of seconds you would like between data points.");
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
            Console.Clear();

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
            Thread.Sleep(2000);
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
            Console.WriteLine("f. User Program.");
            Console.WriteLine("g. Write Data to Text.");
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