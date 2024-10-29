﻿using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Contracts.Repositories
{
    public interface IUserRepositoryAsync : IRepositoryAsync<User>
    {
        Task<User> GetUserByEmailAsync(string email);
    }
}
