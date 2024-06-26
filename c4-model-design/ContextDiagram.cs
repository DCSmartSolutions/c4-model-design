using Structurizr;

namespace c4_model_design
{
	public class ContextDiagram
	{
		private readonly C4 c4;
		public SoftwareSystem LifeTravelSystem { get; private set; }
		public SoftwareSystem GoogleMaps { get; private set; }
		public SoftwareSystem FirebaseAuthentication { get; private set; }
		public SoftwareSystem PaymentGateway { get; private set; }
		public Person Turista { get; private set; }
		public Person Agencia { get; private set; }

		public ContextDiagram(C4 c4)
		{
			this.c4 = c4;
		}

		public void Generate() {
			AddElements();
			AddRelationships();
			ApplyStyles();
			CreateView();
		}

		private void AddElements() {
			AddPeople();
			AddSoftwareSystems();
		}

		private void AddPeople()
		{
            Turista = c4.Model.AddPerson("Tourist", "Peruvian citizen.");
            Agencia = c4.Model.AddPerson("Agency", "Peruvian company.");
		}

		private void AddSoftwareSystems()
		{
			LifeTravelSystem = c4.Model.AddSoftwareSystem("Tourist package management system for agencies with Iot devices", "Allows tracking and monitoring of tour packages sold by agencies and purchased by tourists. Allows the geolocation of tourists during tours, real-time weather monitoring of tour locations and reallocation of transportation.");
			GoogleMaps = c4.Model.AddSoftwareSystem("Google Maps", "Platform that offers a REST API for geo-referenced information.");
			FirebaseAuthentication = c4.Model.AddSoftwareSystem("Firebase Authentication System", "System that provides backend services, easy-to-use SDKs, and ready-made UI libraries to authenticate users to your app");
            PaymentGateway = c4.Model.AddSoftwareSystem("Payment Gateway API", "API operates to integrate a payment solution with another existing application, and connect a LifeTravel's checkout function to the payment system");
		}

		private void AddRelationships() {
			Turista.Uses(LifeTravelSystem, "Make inquiries to be aware of the tour packages that he has purchased.");
            Agencia.Uses(LifeTravelSystem, "Make inquiries about his sold tour packages");

			LifeTravelSystem.Uses(FirebaseAuthentication, "Use system for user authentication");
			LifeTravelSystem.Uses(GoogleMaps, "Use Google maps API");
			LifeTravelSystem.Uses(PaymentGateway, "Use Payment Gateway API");
		}

		private void ApplyStyles() {
			SetTags();

			Styles styles = c4.ViewSet.Configuration.Styles;

			styles.Add(new ElementStyle(nameof(Turista))
			{
				Background = "#0a60ff",
				Color = "#ffffff",
				Shape = Shape.Person,
				FontSize = 20
			});

            styles.Add(new ElementStyle(nameof(Agencia)) { Background = "#aa60af", Color = "#ffffff", Shape = Shape.Person, FontSize=20 });

			styles.Add(new ElementStyle(nameof(LifeTravelSystem)) { Background = "#008f39", Color = "#ffffff", Shape = Shape.RoundedBox, FontSize=20 });
			styles.Add(new ElementStyle(nameof(GoogleMaps)) { Background = "#90714c", Color = "#ffffff", Shape = Shape.RoundedBox });
			styles.Add(new ElementStyle(nameof(FirebaseAuthentication)) { Background = "#90714c", Color = "#ffffff", Shape = Shape.RoundedBox });
			styles.Add(new ElementStyle(nameof(PaymentGateway)) { Background = "#90714c", Color = "#ffffff", Shape = Shape.RoundedBox });
		}

		private void SetTags()
		{
			Turista.AddTags(nameof(Turista));
            Agencia.AddTags(nameof(Agencia));

			LifeTravelSystem.AddTags(nameof(LifeTravelSystem));
			GoogleMaps.AddTags(nameof(GoogleMaps));
			FirebaseAuthentication.AddTags(nameof(FirebaseAuthentication));
			PaymentGateway.AddTags(nameof(PaymentGateway));
		}

		private void CreateView() {
			SystemContextView contextView = c4.ViewSet.CreateSystemContextView(LifeTravelSystem, "Context", "Context Level Diagram");
			contextView.AddAllSoftwareSystems();
			contextView.AddAllPeople();
		}
	}
}