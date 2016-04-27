using System;

public enum itemType
{
    Weapon,
    Ammo,
    Scrap
}

public interface inventoryItem
{
    itemType inventoryItemType { get; }
    Guid itemId { get; }

    void updateItem();
}
