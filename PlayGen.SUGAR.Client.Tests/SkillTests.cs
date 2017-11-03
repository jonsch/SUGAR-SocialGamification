﻿using System.Collections.Generic;
using System.Linq;
using PlayGen.SUGAR.Client.Exceptions;
using PlayGen.SUGAR.Common;
using PlayGen.SUGAR.Contracts;
using Xunit;

namespace PlayGen.SUGAR.Client.Tests
{
	public class SkillTests : Evaluations
	{
		[Fact]
		public void CanCreateSkill()
		{
			var game = Helpers.GetOrCreateGame(SUGARClient.Game, $"{nameof(SkillTests)}_Create");

			var skillRequest = new EvaluationCreateRequest()
			{
				Name = "CanCreateSkill",
				ActorType = ActorType.User,
				Token = "CanCreateSkill",
				GameId = game.Id,
				EvaluationCriterias = new List<EvaluationCriteriaCreateRequest>()
				{
					new EvaluationCriteriaCreateRequest()
					{
						EvaluationDataKey = "CanCreateSkill",
						ComparisonType = ComparisonType.Equals,
						CriteriaQueryType = CriteriaQueryType.Any,
						EvaluationDataType = EvaluationDataType.Float,
						Scope = CriteriaScope.Actor,
						Value = "1"

					}
				},
			};

			var response = SUGARClient.Skill.Create(skillRequest);

			Assert.Equal(skillRequest.Token, response.Token);
			Assert.Equal(skillRequest.ActorType, response.ActorType);
		}

		public void CanCreateGlobalSkill()
		{
			var skillRequest = new EvaluationCreateRequest()
			{
				Name = "CanCreateGlobalSkill",
				ActorType = ActorType.User,
				Token = "CanCreateGlobalSkill",
				EvaluationCriterias = new List<EvaluationCriteriaCreateRequest>()
				{
					new EvaluationCriteriaCreateRequest()
					{
						EvaluationDataKey = "CanCreateGlobalSkill",
						ComparisonType = ComparisonType.Equals,
						CriteriaQueryType = CriteriaQueryType.Any,
						EvaluationDataType = EvaluationDataType.Float,
						Scope = CriteriaScope.Actor,
						Value = "1"

					}
				},
			};

			var response = SUGARClient.Skill.Create(skillRequest);

			Assert.Equal(skillRequest.Token, response.Token);
			Assert.Equal(skillRequest.ActorType, response.ActorType);
		}

		[Fact]
		public void CannotCreateDuplicateSkill()
		{
			var game = Helpers.GetOrCreateGame(SUGARClient.Game, $"{nameof(SkillTests)}_Create");

			var skillRequest = new EvaluationCreateRequest()
			{
				Name = "CannotCreateDuplicateSkill",
				ActorType = ActorType.User,
				Token = "CannotCreateDuplicateSkill",
				GameId = game.Id,
				EvaluationCriterias = new List<EvaluationCriteriaCreateRequest>()
				{
					new EvaluationCriteriaCreateRequest()
					{
						EvaluationDataKey = "CannotCreateDuplicateSkill",
						ComparisonType = ComparisonType.Equals,
						CriteriaQueryType = CriteriaQueryType.Any,
						EvaluationDataType = EvaluationDataType.Float,
						Scope = CriteriaScope.Actor,
						Value = "1"

					}
				},
			};

			SUGARClient.Skill.Create(skillRequest);

			Assert.Throws<ClientHttpException>(() => SUGARClient.Skill.Create(skillRequest));
		}

		[Fact]
		public void CannotCreateSkillWithNoName()
		{
			var game = Helpers.GetOrCreateGame(SUGARClient.Game, $"{nameof(SkillTests)}_Create");

			var skillRequest = new EvaluationCreateRequest()
			{
				ActorType = ActorType.User,
				Token = "CannotCreateSkillWithNoName",
				GameId = game.Id,
				EvaluationCriterias = new List<EvaluationCriteriaCreateRequest>()
				{
					new EvaluationCriteriaCreateRequest()
					{
						EvaluationDataKey = "CannotCreateSkillWithNoName",
						ComparisonType = ComparisonType.Equals,
						CriteriaQueryType = CriteriaQueryType.Any,
						EvaluationDataType = EvaluationDataType.Float,
						Scope = CriteriaScope.Actor,
						Value = "1"

					}
				},
			};

			Assert.Throws<ClientHttpException>(() => SUGARClient.Skill.Create(skillRequest));
		}

		[Fact]
		public void CannotCreateSkillWithNoToken()
		{
			var game = Helpers.GetOrCreateGame(SUGARClient.Game, $"{nameof(SkillTests)}_Create");

			var skillRequest = new EvaluationCreateRequest()
			{
				Name = "CannotCreateSkillWithNoToken",
				ActorType = ActorType.User,
				GameId = game.Id,
				EvaluationCriterias = new List<EvaluationCriteriaCreateRequest>()
				{
					new EvaluationCriteriaCreateRequest()
					{
						EvaluationDataKey = "CannotCreateSkillWithNoToken",
						ComparisonType = ComparisonType.Equals,
						CriteriaQueryType = CriteriaQueryType.Any,
						EvaluationDataType = EvaluationDataType.Float,
						Scope = CriteriaScope.Actor,
						Value = "1"

					}
				},
			};

			Assert.Throws<ClientHttpException>(() => SUGARClient.Skill.Create(skillRequest));
		}

