﻿using System.Windows;
using System.Windows.Controls;
using GitHub.VisualStudio.TeamExplorerConnect;
using GitHub.UI.Helpers;

namespace GitHub.VisualStudio.UI.Views
{
    /// <summary>
    /// This is a temporary placeholder class until the VS Connect section
    /// is done
    /// </summary>
    public partial class GitHubConnectContent : UserControl
    {
        public GitHubConnectContent()
        {
            SharedDictionaryManager.Load("GitHub.VisualStudio", Resources);
            SharedDictionaryManager.Load("GitHub.UI", Resources);
            InitializeComponent();
        }

        void cloneLink_Click(object sender, RoutedEventArgs e)
        {
            cloneLink.IsEnabled = false;
            ViewModel.DoClone();
            cloneLink.IsEnabled = true;
        }

        void createLink_Click(object sender, RoutedEventArgs e)
        {
            createLink.IsEnabled = false;
            ViewModel.DoCreate();
            createLink.IsEnabled = true;
        }

        void signOut_Click(object sender, RoutedEventArgs e)
        {
            signOut.IsEnabled = false;
            ViewModel.SignOut();
            signOut.IsEnabled = true;
        }

        public GitHubConnectSection ViewModel
        {
            get { return (GitHubConnectSection)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }

        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register(
                "ViewModel",
                typeof(GitHubConnectSection),
                typeof(GitHubConnectContent));
    }
}
