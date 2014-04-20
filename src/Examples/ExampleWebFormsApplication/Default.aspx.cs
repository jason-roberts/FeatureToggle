using System;

namespace ExampleWebFormsApplication
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Feature2Button.Visible = new Feature2Toggle().FeatureEnabled;

            NewYears2000FeatureButton.Visible = new NewYears2000Feature().FeatureEnabled;

            NewYears3000FeatureButton.Visible = new NewYears3000Feature().FeatureEnabled;
        }
    }
}