using System;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using q5id.platform.email.dal.Entities;
using q5id.platform.email.dal.Interfaces;
using System.Data;

namespace q5id.platform.email.dal.Repositories
{
	public class EmailRepository : IEmailRepository 
	{
        private readonly EmailContext _context;
        private readonly ILogger<EmailRepository> _logger;

		public EmailRepository(EmailContext context, ILogger<EmailRepository> logger)
		{
            _context = context;
            _logger = logger;
		}

        /// <summary>
        /// Create Email entry into Database
        /// </summary>
        /// <param name="entity"></param>
        public async Task Create(EmailEntity entity)
        {
            using (var dbContext = _context)
            {
                // Just insert email - duplicates allowed
                try
                {
                    entity.CreateDateTime = DateTime.Now;
                    _context.Emails.Add(entity);
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    _logger.LogError("While inserting email {email} into database | Exception: {exception}"
                        , entity.EmailAddress, ex.InnerException);
                }

            }
        }


        //TODO: Phase II
        /// <summary>
        /// Search for Email Address
        /// </summary>
        /// <param name="emailAddress"></param>
        /// <returns></returns>
        public async Task<EmailEntity> Search(string emailAddress)
        {
            using (var dbContext = _context)
            {
                return await _context.Emails
                .Where(x => x.EmailAddress.ToLower() == emailAddress.ToLower())
                .Select(e => new EmailEntity
                {
                    Id = e.Id,
                    EmailAddress = e.EmailAddress,
                    CreateDateTime = e.CreateDateTime,
                    IsConsumer = Convert.ToBoolean(e.IsConsumer),
                    IsBusiness = Convert.ToBoolean(e.IsBusiness),
                    IsInvestor = Convert.ToBoolean(e.IsInvestor)
                })
                .FirstOrDefaultAsync().ConfigureAwait(false);
            }
        }

        //TODO: Phase II
        /// <summary>
        /// Update EmailEntity record
        /// </summary>
        /// <param name="entity">New EmailEntity</param>
        /// <param name="existingEmail">Existing EmailEntity to update</param>
        /// <returns></returns>
        public async Task<EmailEntity> Update(EmailEntity entity, EmailEntity existingEmail)
        {
            using (var dbContext = _context)
            {
                existingEmail.UpdateDateTime = DateTime.Now;
                existingEmail.IsBusiness = entity.IsBusiness;
                existingEmail.IsConsumer = entity.IsConsumer;
                existingEmail.IsInvestor = entity.IsInvestor;
                _context.Emails.Update(existingEmail);
                await _context.SaveChangesAsync().ConfigureAwait(false);
                return entity;
            }
        }

        //TODO Phase II
        /// <summary>
        /// Delete email - NOT IMPLEMENTED
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<bool> Delete(long id)
        {
            using (var dbContext = _context)
            {
                var result = await _context.Emails.FirstOrDefaultAsync<EmailEntity>(e => e.Id.Equals(id)).ConfigureAwait(false);
                if (result != null)
                {
                    _context.Emails.Remove(result);
                    await _context.SaveChangesAsync().ConfigureAwait(false);
                    return true;
                }
                return false;
            }
        }

        //TODO: Phase II
        /// <summary>
        /// Delete Range of Email from a domain string
        /// i.e.  test.com or q5id.com 
        /// </summary>
        /// <param name="domainString"></param>
        /// <returns></returns>
        public async Task<bool> DeleteByDomain(string domainString)
        {
            using (var dbContext = _context)
            {
                if (!string.IsNullOrEmpty(domainString))
                {
                    var result = await _context.Emails
                        .Where<EmailEntity>(e => e.EmailAddress.Contains("@" + domainString))
                        .ToListAsync()
                        .ConfigureAwait(false);

                    if (result != null)
                    {
                        _context.Emails.RemoveRange(result);
                        await _context.SaveChangesAsync().ConfigureAwait(false);
                        return true;
                    }
                }
                return false;
            }
        }
    }
}

