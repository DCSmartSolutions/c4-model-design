using Structurizr;

namespace c4_model_design
{
    public class ContainerDiagram
    {
        private readonly C4 c4;
        private readonly ContextDiagram contextDiagram;
        public Container MobileApplication { get; set; }
        public Container WebApplication { get; set; }
        public Container ApiRest { get; set; }
        public Container LandingPage { get; set; }
        public Container IoTdevices { get; set; }
        public Container Database { get; set; }

        public ContainerDiagram(C4 c4, ContextDiagram contextDiagram)
        {
            this.c4 = c4;
            this.contextDiagram = contextDiagram;
        }

        public void Generate()
        {
            AddContainers();
            AddRelationships();
            ApplyStyles();
            CreateView();
        }

        private void AddContainers()
        {
            WebApplication = contextDiagram.LifeTravelSystem.AddContainer("Web App", "Allows tourists to purchase package tours and public agencies to purchase package tours.", "Angular");
            IoTdevices = contextDiagram.LifeTravelSystem.AddContainer(" IoT Embedded application", "", "Arduino");
            LandingPage = contextDiagram.LifeTravelSystem.AddContainer("Landing Page", "", "HTML/CSS/JS");
            ApiRest = contextDiagram.LifeTravelSystem.AddContainer("API REST", "API REST", "Java (Spring Boot) port 8080");
            Database = contextDiagram.LifeTravelSystem.AddContainer("DB", "", "MySQL Server RDS AWS");
        }

        private void AddRelationships()
        {
            contextDiagram.Turista.Uses(WebApplication, "Consult");
            contextDiagram.Turista.Uses(LandingPage, "Consult");

            contextDiagram.Agencia.Uses(WebApplication, "Consult");
            contextDiagram.Agencia.Uses(LandingPage, "Consult");

            IoTdevices.Uses(ApiRest, "API Request", "JSON/HTTPS");
            WebApplication.Uses(ApiRest, "API Request", "JSON/HTTPS");

            ApiRest.Uses(Database, "", "");
            ApiRest.Uses(contextDiagram.GoogleMaps, "API Request", "JSON/HTTPS");
            ApiRest.Uses(contextDiagram.PaymentGateway, "API Request", "JSON/HTTPS");
            ApiRest.Uses(contextDiagram.FirebaseAuthentication, "API Request", "JSON/HTTPS");

        }

        private void ApplyStyles()
        {
            SetTags();
            Styles styles = c4.ViewSet.Configuration.Styles;
            styles.Add(new ElementStyle(nameof(WebApplication)) { Background = "#9d33d6", Color = "#ffffff", Shape = Shape.WebBrowser, Icon = "" });
            styles.Add(new ElementStyle(nameof(IoTdevices)) { Background = "#b8c979", Color = "#000000", Shape = Shape.WebBrowser, Icon = "" });
            styles.Add(new ElementStyle(nameof(LandingPage)) { Background = "#929000", Color = "#ffffff", Shape = Shape.WebBrowser, Icon = "" });
            styles.Add(new ElementStyle(nameof(ApiRest)) { Shape = Shape.RoundedBox, Background = "#0000ff", Color = "#ffffff", Icon = "" });
            styles.Add(new ElementStyle(nameof(Database)) { Shape = Shape.Cylinder, Background = "#ff0000", Color = "#ffffff", Icon = "" });
        }

        private void SetTags()
        {
            WebApplication.AddTags(nameof(WebApplication));
            LandingPage.AddTags(nameof(LandingPage));
            ApiRest.AddTags(nameof(ApiRest));
            Database.AddTags(nameof(Database));
            IoTdevices.AddTags(nameof(IoTdevices));
        }

        private void CreateView()
        {
            ContainerView containerView = c4.ViewSet.CreateContainerView(contextDiagram.LifeTravelSystem, "Container", "Container Diagram");
            containerView.AddAllElements();
        }
    }
}