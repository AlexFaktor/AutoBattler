﻿using Game.Core.Resources.Enums.Game;

namespace Game.Core.DatabaseRecords.Things
{
    public class ItemRecord
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public EItem ItemId { get; set; }
    }
}