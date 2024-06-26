using Structurizr;
using System;
using System.Collections.Generic;
using System.Text;

namespace c4_model_design
{
    internal class NotificationDCComponentDiagram
    {
        private readonly C4 c4;
        private readonly ContainerDiagram containerDiagram;
        private readonly ContextDiagram contextDiagram;
        private readonly string componentTag = "Component";
        private readonly IdentityBCComponentDiagram identityAccessComponentDiagram;

        public Component NotificationController { get; set; }
        public Component NotificationApplicationService { get; set; }
        public Component NotificationRepository { get; set; }
        public Component DomainLayer { get; set; }
        public NotificationDCComponentDiagram(C4 c4, ContainerDiagram containerDiagram, ContextDiagram contextDiagram, IdentityBCComponentDiagram indentityAccessComponentDiagram)
        {
            this.c4 = c4;
            this.containerDiagram = containerDiagram;
            this.contextDiagram = contextDiagram;
            this.identityAccessComponentDiagram = indentityAccessComponentDiagram;
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
            NotificationController = containerDiagram.ApiRest.AddComponent("Notification Controller", "", "Java");
            NotificationApplicationService = containerDiagram.ApiRest.AddComponent("Notification Application Service", "", "Java");
            NotificationRepository = containerDiagram.ApiRest.AddComponent("Notification Repository", "", "Java");
            DomainLayer = containerDiagram.ApiRest.AddComponent("Notification Domain Layer", "", "Java");
        }
        private void AddRelationships()
        {
            containerDiagram.WebApplication.Uses(NotificationController, "Makes API calls to");
            NotificationController.Uses(NotificationApplicationService, "[uses]", "");
            NotificationApplicationService.Uses(identityAccessComponentDiagram.UserRepository, "[uses]", "");
            NotificationApplicationService.Uses(NotificationRepository, "[uses]", "");
            NotificationApplicationService.Uses(DomainLayer, "[uses]", ""); 
            NotificationRepository.Uses(containerDiagram.Database, "Use", "");
        }
        private void ApplyStyles()
        {
            NotificationController.AddTags(this.componentTag);
            NotificationApplicationService.AddTags(this.componentTag);
            NotificationRepository.AddTags(this.componentTag);
            DomainLayer.AddTags(this.componentTag);
        }
        private void CreateView()
        {
            string title = "Notification DC Component Diagram";
            ComponentView componentView = c4.ViewSet.CreateComponentView(containerDiagram.ApiRest, title, title);
            componentView.Title = title;
            componentView.Add(containerDiagram.WebApplication);
            componentView.Add(containerDiagram.Database);
            componentView.Add(this.NotificationController);
            componentView.Add(this.NotificationApplicationService);
            componentView.Add(this.NotificationRepository);
            componentView.Add(identityAccessComponentDiagram.UserRepository);
            componentView.Add(this.DomainLayer);

        }
    }
}
