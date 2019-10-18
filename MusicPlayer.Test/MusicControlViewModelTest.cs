using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Music_Player.ViewModels;
using MusicPlayer.Logic.Interfaces;

namespace MusicPlayer.Test
{
    [TestClass]
    public class MusicControlViewModelTest
    {
        [TestMethod]
        public void TestPlayPauseCommand()
        {
            var mockMusicController = new Mock<IMusicController>();

            var mainViewModel = this.SetupMainViewModel(mockMusicController);
            var songViewModel = mainViewModel.MusicControlViewModel;

            var startingPlayState = songViewModel.IsPlaying;

            songViewModel.PlayPauseCommand.Execute(null);

            Assert.IsTrue(songViewModel.IsPlaying != startingPlayState);

            if (songViewModel.IsPlaying)
            {
                mockMusicController.Verify(x => x.Play(), Times.Once);
                mockMusicController.Verify(x => x.Pause(), Times.Never);
            }
            else
            {
                mockMusicController.Verify(x => x.Pause(), Times.Once);
                mockMusicController.Verify(x => x.Play(), Times.Never);
            }

            songViewModel.PlayPauseCommand.Execute(null);

            Assert.IsTrue(songViewModel.IsPlaying == startingPlayState);

            if (songViewModel.IsPlaying)
            {
                mockMusicController.Verify(x => x.Play(), Times.Once);
            }
            else
            {
                mockMusicController.Verify(x => x.Pause(), Times.Once);
            }
        }

        [TestMethod]
        public void TestNextSongCommand()
        {
            var mockMusicController = new Mock<IMusicController>();

            var mainViewModel = this.SetupMainViewModel(mockMusicController);
            var musicControlViewModel = mainViewModel.MusicControlViewModel;

            musicControlViewModel.NextSongCommand.Execute(null);

            mockMusicController.Verify(x => x.NextSong(), Times.Once);
        }

        [TestMethod]
        public void TestPreviousSongCommand()
        {
            var mockMusicController = new Mock<IMusicController>();

            var mainViewModel = this.SetupMainViewModel(mockMusicController);
            var musicControlViewModel = mainViewModel.MusicControlViewModel;

            musicControlViewModel.PreviousSongCommand.Execute(null);

            mockMusicController.Verify(x => x.PreviousSong(), Times.Once);
        }

        [TestMethod]
        public void TestShuffleSongsCommand()
        {
            var mockMusicController = new Mock<IMusicController>();

            var mainViewModel = this.SetupMainViewModel(mockMusicController);
            var musicControlViewModel = mainViewModel.MusicControlViewModel;

            var startingShuffle = musicControlViewModel.Shuffle;

            musicControlViewModel.ShuffleSongsCommand.Execute(null);

            mockMusicController.Verify(x => x.SetShuffle(!startingShuffle), Times.Once);

            musicControlViewModel.ShuffleSongsCommand.Execute(null);

            mockMusicController.Verify(x => x.SetShuffle(startingShuffle), Times.Once);
        }

        private MainWindowViewModel SetupMainViewModel(IMock<IMusicController> musicController)
        {
            return new MainWindowViewModel(musicController.Object);
        }
    }
}
