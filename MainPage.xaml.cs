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
            
            if (DeviceInfo.Current.Platform == DevicePlatform.iOS && DeviceInfo.Current.Idiom == DeviceIdiom.Tablet)
            {
                //DeviceDisplay.MainDisplayInfoChanged += OnSizeChanged;
                this.SizeChanged += OnSizeChanged;
            }
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
        
        private void OnSizeChanged(object? sender, EventArgs e)
        {
            Console.WriteLine("[OrientationState] OnSizeChanged");
            
            Console.WriteLine($"[OrientationState] Current dimensions - Height: {this.Height}, Width: {this.Width}");
            
            if (this.Width > this.Height)
            {
                Console.WriteLine("[OrientationState] Going Landscape");
                VisualStateManager.GoToState(LayoutGrid, "Landscape");
                VisualStateManager.GoToState(BV, "Landscape");
            }
            else
            {
                Console.WriteLine("[OrientationState] Going Portrait");
                VisualStateManager.GoToState(LayoutGrid, "Portrait");
                VisualStateManager.GoToState(BV, "Portrait");
            }
        }
    }

}
