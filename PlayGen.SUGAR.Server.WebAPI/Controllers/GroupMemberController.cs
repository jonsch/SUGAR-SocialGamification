﻿using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlayGen.SUGAR.Common;
using PlayGen.SUGAR.Common.Authorization;
using PlayGen.SUGAR.Contracts;
using PlayGen.SUGAR.Server.Authorization;
using PlayGen.SUGAR.Server.WebAPI.Attributes;
using PlayGen.SUGAR.Server.WebAPI.Extensions;

namespace PlayGen.SUGAR.Server.WebAPI.Controllers
{
	/// <summary>
	/// Web Controller that facilitates User to Group relationship specific operations.
	/// </summary>
	// Values ensured to not be nulled by model validation
	[SuppressMessage("ReSharper", "PossibleInvalidOperationException")]
	[Route("api/[controller]")]
	[Authorize("Bearer")]
	public class GroupMemberController : Controller
	{
		private readonly IAuthorizationService _authorizationService;
		private readonly Core.Controllers.RelationshipController _relationshipCoreController;

		public GroupMemberController(Core.Controllers.RelationshipController relationshipController, IAuthorizationService authorizationService)
		{
			_relationshipCoreController = relationshipController;
			_authorizationService = authorizationService;
		}

		/// <summary>
		/// Get a list of all Users that have sent relationship requests to this groupId.
		/// </summary>
		/// <param name="groupId">ID of the group.</param>
		/// <returns>A list of <see cref="ActorResponse"/> which match the search criteria.</returns>
		[HttpGet("requests/{groupId:int}")]
		[Authorization(ClaimScope.Group, AuthorizationAction.Get, AuthorizationEntity.GroupMemberRequest)]
		public async Task<IActionResult> GetMemberRequests([FromRoute]int groupId)
		{
			if ((await _authorizationService.AuthorizeAsync(User, groupId, HttpContext.ScopeItems(ClaimScope.Group))).Succeeded)
			{
				var users = _relationshipCoreController.GetRequests(groupId, ActorType.User);
				var actorContract = users.ToActorContractList();
				return new ObjectResult(actorContract);
			}
			return Forbid();
		}

		/// <summary>
		/// Get a list of all Groups that have been sent relationship requests for this userId.
		/// </summary>
		/// <param name="userId">ID of the user.</param>
		/// <returns>A list of <see cref="ActorResponse"/> which match the search criteria.</returns>
		[HttpGet("sentrequests/{userId:int}")]
		[Authorization(ClaimScope.User, AuthorizationAction.Get, AuthorizationEntity.GroupMemberRequest)]
		public async Task<IActionResult> GetSentRequests([FromRoute]int userId)
		{
			if ((await _authorizationService.AuthorizeAsync(User, userId, HttpContext.ScopeItems(ClaimScope.User))).Succeeded)
			{
				var requests = _relationshipCoreController.GetSentRequests(userId, ActorType.Group);
				var actorContract = requests.ToActorContractList();
				return new ObjectResult(actorContract);
			}
			return Forbid();
		}

		/// <summary>
		/// Get a list of all Users that have relationships with this groupId.
		/// </summary>
		/// <param name="groupId">ID of the group.</param>
		/// <returns>A list of <see cref="ActorResponse"/> that matches the search criteria.</returns>
		[HttpGet("members/{groupId:int}")]
		public IActionResult GetMembers([FromRoute]int groupId)
		{
			var members = _relationshipCoreController.GetRelatedActors(groupId, ActorType.User);
			var actorContract = members.ToActorContractList();
			return new ObjectResult(actorContract);
		}

		/// <summary>
		/// Get a count of users that have a relationship with this groupId.
		/// </summary>
		/// <param name="groupId">ID of the group.</param>
		/// <returns>A count of members in the group that matches the search criteria.</returns>
		[HttpGet("membercount/{groupId:int}")]
		public IActionResult GetMemberCount([FromRoute]int groupId)
		{
			var count = _relationshipCoreController.GetRelationshipCount(groupId, ActorType.User);
			return new ObjectResult(count);
		}

		/// <summary>
		/// Get a list of all Groups that have a relationship with this userId.
		/// </summary>
		/// <param name="userId">ID of the User.</param>
		/// <returns>A list of <see cref="ActorResponse"/> that matches the search criteria.</returns>
		[HttpGet("usergroups/{userId:int}")]
		public IActionResult GetUserGroups([FromRoute]int userId)
		{
			var groups = _relationshipCoreController.GetRelatedActors(userId, ActorType.Group);
			var actorContract = groups.ToActorContractList();
			return new ObjectResult(actorContract);
		}

