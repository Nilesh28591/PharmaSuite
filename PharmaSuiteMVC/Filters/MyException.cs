using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace PharmaSuiteMVC.Filters
{
    public class MyException: ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            string logmsg = $"[{DateTime.Now} This is Causing Exception : {context.Exception.Message}]";
            string logdir = Path.Combine(Directory.GetCurrentDirectory(), "Logs");

            if (!Directory.Exists(logdir))
            {
                Directory.CreateDirectory(logdir);
            }

            string logpath = Path.Combine(logdir, "log.txt");
            File.AppendAllText(logpath, logmsg);

            var result = new ViewResult { ViewName = "Error" };

            context.Result = new RedirectToActionResult("Error", "Home", new { msg = logmsg });
            context.ExceptionHandled = true;


        }
    }
}
