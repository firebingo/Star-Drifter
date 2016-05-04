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
    int count { get; set; }
    string itemName { get; }

    void updateItem();
}
