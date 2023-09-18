using System;
using System.IO;
using System.Timers;

class Program
{
    private static string sourceFolderName = "Source";
    private static string replicaFolderName = "Replica";
    private static string logFileName = "LogFile.log";

    private static string sourceFolderPath;
    private static string replicaFolderPath;
    private static string logFilePath;
    static int synchronizationInterval;

    static void Main(string[] args)
    {
        string currentDirectory = Environment.CurrentDirectory;

        sourceFolderPath = Path.Combine(currentDirectory, sourceFolderName);
        replicaFolderPath = Path.Combine(currentDirectory, replicaFolderName);
        logFilePath = Path.Combine(currentDirectory, logFileName);

        if (args.Length != 4)
        {
            Console.WriteLine("Usage: FolderSync <sourceFolder> <replicaFolder> <syncIntervalInSeconds> <logFilePath>");
            return;
        }

        sourceFolderPath = args[0];
        replicaFolderPath = args[1];
        synchronizationInterval = int.Parse(args[2]);
        logFilePath = args[3];

        // Create log file or append to an existing one
        using (var logFile = File.AppendText(logFilePath))
        {
            logFile.WriteLine($"[{DateTime.Now}] Program started.");
        }

        // Start periodic synchronization timer
        var timer = new System.Timers.Timer(synchronizationInterval * 1000);
        timer.Elapsed += TimerElapsed;
        timer.Start();

        Console.WriteLine("Press Enter to exit.");
        Console.ReadLine();
    }

    private static void TimerElapsed(object sender, ElapsedEventArgs e)
    {
        SynchronizeFolders(sourceFolderPath, replicaFolderPath);
    }

    private static void SynchronizeFolders(string source, string replica)
    {
        try
        {
            // Ensure the replica folder exists
            Directory.CreateDirectory(replica);

            // Get the list of files and subdirectories in the source folder
            string[] sourceEntries = Directory.GetFileSystemEntries(source);

            foreach (string sourceEntry in sourceEntries)
            {
                string entryName = Path.GetFileName(sourceEntry);
                string replicaEntryPath = Path.Combine(replica, entryName);

                if (Directory.Exists(sourceEntry))
                {
                    SynchronizeFolders(sourceEntry, replicaEntryPath);
                }
                else
                {
                    File.Copy(sourceEntry, replicaEntryPath, true); // Use "true" to overwrite if the file already exists
                    LogOperation($"Copied {entryName} from source to replica.");
                }
            }
        }
        catch (Exception ex)
        {
            LogOperation($"Error during synchronization: {ex.Message}");
        }
    }

    private static void LogOperation(string message)
    {
        using (var logFile = File.AppendText(logFilePath))
        {
            logFile.WriteLine($"[{DateTime.Now}] {message}");
        }

        Console.WriteLine($"[{DateTime.Now}] {message}");
    }
}