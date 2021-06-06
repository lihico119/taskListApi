using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using TestApi.BO;

namespace TestApi.BL
{
    public class TaskHandler
    {
        private static string filesPath = System.Web.Hosting.HostingEnvironment.MapPath("~/Files");
        private static string DbPath = System.Web.Hosting.HostingEnvironment.MapPath("~/Files/DataBase.json");
        public ReplyBo<bool> InsertTask(Task task)
        {
            byte[] bytes = Convert.FromBase64String(task.PicturePath.Split(',')[1]);
            Image image;
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                image = Image.FromStream(ms);
            }
            if (!File.Exists(DbPath))
            {
                return new ReplyBuilder<bool>().setStatusCode(100);
            }
            var tasks = GetTasks().ModelBo;
            task.PicturePath = $"http://localhost:4000/Files/{tasks.Tasks.Count}.png";
            image.Save($"{filesPath}/{tasks.Tasks.Count}.png");
            tasks.Tasks.Add(tasks.Tasks.Count, task);
            File.WriteAllText(DbPath, JsonConvert.SerializeObject(tasks));
            return new ReplyBuilder<bool>().setStatusCode(0);
        }

        public ReplyBo<TaskCollection> GetTasks()
        {
            TaskCollection tasks;
            if (!File.Exists(DbPath))
            {
                return new ReplyBuilder<TaskCollection>().setStatusCode(100);
            }
            try
            {
                using (StreamReader sr = new StreamReader(DbPath))
                {
                    string json = sr.ReadToEnd();
                    tasks = JsonConvert.DeserializeObject<TaskCollection>(json);
                    return new ReplyBuilder<TaskCollection>().setStatusCode(0).setModelBo(tasks);
                }
            }
            catch (Exception ex)
            {
                return new ReplyBuilder<TaskCollection>().setStatusCode(500).setErrorMessage(JsonConvert.SerializeObject(ex));
            }
        }
    }
}