		[Fact]
		public void CannotCreateSkillWithNoEvaluationCriteria()
		{
			var game = Helpers.GetOrCreateGame(SUGARClient.Game, $"{nameof(SkillTests)}_Create");

			var skillRequest = new EvaluationCreateRequest()
			{
				Name = "CannotCreateSkillWithNoEvaluationCriteria",
				ActorType = ActorType.User,
				Token = "CannotCreateSkillWithNoEvaluationCriteria",
				GameId = game.Id,
			};

			Assert.Throws<ClientHttpException>(() => SUGARClient.Skill.Create(skillRequest));
		}

		[Fact]
		public void CannotCreateSkillWithNoEvaluationCriteriaKey()
		{
			var game = Helpers.GetOrCreateGame(SUGARClient.Game, $"{nameof(SkillTests)}_Create");

			var skillRequest = new EvaluationCreateRequest()
			{
				Name = "CannotCreateSkillWithNoEvaluationCriteriaKey",
				ActorType = ActorType.User,
				Token = "CannotCreateSkillWithNoEvaluationCriteriaKey",
				GameId = game.Id,
				EvaluationCriterias = new List<EvaluationCriteriaCreateRequest>()
				{
					new EvaluationCriteriaCreateRequest()
					{
						ComparisonType = ComparisonType.Equals,
						CriteriaQueryType = CriteriaQueryType.Any,
						EvaluationDataType = EvaluationDataType.Float,
						Scope = CriteriaScope.Actor,
						Value = "1"

					}
				},
			};

			Assert.Throws<ClientHttpException>(() => SUGARClient.Skill.Create(skillRequest));
		}

		[Fact]
		public void CannotCreateSkillWithNoEvaluationCriteriaValue()
		{
			var game = Helpers.GetOrCreateGame(SUGARClient.Game, $"{nameof(SkillTests)}_Create");

			var skillRequest = new EvaluationCreateRequest()
			{
				Name = "CannotCreateSkillWithNoEvaluationCriteriaValue",
				ActorType = ActorType.User,
				Token = "CannotCreateSkillWithNoEvaluationCriteriaValue",
				GameId = game.Id,
				EvaluationCriterias = new List<EvaluationCriteriaCreateRequest>()
				{
					new EvaluationCriteriaCreateRequest()
					{
						EvaluationDataKey = "CannotCreateSkillWithNoEvaluationCriteriaValue",
						ComparisonType = ComparisonType.Equals,
						CriteriaQueryType = CriteriaQueryType.Any,
						EvaluationDataType = EvaluationDataType.Float,
						Scope = CriteriaScope.Actor,
					}
				},
			};

			Assert.Throws<ClientHttpException>(() => SUGARClient.Skill.Create(skillRequest));
		}

		[Fact]
		public void CannotCreateSkillWithNoEvaluationCriteriaDataTypeMismatch()
		{
			var game = Helpers.GetOrCreateGame(SUGARClient.Game, $"{nameof(SkillTests)}_Create");

			var skillRequest = new EvaluationCreateRequest()
			{
				Name = "CannotCreateSkillWithNoEvaluationCriteriaDataTypeMismatch",
				ActorType = ActorType.User,
				Token = "CannotCreateSkillWithNoEvaluationCriteriaDataTypeMismatch",
				GameId = game.Id,
				EvaluationCriterias = new List<EvaluationCriteriaCreateRequest>()
				{
					new EvaluationCriteriaCreateRequest()
					{
						EvaluationDataKey = "CannotCreateSkillWithNoEvaluationCriteriaValue",
						ComparisonType = ComparisonType.Equals,
						CriteriaQueryType = CriteriaQueryType.Any,
						EvaluationDataType = EvaluationDataType.Float,
						Scope = CriteriaScope.Actor,
						Value = "A string"
					}
				},
			};

			Assert.Throws<ClientHttpException>(() => SUGARClient.Skill.Create(skillRequest));
		}

