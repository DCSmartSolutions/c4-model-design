using Structurizr;
using System;
using System.Collections.Generic;
using System.Text;

namespace c4_model_design
{
    internal class DataReportAndAnalyticsComponentDiagram
    {
        private readonly C4 c4;
        private readonly ContainerDiagram containerDiagram;
        private readonly ContextDiagram contextDiagram;
        private readonly string componentTag = "Component";
        private readonly IotAssetManagementComponent iotAssetManagementComponent;
        private readonly NotificationDCComponentDiagram notificationComponentDiagram;

        public Component DataIngestionService { get; set; }
        public Component DataProcessingService { get; set; }
        public Component DataRepository { get; set; }
        public Component ReportingService { get; set; }
        public Component AnalyticsService { get; set; }
        public Component NotificationServiceAdapter { get; set; }

        public DataReportAndAnalyticsComponentDiagram(C4 c4, ContainerDiagram containerDiagram, ContextDiagram contextDiagram, IotAssetManagementComponent iotAssetManagementComponent, NotificationDCComponentDiagram notificationComponentDiagram)
        {
            this.c4 = c4;
            this.containerDiagram = containerDiagram;
            this.contextDiagram = contextDiagram;
            this.iotAssetManagementComponent = iotAssetManagementComponent;
            this.notificationComponentDiagram = notificationComponentDiagram;
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
            DataIngestionService = containerDiagram.ApiRest.AddComponent("Data Ingestion Service", "Ingests data from IoT devices and other sources", "Java");
            DataProcessingService = containerDiagram.ApiRest.AddComponent("Data Processing Service", "Processes and transforms data for analytics", "Java");
            DataRepository = containerDiagram.Database.AddComponent("Data Repository", "Stores processed data", "Relational Database");
            ReportingService = containerDiagram.ApiRest.AddComponent("Reporting Service", "Generates reports based on data analysis", "Java");
            AnalyticsService = containerDiagram.ApiRest.AddComponent("Analytics Service", "Performs data analytics and machine learning", "Java");
            NotificationServiceAdapter = containerDiagram.ApiRest.AddComponent("Notification Service Adapter", "Sends notifications based on data analysis results", "Java");
        }

        private void AddRelationships()
        {
            containerDiagram.IoTdevices.Uses(DataIngestionService, "Sends data to");
            DataIngestionService.Uses(DataProcessingService, "Sends raw data to");
            DataProcessingService.Uses(DataRepository, "Stores processed data in");
            DataProcessingService.Uses(AnalyticsService, "Sends data for analysis to");
            AnalyticsService.Uses(DataRepository, "Fetches data from");
            ReportingService.Uses(DataRepository, "Fetches data from");
            ReportingService.Uses(AnalyticsService, "Uses analytics results from");
            AnalyticsService.Uses(NotificationServiceAdapter, "Triggers notifications via");
            NotificationServiceAdapter.Uses(notificationComponentDiagram.NotificationController, "Sends notifications to");
            AnalyticsService.Uses(containerDiagram.Database, "Uses", "");
        }

        private void ApplyStyles()
        {
            DataIngestionService.AddTags(this.componentTag);
            DataProcessingService.AddTags(this.componentTag);
            DataRepository.AddTags(this.componentTag);
            ReportingService.AddTags(this.componentTag);
            AnalyticsService.AddTags(this.componentTag);
            NotificationServiceAdapter.AddTags(this.componentTag);
        }

        private void CreateView()
        {
            string title = "Data Report & Analytics BC Component Diagram";
            ComponentView componentView = c4.ViewSet.CreateComponentView(containerDiagram.ApiRest, title, title);
            componentView.Title = title;
            componentView.Add(containerDiagram.IoTdevices);
            componentView.Add(DataIngestionService);
            componentView.Add(DataProcessingService);
            componentView.Add(DataRepository);
            componentView.Add(ReportingService);
            componentView.Add(AnalyticsService);
            componentView.Add(NotificationServiceAdapter);
            componentView.Add(notificationComponentDiagram.NotificationController);
        }
    }
}
