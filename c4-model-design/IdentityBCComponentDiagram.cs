using Structurizr;
using System;
using System.Collections.Generic;
using System.Text;

namespace c4_model_design
{
    internal class IdentityBCComponentDiagram
    {
        private readonly C4 c4;
        private readonly ContainerDiagram containerDiagram;
        private readonly ContextDiagram contextDiagram;
        private readonly string componentTag = "Component";
        public Component SignInController { get; set; }
        public Component SignInApplicationService { get; set; }
        public Component SignUpController { get; set; }
        public Component SignUpApplicationService { get; set; }
        public Component UserRepository { get; set; }
        public Component FirebaseAdapter { get; set; }
        public Component IdentityDomainLayer { get; set; }
        public IdentityBCComponentDiagram(C4 c4, ContainerDiagram containerDiagram, ContextDiagram contextDiagram)
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
            SignInController = containerDiagram.ApiRest.AddComponent("Sign In Controller", "", "Java");
            SignInApplicationService = containerDiagram.ApiRest.AddComponent("Sign In Application Service", "", "Java");
            SignUpController = containerDiagram.ApiRest.AddComponent("Sign Up Controller", "", "Java");
            SignUpApplicationService = containerDiagram.ApiRest.AddComponent("Sign Up Application Service", "", "Java");
            UserRepository = containerDiagram.ApiRest.AddComponent("User Repository", "", "Java");
            FirebaseAdapter = containerDiagram.ApiRest.AddComponent("Firebase Adapter", "", "Java");
            IdentityDomainLayer = containerDiagram.ApiRest.AddComponent("Identity Domain Layer", "", "Java");
        }
        private void AddRelationships()
        {
            containerDiagram.WebApplication.Uses(SignInController, "Makes API calls to");
            containerDiagram.MobileApplication.Uses(SignInController, "Makes API calls to");
            containerDiagram.WebApplication.Uses(SignUpController, "Makes API calls to");
            containerDiagram.MobileApplication.Uses(SignUpController, "Makes API calls to");
            SignInController.Uses(FirebaseAdapter, "[JDBC]", "");
            SignUpController.Uses(FirebaseAdapter, "[JDBC]", "");
            SignInController.Uses(SignInApplicationService, "[JDBC]", "");
            SignInApplicationService.Uses(UserRepository, "[JDBC]", "");
            SignInApplicationService.Uses(IdentityDomainLayer, "[JDBC]", "");
            SignUpController.Uses(SignUpApplicationService, "[JDBC]", "");
            SignUpApplicationService.Uses(UserRepository, "[JDBC]", "");
            SignUpApplicationService.Uses(IdentityDomainLayer, "[JDBC]", "");
            UserRepository.Uses(containerDiagram.Database, "Use", "");
            FirebaseAdapter.Uses(contextDiagram.FirebaseAuthentication, "Use", "");
        }
        private void ApplyStyles()
        {
            Styles styles = c4.ViewSet.Configuration.Styles;
            styles.Add(new ElementStyle(this.componentTag) { Shape = Shape.Component, Background = "#facc2e", Icon = "" });
            SignInController.AddTags(this.componentTag);
            SignInApplicationService.AddTags(this.componentTag);
            SignUpController.AddTags(this.componentTag);
            SignUpApplicationService.AddTags(this.componentTag);
            UserRepository.AddTags(this.componentTag);
            FirebaseAdapter.AddTags(this.componentTag);
            IdentityDomainLayer.AddTags(this.componentTag);
        }
        private void CreateView()
        {
            string title = "Identity and Access DC Component Diagram";
            ComponentView componentView = c4.ViewSet.CreateComponentView(containerDiagram.ApiRest, title, title);
            componentView.Title = title;
            componentView.Add(containerDiagram.WebApplication);
            componentView.Add(containerDiagram.Database);
            componentView.Add(containerDiagram.MobileApplication);
            componentView.Add(this.SignInController);
            componentView.Add(this.SignInApplicationService);
            componentView.Add(this.SignUpController);
            componentView.Add(this.SignUpApplicationService);
            componentView.Add(this.UserRepository);
            componentView.Add(this.FirebaseAdapter);
            componentView.Add(this.IdentityDomainLayer);
            componentView.Add(contextDiagram.FirebaseAuthentication);
        }
    }
}