		[Fact]
		public void CanGetSkillsByGame()
		{
			var game = Helpers.GetOrCreateGame(SUGARClient.Game, $"{nameof(SkillTests)}_GameGet");

			var skillRequestOne = new EvaluationCreateRequest()
			{
				Name = "CanGetSkillsByGameOne",
				ActorType = ActorType.User,
				Token = "CanGetSkillsByGameOne",
				GameId = game.Id,
				EvaluationCriterias = new List<EvaluationCriteriaCreateRequest>()
				{
					new EvaluationCriteriaCreateRequest()
					{
						EvaluationDataKey = "CanGetSkillsByGameOne",
						ComparisonType = ComparisonType.Equals,
						CriteriaQueryType = CriteriaQueryType.Any,
						EvaluationDataType = EvaluationDataType.Float,
						Scope = CriteriaScope.Actor,
						Value = "1"

					}
				},
			};

			var responseOne = SUGARClient.Skill.Create(skillRequestOne);

			var skillRequestTwo = new EvaluationCreateRequest()
			{
				Name = "CanGetSkillsByGameTwo",
				ActorType = ActorType.User,
				Token = "CanGetSkillsByGameTwo",
				GameId = game.Id,
				EvaluationCriterias = new List<EvaluationCriteriaCreateRequest>()
				{
					new EvaluationCriteriaCreateRequest()
					{
						EvaluationDataKey = "CanGetSkillsByGameTwo",
						ComparisonType = ComparisonType.Equals,
						CriteriaQueryType = CriteriaQueryType.Any,
						EvaluationDataType = EvaluationDataType.Float,
						Scope = CriteriaScope.Actor,
						Value = "1"

					}
				},
			};

			var responseTwo = SUGARClient.Skill.Create(skillRequestTwo);

			var getSkill = SUGARClient.Skill.GetByGame(game.Id);

			Assert.Equal(2, getSkill.Count());
		}

		[Fact]
		public void CanGetSkillByKeys()
		{
			var game = Helpers.GetOrCreateGame(SUGARClient.Game, $"{nameof(SkillTests)}_Get");

			var skillRequest = new EvaluationCreateRequest()
			{
				Name = "CanGetSkillByKeys",
				ActorType = ActorType.User,
				Token = "CanGetSkillByKeys",
				GameId = game.Id,
				EvaluationCriterias = new List<EvaluationCriteriaCreateRequest>()
				{
					new EvaluationCriteriaCreateRequest()
					{
						EvaluationDataKey = "CanGetSkillByKeys",
						ComparisonType = ComparisonType.Equals,
						CriteriaQueryType = CriteriaQueryType.Any,
						EvaluationDataType = EvaluationDataType.Float,
						Scope = CriteriaScope.Actor,
						Value = "1"

					}
				},
			};

			var response = SUGARClient.Skill.Create(skillRequest);

			var getSkill = SUGARClient.Skill.GetById(skillRequest.Token, skillRequest.GameId.Value);

			Assert.Equal(response.Name, getSkill.Name);
			Assert.Equal(skillRequest.Name, getSkill.Name);
		}

		[Fact]
		public void CannotGetNotExistingSkillByKeys()
		{
			var game = Helpers.GetOrCreateGame(SUGARClient.Game, $"{nameof(SkillTests)}_Get");

			var getSkill = SUGARClient.Skill.GetById("CannotGetNotExistingSkillByKeys", game.Id);

			Assert.Null(getSkill);
		}

		[Fact]
		public void CannotGetSkillByEmptyToken()
		{
			var game = Helpers.GetOrCreateGame(SUGARClient.Game, $"{nameof(SkillTests)}_Get");

			Assert.Throws<ClientException>(() => SUGARClient.Skill.GetById("", game.Id));
		}
        
		[Fact]
		public void CanGetGlobalSkillByToken()
		{
			var skillRequest = new EvaluationCreateRequest()
			{
				Name = "CanGetGlobalSkillByToken",
				ActorType = ActorType.User,
				Token = "CanGetGlobalSkillByToken",
				EvaluationCriterias = new List<EvaluationCriteriaCreateRequest>()
				{
					new EvaluationCriteriaCreateRequest()
					{
						EvaluationDataKey = "CanGetGlobalSkillByToken",
						ComparisonType = ComparisonType.Equals,
						CriteriaQueryType = CriteriaQueryType.Any,
						EvaluationDataType = EvaluationDataType.Float,
						Scope = CriteriaScope.Actor,
						Value = "1"

					}
				},
			};

			var response = SUGARClient.Skill.Create(skillRequest);

			var getSkill = SUGARClient.Skill.GetGlobalById(skillRequest.Token);

			Assert.Equal(response.Name, getSkill.Name);
			Assert.Equal(skillRequest.Name, getSkill.Name);
		}

		[Fact]
		public void CannotGetNotExistingGlobalSkillByKeys()
		{
			var getSkill = SUGARClient.Skill.GetGlobalById("CannotGetNotExistingGlobalSkillByKeys");

			Assert.Null(getSkill);
		}

		[Fact]
		public void CannotGetGlobalSkillByEmptyToken()
		{
			Assert.Throws<ClientException>(() => SUGARClient.Skill.GetGlobalById(""));
		}
        
		[Fact]
		public void CannotGetBySkillsByNotExistingGameId()
		{
			var getSkills = SUGARClient.Skill.GetByGame(-1);

			Assert.Empty(getSkills);
		}

