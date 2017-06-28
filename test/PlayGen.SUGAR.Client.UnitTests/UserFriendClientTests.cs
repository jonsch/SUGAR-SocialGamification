﻿using System.Linq;
using NUnit.Framework;
using PlayGen.SUGAR.Client.Exceptions;
using PlayGen.SUGAR.Contracts;

namespace PlayGen.SUGAR.Client.UnitTests
{
	public class UserFriendClientTests : ClientTestsBase
	{
		[Test]
		public void CanCreateRequest()
		{
			var requestor = Helpers.GetOrCreateUser(SUGARClient.User, "CanCreateRequestR");
			var acceptor = Helpers.GetOrCreateUser(SUGARClient.User, "CanCreateRequestA");

			var relationshipRequest = new RelationshipRequest
			{
				RequestorId = requestor.Id,
				AcceptorId = acceptor.Id
			};

			var relationshipResponse = SUGARClient.UserFriend.CreateFriendRequest(relationshipRequest);

			Assert.AreEqual(relationshipRequest.RequestorId, relationshipResponse.RequestorId);
			Assert.AreEqual(relationshipRequest.AcceptorId, relationshipResponse.AcceptorId);

			var sent = SUGARClient.UserFriend.GetSentRequests(requestor.Id);

			Assert.AreEqual(1, sent.Items.Count());

			var received = SUGARClient.UserFriend.GetFriendRequests(acceptor.Id);

			Assert.AreEqual(1, received.Items.Count());
		}

		[Test]
		public void CanCreateAutoAcceptedRequest()
		{
			var requestor = Helpers.GetOrCreateUser(SUGARClient.User, "CanCreateAutoAcceptedRequestR");
			var acceptor = Helpers.GetOrCreateUser(SUGARClient.User, "CanCreateAutoAcceptedRequestA");

			var relationshipRequest = new RelationshipRequest
			{
				RequestorId = requestor.Id,
				AcceptorId = acceptor.Id,
				AutoAccept = true
			};

			var relationshipResponse = SUGARClient.UserFriend.CreateFriendRequest(relationshipRequest);

			Assert.AreEqual(relationshipRequest.RequestorId, relationshipResponse.RequestorId);
			Assert.AreEqual(relationshipRequest.AcceptorId, relationshipResponse.AcceptorId);

			var sent = SUGARClient.UserFriend.GetFriends(requestor.Id);

			Assert.AreEqual(1, sent.Items.Count());

			var received = SUGARClient.UserFriend.GetFriends(acceptor.Id);

			Assert.AreEqual(1, received.Items.Count());
		}

		[Test]
		public void CannotCreateDuplicateRequest()
		{
			var requestor = Helpers.GetOrCreateUser(SUGARClient.User, "CannotCreateDuplicateRequestR");
			var acceptor = Helpers.GetOrCreateUser(SUGARClient.User, "CannotCreateDuplicateRequestA");

			var relationshipRequest = new RelationshipRequest
			{
				RequestorId = requestor.Id,
				AcceptorId = acceptor.Id
			};

			var relationshipResponse = SUGARClient.UserFriend.CreateFriendRequest(relationshipRequest);

			Assert.Throws<ClientHttpException>(() => SUGARClient.UserFriend.CreateFriendRequest(relationshipRequest));
		}

		[Test]
		public void CannotCreateDuplicateRequestOfAccepted()
		{
			var requestor = Helpers.GetOrCreateUser(SUGARClient.User, "DuplicateRequestOfAcceptedR");
			var acceptor = Helpers.GetOrCreateUser(SUGARClient.User, "DuplicateRequestOfAcceptedA");

			var relationshipRequest = new RelationshipRequest
			{
				RequestorId = requestor.Id,
				AcceptorId = acceptor.Id,
				AutoAccept = true
			};

			var relationshipResponse = SUGARClient.UserFriend.CreateFriendRequest(relationshipRequest);

			relationshipRequest.AutoAccept = false;

			Assert.Throws<ClientHttpException>(() => SUGARClient.UserFriend.CreateFriendRequest(relationshipRequest));
		}

