using MassTransit;
using Shared.Events.Order;
using Shared.Events.Payment;
using Shared.Events.Stock;

namespace SagaStateMachine.Models
{
    public class AppSagaStateMachine : MassTransitStateMachine<SagaEntity>
    {
        public Event<OrderCreatedEvent> OrderCreatedEvent { get; set; } = default!;
        public Event<PaymentCompletedEvent> PaymentCompletedEvent { get; set; } = default!;
        public Event<PaymentFailedEvent> PaymentFailedEvent { get; set; } = default!;

        public Event<StockReservedEvent> StockReservedEvent { get; set; } = default!;
        public Event<StockNotReservedEvent> StockNotReservedEvent { get; set; } = default!;


        public State OrderCreatedState { get; set; } = default!;
        public State StockReservedState { get; set; } = default!;
        public State StockNotReservedState { get; set; } = default!;
        public State PaymentCompletedState { get; set; } = default!;
        public State PaymentFailedState { get; set; } = default!;

        public AppSagaStateMachine()
        {
            InstanceState(x => x.State);


            Event(() => OrderCreatedEvent,
                x => x.CorrelateBy<string>(y => y.OrderCode, z => z.Message.OrderCode)
                    .SelectId(context => NewId.NextGuid()));


            Event(() => StockReservedEvent, x => x.CorrelateById(y => y.Message.CorrelationId));

            Event(() => StockNotReservedEvent, x => x.CorrelateById(y => y.Message.CorrelationId));

            Event(() => PaymentCompletedEvent, x => x.CorrelateById(y => y.Message.CorrelationId));

            Event(() => PaymentFailedEvent, x => x.CorrelateById(y => y.Message.CorrelationId));


            Initially(When(OrderCreatedEvent).Then(context =>
            {
                context.Saga.BuyerId = context.Message.BuyerId;
                context.Saga.OrderCode = context.Message.OrderCode;
                context.Saga.Created = DateTime.Now;
                context.Saga.CardNameSurname = context.Message.CardNameSurname;
                context.Saga.CardNumber = context.Message.CardNumber;
                context.Saga.TotalPrice = context.Message.TotalPrice;
            }).TransitionTo(OrderCreatedState).Publish(context =>
                new StockReserveStartMessage(context.Message.OrderItems)
                    { CorrelationId = context.Saga.CorrelationId }));


            // builder design pattern => fluent style;


            During(OrderCreatedState,
                When(StockReservedEvent).TransitionTo(StockReservedState).Publish(context =>
                    new PaymentStartMessage(context.Saga.CardNumber,
                            context.Saga.CardNameSurname, context.Saga.TotalPrice)
                        { CorrelationId = context.Saga.CorrelationId })
                , When(StockNotReservedEvent).TransitionTo(StockNotReservedState).Publish(context => new OrderStatusMessage(context.Saga.OrderCode, 2, context.Message.Reason))
            );


            During(StockReservedState,
                When(PaymentCompletedEvent).TransitionTo(PaymentCompletedState)
                    .Publish(context => new OrderStatusMessage(context.Saga.OrderCode, 1, "")).Finalize(),
                When(PaymentFailedEvent).TransitionTo(PaymentFailedState)
                    .Publish(context => new OrderStatusMessage(context.Saga.OrderCode, 3, context.Message.Reason))
                    .Publish(context => new StockRollbackStart(context.Saga.OrderCode))
            );


            // SetCompletedWhenFinalized();
        }
    }
}