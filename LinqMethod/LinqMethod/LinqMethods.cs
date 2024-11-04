using Dumpify;
using System.Collections.Frozen;

namespace LinqMethod;

public static class LinqMethods
{
    public static void Show(LinqMethodType type)
    {
        switch (type)
        {
            case LinqMethodType.Filtering:
                Filtering();
                break;
            case LinqMethodType.Partitioning:
                Partitioning();
                break;
            case LinqMethodType.Projection:
                Projection();
                break;
            case LinqMethodType.ExistenceOrQuantityChecks:
                ExistenceOrQuantityChecks();
                break;
            case LinqMethodType.SequenceManipulation:
                SequenceManipulation();
                break;
            case LinqMethodType.AggregationMethod:
                AggregationMethod();
                break;
            case LinqMethodType.ElementOperators:
                ElementOperators();
                break;
            case LinqMethodType.ConversionMethods:
                ConversionMethods();
                break;
            case LinqMethodType.GenerationMethods:
                GenerationMethods();
                break;
            case LinqMethodType.SetOperations:
                SetOperations();
                break;
            case LinqMethodType.JoiningAndGrouping:
                JoiningAndGrouping();
                break;
            case LinqMethodType.Sorting:
                Sorting();
                break;
            default:
                ShowAll();
                break;
        }
    }

    private static void ShowAll()
    {
        Filtering();
        Partitioning();
        Projection();
        ExistenceOrQuantityChecks();
        SequenceManipulation();
        AggregationMethod();
        ElementOperators();
        ConversionMethods();
        GenerationMethods();
        SetOperations();
        JoiningAndGrouping();
        Sorting();
    }

    /// <summary>
    /// Where & OfType (All are Deferred Execution)
    /// </summary>
    private static void Filtering()
    {
        Console.WriteLine("#################");
        Console.WriteLine(nameof(Filtering));
        Console.WriteLine("#################");
        Enumerable.Range(0, 5).Where(x => x > 3).Dump("Where");
        IEnumerable<object> collection = [1, "azerty", 2, 3, 4, 5];
        //OfType permet de filtrer sur les éléments du type spécifié
        collection.OfType<int>().Dump("OfType");
    }

    /// <summary>
    /// Skip, Take, SkipLast, TakeLast, SkipWhile, TakeWhile  (All are Deferred Execution)
    /// </summary>
    private static void Partitioning()
    {
        Console.WriteLine("#################");
        Console.WriteLine(nameof(Partitioning));
        Console.WriteLine("#################");
        IEnumerable<int> collection = Enumerable.Range(0, 5);
        collection.Skip(3).Dump("Skip");
        collection.Take(3).Dump("Take");
        collection.SkipLast(3).Dump("SkipLast");
        collection.TakeLast(3).Dump("TakeLast");
        collection.SkipWhile(x => x < 2).Dump("SkipWhile");
        collection.TakeWhile(x => x < 2).Dump("TakeWhile");
    }

    /// <summary>
    /// Select (+ with index), SelectMany (+ with index), Cast, Chunk (All are Deferred Execution)
    /// </summary>
    private static void Projection()
    {
        Console.WriteLine("#################");
        Console.WriteLine(nameof(Projection));
        Console.WriteLine("#################");
        IEnumerable<int> collection = Enumerable.Range(0, 6);
        collection.Select(x => x.ToString()).Dump("Select");
        collection.Select((x, i) => $"Value : {x}, index : {i}").Dump("Select with index");
        IEnumerable<List<int>> collection2 = [[1, 2, 3], [4, 5, 6]];
        collection2.SelectMany(x => x).Dump("SelectMany");
        collection2.SelectMany((x, i) => x.Select(y => $"Value : {y}, index : {i}")).Dump("SelectMany with index");
        collection.Cast<object>().Dump("Cast");
        collection.Chunk(3).Dump("Chunk");
    }

    /// <summary>
    /// Any (Immediate Execution), All (Immediate Execution), Contains (Immediate Execution)
    /// </summary>
    private static void ExistenceOrQuantityChecks()
    {
        Console.WriteLine("#################");
        Console.WriteLine(nameof(ExistenceOrQuantityChecks));
        Console.WriteLine("#################");
        IEnumerable<int> collection = Enumerable.Range(0, 6);
        collection.Any(x => x < 2).Dump("Any");
        collection.All(x => x < 2).Dump("All");
        collection.Contains(12).Dump("Contains");
    }

    /// <summary>
    /// Append, Prepend
    /// </summary>
    private static void SequenceManipulation()
    {
        Console.WriteLine("#################");
        Console.WriteLine(nameof(SequenceManipulation));
        Console.WriteLine("#################");
        IEnumerable<int> collection = Enumerable.Range(0, 6);
        collection.Append(6).Dump("Append");
        collection.Prepend(-1).Dump("Prepend");
    }

