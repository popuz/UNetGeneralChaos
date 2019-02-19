using UnityEngine.Networking;

public class SyncListItem : SyncList<Item>
{
    protected override void SerializeItem(NetworkWriter writer, Item item) => writer.Write(ItemBase.GetItemId(item));
    protected override Item DeserializeItem(NetworkReader reader) => ItemBase.GetItem(reader.ReadInt32());
}