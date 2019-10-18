using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Music_Player.ViewModels;
using MusicPlayer.Data.BaseTypes;
using MusicPlayer.Data.Interfaces;
using MusicPlayer.Logic.Interfaces;
using WMPLib;

namespace MusicPlayer.Test
{
    [TestClass]
    public class PlaylistViewModelTest
    {
        public TestContext TestContext { get; set; }

        [TestMethod]
        public void TestPlayPlaylistCommand()
        {
            var mockMusicController = new Mock<IMusicController>();
            mockMusicController.SetupGet(controller => controller.PlaylistCollection).Returns(new List<IPlaylist>());

            var mockMediaPlaylist = new Mock<IWMPPlaylist>();
            mockMediaPlaylist.Setup(playlist => playlist.count).Returns(5);

            var mockPlaylist = new Mock<IPlaylist>();
            mockPlaylist.SetupGet(playlist => playlist.MediaPlaylist).Returns(mockMediaPlaylist.Object);

            var mainViewModel = new MainWindowViewModel(mockMusicController.Object);
            var playlistViewModel = mainViewModel.PlaylistViewModel;

            playlistViewModel.PlayPlaylistCommand.Execute(mockPlaylist.Object);

            mockMusicController.VerifySet(x => x.CurrentPlaylist = It.IsAny<IPlaylist>(), Times.Once);
        }

        [TestMethod]
        public void TestCannotPlayEmptyPlaylistCommand()
        {
            var mockMusicController = new Mock<IMusicController>();
            mockMusicController.SetupGet(controller => controller.PlaylistCollection).Returns(new List<IPlaylist>());

            var mockMediaPlaylist = new Mock<IWMPPlaylist>();
            mockMediaPlaylist.Setup(playlist => playlist.count).Returns(0);

            var mockPlaylist = new Mock<IPlaylist>();
            mockPlaylist.SetupGet(playlist => playlist.MediaPlaylist).Returns(mockMediaPlaylist.Object);

            var mainViewModel = new MainWindowViewModel(mockMusicController.Object);
            var playlistViewModel = mainViewModel.PlaylistViewModel;

            playlistViewModel.PlayPlaylistCommand.Execute(mockPlaylist.Object);

            mockMusicController.VerifySet(x => x.CurrentPlaylist = It.IsAny<IPlaylist>(), Times.Never);
        }

        [TestMethod]
        public void TestOpenAddToPlaylistCommand()
        {
            var mockMusicController = new Mock<IMusicController>();
            mockMusicController.SetupGet(controller => controller.PlaylistCollection).Returns(new List<IPlaylist>());

            var mainViewModel = new MainWindowViewModel(mockMusicController.Object);
            var playlistViewModel = mainViewModel.PlaylistViewModel;

            var addToPlaylist = playlistViewModel.AddingSongToPlaylist;

            playlistViewModel.OpenAddToPlaylistCommand.Execute(null);

            Assert.IsTrue(playlistViewModel.AddingSongToPlaylist != addToPlaylist);
        }

        [TestMethod]
        public void TestAddSongToPlaylistCommand()
        {
            var mockMusicController = new Mock<IMusicController>();
            mockMusicController.SetupGet(controller => controller.PlaylistCollection).Returns(new List<IPlaylist>());

            var mockPlaylist = new Mock<IPlaylist>();
            mockPlaylist.SetupGet(playlist => playlist.Name).Returns("Mock Playlist");
            mockPlaylist.SetupGet(playlist => playlist.MediaPlaylist.count).Returns(5);

            var mainViewModel = new MainWindowViewModel(mockMusicController.Object);
            var playlistViewModel = mainViewModel.PlaylistViewModel;

            playlistViewModel.AddToPlaylistCommand.Execute(mockPlaylist.Object);

            mockMusicController.Verify(x => x.SetUpPlaylist(mockPlaylist.Object.Name, It.IsAny<Song[]>()), Times.Once);
        }

        [TestMethod]
        public void TestCanAddToPlaylist()
        {
            var mockMusicController = new Mock<IMusicController>();
            mockMusicController.SetupGet(controller => controller.PlaylistCollection).Returns(new List<IPlaylist>());

            var mockPlaylist = new Mock<IPlaylist>();
            mockPlaylist.SetupGet(playlist => playlist.Name).Returns("Playlist");

            var mainViewModel = new MainWindowViewModel(mockMusicController.Object);
            var playlistViewModel = mainViewModel.PlaylistViewModel;

            playlistViewModel.AddingSongToPlaylist = true;

            playlistViewModel.SelectedPlaylistChangedCommand.Execute(mockPlaylist.Object);

            Assert.IsTrue(playlistViewModel.CanAddToSelectedPlaylist);
        }

        [TestMethod]
        public void TestCantAddToAllSongsPlaylist()
        {
            var mockMusicController = new Mock<IMusicController>();
            mockMusicController.SetupGet(controller => controller.PlaylistCollection).Returns(new List<IPlaylist>());

            var mockPlaylist = new Mock<IPlaylist>();
            mockPlaylist.SetupGet(playlist => playlist.Name).Returns("All songs");

            var mainViewModel = new MainWindowViewModel(mockMusicController.Object);
            var playlistViewModel = mainViewModel.PlaylistViewModel;

            playlistViewModel.AddingSongToPlaylist = true;

            playlistViewModel.SelectedPlaylistChangedCommand.Execute(mockPlaylist.Object);

            Assert.IsTrue(!playlistViewModel.CanAddToSelectedPlaylist);
        }

        [TestMethod]
        public void TestEmptyPlaylistSelectedShowsNoSongs()
        {
            var mockMusicController = new Mock<IMusicController>();
            mockMusicController.SetupGet(controller => controller.PlaylistCollection).Returns(new List<IPlaylist>());

            var mainViewModel = new MainWindowViewModel(mockMusicController.Object);
            var playlistViewModel = mainViewModel.PlaylistViewModel;

            playlistViewModel.AddingSongToPlaylist = false;
            playlistViewModel.CurrentPlaylistSongCollection = new ObservableCollection<Song>()
            {
                new Song(string.Empty),
                new Song(string.Empty)
            };

            playlistViewModel.SelectedPlaylistChangedCommand.Execute(null);

            Assert.IsTrue(playlistViewModel.CurrentPlaylistSongCollection.Count == 0);
        }
    }
}
