# FolderSynchronizer
Program that synchronizes two folders: source and replica

Open CMD and navigate with "cd" to TestTask\TestTask\bin\Debug\net6.0 

Run this command: 
TestTask.exe partition\to\the\TestTask\Source\ partition\to\the\TestTask\Replica\ 60 partition\to\the\TestTask\Log\LogFile.log

After running the program, wait for at least 60 seconds (or the specified synchronization interval) to allow the timer to elapse and trigger the synchronization process. During this time, ensure that the program is running and that the console window remains open.

There will also be a Log folder and it will display all information that is needed: 
"
[9/18/2023 11:28:26 PM] Program started.
[9/18/2023 11:29:26 PM] Copied Test.txt from source to replica.
" 
