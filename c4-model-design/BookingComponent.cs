using Structurizr;
using System;
using System.Collections.Generic;
using System.Text;

namespace c4_model_design
{
    internal class BookingComponent
    {
        private readonly C4 c4;
        private readonly ContainerDiagram containerDiagram;
        private readonly ContextDiagram contextDiagram;
        private readonly string componentTag = "Component";
        private readonly IdentityBCComponentDiagram identityAccessComponentDiagram;
        private readonly TransportationComponentDiagram transportationComponentDiagram;
        private readonly TourExperienceBCComponentDiagram tourExperienceBCComponentDiagram;
        private readonly NotificationDCComponentDiagram notificationDCComponentDiagram;
        public Component BookingController { get; set; }
        public Component BookingApplicationService { get; set; }
        public Component BookingRepository { get; set; }
        public Component DomainLayer { get; set; }
        public BookingComponent(C4 c4, ContainerDiagram containerDiagram, ContextDiagram contextDiagram, IdentityBCComponentDiagram indentityAccessComponentDiagram, TransportationComponentDiagram transportationComponentDiagram, TourExperienceBCComponentDiagram tourExperienceBCComponentDiagram, NotificationDCComponentDiagram notificationDCComponentDiagram)
        {
            this.c4 = c4;
            this.containerDiagram = containerDiagram;
            this.contextDiagram = contextDiagram;
            this.identityAccessComponentDiagram = indentityAccessComponentDiagram;
            this.transportationComponentDiagram = transportationComponentDiagram;
            this.tourExperienceBCComponentDiagram = tourExperienceBCComponentDiagram;
            this.notificationDCComponentDiagram = notificationDCComponentDiagram;
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
            BookingController = containerDiagram.ApiRest.AddComponent("Booking Controller", "", "Java");
            BookingApplicationService = containerDiagram.ApiRest.AddComponent("Booking Application Service", "", "Java");
            BookingRepository = containerDiagram.ApiRest.AddComponent("Booking Repository", "", "Java");
            DomainLayer = containerDiagram.ApiRest.AddComponent("Booking Domain Layer", "", "Java");
        }
        private void AddRelationships()
        {
            containerDiagram.WebApplication.Uses(BookingController, "Makes API calls to");
            containerDiagram.MobileApplication.Uses(BookingController, "Makes API calls to");
            BookingController.Uses(BookingApplicationService, "[JDBC]", "");
            BookingApplicationService.Uses(identityAccessComponentDiagram.UserRepository, "[JDBC]", "");
            BookingApplicationService.Uses(tourExperienceBCComponentDiagram.TourPackageRepository, "[JDBC]", "");
            BookingApplicationService.Uses(transportationComponentDiagram.AutomobileRepository, "[JDBC]", "");
            BookingApplicationService.Uses(notificationDCComponentDiagram.NotificationRepository, "[JDBC]", "");
            BookingApplicationService.Uses(BookingRepository, "[JDBC]", "");
            BookingApplicationService.Uses(DomainLayer, "[JDBC]", "");
            BookingRepository.Uses(containerDiagram.Database, "Use", "");
        }
        private void ApplyStyles()
        {
            BookingController.AddTags(this.componentTag);
            BookingApplicationService.AddTags(this.componentTag);
            BookingRepository.AddTags(this.componentTag);
            DomainLayer.AddTags(this.componentTag);
        }
        private void CreateView()
        {
            string title = "Booking BC Component Diagram";
            ComponentView componentView = c4.ViewSet.CreateComponentView(containerDiagram.ApiRest, title, title);
            componentView.Title = title;
            componentView.Add(containerDiagram.WebApplication);
            componentView.Add(containerDiagram.MobileApplication);
            componentView.Add(containerDiagram.Database);
            componentView.Add(BookingController);
            componentView.Add(BookingApplicationService);
            componentView.Add(BookingRepository);
            componentView.Add(DomainLayer);
            componentView.Add(identityAccessComponentDiagram.UserRepository);
            componentView.Add(tourExperienceBCComponentDiagram.TourPackageRepository);
            componentView.Add(transportationComponentDiagram.AutomobileRepository);
            componentView.Add(notificationDCComponentDiagram.NotificationRepository);

        }
    }
}
