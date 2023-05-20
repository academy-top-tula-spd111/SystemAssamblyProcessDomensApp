using System;
using System.Reflection;
using System.Runtime.Loader;

//AppDomain appDomain  = AppDomain.CurrentDomain;
//Console.WriteLine($"Name: {appDomain.FriendlyName}");
//Console.WriteLine($"Base Directory: {appDomain.BaseDirectory}");
Console.WriteLine("\nassembly coll 1");
PrintAssamblies();

Func();

GC.Collect();
GC.WaitForPendingFinalizers();


Console.WriteLine("\nassembly coll 3");
PrintAssamblies();

void Func()
{
    var context = new AssemblyLoadContext(name: "MyLib", isCollectible: true);
    context.Unloading += Context_Uploading;

    var assemblyPath = Path.Combine(Directory.GetCurrentDirectory(), "MyLib.dll");
    Assembly assembly = context.LoadFromAssemblyPath(assemblyPath);

    Console.WriteLine("\nassembly coll 2");
    PrintAssamblies();

    Type type = assembly.GetType("MyLib.Math");
    if (type is not null)
    {
        MethodInfo method1 = type.GetMethod("SquarePrint", BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
        method1?.Invoke(null, new object[] { 25 });
    }
    context.Unload();
}

void Context_Uploading(AssemblyLoadContext obj)
{
    Console.WriteLine("MyLib upload");
}

void PrintAssamblies()
{
    Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
    foreach (Assembly assembly in assemblies)
        Console.WriteLine(assembly.GetName().Name);
}

