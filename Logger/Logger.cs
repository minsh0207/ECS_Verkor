using System;
using System.IO;
using System.Threading;


namespace ECSLogger
{

    /// <summary>
    /// ERROR 보다 큰 값의 FLAG를 주면 별도 파일에도 남긴다
    /// </summary>
    public enum LOG_LEVEL
    {
        ALL = 0,
        INFO,
        WARN,

        SPECIAL_LOG_LEVEL_BEGIN, // 이 이후의 Log Level 은 ALL 로그도 남고, 개별 파일로도 남음
        ERROR,
        BCR,
        BIZ,
    }

    public static class Logger
    {


        #region [Log Settings]

        //
        // LogLevelSet 에 설정된 것보다 크거나 같은 것만 로그로 남는다 (하지만 거의 안 쓸듯)
        public static LOG_LEVEL LogLevelSet = LOG_LEVEL.ALL;

        //
        // 로그 파일의 루트 폴더와 기본 파일명만 여기서 정해주면 된다..
        public static string LogRoot = StaticParamClass.LOGGER_LOG_ROOT;
        public static string LogBaseFileName = StaticParamClass.LOGGER_LOG_BASEFILENAME;

        //
        // 로그 파일 이름 및 로그 기록 시 사용할 포맷
        public static string Log_DateTimeFormat_Day = StaticParamClass.LOGGER_LOG_DATETIMEFORMAT_DAY;
        public static string Log_DateTimeFormat_Sec = StaticParamClass.LOGGER_LOG_DATETIMEFORMAT_SEC;
        public static string Log_DateTimeFormat_MiliSec = StaticParamClass.LOGGER_LOG_DATETIMEFORMAT_MILISEC;

        #endregion


        /// <summary>
        /// ALL 로그에는 모두 다 남고,
        /// level >= LOG_LEVEL.ERROR 이면 해당 level 에 해당하는 파일에도 남긴다 (ALL Log 에도 물론 남김)
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public static string GetLogFileName(LOG_LEVEL level, string baseFileName=null)
        {
            string file_name;
            string folder_name;

            if (level < LOG_LEVEL.ERROR)
            {
                if(baseFileName == null)
                    file_name = string.Format("{0}_ALL.log", LogBaseFileName);
                else
                    file_name = string.Format("{0}_ALL.log", baseFileName);
                folder_name = "ALL";
            }
            else
            {
                if (baseFileName == null)
                    file_name = string.Format("{0}_{1}.log", LogBaseFileName, level.ToString());
                else
                    file_name = string.Format("{0}_{1}.log", baseFileName, level.ToString());
                folder_name = level.ToString();
            }

            string logFolderYear = System.DateTime.Now.ToString(@"yyyy");
            string logFolderMonth = System.DateTime.Now.ToString(@"MM");
            string logFolder = string.Format(@"{0}\{1}\{2}\{3}", LogRoot, logFolderYear, logFolderMonth, folder_name);

            System.IO.Directory.CreateDirectory(logFolder);
            string filepath = string.Format(@"{0}\[{1}]_{2}", logFolder, System.DateTime.Now.ToString(Log_DateTimeFormat_Day), file_name);

            return filepath;
        }

        public static void WriteLog(string log, LOG_LEVEL level = LOG_LEVEL.ALL, string baseFileName = null)
        {

            string line = string.Format("{0}, {1}, {2}",
                System.DateTime.Now.ToString(Log_DateTimeFormat_MiliSec),
                level,
                log);

#if DEBUG
            string caller = (new System.Diagnostics.StackTrace()).GetFrame(1).GetMethod().Name;
            System.Console.WriteLine(caller + ":" + line);
#endif



            // check logging level...
            if (level < LogLevelSet)
            {
                return;
            }



            try
            {
                if (Monitor.TryEnter(_lock, TimeSpan.FromSeconds(1)))
                {
                    try
                    {
                        // leave all log
                        string path = GetLogFileName(LOG_LEVEL.ALL, baseFileName);
                        using (TextWriter tw = TextWriter.Synchronized(File.AppendText(path)))
                        {
                            tw.WriteLine(line);
                            tw.Flush();
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("LogWriter(LOG-DEFAULT) Exception=[{0}], Log=[{1}]", ex.Message, log);
                    }
                    finally
                    {
                        Monitor.Exit(_lock);
                    }
                }



                if (level > LOG_LEVEL.SPECIAL_LOG_LEVEL_BEGIN)
                {

                    if (Monitor.TryEnter(_lock, TimeSpan.FromSeconds(1)))
                    {
                        try
                        {
                            // leave special log : ONLY WHEN level >= ERROR
                            string path = GetLogFileName(level, baseFileName);
                            using (TextWriter tw = TextWriter.Synchronized(File.AppendText(path)))
                            {
                                tw.WriteLine(line);
                                tw.Flush();
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("LogWriter(LOG-SPECIAL) Exception=[{0}], Log=[{1}]", ex.Message, log);
                        }
                        finally
                        {
                            Monitor.Exit(_lock);
                        }
                    }
                }
            }
            catch (Exception)
            {
                // 어쩌라고... 로그찍다가 익셉션 나면...
                //
#if DEBUG
                Console.WriteLine("LogWriter Exception, Log=[{0}]", log);
#endif
            }

        }
        private static readonly object _lock = new object();
    }


    #region StaticClassForLogFile
    public static class StaticParamClass
    {
        // Logger
        public static string LOGGER_LOG_ROOT = @"D:\Formation\Log\Selector";
        //public static string LOGGER_LOG_ROOT_SELECTOR = @"D:\Formation\Log\Selector";
        //public static string LOGGER_LOG_ROOT_GRADER = @"D:\Formation\Log\Grader";
        //public static string LOGGER_LOG_BASEFILENAME = "ECS";
        public static string LOGGER_LOG_BASEFILENAME = "SELECTOR";
        public static string LOGGER_LOG_DATETIMEFORMAT_DAY = "yyyy-MM-dd";
        public static string LOGGER_LOG_DATETIMEFORMAT_SEC = "yyyy-MM-dd HH:mm:ss";
        public static string LOGGER_LOG_DATETIMEFORMAT_MILISEC = "yyyy-MM-dd HH:mm:ss.fff";
    }
    #endregion

}