		[Fact]
		public void CanUpdateSkill()
		{
			var game = Helpers.GetOrCreateGame(SUGARClient.Game, $"{nameof(SkillTests)}_Update");

			var skillRequest = new EvaluationCreateRequest()
			{
				Name = "CanUpdateSkill",
				GameId = game.Id,
				ActorType = ActorType.User,
				Token = "CanUpdateSkill",
				EvaluationCriterias = new List<EvaluationCriteriaCreateRequest>()
				{
					new EvaluationCriteriaCreateRequest()
					{
						EvaluationDataKey = "CanUpdateSkill",
						ComparisonType = ComparisonType.Equals,
						CriteriaQueryType = CriteriaQueryType.Any,
						EvaluationDataType = EvaluationDataType.Float,
						Scope = CriteriaScope.Actor,
						Value = "1"

					}
				},
			};

			var response = SUGARClient.Skill.Create(skillRequest);

			var updateRequest = new EvaluationUpdateRequest()
			{
                Id = response.Id,
				Name = "CanUpdateSkill Updated",
				GameId = game.Id,
				ActorType = ActorType.User,
				Token = "CanUpdateSkill",
				EvaluationCriterias = new List<EvaluationCriteriaUpdateRequest>()
				{
					new EvaluationCriteriaUpdateRequest()
					{
                        Id = response.EvaluationCriterias[0].Id,
						EvaluationDataKey = "CanUpdateSkill",
						ComparisonType = ComparisonType.Equals,
						CriteriaQueryType = CriteriaQueryType.Any,
						EvaluationDataType = EvaluationDataType.Float,
						Scope = CriteriaScope.Actor,
						Value = "1"

					}
				},
			};

			SUGARClient.Skill.Update(updateRequest);

			var updateResponse = SUGARClient.Skill.GetById(skillRequest.Token, skillRequest.GameId.Value);

			Assert.NotEqual(response.Name, updateResponse.Name);
			Assert.Equal("CanUpdateSkill Updated", updateResponse.Name);
		}

		[Fact]
		public void CannotUpdateSkillToDuplicateToken()
		{
			var game = Helpers.GetOrCreateGame(SUGARClient.Game, $"{nameof(SkillTests)}_Update");

			var skillRequestOne = new EvaluationCreateRequest()
			{
				Name = "CannotUpdateSkillToDuplicateNameOne",
				GameId = game.Id,
				ActorType = ActorType.User,
				Token = "CannotUpdateSkillToDuplicateNameOne",
				EvaluationCriterias = new List<EvaluationCriteriaCreateRequest>()
				{
					new EvaluationCriteriaCreateRequest()
					{
						EvaluationDataKey = "CannotUpdateSkillToDuplicateNameOne",
						ComparisonType = ComparisonType.Equals,
						CriteriaQueryType = CriteriaQueryType.Any,
						EvaluationDataType = EvaluationDataType.Float,
						Scope = CriteriaScope.Actor,
						Value = "1"

					}
				},
			};

			var responseOne = SUGARClient.Skill.Create(skillRequestOne);

			var skillRequestTwo = new EvaluationCreateRequest()
			{
				Name = "CannotUpdateSkillToDuplicateNameTwo",
				GameId = game.Id,
				ActorType = ActorType.User,
				Token = "CannotUpdateSkillToDuplicateNameTwo",
				EvaluationCriterias = new List<EvaluationCriteriaCreateRequest>()
				{
					new EvaluationCriteriaCreateRequest()
					{
						EvaluationDataKey = "CannotUpdateSkillToDuplicateNameTwo",
						ComparisonType = ComparisonType.Equals,
						CriteriaQueryType = CriteriaQueryType.Any,
						EvaluationDataType = EvaluationDataType.Float,
						Scope = CriteriaScope.Actor,
						Value = "1"

					}
				},
			};

			var responseTwo = SUGARClient.Skill.Create(skillRequestTwo);

			var updateRequest = new EvaluationUpdateRequest()
			{
                Id = responseTwo.Id,
				Name = "CannotUpdateSkillToDuplicateNameOne",
				GameId = game.Id,
				ActorType = ActorType.User,
				Token = "CannotUpdateSkillToDuplicateNameOne",
				EvaluationCriterias = new List<EvaluationCriteriaUpdateRequest>()
				{
					new EvaluationCriteriaUpdateRequest()
					{
                        Id = responseTwo.EvaluationCriterias[0].Id,
						EvaluationDataKey = "CannotUpdateSkillToDuplicateNameTwo",
						ComparisonType = ComparisonType.Equals,
						CriteriaQueryType = CriteriaQueryType.Any,
						EvaluationDataType = EvaluationDataType.Float,
						Scope = CriteriaScope.Actor,
						Value = "1"

					}
				},
			};

			Assert.Throws<ClientHttpException>(() => SUGARClient.Skill.Update(updateRequest));
		}

		[Fact]
		public void CannotUpdateNonExistingSkill()
		{
			var game = Helpers.GetOrCreateGame(SUGARClient.Game, $"{nameof(SkillTests)}_Update");

			var updateRequest = new EvaluationUpdateRequest()
			{
                Id = int.MaxValue,
				Name = "CannotUpdateNonExistingSkill",
				GameId = game.Id,
				ActorType = ActorType.User,
				Token = "CannotUpdateNonExistingSkill",
				EvaluationCriterias = new List<EvaluationCriteriaUpdateRequest>()
				{
					new EvaluationCriteriaUpdateRequest()
					{
                        Id = int.MaxValue,
						EvaluationDataKey = "CannotUpdateNonExistingSkill",
						ComparisonType = ComparisonType.Equals,
						CriteriaQueryType = CriteriaQueryType.Any,
						EvaluationDataType = EvaluationDataType.Float,
						Scope = CriteriaScope.Actor,
						Value = "1"

					}
				},
			};

			Assert.Throws<ClientHttpException>(() => SUGARClient.Skill.Update(updateRequest));
		}

