﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersonTransation.Models.Entities
{
    public class TransactionModel
    {
        [Key]
        public Guid Id { get; init; }
        public string Description { get; private set; }
        public string Status { get; private set; }
        public decimal Value { get; private set; }
        public DateTime Date { get; private set; }
        public Guid UserId { get; set; }
        [ForeignKey("UserId")]
        public UserModel User { get; set; } 


        public TransactionModel(string description, string status, decimal value, DateTime date, Guid userId)
        {
            Id = Guid.NewGuid();
            Description = description;
            Status = status;
            Value = value;
            Date = date;
            UserId = userId;
        }

        public TransactionModel(string description, string status, decimal value, DateTime date)
        {
            Id = Guid.NewGuid();
            Description = description;
            Status = status;
            Value = value;
            Date = date;
        }

        public void ChangeAttributes(string description, string status, decimal value, DateTime date)
        {
            if (description != null) Description = description;
            if (value != default) Value = value;
            if (status != default) Status = status;
            if (date != default) Date = date;
        }
    }
}