    /// <summary>
    /// Count, TryGetNonEnumerableCount, Max, MaxBy, Min, MinBy , Sum, Average, Aggregate
    /// (Immediate Execution)
    /// </summary>
    private static void AggregationMethod()
    {
        Console.WriteLine("#################");
        Console.WriteLine(nameof(AggregationMethod));
        Console.WriteLine("#################");
        IEnumerable<int> collection = Enumerable.Range(1, 6);
        collection.Count(x => x < 2).Dump("Count");
        //Return true because it is executed
        collection.TryGetNonEnumeratedCount(out int countImmediate).Dump("TryGetNonEnumeratedCount");
        //Return false because Where is not executed yet
        collection.Where(x => x > 2).TryGetNonEnumeratedCount(out int count).Dump("TryGetNonEnumeratedCount deferred exe");
        collection.LongCount().Dump("LongCount");

        collection.Max().Dump("Max");
        collection.Max(x => x * -2).Dump("Max with predicate");
        collection.Min().Dump("Min");
        collection.Min(x => x * -2).Dump("Min with predicate");

        IEnumerable<Personne> personnes = [new("rbo", 36), new("you", 20), new("me", 16)];
        personnes.MaxBy(x => x.Age).Dump("MaxBy");
        personnes.MinBy(x => x.Age).Dump("MinBy");

        collection.Sum().Dump("Sum");
        collection.Average().Dump("Average");


        collection.Aggregate((x, y) => x + y).Dump("Aggregate");
        //overload 1
        collection.Select(x => x.ToString()).Aggregate((x, y) => $"{x}, {y}").Dump("Aggregate overload 1");
        //overload 2 give a seed
        collection.Aggregate(10, (x, y) => x + y).Dump("Aggregate overload 2");
        //overload 3 give a seed then add function
        collection.Aggregate(0, (x, y) => x + y, x => (float)x / collection.Count()).Dump("Aggregate overload 3");
    }

    /// <summary>
    /// First, FirstOrDefault, Single, SingleOrDefault, Last, LastOrDefault, ElementAt, DefaultIfEmpty
    /// (Immediate Execution)
    /// </summary>
    private static void ElementOperators()
    {
        Console.WriteLine("#################");
        Console.WriteLine(nameof(ElementOperators));
        Console.WriteLine("#################");
        IEnumerable<int> collection = Enumerable.Range(1, 6);
        collection.First(x => x > 2).Dump("First");
        collection.FirstOrDefault(x => x > 8, -1).Dump("FirstOrDefault");
        //Signifie qu'il ne doit y avoir qu'un seul élément supérieur au predicat sinon exception (InvalidOperationException)
        collection.Single(x => x > 5).Dump("Single");
        collection.SingleOrDefault(x => x > 6, -1).Dump("SingleOrDefault");

        collection.Last(x => x > 2).Dump("Last");
        collection.LastOrDefault(x => x > 8, -1).Dump("LastOrDefault");
        collection.ElementAt(1).Dump("ElementAt");
        collection.ElementAtOrDefault(12).Dump("ElementAtOrDefault");

        collection = [];
        collection.DefaultIfEmpty(5).Dump("DefaultIfEmpty");
    }

    /// <summary>
    /// ToArray, ToList, ToHashSet, ToFrozenSet, ToDictionary, ToFrozenDictionary, ToLookup
    /// (Immediate Execution)
    /// </summary>
    private static void ConversionMethods()
    {
        Console.WriteLine("#################");
        Console.WriteLine(nameof(ConversionMethods));
        Console.WriteLine("#################");
        IEnumerable<int> collection = Enumerable.Range(1, 6);
        collection.ToArray().Dump("ToArray");
        collection.ToList().Dump("ToList");
        collection.ToHashSet().Dump("ToHashSet");
        collection.ToFrozenSet().Dump("ToFrozenSet");

        collection.ToDictionary(key => key, value => value).Dump("ToDictionary");
        collection.ToFrozenDictionary(key => key, value => value).Dump("ToFrozenDictionary");

        IEnumerable<Personne> personnes = [new("rbo", 36), new("you", 20), new("me", 16), new("me2", 16)];
        personnes.ToLookup(x => x.Age).Dump("ToLookup");
        personnes.ToLookup(x => x.Age)[20].Dump("ToLookup access by index");
    }

    /// <summary>
    /// AsEnumerable (Deferred Execution), AsQueryable (Deferred Execution)
    /// Range (Immediate Execution), Repeat (Immediate Execution), Empty (Immediate Execution)
    /// </summary>
    private static void GenerationMethods()
    {
        Console.WriteLine("#################");
        Console.WriteLine(nameof(GenerationMethods));
        Console.WriteLine("#################");

        List<Personne> personnes = [new("rbo", 36), new("you", 20), new("me", 16), new("me2", 16)];
        personnes.AsEnumerable().Dump("AsEnumerable");
        personnes.AsQueryable().Dump("AsQueryable");

        Enumerable.Range(1, 6).Dump("Range");
        Enumerable.Repeat(1, 6).Dump("Repeat");
        Enumerable.Empty<int>().Dump("Empty");
    }

