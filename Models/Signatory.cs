﻿namespace Api.BizSign.Models;

public class Signatory
{
    public Guid Id { get; set; }
    
    public string Name { get; set; } = string.Empty;
    
    public string Email { get; set; } = string.Empty; 
    
    public string Phone { get; set; } = string.Empty; 
    
    public string Role { get; set; } = string.Empty; 
    
    public string Company { get; set; } = string.Empty; 
}