﻿using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;
using System.Runtime.CompilerServices;

namespace MinProject.Models
{

    public class User
    {
        [Key]
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? PasswordHash { get; set; }
        public string? PasswordSalt { get; set; }
        public virtual Order? Order { get; set; }
        public virtual Notifaction? Notifaction { get; set; }
    }

    public class Products
    {
        [Key]
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        [NotMapped]
        public IFormFile? formFile { get; set; }
        public byte[]? Data { get; set; }
        public string? FileName { get; set; }
        public bool IsDeleted { get; set; }

        public virtual Order? Order { get; set; }
    }

    public class Order
    {
        [Key]
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public Guid UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User? User { get; set; }
        public Guid ProductId { get; set; }
        [ForeignKey("ProductId")]
        public virtual Products? Products { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? LastUpdatedDate { get; set; }

        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }

        public bool IsDeleted { get; set; }
    }

    public class Notifaction
    {
        [Key]
        public Guid Id { get; set; }

        public virtual User? User { get; set; }
        public Guid UserId { get; set; }

        public string? Message { get; set; }

        public DateTime CreatedDate { get; set; }
    }

    public class State
    {
        [Key]
        public Guid StateCodePk { get; set; }
        public Guid CountryCodeFk { get; set; }
        [ForeignKey("CountryCodeFk")]
        public virtual Country? Country { get; set; }
        public string? StateName { get; set; }
        public City? City { get; set; }
    }

    public class City
    {
        [Key]
        public Guid CityCodePk { get; set; }
        public Guid StateCodeFk { get; set; }
        [ForeignKey("StateCodeFk")]
        public virtual State? State { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CityCode { get; set; }
    }
    public class Country
    {
        [Key]
        public Guid CountryCodePk { get; set; }
        public string? CountryName { get; set; }
        public virtual State? State { get; set; }
    }

    public class RegisterModel
    {
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? ComformPassword { get; set; }
    }

    public class Login
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}