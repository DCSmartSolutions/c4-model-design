using Structurizr;
using System;
using System.Collections.Generic;
using System.Text;

namespace c4_model_design
{
    internal class TourExperienceBCComponentDiagram
    {
        private readonly C4 c4;
        private readonly ContainerDiagram containerDiagram;
        private readonly ContextDiagram contextDiagram;
        private readonly string componentTag = "Component";
        private readonly IdentityBCComponentDiagram identityAccessComponentDiagram;
        public Component TourPackageController { get; set; }
        public Component TourPackageApplicationService { get; set; }
        public Component TourPackageRepository { get; set; }
        public Component TourPackageDomainLayer { get; set; }
        public TourExperienceBCComponentDiagram(C4 c4, ContainerDiagram containerDiagram, ContextDiagram contextDiagram, IdentityBCComponentDiagram indentityAccessComponentDiagram)
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
            TourPackageController = containerDiagram.ApiRest.AddComponent("Tour Package Controller", "", "Java");
            TourPackageApplicationService = containerDiagram.ApiRest.AddComponent("Tour Package Application Service", "", "Java");
            TourPackageRepository = containerDiagram.ApiRest.AddComponent("Tour Package Repository", "", "Java");
            TourPackageDomainLayer = containerDiagram.ApiRest.AddComponent("Tour Package Domain Layer", "", "Java");
        }
        private void AddRelationships()
        {
            containerDiagram.WebApplication.Uses(TourPackageController, "Makes API calls to");
            TourPackageController.Uses(TourPackageApplicationService, "[uses]", "");
            TourPackageApplicationService.Uses(identityAccessComponentDiagram.UserRepository, "[uses]", "");
            TourPackageApplicationService.Uses(TourPackageRepository, "[uses]", "");
            TourPackageApplicationService.Uses(TourPackageDomainLayer, "[uses]", "");   
            TourPackageRepository.Uses(containerDiagram.Database, "Use", "");
        }
        private void ApplyStyles()
        {
            TourPackageController.AddTags(this.componentTag);
            TourPackageApplicationService.AddTags(this.componentTag);
            TourPackageRepository.AddTags(this.componentTag);
            TourPackageDomainLayer.AddTags(this.componentTag);
        }
        private void CreateView()
        {
            string title = "Tour Experience BC Component Diagram";
            ComponentView componentView = c4.ViewSet.CreateComponentView(containerDiagram.ApiRest, title, title);
            componentView.Title = title;
            componentView.Add(containerDiagram.WebApplication);
            componentView.Add(containerDiagram.Database);
            componentView.Add(identityAccessComponentDiagram.UserRepository);
            componentView.Add(this.TourPackageController);
            componentView.Add(this.TourPackageApplicationService);
            componentView.Add(this.TourPackageRepository);
            componentView.Add(this.TourPackageDomainLayer);
        }

    }
}
