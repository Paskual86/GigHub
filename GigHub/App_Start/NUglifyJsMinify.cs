using System.Web;
using System.Web.Optimization;
using NUglify;
using NUglify.JavaScript;

namespace GigHub.App_Start
{
    public class NUglifyJsMinify : IBundleTransform
    {
        private readonly CodeSettings _settings;

        public NUglifyJsMinify()
        {
            _settings = new CodeSettings
            {
                // adjust settings as needed
                PreserveImportantComments = false,
                EvalTreatment = EvalTreatment.Ignore
            };
        }

        public void Process(BundleContext context, BundleResponse response)
        {
            if (response == null || string.IsNullOrWhiteSpace(response.Content))
            {
                return;
            }

            // If you want to skip already-minified files, you can detect ".min.js" in response.Files.
            var result = Uglify.Js(response.Content, _settings);

            if (result.HasErrors)
            {
                // On error: keep original concatenated content to avoid breaking runtime.
                // Consider logging result.Errors here (not implemented to keep this transform dependency-free).
                response.ContentType = "text/javascript";
                return;
            }

            response.Content = result.Code;
            response.ContentType = "text/javascript";
        }
    }
}