		[Fact]
		public void CannotUpdateAchievemenWithNoName()
		{
			var game = Helpers.GetOrCreateGame(SUGARClient.Game, $"{nameof(SkillTests)}_Update");

			var skillRequest = new EvaluationCreateRequest()
			{
				Name = "CannotUpdateAchievemenWithNoName",
				GameId = game.Id,
				ActorType = ActorType.User,
				Token = "CannotUpdateAchievemenWithNoName",
				EvaluationCriterias = new List<EvaluationCriteriaCreateRequest>()
				{
					new EvaluationCriteriaCreateRequest()
					{
						EvaluationDataKey = "CannotUpdateAchievemenWithNoName",
						ComparisonType = ComparisonType.Equals,
						CriteriaQueryType = CriteriaQueryType.Any,
						EvaluationDataType = EvaluationDataType.Float,
						Scope = CriteriaScope.Actor,
						Value = "1"

					}
				},
			};

			var response = SUGARClient.Skill.Create(skillRequest);

			var updateRequest = new EvaluationUpdateRequest()
			{
                Id = response.Id,
				ActorType = ActorType.User,
				Token = "CannotUpdateAchievemenWithNoName",
				GameId = game.Id,
				EvaluationCriterias = new List<EvaluationCriteriaUpdateRequest>()
				{
					new EvaluationCriteriaUpdateRequest()
					{
                        Id = response.EvaluationCriterias[0].Id,
						EvaluationDataKey = "CannotUpdateAchievemenWithNoName",
						ComparisonType = ComparisonType.Equals,
						CriteriaQueryType = CriteriaQueryType.Any,
						EvaluationDataType = EvaluationDataType.Float,
						Scope = CriteriaScope.Actor,
						Value = "1"

					}
				},
			};

			Assert.Throws<ClientHttpException>(() => SUGARClient.Skill.Update(updateRequest));
		}

		[Fact]
		public void CannotUpdateSkillWithNoToken()
		{
			var game = Helpers.GetOrCreateGame(SUGARClient.Game, $"{nameof(SkillTests)}_Update");

			var skillRequest = new EvaluationCreateRequest()
			{
				Name = "CannotUpdateSkillWithNoToken",
				GameId = game.Id,
				ActorType = ActorType.User,
				Token = "CannotUpdateSkillWithNoToken",
				EvaluationCriterias = new List<EvaluationCriteriaCreateRequest>()
				{
					new EvaluationCriteriaCreateRequest()
					{
						EvaluationDataKey = "CannotUpdateSkillWithNoToken",
						ComparisonType = ComparisonType.Equals,
						CriteriaQueryType = CriteriaQueryType.Any,
						EvaluationDataType = EvaluationDataType.Float,
						Scope = CriteriaScope.Actor,
						Value = "1"

					}
				},
			};

			var response = SUGARClient.Skill.Create(skillRequest);

			var updateRequest = new EvaluationUpdateRequest()
			{
                Id = response.Id,
				Name = "CannotUpdateSkillWithNoToken",
				ActorType = ActorType.User,
				GameId = game.Id,
				EvaluationCriterias = new List<EvaluationCriteriaUpdateRequest>()
				{
					new EvaluationCriteriaUpdateRequest()
					{
                        Id = response.EvaluationCriterias[0].Id,
						EvaluationDataKey = "CannotUpdateSkillWithNoToken",
						ComparisonType = ComparisonType.Equals,
						CriteriaQueryType = CriteriaQueryType.Any,
						EvaluationDataType = EvaluationDataType.Float,
						Scope = CriteriaScope.Actor,
						Value = "1"

					}
				},
			};

			Assert.Throws<ClientHttpException>(() => SUGARClient.Skill.Update(updateRequest));
		}

		[Fact]
		public void CannotUpdateSkillWithNoEvaluationCriteria()
		{
			var game = Helpers.GetOrCreateGame(SUGARClient.Game, $"{nameof(SkillTests)}_Update");

			var skillRequest = new EvaluationCreateRequest()
			{
				Name = "CannotUpdateSkillWithNoEvaluationCriteria",
				GameId = game.Id,
				ActorType = ActorType.User,
				Token = "CannotUpdateSkillWithNoEvaluationCriteria",
				EvaluationCriterias = new List<EvaluationCriteriaCreateRequest>()
				{
					new EvaluationCriteriaCreateRequest()
					{
						EvaluationDataKey = "CannotUpdateSkillWithNoEvaluationCriteria",
						ComparisonType = ComparisonType.Equals,
						CriteriaQueryType = CriteriaQueryType.Any,
						EvaluationDataType = EvaluationDataType.Float,
						Scope = CriteriaScope.Actor,
						Value = "1"

					}
				},
			};

			var response = SUGARClient.Skill.Create(skillRequest);

			var updateRequest = new EvaluationUpdateRequest()
			{
                Id = response.Id,
				Name = "CannotUpdateSkillWithNoEvaluationCriteria",
				ActorType = ActorType.User,
				Token = "CannotUpdateSkillWithNoEvaluationCriteria",
				GameId = game.Id,
			};

			Assert.Throws<ClientHttpException>(() => SUGARClient.Skill.Update(updateRequest));
		}

