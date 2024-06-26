using Structurizr;
using System;
using System.Collections.Generic;
using System.Text;

namespace c4_model_design
{
    internal class TransportationComponentDiagram
    {
        private readonly C4 c4;
        private readonly ContainerDiagram containerDiagram;
        private readonly ContextDiagram contextDiagram;
        private readonly string componentTag = "Component";
        private readonly IdentityBCComponentDiagram identityAccessComponentDiagram;
        private readonly TourExperienceBCComponentDiagram tourExperienceBCComponentDiagram;
        public Component AutomobileController { get; set; }
        public Component AutomobileApplicationService { get; set; }
        public Component AutomobileRepository { get; set; }
        public Component AutomobileDomainLayer { get; set; }
        public TransportationComponentDiagram(C4 c4, ContainerDiagram containerDiagram, ContextDiagram contextDiagram, IdentityBCComponentDiagram indentityAccessComponentDiagram, TourExperienceBCComponentDiagram tourExperienceBCComponentDiagram)
        {
            this.c4 = c4;
            this.containerDiagram = containerDiagram;
            this.contextDiagram = contextDiagram;
            this.identityAccessComponentDiagram = indentityAccessComponentDiagram;
            this.tourExperienceBCComponentDiagram = tourExperienceBCComponentDiagram;
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
            AutomobileController = containerDiagram.ApiRest.AddComponent("Vehicle Controller", "", "Java");
            AutomobileApplicationService = containerDiagram.ApiRest.AddComponent("Vehicle Application Service", "", "Java");
            AutomobileRepository = containerDiagram.ApiRest.AddComponent("Ground Transportation Vehicle Repository", "", "Java");
            AutomobileDomainLayer = containerDiagram.ApiRest.AddComponent("Transportation Domain Layer", "", "Java");
        }
        private void AddRelationships()
        {
            containerDiagram.WebApplication.Uses(AutomobileController, "Makes API calls to");
            AutomobileController.Uses(AutomobileApplicationService, "[JDBC]", "");
            AutomobileApplicationService.Uses(identityAccessComponentDiagram.UserRepository, "[JDBC]", "");
            AutomobileApplicationService.Uses(AutomobileRepository, "[JDBC]", "");
            AutomobileApplicationService.Uses(AutomobileDomainLayer, "[JDBC]", "");
            AutomobileRepository.Uses(containerDiagram.Database, "Use", "");
        }
        private void ApplyStyles()
        {
            AutomobileController.AddTags(this.componentTag);
            AutomobileApplicationService.AddTags(this.componentTag);
            AutomobileRepository.AddTags(this.componentTag);
            AutomobileDomainLayer.AddTags(this.componentTag);
        }
        private void CreateView()
        {
            string title = "Transportation BC Component Diagram";
            ComponentView componentView = c4.ViewSet.CreateComponentView(containerDiagram.ApiRest, title, "");
            componentView.Title = title;
            componentView.Add(containerDiagram.WebApplication);
            componentView.Add(containerDiagram.Database);
            componentView.Add(identityAccessComponentDiagram.UserRepository);
            //componentView.Add(tourExperienceBCComponentDiagram.TourPackageRepository);
            componentView.Add(this.AutomobileController);
            componentView.Add(this.AutomobileApplicationService);
            componentView.Add(this.AutomobileRepository);
            componentView.Add(this.AutomobileDomainLayer);
        }
    }
}
