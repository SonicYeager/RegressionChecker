using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using System;
using RegressionCheckerLogic;
using System.Runtime;

namespace RegressionChecker
{
    public static class RegressionCheckerApp
    {
        // Avalonia configuration, don't remove; also used by visual designer.
        public static AppBuilder BuildAvaloniaApp() => AppBuilder.Configure<App>().UsePlatformDetect().LogToTrace();
        public static void Init(string[] args, OnReady onReadyDelegate)
        {
            // Initialization code. Don't use any Avalonia, third-party APIs or any
            // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
            // yet and stuff might break.
            onReady += onReadyDelegate;
            AppBuilder = BuildAvaloniaApp();
            AppBuilder.Start(AppMain, args);
        }

        static void AppMain(Application app, string[] args)
        {
            // Do you startup code here
            MainUI = onReady?.Invoke();
            MainUI?.Show();
            Application = app;

            // Start the main loop
            app.Run(MainUI);
        }

        public static Window? MainUI { get; set; }
        public static AppBuilder? AppBuilder { get; set; }
        public static Application? Application { get; set; }

        public delegate Window OnReady();
        public static event OnReady? onReady;
    }

    static class Program
    {
        public static IMainUI? MainUI { get; set; }
        public static IChartWrapperUI? ChartWrapperUI { get; set; }
        public static ISingleSelectFileOverviewUI? SingleSelectFileOverviewUI { get; set; }
        public static ISingleSelectionOverviewAutomaticAddUI? SingleSelectionOverviewAutomaticAddUI { get; set; }
        public static IMultiSelectFileOverviewUI? MultiSelectFileOverviewUI { get; set; }
        public static ICommandParser? CommandParser { get; set; }
        public static ICSVFileReader? CSVFileReader { get; set; }
        public static IDataConverter? DataConverter { get; set; }
        public static IExternalProgrammLauncher? ExternalProgrammLauncher { get; set; }
        public static IMainController? MainController { get; set; }
        public static IChartWrapperController? ChartWrapperController { get; set; }
        public static IMultiSelectFileOverviewController? MultiSelectFileOverviewController { get; set; }
        public static ISingleSelectFileOverviewController? SingleSelectFileOverviewController { get; set; }
        public static ISingleSelectionOverviewAutomaticAddController? SingleSelectionOverviewAutomaticAddController { get; set; }
        public static List<string> Args { get; set; } = new List<string>();
        public static Thread RunThread { get; set; }

        public static void Main(string[] args)
        {
            GCSettings.LatencyMode = GCLatencyMode.Interactive;
            Args.AddRange(args);
            RegressionCheckerApp.Init(args, AppInit);
        }

        private static Window AppInit()
        {
            MainUI = new MainUI();
            ChartWrapperUI = ((MainUI)MainUI).ChartWrapperUI;
            SingleSelectFileOverviewUI = ((MainUI)MainUI).SingleSelectFileOverviewUI;
            SingleSelectionOverviewAutomaticAddUI = ((MainUI)MainUI).SingleSelectionOverviewAutomaticAddUI;
            MultiSelectFileOverviewUI = ((MainUI)MainUI).MultiSelectFileOverviewUI;
            CommandParser = new CommandParser();
            CSVFileReader = new CSVFileReader();
            DataConverter = new DataConverter();
            ExternalProgrammLauncher = new ExternalProgrammLauncher();
            ChartWrapperController = new ChartWrapperController(DataConverter, CSVFileReader, ExternalProgrammLauncher, ChartWrapperUI);
            MultiSelectFileOverviewController = new MultiSelectFileOverviewController( DataConverter, CSVFileReader, ExternalProgrammLauncher, MultiSelectFileOverviewUI);
            SingleSelectFileOverviewController = new SingleSelectFileOverviewController(DataConverter, CSVFileReader, ExternalProgrammLauncher, SingleSelectFileOverviewUI);
            SingleSelectionOverviewAutomaticAddController = new SingleSelectionOverviewAutomaticAddController(DataConverter, CSVFileReader, ExternalProgrammLauncher,SingleSelectionOverviewAutomaticAddUI);
            MainController = new MainController(
                SingleSelectFileOverviewController, 
                MultiSelectFileOverviewController, 
                SingleSelectionOverviewAutomaticAddController, 
                ChartWrapperController,
                MainUI, 
                CommandParser,
                DataConverter,
                CSVFileReader,
                ExternalProgrammLauncher);

            RunThread = new Thread(() => {
                MainController.Run(Args, () => { Environment.Exit(0); });
            });
            RunThread.IsBackground = true;
            RunThread.Start();

            return (MainUI)MainUI;
        }
    }
}
