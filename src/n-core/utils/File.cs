using System;
using System.IO;
using System.Collections.Generic;

namespace N.Package.Core
{
  public class File
  {
    /// Set of pending paths to process
    private class PendingPathSet
    {
      private readonly int _max;
      private int _count;
      private readonly Queue<string> _pending = new Queue<string>();

      /// Create a new queue with a max count
      public PendingPathSet(int max)
      {
        _max = max;
        _count = 0;
      }

      /// Push an item
      public void Push(string target)
      {
        _pending.Enqueue(target);
      }

      /// Still items to process?
      public bool Any
      {
        get { return (_pending.Count > 0) && (_count < _max); }
      }

      /// Return the next path to process
      public string Next
      {
        get { return _pending.Dequeue(); }
      }

      /// Increment the count manually
      public void Accept()
      {
        _count += 1;
      }
    }

    /// Recursively iterate over all files
    public static IEnumerable<string> EnumerateFiles(string path, bool includeFiles = false, bool includeDirs = false, bool recurse = true, int limitMaxCount = 100)
    {
      var request = new PendingPathSet(limitMaxCount);
      ProcessDirectory(path, request);
      while (request.Any)
      {
        var next = request.Next;
        if (System.IO.File.Exists(next))
        {
          if (includeFiles)
          {
            request.Accept();
            yield return next;
          }
        }
        else if (Directory.Exists(next))
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
      foreach (string fileName in fileEntries)
      {
        request.Push(fileName);
      }

      // Recurse into subdirectories of this directory.
      string[] subdirectoryEntries = Directory.GetDirectories(targetDirectory);
      foreach (string subdirectory in subdirectoryEntries)
      {
        request.Push(subdirectory);
      }
    }
  }
}