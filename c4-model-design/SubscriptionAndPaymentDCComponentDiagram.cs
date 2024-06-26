using Structurizr;
using System;
using System.Collections.Generic;
using System.Text;

namespace c4_model_design
{
    internal class SubscriptionAndPaymentDCComponentDiagram
    {
        private readonly C4 c4;
        private readonly ContainerDiagram containerDiagram;
        private readonly ContextDiagram contextDiagram;
        private readonly string componentTag = "Component";
        
        public Component PaymentController { get; set; }
        public Component SubscriptionController { get; set; }
        public Component PaymentApplicationService { get; set; }
        public Component SubscriptionApplicationService { get; set; }
        public Component PaymentGatewayFacade { get; set; }
        public Component PaymentRepository { get; set; }
        public Component SubscriptionRepository { get; set; }
        public Component DomainLayer { get; set; }
        public SubscriptionAndPaymentDCComponentDiagram(C4 c4, ContainerDiagram containerDiagram, ContextDiagram contextDiagram)
        {
            this.c4 = c4;
            this.containerDiagram = containerDiagram;
            this.contextDiagram = contextDiagram;
        }
        public void Generate()
        {
            AddComponents();
            AddRelationships();
            ApplyStyles();
            CreateView();
        }
        private void AddComponents()
        {
            PaymentController = containerDiagram.ApiRest.AddComponent("Payment Controller", "", "Java");
            SubscriptionController = containerDiagram.ApiRest.AddComponent("Subscription Controller", "", "Java");
            PaymentApplicationService = containerDiagram.ApiRest.AddComponent("Payment Application Service", "", "Java");
            SubscriptionApplicationService = containerDiagram.ApiRest.AddComponent("Subscription Application Service", "", "Java");
            PaymentGatewayFacade = containerDiagram.ApiRest.AddComponent("Payment Gateway Facade", "", "Java");
            PaymentRepository = containerDiagram.ApiRest.AddComponent("Payment Repository", "", "Java");
            SubscriptionRepository = containerDiagram.ApiRest.AddComponent("Subscription Repository", "", "Java");
            DomainLayer = containerDiagram.ApiRest.AddComponent("Domain Layer", "", "Java");
        }
        private void AddRelationships()
        {
            containerDiagram.WebApplication.Uses(PaymentController, "Makes API calls to");
            containerDiagram.WebApplication.Uses(SubscriptionController, "Makes API calls to");
            PaymentController.Uses(PaymentApplicationService, "[uses]", "");
            SubscriptionController.Uses(SubscriptionApplicationService, "[uses]", "");
            PaymentController.Uses(PaymentGatewayFacade, "uSE", "");
            PaymentGatewayFacade.Uses(contextDiagram.PaymentGateway, "JSON/HTTPS");
            PaymentApplicationService.Uses(PaymentRepository, "[uses]", "");
            SubscriptionApplicationService.Uses(SubscriptionRepository, "[uses]", "");
            PaymentApplicationService.Uses(DomainLayer, "[uses]", "");
            SubscriptionApplicationService.Uses(DomainLayer, "[uses]", "");
            PaymentRepository.Uses(containerDiagram.Database, "Use", "");
            SubscriptionRepository.Uses(containerDiagram.Database, "Use", "");
        }
        private void ApplyStyles()
        {
            SetTags();
        }
        private void SetTags()
        {
            PaymentController.AddTags(this.componentTag);
            SubscriptionController.AddTags(this.componentTag);
            PaymentApplicationService.AddTags(this.componentTag);
            SubscriptionApplicationService.AddTags(this.componentTag);
            PaymentGatewayFacade.AddTags(this.componentTag);
            PaymentRepository.AddTags(this.componentTag);
            SubscriptionRepository.AddTags(this.componentTag);
            DomainLayer.AddTags(this.componentTag);
        }
        private void CreateView()
        {
            string title = "Subscription and Payment BC Component Diagram";
            ComponentView componentView = c4.ViewSet.CreateComponentView(containerDiagram.ApiRest,title,title);
            componentView.Title = title;
            componentView.Add(containerDiagram.WebApplication);
            componentView.Add(containerDiagram.Database);
            componentView.Add(contextDiagram.PaymentGateway);
            componentView.Add(this.PaymentController);
            componentView.Add(this.SubscriptionController);
            componentView.Add(this.PaymentApplicationService);
            componentView.Add(this.SubscriptionApplicationService);
            componentView.Add(this.PaymentGatewayFacade);
            componentView.Add(this.PaymentRepository);
            componentView.Add(this.SubscriptionRepository);
            componentView.Add(this.DomainLayer);
        }
    }
}
