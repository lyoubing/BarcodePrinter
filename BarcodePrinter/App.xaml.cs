using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows;

namespace BarcodePrinter
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        public static BaseSettings Settings { get; private set; }
        public App()
        {
            this.Startup += App_Startup;
            this.Exit += App_Exit;
            this.DispatcherUnhandledException += App_DispatcherUnhandledException;
        }

        private void App_Exit(object sender, ExitEventArgs e)
        {
        }

        private void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            e.Handled = true;
            MessageBox.Show(string.Format("程序运行故障:{0}", e.Exception.Message));
            Logger.ErrorLoger.Log(e.Exception.ToString());
        }

        private void App_Startup(object sender, StartupEventArgs e)
        {
            try
            {
                var processName = System.Diagnostics.Process.GetCurrentProcess().ProcessName;
                var process = System.Diagnostics.Process.GetProcessesByName(processName);
                if (process.Length > 1)
                {
                    MessageBox.Show("已经有一个实例在运行!");
                    Shutdown();
                    return;
                }
                StartupUri = new Uri("MainWindow.xaml", UriKind.Relative);
                ShutdownMode = ShutdownMode.OnLastWindowClose;

                Settings = BaseSettings.Load();
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("程序运行故障:{0}", ex.Message));
                Logger.ErrorLoger.Log(ex.ToString());
                Shutdown();
            }
        }
    }
}
