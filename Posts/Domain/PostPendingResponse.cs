using System;

namespace BlogEngineApi.Posts.Domain
{
    public class PostPendingResponse
    {
        public int PostID { get; set; }
        public string Author { get; set; }
        public DateTime? Publish { get; set; }
    }
}