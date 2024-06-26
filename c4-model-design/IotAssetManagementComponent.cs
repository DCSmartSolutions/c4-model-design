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
        public Component LocalizationWristbandController { get; set; }
        public Component IotAssetTrackingApplicationService { get; set; }
        public Component LocalizationWristbandRepository { get; set; }
        public Component TrackingDomainLayer { get; set; }

        public Component ScaleController { get; set; }
        public Component ScaleRepository { get; set; }
        public Component ScaleDomainLayer { get; set; }
        public Component IotAssetScaleApplicationService { get; set; }

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
            LocalizationWristbandController = containerDiagram.ApiRest.AddComponent("Localization Wristband Controller", "", "Java");
            IotAssetTrackingApplicationService = containerDiagram.ApiRest.AddComponent("Iot Asset Tracking Application Service", "", "Java");
            LocalizationWristbandRepository = containerDiagram.ApiRest.AddComponent("Localization Wristband Repository", "", "Java");
            TrackingDomainLayer = containerDiagram.ApiRest.AddComponent("Tracking Domain Layer", "", "Java");

            ScaleController = containerDiagram.ApiRest.AddComponent("Scale Controller", "", "Java");
            ScaleRepository = containerDiagram.ApiRest.AddComponent("Scale Repository", "", "Java");
            ScaleDomainLayer = containerDiagram.ApiRest.AddComponent("Scale Domain Layer", "", "Java");
            IotAssetScaleApplicationService = containerDiagram.ApiRest.AddComponent("Iot Asset Scale Application Service", "", "Java");

            WeatherSensorController = containerDiagram.ApiRest.AddComponent("Weather Sensor Controller", "", "Java");
            WeatherSensorRepository = containerDiagram.ApiRest.AddComponent("Weather Sensor Repository", "", "Java");
            WeatherSensorDomainLayer = containerDiagram.ApiRest.AddComponent("Weather Domain Layer", "", "Java");
            IotAssetWeatherSensorApplicationService = containerDiagram.ApiRest.AddComponent("Iot Asset Weather Sensor Application Service", "", "Java");
        }
        private void AddRelationships()
        {
            containerDiagram.IoTdevices.Uses(LocalizationWristbandController, "Makes API calls to");
            containerDiagram.IoTdevices.Uses(ScaleController, "Makes API calls to");
            containerDiagram.IoTdevices.Uses(WeatherSensorController, "Makes API calls to");

            LocalizationWristbandController.Uses(IotAssetTrackingApplicationService, "[JDBC]", "");
            IotAssetTrackingApplicationService.Uses(LocalizationWristbandRepository, "[JDBC]", "");
            IotAssetTrackingApplicationService.Uses(TrackingDomainLayer, "[JDBC]", "");
            LocalizationWristbandRepository.Uses(containerDiagram.Database, "[JDBC]", "");

            ScaleController.Uses(IotAssetScaleApplicationService, "[JDBC]", "");
            IotAssetScaleApplicationService.Uses(ScaleRepository, "[JDBC]", "");
            IotAssetScaleApplicationService.Uses(ScaleDomainLayer, "[JDBC]", "");
            ScaleRepository.Uses(containerDiagram.Database, "[JDBC]", "");

            WeatherSensorController.Uses(IotAssetWeatherSensorApplicationService, "[JDBC]", "");
            IotAssetWeatherSensorApplicationService.Uses(WeatherSensorRepository, "[JDBC]", "");
            IotAssetWeatherSensorApplicationService.Uses(WeatherSensorDomainLayer, "[JDBC]", "");
            WeatherSensorRepository.Uses(containerDiagram.Database, "[JDBC]", "");
        }
        private void ApplyStyles()
        {
            LocalizationWristbandController.AddTags(this.componentTag);
            IotAssetTrackingApplicationService.AddTags(this.componentTag);
            LocalizationWristbandRepository.AddTags(this.componentTag);
            TrackingDomainLayer.AddTags(this.componentTag);
            ScaleController.AddTags(this.componentTag);
            ScaleRepository.AddTags(this.componentTag);
            ScaleDomainLayer.AddTags(this.componentTag);
            IotAssetScaleApplicationService.AddTags(this.componentTag);
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
            componentView.Add(LocalizationWristbandController);
            componentView.Add(IotAssetTrackingApplicationService);
            componentView.Add(LocalizationWristbandRepository);
            componentView.Add(TrackingDomainLayer);
            componentView.Add(ScaleController);
            componentView.Add(ScaleRepository);
            componentView.Add(ScaleDomainLayer);
            componentView.Add(IotAssetScaleApplicationService);
            componentView.Add(WeatherSensorController);
            componentView.Add(WeatherSensorRepository);
            componentView.Add(WeatherSensorDomainLayer);
            componentView.Add(IotAssetWeatherSensorApplicationService);
        }
    }
}
