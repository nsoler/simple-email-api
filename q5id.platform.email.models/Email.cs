using System;
using System.ComponentModel.DataAnnotations;
using q5id.platform.email.models.enums;

namespace q5id.platform.email.models
{
	public class Email
	{
		[Required]
		public long Id { get; set; }

		[Required]
		[EmailAddress]
		public string EmailAddress { get; set; }

		public DateTime CreateDateTime { get; set; } = DateTime.Now;

		public DateTime UpdateDateTime { get; set; }

		public bool IsConsumer { get; set; } = false;

		public bool IsBusiness { get; set; } = false;

		public bool IsInvestor { get; set; } = false;

		//TODO: Phase II
		// public Actions Action { get; set; } = Actions.New;

		//TODO: Phase II
		// public Campaign? Campaign { get; set; } 

	}
}