		[Test]
		public void CannotCreateDuplicateAutoAcceptedRequest()
		{
			var requestor = Helpers.GetOrCreateUser(SUGARClient.User, "DuplicateAutoAcceptedRequestR");
			var acceptor = Helpers.GetOrCreateUser(SUGARClient.User, "DuplicateAutoAcceptedRequestA");

			var relationshipRequest = new RelationshipRequest
			{
				RequestorId = requestor.Id,
				AcceptorId = acceptor.Id
			};

			var relationshipResponse = SUGARClient.UserFriend.CreateFriendRequest(relationshipRequest);

			relationshipRequest.AutoAccept = true;

			Assert.Throws<ClientHttpException>(() => SUGARClient.UserFriend.CreateFriendRequest(relationshipRequest));
		}

		[Test]
		public void CannotCreateRequestWithNonExistingUser()
		{
			var acceptor = Helpers.GetOrCreateUser(SUGARClient.User, "CannotCreateRequestWithNonExistingUser");

			var relationshipRequest = new RelationshipRequest
			{
				RequestorId = -1,
				AcceptorId = acceptor.Id
			};

			Assert.Throws<ClientHttpException>(() => SUGARClient.UserFriend.CreateFriendRequest(relationshipRequest));
		}

		[Test]
		public void CannotCreateRequestWithNonExistingGroup()
		{
			var requestor = Helpers.GetOrCreateUser(SUGARClient.User, "RequestWithNonExistingGroup");

			var relationshipRequest = new RelationshipRequest
			{
				RequestorId = requestor.Id,
				AcceptorId = -1
			};

			Assert.Throws<ClientHttpException>(() => SUGARClient.UserFriend.CreateFriendRequest(relationshipRequest));
		}

		[Test]
		public void CanAcceptRequest()
		{
			var requestor = Helpers.GetOrCreateUser(SUGARClient.User, "CanAcceptRequestR");
			var acceptor = Helpers.GetOrCreateUser(SUGARClient.User, "CanAcceptRequestA");

			var relationshipRequest = new RelationshipRequest
			{
				RequestorId = requestor.Id,
				AcceptorId = acceptor.Id
			};

			var relationshipResponse = SUGARClient.UserFriend.CreateFriendRequest(relationshipRequest);

			var sent = SUGARClient.UserFriend.GetSentRequests(requestor.Id);

			Assert.AreEqual(1, sent.Items.Count());

			var received = SUGARClient.UserFriend.GetFriendRequests(acceptor.Id);

			Assert.AreEqual(1, received.Items.Count());

			var relationshipStatusUpdate = new RelationshipStatusUpdate
			{
				RequestorId = requestor.Id,
				AcceptorId = acceptor.Id,
				Accepted = true
			};

			SUGARClient.UserFriend.UpdateFriendRequest(relationshipStatusUpdate);

			sent = SUGARClient.UserFriend.GetSentRequests(requestor.Id);

			Assert.AreEqual(0, sent.Items.Count());

			received = SUGARClient.UserFriend.GetFriendRequests(acceptor.Id);

			Assert.AreEqual(0, received.Items.Count());

			sent = SUGARClient.UserFriend.GetFriends(requestor.Id);

			Assert.AreEqual(1, sent.Items.Count());

			received = SUGARClient.UserFriend.GetFriends(acceptor.Id);

			Assert.AreEqual(1, received.Items.Count());
		}

