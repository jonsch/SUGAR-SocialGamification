﻿using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlayGen.SUGAR.Common.Authorization;
using PlayGen.SUGAR.Contracts;
using PlayGen.SUGAR.Server.Authorization;
using PlayGen.SUGAR.Server.Core.Controllers;
using PlayGen.SUGAR.Server.WebAPI.Attributes;
using PlayGen.SUGAR.Server.WebAPI.Extensions;

namespace PlayGen.SUGAR.Server.WebAPI.Controllers
{
	/// <summary>
	/// Web Controller that facilitates Leaderboard specific operations.
	/// </summary>
	// Values ensured to not be nulled by model validation
	[SuppressMessage("ReSharper", "PossibleInvalidOperationException")]
	[Route("api/[controller]")]
	[Authorize("Bearer")]
	public class LeaderboardsController : Controller
	{
		private readonly IAuthorizationService _authorizationService;
		private readonly LeaderboardController _leaderboardController;

		public LeaderboardsController(LeaderboardController leaderboardController, IAuthorizationService authorizationService)
		{
			_leaderboardController = leaderboardController;
			_authorizationService = authorizationService;
		}

		/// <summary>
		/// Find a list of leaderboards that match the gameId provided.
		/// If global is provided instead of a gameId, get all global leaderboards, ie. leaderboards that are not associated with a specific game.
		/// </summary>
		/// <param name="gameId">Game ID</param>
		/// <returns>Returns a list of <see cref="LeaderboardResponse"/> that holds Leaderboard details</returns>
		[HttpGet("global/list")]
		[HttpGet("game/{gameId:int}/list")]
		public IActionResult Get([FromRoute]int gameId)
		{
			var leaderboard = _leaderboardController.Get(gameId);
			var leaderboardContract = leaderboard.ToContractList();
			return new ObjectResult(leaderboardContract);
		}

		/// <summary>
		/// Find a single leaderboard matching the token and gameId.
		/// If global is provided instead of a gameId, get a global leaderboard with a matching token.
		/// </summary>
		/// <param name="token">Token of the leaderboard</param>
		/// <param name="gameId">Game ID</param>
		/// <returns>Returns a single <see cref="LeaderboardResponse"/> that holds Leaderboard details</returns>
		[HttpGet("{token}/global")]
		[HttpGet("{token}/{gameId:int}")]
		public IActionResult Get([FromRoute]string token, [FromRoute]int gameId)
		{
			var leaderboard = _leaderboardController.Get(token, gameId);
			var leaderboardContract = leaderboard.ToContract();
			return new ObjectResult(leaderboardContract);
		}

		/// <summary>
		/// Create a new Leaderboard.
		/// Requires <see cref="LeaderboardRequest"/> Name and Token to be unique to that GameId.
		/// </summary>
		/// <param name="newLeaderboard"><see cref="LeaderboardRequest"/> object that holds the details of the new Leaderboard.</param>
		/// <returns>Returns a <see cref="LeaderboardResponse"/> object containing details for the newly created Leaderboard.</returns>
		[HttpPost("create")]
		[ArgumentsNotNull]
		[Authorization(ClaimScope.Game, AuthorizationAction.Create, AuthorizationEntity.Leaderboard)]
		public async Task<IActionResult> Create([FromBody] LeaderboardRequest newLeaderboard)
		{
			if ((await _authorizationService.AuthorizeAsync(User, newLeaderboard.GameId, HttpContext.ScopeItems(ClaimScope.Game))).Succeeded)
			{
				var leaderboard = newLeaderboard.ToModel();
				_leaderboardController.Create(leaderboard);
				var leaderboardContract = leaderboard.ToContract();
				return new ObjectResult(leaderboardContract);
			}
			return Forbid();
		}

		/// <summary>
		/// Get the standings for a Leaderboard using a <see cref="LeaderboardStandingsRequest"/>.
		/// </summary>
		/// <param name="leaderboardDetails"><see cref="LeaderboardStandingsRequest"/> object that holds the details that are wanted from the Leaderboard.</param>
		/// <returns>Returns a list of <see cref="LeaderboardStandingsResponse"/> that detail actor positions in the leaderboard.</returns>
		[HttpPost("standings")]
		public IActionResult GetLeaderboardStandings([FromBody]LeaderboardStandingsRequest leaderboardDetails)
		{
			var leaderboard = _leaderboardController.Get(leaderboardDetails.LeaderboardToken, leaderboardDetails.GameId.Value);
			var standings = _leaderboardController.GetStandings(leaderboard, leaderboardDetails.ToCore());
			var standingsContract = standings.ToContractList();
			return new ObjectResult(standingsContract);
		}

		/// <summary>
		/// Update an existing Leaderboard.
		/// </summary>
		/// <param name="leaderboard"><see cref="LeaderboardRequest"/> object that holds the details of the Leaderboard.</param>
		[HttpPut("update")]
		[ArgumentsNotNull]
		[Authorization(ClaimScope.Game, AuthorizationAction.Update, AuthorizationEntity.Leaderboard)]
		public async Task<IActionResult> Update([FromBody] LeaderboardRequest leaderboard)
		{
			if ((await _authorizationService.AuthorizeAsync(User, leaderboard.GameId, HttpContext.ScopeItems(ClaimScope.Game))).Succeeded)
			{
				var leaderboardModel = leaderboard.ToModel();
				_leaderboardController.Update(leaderboardModel);
				return Ok();
			}
			return Forbid();
		}

		/// <summary>
		/// Delete Leaderboard with the token and gameId provided.
		/// If global is provided instead of a gameId, delete a global leaderboard with a matching token.
		/// </summary>
		/// <param name="token">Token of Leaderboard</param>
		/// <param name="gameId">ID of the Game the Leaderboard is for</param>
		[HttpDelete("{token}/global")]
		[HttpDelete("{token}/{gameId:int}")]
		[Authorization(ClaimScope.Game, AuthorizationAction.Delete, AuthorizationEntity.Leaderboard)]
		public async Task<IActionResult> Delete([FromRoute]string token, [FromRoute]int gameId)
		{
			if ((await _authorizationService.AuthorizeAsync(User, gameId, HttpContext.ScopeItems(ClaimScope.Game))).Succeeded)
			{
				_leaderboardController.Delete(token, gameId);
				return Ok();
			}
			return Forbid();
		}
	}
}