using Microsoft.AspNetCore.Identity;
using NepFlex.Core.Entities.ResourceModels;
using NepFlex.DataAccess.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NepFlex.DataAccess.Identity
{
    public class CustomUserStore : IUserPasswordStore<UserRegisterRequest>, IUserEmailStore<UserRegisterRequest>
    {
        private readonly IOnlinePasalContext _context;

        public CustomUserStore(IOnlinePasalContext context)
        {
            _context = context;
        }
        public Task<IdentityResult> CreateAsync(UserRegisterRequest user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> DeleteAsync(UserRegisterRequest user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Task<UserRegisterRequest> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
        {
            //throw new NotImplementedException();
            return FindByNameAsync(normalizedEmail, cancellationToken);
        }

        public Task<UserRegisterRequest> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<UserRegisterRequest> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetEmailAsync(UserRegisterRequest user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<bool> GetEmailConfirmedAsync(UserRegisterRequest user, CancellationToken cancellationToken)
        {
            // throw new NotImplementedException();
            return Task.FromResult(true);
        }

        public Task<string> GetNormalizedEmailAsync(UserRegisterRequest user, CancellationToken cancellationToken)
        {
            //throw new NotImplementedException();
            return Task.FromResult(user.UserEmail);
        }

        public Task<string> GetNormalizedUserNameAsync(UserRegisterRequest user, CancellationToken cancellationToken)
        {
            //throw new NotImplementedException();
            return Task.FromResult(user.Username);
        }

        public Task<string> GetPasswordHashAsync(UserRegisterRequest user, CancellationToken cancellationToken)
        {
            //throw new NotImplementedException();
            return Task.FromResult(user.PasswordHash);
        }

        public Task<string> GetUserIdAsync(UserRegisterRequest user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
            //return Task.FromResult(user.UserId);
        }

        public Task<string> GetUserNameAsync(UserRegisterRequest user, CancellationToken cancellationToken)
        {
            //throw new NotImplementedException();
            return Task.FromResult(user.Username);
        }

        public Task<bool> HasPasswordAsync(UserRegisterRequest user, CancellationToken cancellationToken)
        {
            //throw new NotImplementedException();
            return Task.FromResult(!string.IsNullOrEmpty(user.PasswordHash));
        }

        public Task SetEmailAsync(UserRegisterRequest user, string email, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetEmailConfirmedAsync(UserRegisterRequest user, bool confirmed, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetNormalizedEmailAsync(UserRegisterRequest user, string normalizedEmail, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetNormalizedUserNameAsync(UserRegisterRequest user, string normalizedName, CancellationToken cancellationToken)
        {
            //throw new NotImplementedException();
            return Task.CompletedTask;
        }

        public Task SetPasswordHashAsync(UserRegisterRequest user, string passwordHash, CancellationToken cancellationToken)
        {
            //throw new NotImplementedException();
            user.PasswordHash = passwordHash;
            user.PSWDHASH = user.PasswordHash;
            user.PSWDSALT = null;
            return Task.CompletedTask;
        }

        public Task SetUserNameAsync(UserRegisterRequest user, string userName, CancellationToken cancellationToken)
        {
            //throw new NotImplementedException();
            user.Username = userName;
            return Task.CompletedTask;
        }

        public Task<IdentityResult> UpdateAsync(UserRegisterRequest user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
