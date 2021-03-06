﻿using System.Linq;
using PlayGen.SUGAR.Client.EvaluationEvents;
using PlayGen.SUGAR.Client.Exceptions;
using PlayGen.SUGAR.Common.Authorization;

using Xunit;

namespace PlayGen.SUGAR.Client.Tests
{
	public class AchievementClientTests : EvaluationClientTests
	{ 
		[Fact]
		public void CanDisableNotifications()
		{
			// Assign
			var key = "Achievement_CanDisableNotifications";
			var loggedInAccount = Helpers.CreateAndLoginGlobal(Fixture.SUGARClient, key);
			var game = Helpers.GetGame(Fixture.SUGARClient.Game, key);

			Fixture.SUGARClient.Achievement.EnableNotifications(true);

			EvaluationNotification notification;
			while (Fixture.SUGARClient.Achievement.TryGetPendingNotification(out notification))
			{
			}

			Fixture.SUGARClient.Achievement.EnableNotifications(false);

			CompleteGenericEvaluation(key, loggedInAccount.User.Id, game.Id);

			// Act
			var didGetnotification = Fixture.SUGARClient.Achievement.TryGetPendingNotification(out notification);

			// Assert
			Assert.False(didGetnotification);
			Assert.Null(notification);
		}

		[Fact]
		public void CanGetNotifications()
		{
			// Assign
			var key = "Achievement_CanGetNotifications";
			Helpers.Login(Fixture.SUGARClient, key, key, out var game, out var loggedInAccount);

			Fixture.SUGARClient.Achievement.EnableNotifications(true);

			CompleteGenericEvaluation(key, loggedInAccount.User.Id, game.Id);

			// Act
			var didGetnotification = false;
			EvaluationNotification gotNotification= null;
			var didGetSpecificConfiguration = false;

			while (Fixture.SUGARClient.Achievement.TryGetPendingNotification(out var notification))
			{
				didGetnotification = true;
				gotNotification = notification;
				didGetSpecificConfiguration |= notification.Name == key;
			}

			// Assert
			Assert.True(didGetnotification);
			Assert.NotNull(gotNotification);

			Assert.True(didGetSpecificConfiguration);
		}

		[Fact]
		public void DontGetAlreadyRecievedNotifications()
		{
			// Assign
			var key = "Achievement_DontGetAlreadyRecievedNotifications";
			var loggedInAccount = Helpers.CreateAndLoginGlobal(Fixture.SUGARClient, key);
			var game = Helpers.GetGame(Fixture.SUGARClient.Game, key);

			Fixture.SUGARClient.Achievement.EnableNotifications(true);

			CompleteGenericEvaluation(key, loggedInAccount.User.Id, game.Id);

			EvaluationNotification notification;
			while (Fixture.SUGARClient.Achievement.TryGetPendingNotification(out notification))
			{
			}

			CompleteGenericEvaluation(key, loggedInAccount.User.Id, game.Id);

			// Act
			var didGetSpecificConfiguration = false;
			while (Fixture.SUGARClient.Achievement.TryGetPendingNotification(out notification))
			{
				didGetSpecificConfiguration |= notification.Name == key;
			}

			// Assert
			Assert.False(didGetSpecificConfiguration);
		}

		[Fact]
		public void CanGetGlobalAchievementProgress()
		{
			var key = "Achievement_CanGetGlobalAchievementProgress";
			var loggedInAccount = Helpers.CreateAndLoginGlobal(Fixture.SUGARClient, key);

			var progressGame = Fixture.SUGARClient.Achievement.GetGlobalProgress(loggedInAccount.User.Id);
			Assert.NotEmpty(progressGame);

			var progressAchievement = Fixture.SUGARClient.Achievement.GetGlobalAchievementProgress(key, loggedInAccount.User.Id);
			Assert.Equal(0, progressAchievement.Progress);

			CompleteGenericEvaluation(key, loggedInAccount.User.Id, Platform.GlobalGameId);

			progressAchievement = Fixture.SUGARClient.Achievement.GetGlobalAchievementProgress(key, loggedInAccount.User.Id);
			Assert.True(progressAchievement.Progress >= 1);
		}

		[Fact]
		public void CannotGetNotExistingGlobalAchievementProgress()
		{
			var key = "Achievement_CannotGetNotExistingGlobalAchievementProgress";
			var loggedInAccount = Helpers.CreateAndLoginGlobal(Fixture.SUGARClient, key);

			Assert.Throws<ClientHttpException>(() => Fixture.SUGARClient.Achievement.GetGlobalAchievementProgress(key, loggedInAccount.User.Id));
		}

		[Fact]
		public void CanGetAchievementProgress()
		{
			var key = "Achievement_CanGetAchievementProgress";
			var loggedInAccount = Helpers.CreateAndLoginGlobal(Fixture.SUGARClient, key);
			var game = Helpers.GetGame(Fixture.SUGARClient.Game, key);

			var progressGame = Fixture.SUGARClient.Achievement.GetGameProgress(game.Id, loggedInAccount.User.Id);
			Assert.Equal(1, progressGame.Count());

			var progressAchievement = Fixture.SUGARClient.Achievement.GetAchievementProgress(key, game.Id, loggedInAccount.User.Id);
			Assert.Equal(0, progressAchievement.Progress);

			CompleteGenericEvaluation(key, loggedInAccount.User.Id, game.Id);

			progressAchievement = Fixture.SUGARClient.Achievement.GetAchievementProgress(key, game.Id, loggedInAccount.User.Id);
			Assert.Equal(1, progressAchievement.Progress);
		}

		[Fact]
		public void CannotGetNotExistingAchievementProgress()
		{
			var key = "Achievement_CannotGetNotExistingAchievementProgress";
			var loggedInAccount = Helpers.CreateAndLoginGlobal(Fixture.SUGARClient, key);
			var game = Helpers.GetGame(Fixture.SUGARClient.Game, key);

			Assert.Throws<ClientHttpException>(() => Fixture.SUGARClient.Achievement.GetAchievementProgress(key, game.Id, loggedInAccount.User.Id));
		}

		public AchievementClientTests(ClientTestsFixture fixture)
			: base(fixture)
		{
		}
	}
}