		[Test]
		public void CanRejectRequest()
		{
			var requestor = Helpers.GetOrCreateUser(SUGARClient.User, "CanRejectRequestR");
			var acceptor = Helpers.GetOrCreateUser(SUGARClient.User, "CanRejectRequestA");

			var relationshipRequest = new RelationshipRequest
			{
				RequestorId = requestor.Id,
				AcceptorId = acceptor.Id
			};

			var relationshipResponse = SUGARClient.UserFriend.CreateFriendRequest(relationshipRequest);

			var sent = SUGARClient.UserFriend.GetSentRequests(requestor.Id);

			Assert.AreEqual(1, sent.Items.Count());

			var received = SUGARClient.UserFriend.GetFriendRequests(acceptor.Id);

			Assert.AreEqual(1, received.Items.Count());

			var relationshipStatusUpdate = new RelationshipStatusUpdate
			{
				RequestorId = requestor.Id,
				AcceptorId = acceptor.Id,
				Accepted = false
			};

			SUGARClient.UserFriend.UpdateFriendRequest(relationshipStatusUpdate);

			sent = SUGARClient.UserFriend.GetSentRequests(requestor.Id);

			Assert.AreEqual(0, sent.Items.Count());

			received = SUGARClient.UserFriend.GetFriendRequests(acceptor.Id);

			Assert.AreEqual(0, received.Items.Count());

			sent = SUGARClient.UserFriend.GetFriends(requestor.Id);

			Assert.AreEqual(0, sent.Items.Count());

			received = SUGARClient.UserFriend.GetFriends(acceptor.Id);

			Assert.AreEqual(0, received.Items.Count());
		}

		[Test]
		public void CannotUpdateAlreadyAcceptedRequest()
		{
			var requestor = Helpers.GetOrCreateUser(SUGARClient.User, "CannotUpdateAlreadyAcceptedRequestR");
			var acceptor = Helpers.GetOrCreateUser(SUGARClient.User, "CannotUpdateAlreadyAcceptedRequestA");

			var relationshipRequest = new RelationshipRequest
			{
				RequestorId = requestor.Id,
				AcceptorId = acceptor.Id,
				AutoAccept = true
			};

			var relationshipResponse = SUGARClient.UserFriend.CreateFriendRequest(relationshipRequest);

			var relationshipStatusUpdate = new RelationshipStatusUpdate
			{
				RequestorId = requestor.Id,
				AcceptorId = acceptor.Id,
				Accepted = true
			};

			Assert.Throws<ClientHttpException>(() => SUGARClient.UserFriend.UpdateFriendRequest(relationshipStatusUpdate));
		}

		[Test]
		public void CannotUpdateNotExistingRequest()
		{
			var relationshipStatusUpdate = new RelationshipStatusUpdate
			{
				RequestorId = -1,
				AcceptorId = -1,
				Accepted = true
			};

			Assert.Throws<ClientHttpException>(() => SUGARClient.UserFriend.UpdateFriendRequest(relationshipStatusUpdate));
		}

		[Test]
		public void CanUpdateRelationship()
		{
			var requestor = Helpers.GetOrCreateUser(SUGARClient.User, "CanUpdateRelationshipR");
			var acceptor = Helpers.GetOrCreateUser(SUGARClient.User, "CanUpdateRelationshipA");

			var relationshipRequest = new RelationshipRequest
			{
				RequestorId = requestor.Id,
				AcceptorId = acceptor.Id,
				AutoAccept = true
			};

			var relationshipResponse = SUGARClient.UserFriend.CreateFriendRequest(relationshipRequest);

			var sent = SUGARClient.UserFriend.GetFriends(requestor.Id);

			Assert.AreEqual(1, sent.Items.Count());

			var received = SUGARClient.UserFriend.GetFriends(acceptor.Id);

			Assert.AreEqual(1, received.Items.Count());

			var relationshipStatusUpdate = new RelationshipStatusUpdate
			{
				RequestorId = requestor.Id,
				AcceptorId = acceptor.Id
			};

			SUGARClient.UserFriend.UpdateFriend(relationshipStatusUpdate);

			sent = SUGARClient.UserFriend.GetFriends(requestor.Id);

			Assert.AreEqual(0, sent.Items.Count());

			received = SUGARClient.UserFriend.GetFriends(acceptor.Id);

			Assert.AreEqual(0, received.Items.Count());
		}

