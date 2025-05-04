using System;
using System.Collections.Generic;
using Xunit;

using GetWatch.Services;

namespace GetWatch.Tests
{
    public class UserTest
    {
        [Fact]
        public void onNotified_ShouldAddCommentToUserComments()
        {
            // Arrange
            var user = new User("TestUser");
            var commentSection = new CommentSection("TestSection");
            user.SubscribeTo(commentSection);


            var comment = new Comment("TestUser2","new message");
            commentSection.AddItem(comment); 

            // Assert
            Assert.Contains(comment, user.Comments);
        }
    }
}