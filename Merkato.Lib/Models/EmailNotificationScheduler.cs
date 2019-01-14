using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace  Merkato.Lib.Models
{
    /// <summary>
    /// 
    /// </summary>
    public enum SchedulerMode
    {
        /// <summary>
        /// 
        /// </summary>
        INTERVAL,
        /// <summary>
        /// 
        /// </summary>
        DAY,
    }
    /// <summary>
    /// 
    /// </summary>
    public class EmailNotificationScheduler
    {
        /// <summary>
        /// 
        /// </summary>
        public const string KEY_NAME = "EmailNotificationScheduler";
        /// <summary>
        /// 
        /// </summary>
        public EmailNotificationScheduler()
        {
            Interval = new TimeSpan(01, 00, 0);
            ScheduledTime = DateTime.Parse("05:30");
        }
        /// <summary>
        /// Le mode d'execution du processus de notification
        /// </summary>
        public SchedulerMode Mode { get; set; }
        /// <summary>
        /// Si le mode est INTERVAL alors cette valeur indique 
        /// l'interval d'execution du processus de notification.
        /// </summary>
        [DisplayFormat(DataFormatString = "{0:hh\\:mm\\:ss}", ApplyFormatInEditMode = true)]
        public TimeSpan Interval { get; set; }
        /// <summary>
        /// Si le mode est DAY alors cette valeur indique l'heure à laquel 
        /// le processusde notification va s'executer.
        /// </summary>

        [DataType(DataType.Time)]
        public DateTime ScheduledTime { get; set; }
        //public DayOfWeek DayOfWeek { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public DateTime NextExecution { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime LastExectuion { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="jsonData"></param>
        /// <returns></returns>
        public static EmailNotificationScheduler Parse(string jsonData)
        {
            return JsonConvert.DeserializeObject<EmailNotificationScheduler>(jsonData);
        }
    }
    //<add key = "SmtpPassword" value="SageWorkflow" />
    //<add key = "SmtpUser" value="GhpostSageWorkflow@gmail.com" />
    //<add key = "SmtpPort" value="587" />
    //<add key = "SmtpServerAdress" value="smtp.gmail.com" />
    //<add key = "MonitorMail" value="sip.sage.workflow@gmail.com" />
    //<add key = "EnableSsl" value="true" />

    //<add key = "NotificationRootPath" value="http://localhost/SageWorkflowWeb/Home/Details"/>
    /// <summary>
    /// 
    /// </summary>
    public class SmtpCredential
    {
        /// <summary>
        /// 
        /// </summary>
        public SmtpCredential()
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static SmtpCredential Parse(string data)
        {
            //var b = Convert.FromBase64String(data);
            //var s = Encoding.UTF8.GetString(b);
            return JsonConvert.DeserializeObject<SmtpCredential>(data);
        }
        /// <summary>
        /// 
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var s = JsonConvert.SerializeObject(this);
            return s;
            //var b = Encoding.UTF8.GetBytes(s);
            //var bas64string = Convert.ToBase64String(b);
            //return bas64string;
        }
    }
    /// <summary>
    /// 
    /// </summary>
    public class SmtpConfig
    {
        /// <summary>
        /// 
        /// </summary>
        public const string KEY_NAME = "SmtpConfig";
        /// <summary>
        /// 
        /// </summary>
        public SmtpCredential Credential { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ServerAdress { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool EnableSsl { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int ServerPort { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string CopyMail { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CredentialValue { get; set; }

        //public override string ToString()
        //{
        //    CredentialValue = Credential.ToString();
        //    return JsonConvert.SerializeObject(this);
        //}

        //public SmtpConfig Parse(string jsonData)
        //{
        //    var config = JsonConvert.DeserializeObject<SmtpConfig>(jsonData);
        //    config.Credential = SmtpCredential.Parse(config.CredentialValue);
        //    return config;
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public void Save(MerkatoDbContext context)
        {

        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class TaskServiceParameter
    {
        /// <summary>
        /// 
        /// </summary>
        public TaskServiceParameter()
        {
            ServiceStateNotificationReceiver = "sipmerkato@gmail.com; sipconsult18@gmail.com";
        }
        /// <summary>
        /// 
        /// </summary>
        public string ServiceStateNotificationReceiver { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int ExecutionIntervalMinute { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="jsonData"></param>
        /// <returns></returns>
        public static TaskServiceParameter Parse(string jsonData)
        {
            return JsonConvert.DeserializeObject<TaskServiceParameter>(jsonData);
        }

    }
}
