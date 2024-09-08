using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MassTransit;

namespace SagaStateMachine.Models
{
    internal class AppSagaStateMachine : MassTransitStateMachine<SagaEntity>
    {
    }
}