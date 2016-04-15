using System;
using System.Windows;
using System.Windows.Controls;
using RepoLocalDB;

namespace NodePad
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            for (var i = 0; i < 200; i++)
            {
                System.Diagnostics.Debug.WriteLine($"let guid{i} = Guid.Parse(\"{ Guid.NewGuid()}\")");
            }
            using (var ctx = new NodePadContext())
            {
                //var vr = new VectorRecord { VectorFormat = VectorFormat.Dense, Length = 25, Coords = "Coords", Values = "Values", Description = "Description" };
                //ctx.VectorRecords.Add(vr);
                //ctx.SaveChanges();
            }

        }
    }
}
