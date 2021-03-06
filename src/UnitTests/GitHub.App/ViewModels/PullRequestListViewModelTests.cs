﻿using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Windows.Media.Imaging;
using GitHub.Collections;
using GitHub.Models;
using GitHub.Services;
using GitHub.ViewModels;
using NSubstitute;
using Xunit;

namespace UnitTests.GitHub.App.ViewModels
{
    public class PullRequestListViewModelTests : TestBaseClass
    {
        [Fact]
        public void SelectingAssigneeShouldTriggerFilter()
        {
            var repositoryHost = CreateRepositoryHost();
            var repository = Substitute.For<ISimpleRepositoryModel>();
            var prViewModel = new PullRequestListViewModel(repositoryHost, repository);

            prViewModel.Initialize(null);
            prViewModel.PullRequests.DidNotReceive().Filter = AnyFilter;

            prViewModel.SelectedAssignee = prViewModel.PullRequests[0].Assignee;
            prViewModel.PullRequests.Received(1).Filter = AnyFilter;
        }

        [Fact]
        public void ResettingAssigneeToNoneShouldNotTriggerFilter()
        {
            var repositoryHost = CreateRepositoryHost();
            var repository = Substitute.For<ISimpleRepositoryModel>();
            var prViewModel = new PullRequestListViewModel(repositoryHost, repository);

            prViewModel.Initialize(null);
            prViewModel.PullRequests.DidNotReceive().Filter = AnyFilter;

            prViewModel.SelectedAssignee = prViewModel.PullRequests[0].Assignee;
            prViewModel.PullRequests.Received(1).Filter = AnyFilter;

            // Setting the Assignee filter to [None] should not trigger a filter:
            // doing this will remove the [None] entry from Assignees, which will cause
            // the selection in the view to be set to null which will reset the filter.
            prViewModel.SelectedAssignee = prViewModel.EmptyUser;
            prViewModel.PullRequests.Received(1).Filter = AnyFilter;
        }

        [Fact]
        public void SelectingAuthorShouldTriggerFilter()
        {
            var repositoryHost = CreateRepositoryHost();
            var repository = Substitute.For<ISimpleRepositoryModel>();
            var prViewModel = new PullRequestListViewModel(repositoryHost, repository);

            prViewModel.Initialize(null);
            prViewModel.PullRequests.DidNotReceive().Filter = AnyFilter;

            prViewModel.SelectedAuthor = prViewModel.PullRequests[0].Author;
            prViewModel.PullRequests.Received(1).Filter = AnyFilter;
        }

        [Fact]
        public void ResettingAuthorToNoneShouldNotTriggerFilter()
        {
            var repositoryHost = CreateRepositoryHost();
            var repository = Substitute.For<ISimpleRepositoryModel>();
            var prViewModel = new PullRequestListViewModel(repositoryHost, repository);

            prViewModel.Initialize(null);
            prViewModel.PullRequests.DidNotReceive().Filter = AnyFilter;

            prViewModel.SelectedAuthor = prViewModel.PullRequests[0].Author;
            prViewModel.PullRequests.Received(1).Filter = AnyFilter;

            // Setting the Author filter to [None] should not trigger a filter:
            // doing this will remove the [None] entry from Authors, which will cause
            // the selection in the view to be set to null which will reset the filter.
            prViewModel.SelectedAuthor = prViewModel.EmptyUser;
            prViewModel.PullRequests.Received(1).Filter = AnyFilter;
        }

        Func<IPullRequestModel, int, IList<IPullRequestModel>, bool> AnyFilter =>
            Arg.Any<Func<IPullRequestModel, int, IList<IPullRequestModel>, bool>>();

        IRepositoryHost CreateRepositoryHost()
        {
            var result = Substitute.For<IRepositoryHost>();
            var modelService = Substitute.For<IModelService>();
            var bitmapSource = Observable.Empty<BitmapImage>();

            var pullRequest = new PullRequestModel(
                1,
                "PR1",
                new Account("foo", true, false, 1, 0, bitmapSource),
                new Account("foo", true, false, 1, 0, bitmapSource),
                DateTimeOffset.MinValue);

            var pullRequestCollection = Substitute.For<ITrackingCollection<IPullRequestModel>>();
            pullRequestCollection[0].Returns(pullRequest);

            modelService.GetPullRequests(
                Arg.Any<ISimpleRepositoryModel>(),
                Arg.Any<ITrackingCollection<IPullRequestModel>>())
               .Returns(pullRequestCollection);
            result.ModelService.Returns(modelService);

            return result;
        }
    }
}
