using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MassTransit;

namespace SagaStateMachine.Models
{
    public class SagaEntity : SagaStateMachineInstance
    {
        public Guid CorrelationId { get; set; }

        public string State { get; set; } = default!;

        public DateTime Created { get; set; }

        #region From events

        public string BuyerId { get; set; } = default!;
        public string OrderCode { get; set; } = default!;

        public decimal TotalPrice { get; set; } = default!;


        public string CardNumber { get; set; } = default!;
        public string CardNameSurname { get; set; } = default!;

        #endregion
    }
}