		/// <summary>
		/// Create a new relationship request between a User and Group.
		/// Requires a relationship between the User and Group to not already exist.
		/// </summary>
		/// <param name="relationship"><see cref="RelationshipRequest"/> object that holds the details of the new relationship request.</param>
		/// <returns>A <see cref="RelationshipResponse"/> containing the new Relationship details.</returns>
		[HttpPost]
		[ArgumentsNotNull]
		[Authorization(ClaimScope.Group, AuthorizationAction.Create, AuthorizationEntity.GroupMemberRequest)]
		[Authorization(ClaimScope.User, AuthorizationAction.Create, AuthorizationEntity.GroupMemberRequest)]
		public async Task<IActionResult> CreateMemberRequest([FromBody]RelationshipRequest relationship)
		{
			if ((await _authorizationService.AuthorizeAsync(User, relationship.RequestorId, HttpContext.ScopeItems(ClaimScope.User))).Succeeded ||
				(await _authorizationService.AuthorizeAsync(User, relationship.AcceptorId, HttpContext.ScopeItems(ClaimScope.Group))).Succeeded)
			{
				var request = relationship.ToRelationshipModel();
				_relationshipCoreController.CreateRequest(relationship.ToRelationshipModel(), relationship.AutoAccept);
				var relationshipContract = request.ToContract();
				return new ObjectResult(relationshipContract);
			}
			return Forbid();
		}

		/// <summary>
		/// Update an existing relationship request between a User and a Group.
		/// Requires the relationship request to already exist between the User and Group.
		/// </summary>
		/// <param name="relationship"><see cref="RelationshipStatusUpdate"/> object that holds the details of the relationship.</param>
		[HttpPut("request")]
		[ArgumentsNotNull]
		[Authorization(ClaimScope.Group, AuthorizationAction.Update, AuthorizationEntity.GroupMemberRequest)]
		[Authorization(ClaimScope.User, AuthorizationAction.Update, AuthorizationEntity.GroupMemberRequest)]
		public async Task<IActionResult> UpdateMemberRequest([FromBody] RelationshipStatusUpdate relationship)
		{
			if ((await _authorizationService.AuthorizeAsync(User, relationship.RequestorId, HttpContext.ScopeItems(ClaimScope.User))).Succeeded ||
				(await _authorizationService.AuthorizeAsync(User, relationship.AcceptorId, HttpContext.ScopeItems(ClaimScope.Group))).Succeeded)
			{
				var relation = new RelationshipRequest
				{
					RequestorId = relationship.RequestorId,
					AcceptorId = relationship.AcceptorId
				};
				_relationshipCoreController.UpdateRequest(relation.ToRelationshipModel(), relationship.Accepted);
				return Ok();
			}
			return Forbid();
		}

        /// <summary>
        /// Update an existing relationship between a User and a Group.
        /// Requires the relationship to already exist between the User and Group.
        /// </summary>
        /// <param name="relationship"><see cref="RelationshipStatusUpdate"/> object that holds the details of the relationship.</param>
		[HttpPut] // todo change to a remove that takes both members of the relationship
        [ArgumentsNotNull]
		[Authorization(ClaimScope.Group, AuthorizationAction.Delete, AuthorizationEntity.GroupMember)]
		[Authorization(ClaimScope.User, AuthorizationAction.Delete, AuthorizationEntity.GroupMember)]
		public async Task<IActionResult> RemoveMember([FromBody] RelationshipStatusUpdate relationship)
		{
			if ((await _authorizationService.AuthorizeAsync(User, relationship.RequestorId, HttpContext.ScopeItems(ClaimScope.User))).Succeeded ||
				(await _authorizationService.AuthorizeAsync(User, relationship.AcceptorId, HttpContext.ScopeItems(ClaimScope.Group))).Succeeded)
			{
				var relation = new RelationshipRequest
				{
					RequestorId = relationship.RequestorId,
					AcceptorId = relationship.AcceptorId
				};
				_relationshipCoreController.Delete(relation.ToRelationshipModel());
				return Ok();
			}
			return Forbid();
		}
	}
}