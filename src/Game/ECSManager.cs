using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class ECSManager
{
    private int nextEntityId = 0;
    private readonly List<ISystem> systems = new();

    private readonly Dictionary<Type, IComponentStore> componentStores = new();

    public int CreateEntity() => nextEntityId++;

    public void AddSystem(ISystem system) => systems.Add(system);

    public void UpdateSystems(float dt)
    {
        foreach (var system in systems)
            system.Update(this, dt);
    }

    // --- Composants dynamiques ---
    public void AddComponent<T>(int entityId, T component) where T : struct
    {
        if (!componentStores.TryGetValue(typeof(T), out var store))
        {
            store = new ComponentStore<T>();
            componentStores[typeof(T)] = store;
        }

        ((ComponentStore<T>)store).Add(entityId, component);
    }

    public ComponentStore<T> GetComponents<T>() where T : struct
    {
        if (componentStores.TryGetValue(typeof(T), out var store))
            return (ComponentStore<T>)store;
        else
            return new ComponentStore<T>(); // vide mais compatible
    }
}

// Interface pour stocker dynamiquement n'importe quel type de composant
public interface IComponentStore { }

// Stockage performant des composants : contigu en mémoire
public class ComponentStore<T> : IComponentStore where T : struct
{
    private readonly List<int> entityIds = new();
    private readonly List<T> components = new();
    private readonly Dictionary<int, int> entityIndex = new();

    public void Add(int entityId, T component)
    {
        if (entityIndex.ContainsKey(entityId))
        {
            components[entityIndex[entityId]] = component;
        }
        else
        {
            entityIds.Add(entityId);
            components.Add(component);
            entityIndex[entityId] = components.Count - 1;
        }
    }

    public bool TryGet(int entityId, out T component)
    {
        if (entityIndex.TryGetValue(entityId, out int index))
        {
            component = components[index];
            return true;
        }

        component = default;
        return false;
    }

    public Span<T> Values => components.ToArray(); // pour lecture seule / iteration
    public Span<int> EntityIds => entityIds.ToArray();

    // Pour itération thread-safe avec index
    public int Count => components.Count;
    public (int entityId, T component) this[int i] => (entityIds[i], components[i]);
}
