using Structurizr;
using System;
using System.Collections.Generic;
using System.Text;

namespace c4_model_design
{
    class ReviewBCComponentDiagram
    {
        private readonly C4 c4;
        private readonly ContainerDiagram containerDiagram;
        private readonly ContextDiagram contextDiagram;
        private readonly string componentTag = "Component";
        private readonly IdentityBCComponentDiagram identityAccessComponentDiagram;

        public Component ReviewController { get; set; }
        public Component ReviewApplicationService { get; set; }
        public Component ReviewRepository { get; set; }
        public Component DomainLayer { get; set; }
        public ReviewBCComponentDiagram(C4 c4, ContainerDiagram containerDiagram, ContextDiagram contextDiagram, IdentityBCComponentDiagram indentityAccessComponentDiagram)
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
            ReviewController = containerDiagram.ApiRest.AddComponent("Review Controller", "", "Java");
            ReviewApplicationService = containerDiagram.ApiRest.AddComponent("Review Application Service", "", "Java");
            ReviewRepository = containerDiagram.ApiRest.AddComponent("Review Repository", "", "Java");
            DomainLayer = containerDiagram.ApiRest.AddComponent("Review Domain Layer", "", "Java");
        }
        private void AddRelationships()
        {
            containerDiagram.WebApplication.Uses(ReviewController, "Makes API calls to");
            ReviewController.Uses(ReviewApplicationService, "[uses]", "");
            ReviewApplicationService.Uses(ReviewRepository, "[uses]", "");
            ReviewApplicationService.Uses(identityAccessComponentDiagram.UserRepository, "[uses]", "");
            ReviewApplicationService.Uses(DomainLayer, "[uses]", "");
            ReviewRepository.Uses(containerDiagram.Database, "Use", "");
        }
        private void ApplyStyles()
        {
            ReviewController.AddTags(this.componentTag);
            ReviewApplicationService.AddTags(this.componentTag);
            ReviewRepository.AddTags(this.componentTag);
            DomainLayer.AddTags(this.componentTag);
        }
        private void CreateView()
        {
            string title = "Review DC Component Diagram";
            ComponentView componentView = c4.ViewSet.CreateComponentView(containerDiagram.ApiRest, title, title);
            componentView.Title = title;
            componentView.Add(containerDiagram.WebApplication);
            componentView.Add(containerDiagram.Database);
            componentView.Add(identityAccessComponentDiagram.UserRepository);
            componentView.Add(this.ReviewController);
            componentView.Add(this.ReviewApplicationService);
            componentView.Add(this.ReviewRepository);
            componentView.Add(this.ReviewRepository);
            componentView.Add(this.DomainLayer);
        }
    }
}
