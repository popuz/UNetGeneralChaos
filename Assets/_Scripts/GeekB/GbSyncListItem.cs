using UnityEngine.Networking;

public class SyncListItem : SyncList<Item>
{
    protected override void SerializeItem(NetworkWriter writer, Item item) => writer.Write(GbItemBase.GetItemId(item));
    protected override Item DeserializeItem(NetworkReader reader) => GbItemBase.GetItem(reader.ReadInt32());
}