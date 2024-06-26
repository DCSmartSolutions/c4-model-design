using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace c4_model_design
{
    internal class BookingBCComponentDiagram
    {
        private readonly C4 c4;
        private readonly ContainerDiagram containerDiagram;
        private readonly ContextDiagram contextDiagram;
        private readonly string componentTag = "Component";
        private readonly IdentityBCComponentDiagram identityAccessComponentDiagram;
        public Component BookingController { get; set; }
        public Component BookingApplicationService { get; set; }
        public Component BookingRepository { get; set; }
        public Component BookingDomainLayer { get; set; }
        public BookingBCComponentDiagram(C4 c4, ContainerDiagram containerDiagram, ContextDiagram contextDiagram, IdentityBCComponentDiagram indentityAccessComponentDiagram)
        {
            this.c4 = c4;
            this.containerDiagram = containerDiagram;
            this.contextDiagram = contextDiagram;
            this.identityAccessComponentDiagram = indentityAccessComponentDiagram;
        }
    }
}
