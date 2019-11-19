using System;
using System.IO;
using Storages;

namespace Filewatch
{
    public class FileWatcher
    {
        public static void Main(string[] args)
        {
            try
            {
                // Create a new FileSystemWatcher and set its properties.
                FileSystemWatcher watcher = new FileSystemWatcher();
                watcher.Path = args[0];

                // Watch both files and subdirectories.
                watcher.IncludeSubdirectories = true;

                // Watch for all changes specified in the NotifyFilters
                //enumeration.
                watcher.NotifyFilter = NotifyFilters.Attributes |
                NotifyFilters.CreationTime |
                NotifyFilters.DirectoryName |
                NotifyFilters.FileName |
                NotifyFilters.LastAccess |
                NotifyFilters.LastWrite |
                NotifyFilters.Security |
                NotifyFilters.Size;

                // Watch all files in directory specified by arg[0].
                watcher.Filter = "*.*";

                // Add event handlers.
                watcher.Changed += new FileSystemEventHandler(OnChanged);
                watcher.Renamed += new RenamedEventHandler(OnRenamed);
                watcher.EnableRaisingEvents = true;

                Console.WriteLine("Press \'q\' to stop this application.");
                Console.WriteLine();

                //Make an infinite loop till 'q' is pressed.
                while (Console.Read() != 'q') ;
            }
            catch (IOException e)
            {
                Console.WriteLine("A Exception Occurred :" + e);
            }

            catch (Exception oe)
            {
                Console.WriteLine("An Exception Occurred :" + oe);
            }
        }

        //Define the event handlers.
        public static void OnChanged(object source, FileSystemEventArgs e)
        {
            //Specify what is done when a file is changed.
            Console.WriteLine("{0}, with path {1} has been {2}", e.Name, e.FullPath, e.ChangeType);
        }

        public static void OnRenamed(object source, RenamedEventArgs e)
        {

            //Specify what is done when a file is renamed.
            if (Path.GetExtension(e.OldFullPath) == Path.GetExtension(e.FullPath))
            {
                Console.WriteLine("{0}, has been renamed to {1}. The file Extension has not been changed." + Environment.NewLine, e.OldFullPath, e.Name);
            }
            else
            {
                string OldExtension = Path.GetExtension(e.OldFullPath);
                string NewExtension = Path.GetExtension(e.FullPath);
                Console.WriteLine("The file extesion of {0}, has been changed to {1}. This might indicate malware.", e.OldFullPath, Path.GetExtension(e.FullPath));
                Storage s = new Storage();
                s.Store(NewExtension);
                s.Check(NewExtension);
            }
        }
    }
}