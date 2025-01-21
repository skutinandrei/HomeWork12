public class OtusDictionary
{
    private class Entry
    {
        public int Key { get; set; }
        public string Value { get; set; }
    }

    private Entry[] _entries;
    private int _capacity;

    public OtusDictionary()
    {
        _capacity = 32;
        _entries = new Entry[_capacity];
    }

    public void Add(int key, string value)
    {
        if (value == null)
            throw new ArgumentNullException(nameof(value));

        bool added = TryAdd(key, value);
        if (!added)
        {
            ResizeAndRehash(key, value);
        }
    }

    private bool TryAdd(int key, string value)
    {
        int index = GetIndex(key, _capacity);

        if (_entries[index] == null)
        {
            _entries[index] = new Entry { Key = key, Value = value };
            return true;
        }
        else if (_entries[index].Key == key)
        {
            _entries[index].Value = value;
            return true;
        }
        else
        {
            return false; // Collision
        }
    }

    private void ResizeAndRehash(int newKey, string newValue)
    {
        int newCapacity = _capacity * 2;
        Entry[] newEntries = new Entry[newCapacity];

        // Rehash existing entries
        foreach (var entry in _entries)
        {
            if (entry != null)
            {
                int newIndex = GetIndex(entry.Key, newCapacity);
                if (newEntries[newIndex] != null)
                {
                    // Recursive resize if collision during rehash
                    ResizeAndRehash(newKey, newValue);
                    return;
                }
                newEntries[newIndex] = entry;
            }
        }

        // Try add new entry
        int targetIndex = GetIndex(newKey, newCapacity);
        if (newEntries[targetIndex] != null)
        {
            ResizeAndRehash(newKey, newValue);
            return;
        }

        newEntries[targetIndex] = new Entry { Key = newKey, Value = newValue };

        // Update state
        _entries = newEntries;
        _capacity = newCapacity;
    }

    public string Get(int key)
    {
        int index = GetIndex(key, _capacity);
        Entry entry = _entries[index];

        if (entry != null && entry.Key == key)
            return entry.Value;

        return null;
    }

    public string this[int key]
    {
        get => Get(key);
        set => Add(key, value);
    }

    private static int GetIndex(int key, int capacity)
    {
        return key % capacity;
    }
}