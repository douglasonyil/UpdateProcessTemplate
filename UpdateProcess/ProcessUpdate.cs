using System.Collections.Concurrent;

namespace UpdateProcess;

public class ProcessUpdate
{
    public readonly ConcurrentDictionary<string, Dictionary<string, object>> _registryStore = new ConcurrentDictionary<string, Dictionary<string, object>>(StringComparer.Ordinal);

    public void Update<T>(string processId, Dictionary<string, T> metadata)
    {
        if (processId == null)
        {
            throw new ArgumentNullException(nameof(processId));
        }
        if (string.IsNullOrWhiteSpace(processId) || string.IsNullOrEmpty(processId))
        {
            throw new ArgumentException(nameof(processId));
        }
        if (metadata == null)
        {
            return;
        }
        // var pId = _registryStore.
        if (!_registryStore.TryGetValue(processId, out var existingMetadata))
        {
            throw new KeyNotFoundException(nameof(processId));
        }
        if (metadata.Count == 0)
        {
            return;
        }
        lock (existingMetadata)
        {
            foreach (var kvp in metadata)
            {
                existingMetadata[kvp.Key] = kvp.Value;
            }
        }
    }
}
