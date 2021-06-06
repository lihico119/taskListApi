using System.Web.Http;
using TestApi.BL;
using TestApi.BO;

namespace TestApi.Controllers
{
    [RoutePrefix("Tasks")]
    public class MainController : ApiController
    {
        [HttpPost]
        [Route("insertask")]
        public ReplyBo<bool> InsertTask(Task task)
        {
            return new TaskHandler().InsertTask(task);
        }

        [HttpGet]
        [Route("getasks")]
        public ReplyBo<TaskCollection> GetAllTasks()
        {
            return new TaskHandler().GetTasks();
        }
    }
}
