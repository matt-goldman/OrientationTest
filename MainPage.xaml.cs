namespace OrientationTest
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        private double _screenWidth;
        public double ScreenWidth
        {
            get => _screenWidth;
            set
            {
                _screenWidth = value;
                OnPropertyChanged();
            }
        }

        private double _boxViewWidth;
        public double BoxViewWidth
        {
            get => _boxViewWidth;
            set
            {
                _boxViewWidth = value;
                OnPropertyChanged();
            }
        }

        public MainPage()
        {
            InitializeComponent();
            BindingContext = this;
        }

        private void OnCounterClicked(object sender, EventArgs e)
        {
            count++;

            if (count == 1)
                CounterBtn.Text = $"Clicked {count} time";
            else
                CounterBtn.Text = $"Clicked {count} times";

            SemanticScreenReader.Announce(CounterBtn.Text);
        }

        private void StateTrigger_IsActiveChanged(object sender, EventArgs e)
        {
            var trigger = (StateTriggerBase)sender;

            if (trigger.IsActive)
            {
                OrientationLabel.Text = "Orientation: Portrait";
            }
            else
            {
                OrientationLabel.Text = "Orientation: Landscape";
            }

            Console.WriteLine($"[OrientationState] Portrait: {trigger.IsActive}");
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            ScreenWidth = width;
            BoxViewWidth = width - 400;
        }
    }

}
