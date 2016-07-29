using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Yburn
{
    public class WorkerLoader
    {
        /********************************************************************************************
		 * Public static members, functions and properties
		 ********************************************************************************************/

        public static Worker CreateInstance(
            string workerName
            )
        {
            Type type = FindFirstType(workerName);
            if(type == null)
            {
                throw new Exception("No Worker has been found.");
            }

            return (Worker)Activator.CreateInstance(type);
        }

        /********************************************************************************************
       * Private/protected static members, functions and properties
       ********************************************************************************************/

        private static Type FindFirstType(
            string workerName
            )
        {
            string[] dllFileNames = Directory.GetFiles(Directory.GetCurrentDirectory(), "*.dll");
            List<Assembly> assemblies = GetAssemblies(dllFileNames);

            return FindFirstType(assemblies, workerName);
        }

        private static List<Assembly> GetAssemblies(
            string[] dllFileNames
            )
        {
            List<Assembly> assemblies = new List<Assembly>(dllFileNames.Length);
            foreach(string dllFile in dllFileNames)
            {
                AssemblyName assemblyName = AssemblyName.GetAssemblyName(dllFile);
                assemblies.Add(Assembly.Load(assemblyName));
            }

            return assemblies;
        }

        private static Type FindFirstType(
            List<Assembly> assemblies,
            string workerName
            )
        {
            foreach(Assembly assembly in assemblies)
            {
                if(assembly != null)
                {
                    Type type = FindFirstType(assembly, workerName);
                    if(type != null)
                    {
                        return type;
                    }
                }
            }

            return null;
        }

        private static Type FindFirstType(
            Assembly assembly,
            string workerName
            )
        {
            foreach(Type type in GetTypes(assembly))
            {
                if(!IsAbstract(type)
                    && IsWorker(type)
                    && type.Name == workerName)
                {
                    return type;
                }
            }

            return null;
        }

        private static Type[] GetTypes(
            Assembly assembly
            )
        {
            try
            {
                return assembly.GetTypes();
            }
            catch(ReflectionTypeLoadException exception)
            {
                throw new Exception(
                    "Unable to load some types from assembly \"" + assembly.FullName + "\".",
                    exception);
            }
        }

        private static bool IsAbstract(
            Type type
            )
        {
            return type.IsInterface || type.IsAbstract;
        }

        private static bool IsWorker(
            Type type
            )
        {
            return type != null
                && (type == typeof(Worker) || IsWorker(type.BaseType));
        }
    }
}