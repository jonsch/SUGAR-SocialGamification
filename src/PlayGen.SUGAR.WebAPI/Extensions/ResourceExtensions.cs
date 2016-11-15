﻿using System.Collections.Generic;
using System.Linq;
using PlayGen.SUGAR.Common.Shared;
using PlayGen.SUGAR.Contracts.Shared;
using PlayGen.SUGAR.Data.Model;

namespace PlayGen.SUGAR.WebAPI.Extensions
{
	public  static class ResourceExtensions
	{
		public static ResourceResponse ToResourceContract(this GameData gameData)
		{
			if (gameData == null)
			{
				return null;
			}

			return new ResourceResponse
			{
				Id = gameData.Id,
				ActorId = gameData.ActorId,
				GameId = gameData.GameId,
				Key = gameData.Key,
				Quantity = long.Parse(gameData.Value),
			};
		}

		public static IEnumerable<ResourceResponse> ToResourceContractList(this IEnumerable<GameData> gameData)
		{
			return gameData.Select(ToResourceContract).ToList();
		}

		public static GameData ToModel(this ResourceAddRequest resourceContract)
		{
			return new GameData
			{
				ActorId = resourceContract.ActorId,
				GameId = resourceContract.GameId,
				Key = resourceContract.Key,
				Value = resourceContract.Quantity.ToString(),
				DataType = GameDataType.Long,
				Category = GameDataCategory.Resource
			};
		}
	}
}
