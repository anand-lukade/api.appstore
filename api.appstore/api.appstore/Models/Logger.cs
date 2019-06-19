using System;

namespace api.appstore.Models
{
    public static class Logger
    {
        public static void Log(string message)
        {
            using (MususAppEntities entity = new MususAppEntities())
            {
                entity.LogMasters.Add(new LogMaster()
                {
                    logtext = message,
                    logtime = DateTime.UtcNow,
                    //LogType=logType
                });
            }
        }
    }
}