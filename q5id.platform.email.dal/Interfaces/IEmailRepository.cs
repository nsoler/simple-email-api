using System;
using System.Threading;

using q5id.platform.email.dal.Entities;

namespace q5id.platform.email.dal.Interfaces
{
	public interface IEmailRepository
	{

		Task Create(EmailEntity entity);
		
		Task<EmailEntity> Search(string emailAddress);

		Task<EmailEntity> Update(EmailEntity entity, EmailEntity existingEmail);

		Task<bool> Delete(long id);

		Task<bool> DeleteByDomain(string domainString);

	}
}

