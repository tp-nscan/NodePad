using System.Windows;
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
            using (var ctx = new NodePadContext())
            {
                //var vr = new VectorRecord { VectorFormat = VectorFormat.Dense, Length = 25, Coords = "Coords", Values = "Values", Description = "Description" };
                //ctx.VectorRecords.Add(vr);
                //ctx.SaveChanges();
            }

        }
    }
}
