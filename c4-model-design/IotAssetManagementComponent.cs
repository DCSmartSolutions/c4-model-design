using Structurizr;
using System;
using System.Collections.Generic;
using System.Text;

namespace c4_model_design
{
    internal class IotAssetManagementComponent
    {
        private readonly C4 c4;
        private readonly ContainerDiagram containerDiagram;
        private readonly ContextDiagram contextDiagram;
        private readonly string componentTag = "Component";

        public Component TrackingDomainLayer { get; set; }


        public Component WeatherSensorController { get; set; }
        public Component WeatherSensorRepository { get; set; }
        public Component WeatherSensorDomainLayer { get; set; }
        public Component IotAssetWeatherSensorApplicationService { get; set; }

        public IotAssetManagementComponent(C4 c4, ContainerDiagram containerDiagram, ContextDiagram contextDiagram)
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
            WeatherSensorController = containerDiagram.ApiRest.AddComponent("Weather Sensor Controller", "", "Java");
            WeatherSensorRepository = containerDiagram.ApiRest.AddComponent("Weather Sensor Repository", "", "Java");
            WeatherSensorDomainLayer = containerDiagram.ApiRest.AddComponent("Weather Domain Layer", "", "Java");
            IotAssetWeatherSensorApplicationService = containerDiagram.ApiRest.AddComponent("Iot Asset Weather Sensor Application Service", "", "Java");
        }
        private void AddRelationships()
        {
            containerDiagram.IoTdevices.Uses(WeatherSensorController, "Makes API calls to");


            WeatherSensorController.Uses(IotAssetWeatherSensorApplicationService, "[uses]", "");
            IotAssetWeatherSensorApplicationService.Uses(WeatherSensorRepository, "[uses]", "");
            IotAssetWeatherSensorApplicationService.Uses(WeatherSensorDomainLayer, "[uses]", "");
            WeatherSensorRepository.Uses(containerDiagram.Database, "[uses]", "");
        }
        private void ApplyStyles()
        {
            WeatherSensorController.AddTags(this.componentTag);
            WeatherSensorRepository.AddTags(this.componentTag);
            WeatherSensorDomainLayer.AddTags(this.componentTag);
            IotAssetWeatherSensorApplicationService.AddTags(this.componentTag);
        }
        private void CreateView()
        {
            string title = "Iot Asset Management BC Component Diagram";
            ComponentView componentView = c4.ViewSet.CreateComponentView(containerDiagram.ApiRest, title, title);
            componentView.Title = title;
            componentView.Add(containerDiagram.IoTdevices);
            componentView.Add(containerDiagram.Database);
            componentView.Add(TrackingDomainLayer);
            componentView.Add(WeatherSensorController);
            componentView.Add(WeatherSensorRepository);
            componentView.Add(WeatherSensorDomainLayer);
            componentView.Add(IotAssetWeatherSensorApplicationService);
        }
    }
}
