using UnityEngine;
using System;
using System.Collections.Generic;
using N.Tests;

namespace N
{
    public class Console
    {
        /// Debugging mode for tests
        public static bool DEBUG = false;

        // Turn debug mode on
        public static void Debug()
        {
            DEBUG = true;
        }

        // Debug message even if debug mode is off
        public static void Debug(string format, params object[] args)
        {
            DEBUG = true;
            Log(format, args);
        }

        /// Clear the Console
        public static void Clear()
        {
            var logEntries = N.Reflect.Type.Resolve("UnityEditorInternal.LogEntries");
            if (logEntries)
            {
                var clearMethod = logEntries.Unwrap().GetMethod("Clear", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public);
                clearMethod.Invoke(null, null);
            }
        }

        public static void Debug(object msg)
        {
            Debug("{0}", msg);
        }

        public static void Log(string format, params object[] args)
        {
            var msg = string.Format(format, args);
            Log(msg);
        }

        public static void Log(object msg)
        {
            Console.Log("{0}", msg);
        }

        public static void Log(string msg)
        {
            if (DEBUG)
            {
                UnityEngine.Debug.Log(" ------- DEBUG ------- " + msg);
            }
            else
            {
                UnityEngine.Debug.Log(msg);
            }
        }

        public static void Error(string msg)
        {
            UnityEngine.Debug.Log(" ------- ERROR ------- " + msg);
        }

        public static void Error(Exception msg)
        {
            Error(msg.ToString());
            if (msg.StackTrace != null)
            {
                foreach (var line in msg.StackTrace.Split('\n'))
                {
                    Error(line);
                }
            }
            UnityEngine.Debug.LogException(msg);
        }

        public static void Error(string format, params object[] args)
        {
            var msg = string.Format(format, args);
            Error(msg);
        }
    }
}
