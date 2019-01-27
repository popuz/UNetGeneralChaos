using UnityEngine.Networking;

public class SyncListItem : SyncList<GbItem>
{
    protected override void SerializeItem(NetworkWriter writer, GbItem item) => writer.Write(GbItemBase.GetItemId(item));
    protected override GbItem DeserializeItem(NetworkReader reader) => GbItemBase.GetItem(reader.ReadInt32());
}