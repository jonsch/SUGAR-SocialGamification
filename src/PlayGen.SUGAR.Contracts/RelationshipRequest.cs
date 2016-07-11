﻿using System.ComponentModel.DataAnnotations;

namespace PlayGen.SUGAR.Contracts
{
	/// <summary>
	/// Encapsulates relationship details.
	/// </summary>
	/// <example>
	/// JSON
	/// {
	/// RequestorId : 1,
	/// AcceptorId : 2,
	/// AutoAccept : true
	/// }
	/// </example>
	public class RelationshipRequest
	{
		[Required]
		public int RequestorId { get; set; }

		[Required]
		public int AcceptorId { get; set; }

		[Required]
		public bool AutoAccept { get; set; }
	}
}
