using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace HullBreach
{
    internal static class BDArmoryExtensions
    {
        internal static Type PartExtensions;
        private static MethodInfo DamageMethod;
        private static MethodInfo MaxDamageMethod;
        private static bool isInstalled;

        static BDArmoryExtensions()
        {
            try
            {
                PartExtensions = AssemblyLoader.loadedAssemblies
                     .Where(a => a.name.Contains("BDArmory.Core")).SelectMany(a => a.assembly.GetExportedTypes())
                     .SingleOrDefault(t => t.FullName == "BDArmory.Core.Extension.PartExtensions");
                DamageMethod = PartExtensions.GetMethod("Damage");
                MaxDamageMethod = PartExtensions.GetMethod("MaxDamage");
                isInstalled = true;
            }
            catch (Exception e)
            {
                isInstalled = false;
            }
        }


        internal static bool BDArmoryIsInstalled()
        {
            return isInstalled;
        }


        internal static float Damage(this Part part)
        {
            return Convert.ToSingle(DamageMethod.Invoke(null, new object[] {part}));
        }

        internal static float MaxDamage(this Part part)
        {
            return Convert.ToSingle(MaxDamageMethod.Invoke(null, new object[] { part }));
        }

    }
}
