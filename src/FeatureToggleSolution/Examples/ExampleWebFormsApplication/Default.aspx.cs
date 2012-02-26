using System;

namespace ExampleWebFormsApplication
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Feature2Button.Visible = new Feature2Toggle().FeatureEnabled;
        }
    }
}