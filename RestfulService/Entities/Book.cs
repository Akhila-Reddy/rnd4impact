using System;
using System.ComponentModel.DataAnnotations;
using Amazon.DynamoDBv2.DataModel;

namespace LibraryManagementSystem.Entities
{
    [DynamoDBTable("books")]
    public class Book
    {
        [DynamoDBHashKey("id")]
        public int Id { get; set; }
        [Required]
        [DynamoDBProperty("title")]
        public string Title { get; set; }
        [Required]
        [DynamoDBProperty("isbn")]
        public string ISBN { get; set; }
        [DynamoDBProperty("author_id")]
        public int AuthorId { get; set; }
        //[DynamoDBProperty("author_id")]
        public Author Author { get; set; }
        [DynamoDBProperty("is_available")] 
        public bool IsAvailable { get; set; }
    }
}

