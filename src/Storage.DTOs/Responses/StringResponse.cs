﻿namespace Storage.DTOs.Responses
{
    public class StringResponse
    {
        public int Identifier { get; set; }

        public string StringValue { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? LastModifiedAt { get; set; }
    }
}