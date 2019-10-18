using System;
using System.Windows;

namespace Music_Player.Views
{
    /// <summary>
    /// Interaction logic for NewPlaylistDialog.xaml
    /// </summary>
    public partial class NewPlaylistDialog : Window
    {
        /// <summary>
        /// Gets the answer box text.
        /// </summary>
        public string Answer => this.AnswerBox.Text;

        public NewPlaylistDialog()
        {
            Owner = Application.Current.MainWindow;
            InitializeComponent();
        }

        /// <summary>
        /// Handles click of the OK button.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The arguments.</param>
        private void OkClicked(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        /// <summary>
        /// Handles when the window content is rendered/loaded.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The arguments.</param>
        private void Window_ContentRendered(object sender, EventArgs e)
        {
            this.AnswerBox.SelectAll();
            this.AnswerBox.Focus();
        }
    }
}
