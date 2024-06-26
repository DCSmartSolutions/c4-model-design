using Structurizr;
using Structurizr.Api;

namespace c4_model_design
{
	public class C4
	{
		private readonly long workspaceId = 86615;
		private readonly string apiKey = "0f7985aa-32d2-41d8-b10e-fb86320bbc18";
		private readonly string apiSecret = "e1323d58-da1a-483b-b05e-98edd10a6d04";

		public StructurizrClient StructurizrClient { get; }
		public Workspace Workspace { get; }
		public Model Model { get; }
		public ViewSet ViewSet { get; }

		public C4()
		{
			string workspaceName = "Software Design & Patterns - C4 Model - LifeTravel";
			string workspaceDescription = "Tourist package management system for agencies with Iot devices";
			StructurizrClient = new StructurizrClient(apiKey, apiSecret);
			Workspace = new Workspace(workspaceName, workspaceDescription);
			Model = Workspace.Model;
			ViewSet = Workspace.Views;
		}

		public void Generate() {
			ContextDiagram contextDiagram = new ContextDiagram(this);
			ContainerDiagram containerDiagram = new ContainerDiagram(this, contextDiagram);
            IdentityBCComponentDiagram identityAccessComponentDiagram = new IdentityBCComponentDiagram(this, containerDiagram, contextDiagram);
            SubscriptionAndPaymentDCComponentDiagram subscriptionAndPaymentDCComponentDiagram = new SubscriptionAndPaymentDCComponentDiagram(this, containerDiagram, contextDiagram);
            NotificationDCComponentDiagram notificationDCComponentDiagram = new NotificationDCComponentDiagram(this, containerDiagram, contextDiagram, identityAccessComponentDiagram);
			ReviewBCComponentDiagram reviewComponentDiagram = new ReviewBCComponentDiagram(this, containerDiagram, contextDiagram, identityAccessComponentDiagram);
			TourExperienceBCComponentDiagram tourExperienceBCComponentDiagram = new TourExperienceBCComponentDiagram(this, containerDiagram, contextDiagram, identityAccessComponentDiagram);
            TransportationComponentDiagram transportationComponentDiagram = new TransportationComponentDiagram(this, containerDiagram, contextDiagram, identityAccessComponentDiagram, tourExperienceBCComponentDiagram);
            BookingComponent bookingComponentDiagram = new BookingComponent(this, containerDiagram, contextDiagram, identityAccessComponentDiagram, transportationComponentDiagram, tourExperienceBCComponentDiagram, notificationDCComponentDiagram);
            ProfileComponent profileComponentDiagram = new ProfileComponent(this, containerDiagram, contextDiagram, identityAccessComponentDiagram);
            IotAssetManagementComponent iotAssetManagementComponent = new IotAssetManagementComponent(this, containerDiagram, contextDiagram);
			DataReportAndAnalyticsComponentDiagram dataReport = new DataReportAndAnalyticsComponentDiagram(this, containerDiagram, contextDiagram, iotAssetManagementComponent, notificationDCComponentDiagram);

            contextDiagram.Generate();
			containerDiagram.Generate();
			identityAccessComponentDiagram.Generate();
            subscriptionAndPaymentDCComponentDiagram.Generate();
			notificationDCComponentDiagram.Generate();
            reviewComponentDiagram.Generate();
			tourExperienceBCComponentDiagram.Generate();
            transportationComponentDiagram.Generate();
            bookingComponentDiagram.Generate();
            profileComponentDiagram.Generate();
            iotAssetManagementComponent.Generate();
			dataReport.Generate();
            StructurizrClient.PutWorkspace(workspaceId, Workspace);
		}
	}
}