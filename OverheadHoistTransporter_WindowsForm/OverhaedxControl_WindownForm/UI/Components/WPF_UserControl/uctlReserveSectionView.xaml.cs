using com.mirle.ibg3k0.ohxc.winform.App;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace com.mirle.ibg3k0.bc.winform.UI.Components.WPFComponents
{
    /// <summary>
    /// uctlReserveSectionView.xaml 的互動邏輯
    /// </summary>
    public partial class uctlReserveSectionView : UserControl, INotifyPropertyChanged
    {
        WindownApplication app = null;
        public event PropertyChangedEventHandler PropertyChanged;
        // Create the OnPropertyChanged method to raise the event
        protected virtual void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public uctlReserveSectionView()
        {
            InitializeComponent();
            DataContext = this;
            refresh_sw.Start();
        }

        #region Methods
        DispatcherTimer _timer = new DispatcherTimer();
        public void Start(WindownApplication _app)
        {
            app = _app;
            //宣告Timer

            //設定呼叫間隔時間為30ms
            _timer.Interval = TimeSpan.FromMilliseconds(5000);

            //加入callback function
            _timer.Tick += _timer_Tick;

            //開始
            _timer.Start();
        }

        public void Stop()
        {
            _timer.Stop();
            _timer = null;
        }

        private void _timer_Tick(object sender, EventArgs e)
        {
            RefreshReserveSectionInfo();
        }
        #endregion Methods


        #region Display
        private BitmapSource _mapBitmapSource;


        public virtual BitmapSource MapBitmapSource
        {
            get
            {
                return _mapBitmapSource;
            }
            set
            {
                if (_mapBitmapSource != value)
                {
                    _mapBitmapSource = value;
                    OnPropertyChanged();
                }
            }
        }

        System.Diagnostics.Stopwatch refresh_sw { get; set; } = new System.Diagnostics.Stopwatch();
        private long syncRefreshReserveSectionInfo = 0;
        public async void RefreshReserveSectionInfo()
        {
            if (System.Threading.Interlocked.Exchange(ref syncRefreshReserveSectionInfo, 1) == 0)
            {
                try
                {

                    Console.WriteLine(refresh_sw.ElapsedMilliseconds);
                    refresh_sw.Restart();
                    var Bitmap = await app.MapBLL.GetReserveInfoFromHttpAsync();
                    MapBitmapSource = Bitmap;
                }
                finally
                {
                    System.Threading.Interlocked.Exchange(ref syncRefreshReserveSectionInfo, 0);
                }
            }

        }

        #endregion

        private void ToolBar_Click(object sender, RoutedEventArgs e)
        {
            RefreshReserveSectionInfo();
        }
    }
}