		[Test]
		public void CannotUpdateNotExistingRelationship()
		{
			var requestor = Helpers.GetOrCreateUser(SUGARClient.User, "CannotUpdateNotExistingRelationshipR");
			var acceptor = Helpers.GetOrCreateUser(SUGARClient.User, "CannotUpdateNotExistingRelationshipA");

			var relationshipStatusUpdate = new RelationshipStatusUpdate
			{
				RequestorId = requestor.Id,
				AcceptorId = acceptor.Id
			};

			Assert.Throws<ClientHttpException>(() => SUGARClient.UserFriend.UpdateFriend(relationshipStatusUpdate));
		}

		[Test]
		public void CanGetFriendRequests()
		{
			var acceptor = Helpers.GetOrCreateUser(SUGARClient.User, "CanGetFriendRequestsA");
			var requestorNames = new[]
			{
				"CanGetFriendRequests1",
				"CanGetFriendRequests2",
				"CanGetFriendRequests3",
				"CanGetFriendRequests4",
				"CanGetFriendRequests5"
			};

			foreach (var name in requestorNames)
			{
				var requestor = Helpers.GetOrCreateUser(SUGARClient.User, name);
				var relationshipRequest = new RelationshipRequest
				{
					RequestorId = requestor.Id,
					AcceptorId = acceptor.Id
				};
				SUGARClient.UserFriend.CreateFriendRequest(relationshipRequest);
			}

			var requests = SUGARClient.UserFriend.GetFriendRequests(acceptor.Id);

			Assert.AreEqual(5, requests.Items.Count());

			var requestCheck = requests.Items.Where(r => requestorNames.Any(rn => r.Name.Contains(rn)));

			Assert.AreEqual(5, requestCheck.Count());
		}

		[Test]
		public void CanGetSentRequests()
		{
			var requestor = Helpers.GetOrCreateUser(SUGARClient.User, "CanGetSentRequestsR");
			var acceptorNames = new[]
			{
				"CanGetSentRequests1",
				"CanGetSentRequests2",
				"CanGetSentRequests3",
				"CanGetSentRequests4",
				"CanGetSentRequests5"
			};

			foreach (var name in acceptorNames)
			{
				var acceptor = Helpers.GetOrCreateUser(SUGARClient.User, name);
				var relationshipRequest = new RelationshipRequest
				{
					RequestorId = requestor.Id,
					AcceptorId = acceptor.Id
				};
				SUGARClient.UserFriend.CreateFriendRequest(relationshipRequest);
			}

			var requests = SUGARClient.UserFriend.GetSentRequests(requestor.Id);

			Assert.AreEqual(5, requests.Items.Count());

			var requestCheck = requests.Items.Where(r => acceptorNames.Any(an => r.Name.Contains(an)));

			Assert.AreEqual(5, requestCheck.Count());
		}

		[Test]
		public void CanGetFriends()
		{
			var acceptor = Helpers.GetOrCreateUser(SUGARClient.User, "CanGetFriendsA");
			var requestorNames = new[]
			{
				"CanGetFriends1",
				"CanGetFriends2",
				"CanGetFriends3",
				"CanGetFriends4",
				"CanGetFriends5"
			};

			foreach (var name in requestorNames)
			{
				var requestor = Helpers.GetOrCreateUser(SUGARClient.User, name);
				var relationshipRequest = new RelationshipRequest
				{
					RequestorId = requestor.Id,
					AcceptorId = acceptor.Id,
					AutoAccept = true
				};
				SUGARClient.UserFriend.CreateFriendRequest(relationshipRequest);
			}

			var members = SUGARClient.UserFriend.GetFriends(acceptor.Id);

			Assert.AreEqual(5, members.Items.Count());

			var memberCheck = members.Items.Where(r => requestorNames.Any(rn => r.Name.Contains(rn)));

			Assert.AreEqual(5, memberCheck.Count());
		}
	}
}