		[Fact]
		public void CannotUpdateSkillWithNoEvaluationCriteriaKey()
		{
			var game = Helpers.GetOrCreateGame(SUGARClient.Game, $"{nameof(SkillTests)}_Update");

			var skillRequest = new EvaluationCreateRequest()
			{
				Name = "CannotUpdateSkillWithNoEvaluationCriteriaKey",
				GameId = game.Id,
				ActorType = ActorType.User,
				Token = "CannotUpdateSkillWithNoEvaluationCriteriaKey",
				EvaluationCriterias = new List<EvaluationCriteriaCreateRequest>()
				{
					new EvaluationCriteriaCreateRequest()
					{
						EvaluationDataKey = "CannotUpdateSkillWithNoEvaluationCriteriaKey",
						ComparisonType = ComparisonType.Equals,
						CriteriaQueryType = CriteriaQueryType.Any,
						EvaluationDataType = EvaluationDataType.Float,
						Scope = CriteriaScope.Actor,
						Value = "1"

					}
				},
			};

			var response = SUGARClient.Skill.Create(skillRequest);

			var updateRequest = new EvaluationUpdateRequest()
			{
                Id = response.Id,
				Name = "CannotUpdateSkillWithNoEvaluationCriteriaKey",
				ActorType = ActorType.User,
				Token = "CannotUpdateSkillWithNoEvaluationCriteriaKey",
				GameId = game.Id,
				EvaluationCriterias = new List<EvaluationCriteriaUpdateRequest>()
				{
					new EvaluationCriteriaUpdateRequest()
					{
                        Id = response.EvaluationCriterias[0].Id,
						ComparisonType = ComparisonType.Equals,
						CriteriaQueryType = CriteriaQueryType.Any,
						EvaluationDataType = EvaluationDataType.Float,
						Scope = CriteriaScope.Actor,
						Value = "1"

					}
				},
			};

			Assert.Throws<ClientHttpException>(() => SUGARClient.Skill.Update(updateRequest));
		}

		[Fact]
		public void CannotUpdateSkillWithNoEvaluationCriteriaValue()
		{
			var game = Helpers.GetOrCreateGame(SUGARClient.Game, $"{nameof(SkillTests)}_Update");

			var skillRequest = new EvaluationCreateRequest()
			{
				Name = "CannotUpdateSkillWithNoEvaluationCriteriaValue",
				GameId = game.Id,
				ActorType = ActorType.User,
				Token = "CannotUpdateSkillWithNoEvaluationCriteriaValue",
				EvaluationCriterias = new List<EvaluationCriteriaCreateRequest>()
				{
					new EvaluationCriteriaCreateRequest()
					{
						EvaluationDataKey = "CannotUpdateSkillWithNoEvaluationCriteriaValue",
						ComparisonType = ComparisonType.Equals,
						CriteriaQueryType = CriteriaQueryType.Any,
						EvaluationDataType = EvaluationDataType.Float,
						Scope = CriteriaScope.Actor,
						Value = "1"

					}
				},
			};

			var response = SUGARClient.Skill.Create(skillRequest);

			var updateRequest = new EvaluationUpdateRequest()
			{
                Id = response.Id,
				Name = "CannotUpdateSkillWithNoEvaluationCriteriaValue",
				ActorType = ActorType.User,
				Token = "CannotUpdateSkillWithNoEvaluationCriteriaValue",
				GameId = game.Id,
				EvaluationCriterias = new List<EvaluationCriteriaUpdateRequest>()
				{
					new EvaluationCriteriaUpdateRequest()
					{
                        Id = response.EvaluationCriterias[0].Id,
						EvaluationDataKey = "CannotUpdateSkillWithNoEvaluationCriteriaValue",
						ComparisonType = ComparisonType.Equals,
						CriteriaQueryType = CriteriaQueryType.Any,
						EvaluationDataType = EvaluationDataType.Float,
						Scope = CriteriaScope.Actor,
					}
				},
			};

			Assert.Throws<ClientHttpException>(() => SUGARClient.Skill.Update(updateRequest));
		}

