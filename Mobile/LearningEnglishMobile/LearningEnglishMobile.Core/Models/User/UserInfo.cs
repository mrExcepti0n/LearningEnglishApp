using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace LearningEnglishMobile.Core.Models.User
{
    public class UserInfo
    {
        [JsonProperty("sub")]
        public string UserId { get; set; }

        [JsonProperty("preferred_username")]
        public string PreferredUsername { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("email_verified")]
        public bool EmailVerified { get; set; }
    }
}