    /// <summary>
    /// Distinct, DistinctBy, Union, Intersect, IntersectBy, Except, ExceptBy, SequenceEqual
    /// (Deferred Execution)
    /// </summary>
    private static void SetOperations()
    {
        Console.WriteLine("#################");
        Console.WriteLine(nameof(SetOperations));
        Console.WriteLine("#################");
        IEnumerable<int> collection = [1, 2, 3, 1];
        collection.Distinct().Dump("Distinct");
        IEnumerable<Personne> personnes = [new("rbo", 36), new("you", 20), new("me", 16), new("me2", 16)];
        personnes.DistinctBy(x => x.Age).Dump("DistinctBy");

        IEnumerable<int> collection1 = [1, 2, 3];
        IEnumerable<int> collection2 = [2, 3, 4, 5];
        collection1.Union(collection2).Dump("Union");
        collection1.Intersect(collection2).Dump("Intersect");
        collection1.Except(collection2).Dump("Except");

        IEnumerable<Personne> personnes2 = [new("azerty", 30), new("u2", 20), new("sme", 16), new("me2", 16)];
        personnes.UnionBy(personnes2, x => x.Age).Dump("UnionBy");
        personnes.IntersectBy(personnes2.Select(x => x.Age), p => p.Age).Dump("IntersectBy");
        personnes.ExceptBy(personnes2.Select(x => x.Age), p => p.Age).Dump("ExceptBy");

        IEnumerable<int> collection3 = [1, 2, 3];
        collection1.SequenceEqual(collection3).Dump("SequenceEqual true");
        collection1.SequenceEqual(collection2).Dump("SequenceEqual false");
    }

    /// <summary>
    /// Zip, Join, GroupJoin, Concat, GrouBy
    /// (Deferred Execution)
    /// </summary>
    private static void JoiningAndGrouping()
    {
        Console.WriteLine("#################");
        Console.WriteLine(nameof(JoiningAndGrouping));
        Console.WriteLine("#################");
        IEnumerable<int> collection1 = [1, 2, 3];
        IEnumerable<string> collection2 = ["A", "B", "C", "D"];
        IEnumerable<string> collection3 = ["*", "&", "|", "^"];

        collection1.Zip(collection2).Dump("Zip"); //D will be ignored
        collection1.Zip(collection2, collection3).Dump("Zip V2"); //D and ^ will be ignored

        IEnumerable<Customer> customers = [new(1, "rbo", 36), new(2, "you", 20), new(3, "me", 16)];
        IEnumerable<Product> products = [new(10, "shoes", 1), new(20, "pants", 2), new(30, "glasses", 1)];

        customers.Join(products, c => c.Id, p => p.PersonId, (customer, product) => $"{customer.Name} bought {product.Name}").Dump("Join");

        customers.GroupJoin(products, c => c.Id, p => p.PersonId,
            (customer, products) => $"{customer.Name} bought {string.Join(',', products.Select(x => x.Name))}").Dump("GroupJoin");

        collection2.Concat(collection3).Dump("Concat");

        IEnumerable<Personne> personnes = [new("azerty", 30), new("u2", 20), new("sme", 16), new("me2", 16)];
        personnes.GroupBy(x => x.Age).Dump("GroupBy");
    }

    /// <summary>
    /// OrderBy, OrderByDescending, ThenBy, ThenByDescending, Reverse, Order, OrderDescending
    /// (Deferred Execution)
    /// </summary>
    private static void Sorting()
    {
        Console.WriteLine("#################");
        Console.WriteLine(nameof(Sorting));
        Console.WriteLine("#################");

        IEnumerable<int> collection = [11, 16, 9];
        collection.Order().Dump("Order");
        collection.Reverse().Dump("Reverse");
        IEnumerable<Personne> personnes = [new("azerty", 30), new("u2", 20), new("rbo", 16), new("me2", 16)];
        personnes.OrderBy(x => x.Age).Dump("OrderBy");
        personnes.OrderByDescending(x => x.Age).Dump("OrderByDescending");

        IEnumerable<Product> products = [new(10, "shoes", 1), new(20, "pants", 2), new(30, "glasses", 1)];
        products.OrderBy(x => x.Name).ThenBy(x => x.PersonId).Dump("ThenBy");
        products.OrderBy(x => x.Name).ThenByDescending(x => x.PersonId).Dump("ThenByDescending");
    }
}

internal record Personne(string Name, int Age);
internal record Product(int Id, string Name, int PersonId);
internal record Customer(int Id, string Name, int Age);
