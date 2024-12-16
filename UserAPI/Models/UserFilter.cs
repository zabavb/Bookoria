﻿using Library.UserEntities;

namespace UserAPI.Models
{
    public class UserFilter
    {
        public DateTime? DateOfBirthStart { get; set; }
        public DateTime? DateOfBirthEnd { get; set; }
        public RoleType? Role { get; set; }
    }
}