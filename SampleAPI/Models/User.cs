﻿namespace SampleAPI.Models
{
    public class User
    {
            public int Id { get; set; }
            public string Username { get; set; }
            public string Password { get; set; }
            public string Salt { get; set; }
            public bool IsActive { get; set; }
    }
}
