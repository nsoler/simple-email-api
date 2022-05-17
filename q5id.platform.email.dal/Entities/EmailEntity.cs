using System;
using System.ComponentModel.DataAnnotations;

namespace q5id.platform.email.dal.Entities
{
	public class EmailEntity
	{
        [Key]
		[Required]
		public long Id { get; set; }

		[Required]
		public string EmailAddress { get; set; }

		public DateTime CreateDateTime { get; set; } = DateTime.Now;

		public DateTime UpdateDateTime { get; set; } = DateTime.Now;

		public bool IsConsumer { get; set; } = false;

		public bool IsBusiness { get; set; } = false;

		public bool IsInvestor { get; set; } = false;

		//TODO: Phase II
		// public Actions Action { get; set; } = Actions.New;


		//TODO: Phase II
		// public Campaign? Campaign { get; set; }
		//    = new Campaign() { IsActive = true, CampaignDescription = "General", CampaignId = 0 };
	}

	public enum Actions
    {
		New = 0,
		Contacted = 1,
		Other = 2,
	}
}