		[Fact]
		public void CannotUpdateSkillWithNoEvaluationCriteriaDataTypeMismatch()
		{
			var game = Helpers.GetOrCreateGame(SUGARClient.Game, $"{nameof(SkillTests)}_Update");

			var skillRequest = new EvaluationCreateRequest()
			{
				Name = "CannotUpdateSkillWithNoEvaluationCriteriaDataTypeMismatch",
				GameId = game.Id,
				ActorType = ActorType.User,
				Token = "CannotUpdateSkillWithNoEvaluationCriteriaDataTypeMismatch",
				EvaluationCriterias = new List<EvaluationCriteriaCreateRequest>()
				{
					new EvaluationCriteriaCreateRequest()
					{
						EvaluationDataKey = "CannotUpdateSkillWithNoEvaluationCriteriaDataTypeMismatch",
						ComparisonType = ComparisonType.Equals,
						CriteriaQueryType = CriteriaQueryType.Any,
						EvaluationDataType = EvaluationDataType.Float,
						Scope = CriteriaScope.Actor,
						Value = "1"

					}
				},
			};

			var response = SUGARClient.Skill.Create(skillRequest);

			var updateRequest = new EvaluationUpdateRequest()
			{
                Id = response.Id,
				Name = "CannotUpdateSkillWithNoEvaluationCriteriaDataTypeMismatch",
				ActorType = ActorType.User,
				Token = "CannotUpdateSkillWithNoEvaluationCriteriaDataTypeMismatch",
				GameId = game.Id,
				EvaluationCriterias = new List<EvaluationCriteriaUpdateRequest>()
				{
					new EvaluationCriteriaUpdateRequest()
					{
                        Id = response.EvaluationCriterias[0].Id,
						EvaluationDataKey = "CannotUpdateSkillWithNoEvaluationCriteriaDataTypeMismatch",
						ComparisonType = ComparisonType.Equals,
						CriteriaQueryType = CriteriaQueryType.Any,
						EvaluationDataType = EvaluationDataType.Float,
						Scope = CriteriaScope.Actor,
						Value = "A string"
					}
				},
			};

			Assert.Throws<ClientHttpException>(() => SUGARClient.Skill.Update(updateRequest));
		}

		[Fact]
		public void CanDeleteSkill()
		{
			var game = Helpers.GetOrCreateGame(SUGARClient.Game, $"{nameof(SkillTests)}_Delete");

			var skillRequest = new EvaluationCreateRequest()
			{
				Name = "CanDeleteSkill",
				ActorType = ActorType.User,
				Token = "CanDeleteSkill",
				GameId = game.Id,
				EvaluationCriterias = new List<EvaluationCriteriaCreateRequest>()
				{
					new EvaluationCriteriaCreateRequest()
					{
						EvaluationDataKey = "CanDeleteSkill",
						ComparisonType = ComparisonType.Equals,
						CriteriaQueryType = CriteriaQueryType.Any,
						EvaluationDataType = EvaluationDataType.Float,
						Scope = CriteriaScope.Actor,
						Value = "1"

					}
				},
			};

			var response = SUGARClient.Skill.Create(skillRequest);

			var getSkill = SUGARClient.Skill.GetById(skillRequest.Token, skillRequest.GameId.Value);

			Assert.NotNull(getSkill);

			SUGARClient.Skill.Delete(skillRequest.Token, skillRequest.GameId.Value);

			getSkill = SUGARClient.Skill.GetById(skillRequest.Token, skillRequest.GameId.Value);

			Assert.Null(getSkill);
		}

		[Fact]
		public void CannotDeleteNonExistingSkill()
		{
			var game = Helpers.GetOrCreateGame(SUGARClient.Game, $"{nameof(SkillTests)}_Delete");

			Assert.Throws<ClientHttpException>(() => SUGARClient.Skill.Delete("CannotDeleteNonExistingSkill", game.Id));
		}

		[Fact]
		public void CannotDeleteSkillByEmptyToken()
		{
			var game = Helpers.GetOrCreateGame(SUGARClient.Game, $"{nameof(SkillTests)}_Delete");

			Assert.Throws<ClientException>(() => SUGARClient.Skill.Delete("", game.Id));
		}

		[Fact]
		public void CanDeleteGlobalSkill()
		{
			var skillRequest = new EvaluationCreateRequest()
			{
				Name = "CanDeleteGlobalSkill",
				ActorType = ActorType.User,
				Token = "CanDeleteGlobalSkill",
				EvaluationCriterias = new List<EvaluationCriteriaCreateRequest>()
				{
					new EvaluationCriteriaCreateRequest()
					{
						EvaluationDataKey = "CanDeleteGlobalSkill",
						ComparisonType = ComparisonType.Equals,
						CriteriaQueryType = CriteriaQueryType.Any,
						EvaluationDataType = EvaluationDataType.Float,
						Scope = CriteriaScope.Actor,
						Value = "1"

					}
				},
			};

			var response = SUGARClient.Skill.Create(skillRequest);

			var getSkill = SUGARClient.Skill.GetGlobalById(skillRequest.Token);

			Assert.NotNull(getSkill);

			SUGARClient.Skill.DeleteGlobal(skillRequest.Token);

			getSkill = SUGARClient.Skill.GetGlobalById(skillRequest.Token);

			Assert.Null(getSkill);
		}

		[Fact]
		public void CannotDeleteNonExistingGlobalSkill()
		{
			Assert.Throws<ClientHttpException>(() => SUGARClient.Skill.DeleteGlobal("CannotDeleteNonExistingGlobalSkill"));
		}

		[Fact]
		public void CannotDeleteGlobalSkillByEmptyToken()
		{
			Assert.Throws<ClientException>(() => SUGARClient.Skill.DeleteGlobal(""));
		}

