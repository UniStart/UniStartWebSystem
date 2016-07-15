﻿using System.Collections;
using System.Collections.Generic;

namespace Unistart.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string University { get; set; }
        public IEnumerable<Comment> Comments { get; set; }
    }
}