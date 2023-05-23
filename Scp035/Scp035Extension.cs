using Synapse3.SynapseModule.Item;

namespace Scp035;

public static class Scp035Extension
{
    public static bool IsScp035Item(this SynapseItem item) => item != null && item.Name.Contains("Scp035-Item-");
}
