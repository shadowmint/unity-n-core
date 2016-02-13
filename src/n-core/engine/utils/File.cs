using System;
using System.IO;
using System.Collections.Generic;

namespace N
{
    public class File
    {
        /// Set of pending paths to process
        private class PendingPathSet
        {
            private int max;
            private int count = 0;
            private Queue<string> pending = new Queue<string>();

            /// Create a new queue with a max count
            public PendingPathSet(int max)
            {
                this.max = max;
                this.count = 0;
            }

            /// Push an item
            public void Push(string target)
            { pending.Enqueue(target); }

            /// Still items to process?
            public bool Any { get { return (pending.Count > 0) && (count < max); } }

            /// Return the next path to process
            public string Next { get { return pending.Dequeue(); } }

            /// Increment the count manually
            public void Accept() { count += 1; }
        }

        /// Recursively iterate over all files
        public static IEnumerable<string> EnumerateFiles(string path, bool includeFiles = false, bool includeDirs = false, bool recurse = true, int limitMaxCount = 100)
        {
            var request = new PendingPathSet(limitMaxCount);
            ProcessDirectory(path, request);
            while (request.Any)
            {
                var next = request.Next;
                if(System.IO.File.Exists(next))
                {
                    if (includeFiles)
                    {
                        request.Accept();
                        yield return next;
                    }
                }
                else if(Directory.Exists(next))
                {
                    if (includeDirs)
                    {
                        request.Accept();
                        yield return next;
                    }
                    if ((next != ".") && (recurse))
                    {
                        ProcessDirectory(next, request);
                    }
                }
            }
        }

        /// Process all files in the directory passed in, recurse on any directories
        /// that are found, and process the files they contain.
        private static void ProcessDirectory(string targetDirectory, PendingPathSet request)
        {
            // Process the list of files found in the directory.
            string[] fileEntries = Directory.GetFiles(targetDirectory);
            foreach(string fileName in fileEntries)
            {
               request.Push(fileName);
            }

            // Recurse into subdirectories of this directory.
            string[] subdirectoryEntries = Directory.GetDirectories(targetDirectory);
            foreach(string subdirectory in subdirectoryEntries) {
             request.Push(subdirectory); }
        }
    }
}
