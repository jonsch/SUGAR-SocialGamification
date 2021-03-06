﻿using System.Collections.Generic;
using PlayGen.SUGAR.Common.Authorization;

namespace PlayGen.SUGAR.Server.Model
{
	public class Role
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public ClaimScope ClaimScope { get; set; }

		public bool Default { get; set; }

		public virtual List<RoleClaim> RoleClaims { get; set; }
	}
}
