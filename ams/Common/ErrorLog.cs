﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO; 
namespace ams.Common
{
    public class ErrorLog
    {
        protected static string strLogFilePath = string.Empty;
        private static StreamWriter sw = null;
        private const string LINE = "_____________________________________________________________________________________________";


        /// <summary>
        /// Setting LogFile path. If the logfile path is null then it will update error info into LogFile.txt under
        /// application directory.
        /// </summary>
        public static string LogFilePath
        {
            set
            {
                strLogFilePath = value;
            }
            get
            {
                return strLogFilePath;
            }
        }
        /// <summary>
        /// Write error log entry for window event if the bLogType is true. Otherwise, write the log entry to
        /// customized text-based text file
        /// </summary>
        /// <param name="bLogType"></param>
        /// <param name="objException"></param>
        /// <returns>false if the problem persists</returns>
        public static bool Write(Exception objException)
        {
            try
            {
                if (true != CustomErrorRoutine(objException))
                    return false;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static void Write(System.Windows.Forms.Form form,Exception objException, string strCaption)
        {
            try
            {
                CustomErrorRoutine(objException);
                System.Windows.Forms.MessageBox.Show(form,objException.Message,strCaption);
               
            }
            catch (Exception)
            {
                //return false;
            }
        }

        public static void Write(System.Windows.Forms.Form form, Exception objException)
        {
            try
            {
                CustomErrorRoutine(objException);
                System.Windows.Forms.MessageBox.Show(form, objException.Message, "错误提示",System.Windows.Forms.MessageBoxButtons.OK,System.Windows.Forms.MessageBoxIcon.Error);

            }
            catch (Exception)
            {
                //return false;
            }
        }
        /// <summary>
        /// If the LogFile path is empty then, it will write the log entry to LogFile.txt under application directory.
        /// If the LogFile.txt is not availble it will create it
        /// If the Log File path is not empty but the file is not availble it will create it.
        /// </summary>
        /// <param name="objException"></param>
        /// <returns>false if the problem persists</returns>
        private static bool CustomErrorRoutine(Exception objException)
        {
            string strPathName = string.Empty;
            if (strLogFilePath.Equals(string.Empty))
            {
                //Get Default log file path "LogFile.txt"
                strPathName = GetLogFilePath();
            }
            else
            {

                //If the log file path is not empty but the file is not available it will create it
                if (false == File.Exists(strLogFilePath))
                {
                    if (false == CheckDirectory(strLogFilePath))
                        return false;

                    FileStream fs = new FileStream(strLogFilePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                    fs.Close();
                }
                strPathName = strLogFilePath;

            }

            bool bReturn = true;
            // write the error log to that text file
            if (true != WriteErrorLog(strPathName, objException))
            {
                bReturn = false;
            }
            return bReturn;
        }
        /// <summary>
        /// Write Source,method,date,time,computer,error and stack trace information to the text file
        /// </summary>
        /// <param name="strPathName"></param>
        /// <param name="objException"></param>
        /// <returns>false if the problem persists</returns>
        private static bool WriteErrorLog(string strPathName, Exception objException)
        {
            bool bReturn = false;
            string strException = string.Empty;
            try
            {
                sw = new StreamWriter(strPathName, true);
                sw.WriteLine(LINE);                
                sw.WriteLine("Timestamp       : "+DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));
                sw.WriteLine("Data            : " + objException.Data);
                sw.WriteLine("HelpLink        : " + objException.HelpLink);
                if(objException.InnerException != null)
                    sw.WriteLine("InnerException  : " + objException.InnerException.ToString());
                sw.WriteLine("Message		: " + objException.Message.ToString().Trim());
                sw.WriteLine("Source		: " + objException.Source.ToString().Trim());
                sw.WriteLine("Stack Trace	: " + objException.StackTrace.ToString().Trim());
                sw.WriteLine("TargetSite	: " + objException.TargetSite.Name.ToString());
                sw.WriteLine(LINE);
                sw.Flush();
                sw.Close();
                bReturn = true;
            }
            catch (Exception ex)
            {
                string str = ex.Message;
                bReturn = false;
            }
            return bReturn;
        }
        /// <summary>
        /// Check the log file in applcation directory. If it is not available, creae it
        /// </summary>
        /// <returns>Log file path</returns>
        private static string GetLogFilePath()
        {
            try
            {
                // get the base directory
                string baseDir = AppDomain.CurrentDomain.BaseDirectory + AppDomain.CurrentDomain.RelativeSearchPath;

                // search the file below the current directory
                string retFilePath = baseDir + "//" + "Log.txt";

                // if exists, return the path
                if (File.Exists(retFilePath) == true)
                    return retFilePath;
                //create a text file
                else
                {
                    if (false == CheckDirectory(retFilePath))
                        return string.Empty;

                    FileStream fs = new FileStream(retFilePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                    fs.Close();
                }

                return retFilePath;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
        /// <summary>
        /// Create a directory if not exists
        /// </summary>
        /// <param name="strLogPath"></param>
        /// <returns></returns>
        private static bool CheckDirectory(string strLogPath)
        {
            try
            {
                int nFindSlashPos = strLogPath.Trim().LastIndexOf("\\");
                string strDirectoryname = strLogPath.Trim().Substring(0, nFindSlashPos);

                if (false == Directory.Exists(strDirectoryname))
                    Directory.CreateDirectory(strDirectoryname);

                return true;
            }
            catch (Exception)
            {
                return false;

            }
        }

        private static string GetApplicationPath()
        {
            try
            {
                string strBaseDirectory = AppDomain.CurrentDomain.BaseDirectory.ToString();
                int nFirstSlashPos = strBaseDirectory.LastIndexOf("\\");
                string strTemp = string.Empty;

                if (0 < nFirstSlashPos)
                    strTemp = strBaseDirectory.Substring(0, nFirstSlashPos);

                int nSecondSlashPos = strTemp.LastIndexOf("\\");
                string strTempAppPath = string.Empty;
                if (0 < nSecondSlashPos)
                    strTempAppPath = strTemp.Substring(0, nSecondSlashPos);

                string strAppPath = strTempAppPath.Replace("bin", "");
                return strAppPath;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
    }
}
