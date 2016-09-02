using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace MyHelper
{
    /// <summary>
    /// 日志类型
    /// </summary>
    public enum LogType : int
    {
        错误 = 0,
        提示 = 1,
        警告 = 2,
    }

    /// <summary>
    /// 改用单例(Singleton)模式实现，避免由于资源操作时导致的性能或损耗
    /// </summary>
    public class LogHelper
    {
        //LogHelper 对象实例
        private static  LogHelper instance;

        //静态只读的进程辅助对象
        private static readonly object syncRoot = new object();
        private static readonly object lockObj = new object();

        /// <summary>
        /// 构造函数私有，外部代码不能用new来实例化它
        /// </summary>
        private LogHelper() { }

        /// <summary>
        /// 获得本类实例的唯一全局访问点，双重锁定(Double Check Locking)，保证多线程下的安全
        /// </summary>
        /// <returns></returns>
        public static LogHelper GetInstance()
        {
            if (instance==null)
            {
                lock (syncRoot)
                {
                    if (instance==null)
                    {
                        instance = new LogHelper();
                    }
                }
            }
            return instance;
        }
       

        /// <summary>
        /// 属性，读写日志文件夹字段
        /// </summary>
        public string LogFolder
        {
            get
            {
                return logFolder;
            }
            set
            {
                logFolder=value;
            }
        }

        //日志文件夹
        //System.IO.Path.GetTempPath()返回的路径有如下几种情况
        //1、C:\Windows\Temp\ （System用户）
        //2、C:\Users\moss_yangziwen\AppData\Local\Temp\2\ （moss_yangziwen用户）
        //private string logFolder = System.IO.Path.GetTempPath();

        //获取和设置当前目录（即该进程从中启动的目录）的完全限定路径
        private string logFolder = System.Environment.CurrentDirectory + "\\";

        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="content"></param>
        /// <param name="logType"></param>
        public  void WriteLog(string content,LogType logType)
        {
            if (!Directory.Exists(logFolder))
            {
                Directory.CreateDirectory(logFolder);
            }
            string filePath = logFolder + DateTime.Now.ToString("yyyyMMdd") + ".txt";
            if (!File.Exists(filePath))
	        {
                HandleLogFile(content, filePath, FileMode.Create, logType);
            }
            else
            {
                HandleLogFile(content, filePath, FileMode.Append, logType);
            }
        }
        
        /// <summary>
        /// 写日志,不带日期时间及日志类型信息
        /// </summary>
        /// <param name="content"></param>
        /// <param name="logType"></param>
        public void WriteLogPure(string content)
        {
            if (!Directory.Exists(logFolder))
            {
                Directory.CreateDirectory(logFolder);
            }
            string filePath = logFolder + DateTime.Now.ToString("yyyyMMdd") + ".txt";
            LogType logType = LogType.提示;
            if (!File.Exists(filePath))
            {
                HandleLogFile(content, filePath, FileMode.Create, logType);
            }
            else
            {
                HandleLogFile(content, filePath, FileMode.Append, logType);
            }
        }

        /// <summary>
        /// 写日志，默认日志类型为【提示】
        /// </summary>
        /// <param name="strContent"></param>
        public void WriteLog(string content)
        {
            if (!Directory.Exists(logFolder))
            {
                Directory.CreateDirectory(logFolder);
            }

            LogType logType = LogType.提示;
            string filePath = logFolder + DateTime.Now.ToString("yyyyMMdd") + ".txt";
            if (!File.Exists(filePath))
            {
                HandleLogFile(content, filePath, FileMode.Create, logType);

                //FileStream fs = new FileStream(filePath, FileMode.Create);
                //StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.UTF8);
                //sw.WriteLine(DateTime.Now.ToString("HH:mm:ss") + " " + logType.ToString() + " " + content);
                //sw.Close();
            }
            else
            {
                HandleLogFile(content, filePath, FileMode.Append, logType);

                //FileStream fs = new FileStream(filePath, FileMode.Append);
                //StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.UTF8);
                //sw.WriteLine(DateTime.Now.ToString("HH:mm:ss") + " " + logType.ToString() + " " + content);
                //sw.Close();
            }
        }
        /// <summary>
        /// 根据输入要求读写日志,加锁防止多线程情况下同时读写同一个日志文件
        /// </summary>
        /// <param name="logContent"></param>
        /// <param name="filePath"></param>
        /// <param name="fileMode"></param>
        /// <param name="logType"></param>
        private void HandleLogFile(string logContent, string filePath, FileMode fileMode, LogType logType)
        {
            lock (lockObj)
            {
                FileStream fs = new FileStream(filePath, fileMode);
                StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.UTF8);
                sw.WriteLine(DateTime.Now.ToString("HH:mm:ss") + " " + logType.ToString() + " " + logContent);
                sw.Close();
            }
        }
    }
}