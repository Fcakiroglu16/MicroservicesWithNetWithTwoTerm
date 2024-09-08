using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace SagaStateMachine.Models
{
    internal class SagaEntityConfiguration : SagaClassMap<SagaEntity>
    {
        public override void Configure(ModelBuilder model)
        {
            var entity = model.Entity<SagaEntity>();
            entity.ToTable("OrderSaga");
            entity.HasKey(x => x.OrderCode);
            entity.Property(x => x.BuyerId).HasMaxLength(256);

            //entity.Property(x => x.CardNumber);
            //entity.Property(x => x.CardNameSurname);
            //entity.Property(x => x.Created);
        }
    }
}