using System;
namespace q5id.platform.email.dal.Entities
{
	public class Campaign
	{
		public long CampaignId { get; set; }

		public string? CampaignDescription { get; set; }

		public bool IsActive { get; set; }
	}
}

