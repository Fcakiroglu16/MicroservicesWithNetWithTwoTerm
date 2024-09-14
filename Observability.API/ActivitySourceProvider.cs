using System.Diagnostics;

namespace Observability.API
{
    public class ActivitySourceProvider
    {
        public static ActivitySource ActivitySource = new ActivitySource("Observability.API.ActivitySource");
    }
}