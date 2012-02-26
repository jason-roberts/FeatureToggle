namespace JasonRoberts.FeatureToggle
{
    public abstract class SqlFeatureToggle : IFeatureToggle
    {
        protected SqlFeatureToggle()
        {
            ToggleValueProvider = new BooleanSqlServerProvider();
        }


        public IBooleanToggleValueProvider ToggleValueProvider { get; set; }


        public bool FeatureEnabled
        {
            get { return ToggleValueProvider.EvaluateBooleanToggleValue(this); }
        }

    }
}
