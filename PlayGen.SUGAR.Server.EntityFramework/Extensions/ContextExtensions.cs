﻿using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PlayGen.SUGAR.Server.Model;

namespace PlayGen.SUGAR.Server.EntityFramework.Extensions
{
	public static class ContextExtensions
	{
		public static void MigrateAndSeed(this SUGARContext context)
		{
			context.Database.Migrate();
			context.EnsureSeeded();
		}

        public static void HandleDetatchedGame(this SUGARContext context, int gameId)
		{
			if (gameId != 0)
			{
				var game = context.Games.FirstOrDefault(a => a.Id == gameId);
				if (game != null && context.Entry(game).State == EntityState.Detached)
				{
					context.Games.Attach(game);
				}
			}
		}

		public static void HandleDetatchedActor(this SUGARContext context, int actorId)
		{
			var actor = context.Actors.FirstOrDefault(a => a.Id == actorId);
			if (actor != null && context.Entry(actor).State == EntityState.Detached)
			{
				context.Actors.Attach(actor);
			}
		}

		public static void HandleDetatchedActor(this SUGARContext context, Actor actor)
		{
			if (actor != null && context.Entry(actor).State == EntityState.Detached)
			{
				context.Actors.Attach(actor);
			}
		}

		public static void HandleDetatchedEvaluationData(this SUGARContext context, EvaluationData evaluationData)
		{
			if (evaluationData != null && context.Entry(evaluationData).State == EntityState.Detached)
			{
				context.EvaluationData.Attach(evaluationData);
			}
		}
	}
}
