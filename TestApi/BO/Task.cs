using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TestApi.BO
{
    public class TaskCollection
    {
        public Dictionary<int, Task> Tasks { get; set; }
    }

    public class Task
    {
        [Required]
        public string TaskName { get; set; }
        [Required]
        public string PicturePath { get; set; }
    }
}