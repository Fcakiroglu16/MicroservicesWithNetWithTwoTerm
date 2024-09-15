using System.Diagnostics;

namespace Observability.API
{
    public class ActivitySourceProvider
    {
        public static ActivitySource ActivitySource = new ActivitySource("Observability.API.ActivitySource");
        //public static ActivitySource ActivitySourceSql = new ActivitySource("Observability.API.ActivitySource2");
    }
}