﻿namespace WebProject2.Models
{
    public class UserLockdownViewModel
    {
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Email { get; set; }
        public bool isLockDown { get; set; }
    }
}
