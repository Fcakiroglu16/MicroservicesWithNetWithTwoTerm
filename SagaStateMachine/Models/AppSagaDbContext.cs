using MassTransit.EntityFrameworkCoreIntegration;
using Microsoft.EntityFrameworkCore;

namespace SagaStateMachine.Models
{
    public class AppSagaDbContext(DbContextOptions options) : SagaDbContext(options)
    {
        protected override IEnumerable<ISagaClassMap> Configurations
        {
            get { yield return new SagaEntityConfiguration(); }
        }
    }
}