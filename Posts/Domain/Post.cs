using System;

namespace BlogEngineApi.Posts.Domain
{
    public class Post
    {
        public int PostID { get; set; }
        public string Content { get; set; }
        public string Author { get; set; }
        public int Status { get; set; }
        public DateTime? Approval { get; set; }
        public DateTime? Publish { get; set; }
    }
}