﻿namespace Domain.Model.Model.Author.QueryModel;

public class AuthorBookQueryModel : AuthorQueryModel
{
    public string? TitleBook { get; set; }
    public int PublishYear { get; set; }
}