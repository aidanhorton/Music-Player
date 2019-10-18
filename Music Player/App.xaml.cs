using System.Windows;
using Microsoft.Win32;
using Music_Player.ViewModels;
using Music_Player.Views;
using MusicPlayer.Logic.Interfaces;
using MusicPlayer.Logic.Models;
using Unity;

namespace Music_Player
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            this.InstantiateService();

            var viewModel = this._container.Resolve<MainWindowViewModel>();
            var window = new MainWindow { DataContext = viewModel };
            window.Show();
        }

        public void InstantiateService()
        {
            this._container = new UnityContainer();

            this.RegisterInterfaces();
        }

        private void RegisterInterfaces()
        {
            this._container.RegisterInstance<IMusicController>(new MusicController());
        }

        private IUnityContainer _container;
    }
}
