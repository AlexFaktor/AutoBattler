﻿namespace Game.Core.Dtos.UserDtos.Telegrams
{
    public class UserTelegramCreateDto
    {
        public string Username { get; set; } = string.Empty;
        public string Hashtag { get; set; } = "HERO";

        public string TelegramId { get; set; } = string.Empty;
        public string? TelegramUsername {  get; set; } = string.Empty;
        public string? FirstName {  get; set; } = string.Empty;
        public string? LastName {  get; set; } = string.Empty;
        public string? Phone {  get; set; } = string.Empty;
        public string? Language {  get; set; } = string.Empty;
    }
}