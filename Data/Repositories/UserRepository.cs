﻿using Common.Utilities;
using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Data.Repositories
{
  public  class UserRepository :Repository<User>
    {
        public UserRepository(ApplicationDbContext dbContext):base(dbContext)
        {
        }

        public Task<User> GetUserAndPassword(string username , string password, CancellationToken cancelationtoken)
        {
            var passwordhash = SecurityHelper.GetSha256Hash(password);
            return Table.Where(p => p.UserName == username && p.PasswordHash == passwordhash).SingleOrDefaultAsync(cancelationtoken);
        }
    }
}
