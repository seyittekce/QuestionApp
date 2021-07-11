using System.IO.Compression;
using System.Web.Mvc;
namespace QuestionApp.Models
{
    public class CompressAttribute : System.Web.Mvc.ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var encodingAccept = filterContext.HttpContext.Request.Headers["Accept-Encoding"];
            if (string.IsNullOrEmpty(encodingAccept))
                return;
            encodingAccept = encodingAccept.ToLowerInvariant();
            var response = filterContext.HttpContext.Response;
            if (encodingAccept.Contains("gzip"))
            {
                response.AddHeader("Content-encoding", "gzip");
                response.Filter = new GZipStream(response.Filter, CompressionMode.Compress);
            }
            else if (encodingAccept.Contains("deflate"))
            {
                response.AddHeader("Content-encoding", "deflate");
                response.Filter = new DeflateStream(response.Filter, CompressionMode.Compress);
            }
        }
    }
}