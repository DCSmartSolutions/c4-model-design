using Structurizr;
using System;
using System.Collections.Generic;
using System.Text;

namespace c4_model_design
{
    internal class ProfileComponent
    {
        private readonly C4 c4;
        private readonly ContainerDiagram containerDiagram;
        private readonly ContextDiagram contextDiagram;
        private readonly string componentTag = "Component";
        private readonly IdentityBCComponentDiagram identityAccessComponentDiagram;
        public Component UserProfileController { get; set; }
        public Component UserProfileApplicationService { get; set; }
        public Component UserProfileRepository { get; set; }
        public Component DomainLayer { get; set; }
        public ProfileComponent(C4 c4, ContainerDiagram containerDiagram, ContextDiagram contextDiagram, IdentityBCComponentDiagram indentityAccessComponentDiagram)
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
            UserProfileController = containerDiagram.ApiRest.AddComponent("Profile Controller", "", "Java");
            UserProfileApplicationService = containerDiagram.ApiRest.AddComponent("Profile Application Service", "", "Java");
            UserProfileRepository = containerDiagram.ApiRest.AddComponent("Profile Repository", "", "Java");
            DomainLayer = containerDiagram.ApiRest.AddComponent("Profile Domain Layer", "", "Java");
        }
        private void AddRelationships()
        {
            containerDiagram.WebApplication.Uses(UserProfileController, "Makes API calls to");
            UserProfileController.Uses(UserProfileApplicationService, "[uses]", "");
            UserProfileApplicationService.Uses(identityAccessComponentDiagram.UserRepository, "[uses]", "");
            UserProfileApplicationService.Uses(UserProfileRepository, "[uses]", "");
            UserProfileApplicationService.Uses(DomainLayer, "[uses]", "");
            UserProfileRepository.Uses(containerDiagram.Database, "Use", "");
        }
        private void ApplyStyles()
        {
            UserProfileController.AddTags(this.componentTag);
            UserProfileApplicationService.AddTags(this.componentTag);
            UserProfileRepository.AddTags(this.componentTag);
            DomainLayer.AddTags(this.componentTag);
        }
        private void CreateView()
        {
            string title = "Profile BC Component Diagram";
            ComponentView componentView = c4.ViewSet.CreateComponentView(containerDiagram.ApiRest, title, title);
            componentView.Title = title;
            componentView.Add(containerDiagram.WebApplication);
            componentView.Add(containerDiagram.Database);
            componentView.Add(identityAccessComponentDiagram.UserRepository);
            componentView.Add(UserProfileController);
            componentView.Add(UserProfileApplicationService);
            componentView.Add(UserProfileRepository);
            componentView.Add(DomainLayer);
            
        }
    }
}
