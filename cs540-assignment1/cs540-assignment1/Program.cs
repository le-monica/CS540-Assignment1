using System;
using System.Diagnostics;
using System.IO;

namespace cs540assignment1
{
    class Homework1
    { 
        static void SystemCall(string command)
        {
            Process process = new Process(); // Create new process to execute command args
            process.StartInfo.FileName = "/bin/bash"; // Use bash as the process to execute commands
            process.StartInfo.Arguments = $"-c \"{command}\""; // Sets command line arg
            process.StartInfo.RedirectStandardOutput = true; // Redirects output to program
            process.StartInfo.UseShellExecute = false; // Do not launch shell
            process.StartInfo.CreateNoWindow = true; // Doesn't open seperate terminal window

            process.Start(); // Starts process, executes command after it's set

            string output = process.StandardOutput.ReadToEnd(); //Reads output
            process.WaitForExit(); // Waits for exit before going to next line

            Console.WriteLine(output); // Prints output of command to program console
        }

        static void Main()
        {
            string command; 
            int choice; 

            do
            {   
                //Print out menu on the console
                Console.WriteLine("Select an option (1-6):\n" +
                    "1. List Directory contents (ls)\n" +
                    "2. Print Working Directory (pwd)\n" +
                    "3. Create New Directory (mkdir)\n" +
                    "4. Display a Message (echo)\n" +
                    "5. Concatenate and display file content (cat)\n" +
                    "6. Exit");

                Console.Write("Enter your choice: ");

                //Read in menu choice (1-6)
                if (!int.TryParse(Console.ReadLine(), out choice))
                {   
                    //Make sure user puts in valid option
                    Console.WriteLine("Invalid choice. Enter a number 1-6.");
                    continue;
                }
               Console.WriteLine();

                //Switch-Case to set command per user input (macOS terminal commands)
                switch (choice)
                {
                    case 1:
                        command = "ls";
                        break;

                    case 2:
                        command = "pwd";
                        break;

                    case 3:
                        Console.Write("Enter directory name: ");
                        string directoryName = Console.ReadLine();
                        command = $"mkdir {directoryName}";
                        break;

                    case 4:
                        Console.Write("Enter a message to display: ");
                        string message = Console.ReadLine();
                        command = $"echo {message}";
                        break;

                    case 5:
                        Console.Write("Enter a file name: ");
                        string fileName = Console.ReadLine();

                        // Check if the file exists before executing the command
                        if (!File.Exists(fileName))
                        {
                            Console.WriteLine("Error: File does not exist.\n");
                            continue;
                        }
                        command = $"cat {fileName}";
                        break;

                    case 6:
                        Console.WriteLine("Exiting program.");
                        return;

                    // Default case to handle other inputs
                    default:
                        Console.WriteLine("Invalid choice. Enter a number 1-6: ");
                        continue;
                }

                // After command is set, send to system call function
                SystemCall(command);

            } while (true);
        }
    }
}