		[Fact]
		public void CanGetGlobalSkillProgress()
		{
			var user = Helpers.GetOrCreateUser(SUGARClient.User, $"{nameof(SkillTests)}_ProgressGet");

			var skillRequest = new EvaluationCreateRequest()
			{
				Name = "CanGetGlobalSkillProgress",
				ActorType = ActorType.Undefined,
				Token = "CanGetGlobalSkillProgress",
				EvaluationCriterias = new List<EvaluationCriteriaCreateRequest>()
				{
					new EvaluationCriteriaCreateRequest()
					{
						EvaluationDataKey = "CanGetGlobalSkillProgress",
						ComparisonType = ComparisonType.Equals,
						CriteriaQueryType = CriteriaQueryType.Any,
						EvaluationDataType = EvaluationDataType.Float,
						Scope = CriteriaScope.Actor,
						Value = "1"
					}
				},
			};

			var response = SUGARClient.Skill.Create(skillRequest);

			var progressGame = SUGARClient.Skill.GetGlobalProgress(user.Id);
			Assert.NotEmpty(progressGame);

			var progressSkill = SUGARClient.Skill.GetGlobalSkillProgress(response.Token, user.Id);
			Assert.Equal(0, progressSkill.Progress);

			var gameData = new EvaluationDataRequest()
			{
				Key = "CanGetGlobalSkillProgress",
				Value = "1",
				CreatingActorId = user.Id,
				EvaluationDataType = EvaluationDataType.Float
			};

			SUGARClient.GameData.Add(gameData);

			progressSkill = SUGARClient.Skill.GetGlobalSkillProgress(response.Token, user.Id);
			Assert.True(progressSkill.Progress >= 1);
		}

		[Fact]
		public void CannotGetNotExistingGlobalSkillProgress()
		{
			var user = Helpers.GetOrCreateUser(SUGARClient.User, $"{nameof(SkillTests)}_ProgressGet");

			Assert.Throws<ClientHttpException>(() => SUGARClient.Skill.GetGlobalSkillProgress("CannotGetNotExistingGlobalSkillProgress", user.Id));
		}

		[Fact]
		public void CanGetSkillProgress()
		{
			var user = Helpers.GetOrCreateUser(SUGARClient.User, $"{nameof(SkillTests)}_ProgressGet");
			var game = Helpers.GetOrCreateGame(SUGARClient.Game, $"{nameof(SkillTests)}_ProgressGet");

			var skillRequest = new EvaluationCreateRequest()
			{
				Name = "CanGetSkillProgress",
				GameId = game.Id,
				ActorType = ActorType.Undefined,
				Token = "CanGetSkillProgress",
				EvaluationCriterias = new List<EvaluationCriteriaCreateRequest>()
				{
					new EvaluationCriteriaCreateRequest()
					{
						EvaluationDataKey = "CanGetSkillProgress",
						ComparisonType = ComparisonType.Equals,
						CriteriaQueryType = CriteriaQueryType.Any,
						EvaluationDataType = EvaluationDataType.Float,
						Scope = CriteriaScope.Actor,
						Value = "1"
					}
				},
			};

			var response = SUGARClient.Skill.Create(skillRequest);

			var progressGame = SUGARClient.Skill.GetGameProgress(game.Id, user.Id);
			Assert.Equal(1, progressGame.Count());

			var progressSkill = SUGARClient.Skill.GetSkillProgress(response.Token, game.Id, user.Id);
			Assert.Equal(0, progressSkill.Progress);

			var gameData = new EvaluationDataRequest()
			{
				Key = "CanGetSkillProgress",
				Value = "1",
				CreatingActorId = user.Id,
				GameId = game.Id,
				EvaluationDataType = EvaluationDataType.Float
			};

			SUGARClient.GameData.Add(gameData);

			progressSkill = SUGARClient.Skill.GetSkillProgress(response.Token, game.Id, user.Id);
			Assert.Equal(1, progressSkill.Progress);
		}

		[Fact]
		public void CannotGetNotExistingSkillProgress()
		{
			var user = Helpers.GetOrCreateUser(SUGARClient.User, $"{nameof(SkillTests)}_ProgressGet");
			var game = Helpers.GetOrCreateGame(SUGARClient.Game, $"{nameof(SkillTests)}_ProgressGet");

			Assert.Throws<ClientHttpException>(() => SUGARClient.Skill.GetSkillProgress("CannotGetNotExistingSkillProgress", game.Id, user.Id));
		}

        #region Helpers
        protected override EvaluationResponse CreateEvaluation(EvaluationCreateRequest skillRequest)
        {
            var getSkill = SUGARClient.Skill.GetById(skillRequest.Token, skillRequest.GameId ?? 0);

            if (getSkill != null)
            {
                if (skillRequest.GameId.HasValue)
                {
                    SUGARClient.Skill.Delete(skillRequest.Token, skillRequest.GameId.Value);
                }
                else
                {
                    SUGARClient.Skill.DeleteGlobal(skillRequest.Token);
                }
            }

            var response = SUGARClient.Skill.Create(skillRequest);

            return response;
        }
        #endregion
	}
}