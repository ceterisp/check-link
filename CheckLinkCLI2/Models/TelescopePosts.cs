using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace CheckLinkCLI2.Models
{
    public class TelescopePosts
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("html")]
        public string Html { get; set; }

        [JsonPropertyName("published")]
        public string Published { get; set; }

        [JsonPropertyName("updated")]
        public string Updated { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }

        [JsonPropertyName("guid")]
        public string Guid { get; set; }

        [JsonPropertyName("feed")]
        public TelescopePostFeeds Feed { get; set; }
    }

    public class TelescopePostFeeds
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("author")]
        public string Author { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }

        [JsonPropertyName("user")]
        public string User { get; set; }

        [JsonPropertyName("link")]
        public string Link { get; set; }

        [JsonPropertyName("etag")]
        public string Etag { get; set; }

        [JsonPropertyName("lastModified")]
        public string LastModified { get; set; }
    }
}